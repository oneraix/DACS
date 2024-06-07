using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DACS.Models;
using DACS.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml;

namespace DACS.Areas.QL.Controllers
{
    [Area("QL")]
    [Authorize(Roles = Role.Role_QL)]
    public class RoomScheduleController : Controller
    {
        private readonly IRoomScheduleRepository _roomScheduleRepository;
        private readonly ApplicationDbContext _context;

        public RoomScheduleController(IRoomScheduleRepository roomScheduleRepository, ApplicationDbContext context)
        {
            _roomScheduleRepository = roomScheduleRepository;
            _context = context;
        }

        // GET: RoomSchedule
        public async Task<IActionResult> Index(DateTime? selectedDate, string selectedCa)
        {
            var date = selectedDate ?? DateTime.Today;
            var roomSchedules = await _roomScheduleRepository.GetAllAsync();
            var filteredSchedules = roomSchedules.Where(rs => rs.Ngay.Date == date.Date);

            if (!string.IsNullOrEmpty(selectedCa))
            {
                if (selectedCa == "Sáng")
                {
                    filteredSchedules = filteredSchedules.Where(rs => rs.ThoiGianBatDau >= new TimeSpan(7, 30, 0) && rs.ThoiGianKetThuc <= new TimeSpan(11, 35, 0));
                }
                else if (selectedCa == "Chiều")
                {
                    filteredSchedules = filteredSchedules.Where(rs => rs.ThoiGianBatDau >= new TimeSpan(12, 30, 0) && rs.ThoiGianKetThuc <= new TimeSpan(16, 35, 0));
                }
            }

            ViewBag.SelectedDate = date.ToString("yyyy-MM-dd");
            ViewBag.SelectedCa = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Value = "Sáng", Text = "Ca Sáng (7:30 - 11:35)" },
                new SelectListItem { Value = "Chiều", Text = "Ca Chiều (12:30 - 16:35)" }
            }, "Value", "Text", selectedCa);

            return View(filteredSchedules);
        }

        // GET: RoomSchedule/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var roomSchedule = await _roomScheduleRepository.GetByIdAsync(id);
            if (roomSchedule == null)
            {
                return NotFound();
            }
            return View(roomSchedule);
        }

        // GET: RoomSchedule/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RoomSchedule/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MonHoc,GiangVien,PhongHoc,Ngay,ThoiGianBatDau,ThoiGianKetThuc")] RoomSchedule roomSchedule)
        {
            if (ModelState.IsValid)
            {
                await _roomScheduleRepository.AddAsync(roomSchedule);
                return RedirectToAction(nameof(Index));
            }
            return View(roomSchedule);
        }

        // GET: RoomSchedule/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var roomSchedule = await _roomScheduleRepository.GetByIdAsync(id);
            if (roomSchedule == null)
            {
                return NotFound();
            }
            return View(roomSchedule);
        }

        // POST: RoomSchedule/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MonHoc,GiangVien,PhongHoc,Ngay,ThoiGianBatDau,ThoiGianKetThuc")] RoomSchedule roomSchedule)
        {
            if (id != roomSchedule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _roomScheduleRepository.UpdateAsync(roomSchedule);
                return RedirectToAction(nameof(Index));
            }
            return View(roomSchedule);
        }

        // GET: RoomSchedule/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var roomSchedule = await _roomScheduleRepository.GetByIdAsync(id);
            if (roomSchedule == null)
            {
                return NotFound();
            }
            return View(roomSchedule);
        }

        // POST: RoomSchedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _roomScheduleRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: RoomSchedule/Upload
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("File", "Please upload a valid file.");
                return RedirectToAction(nameof(Index));
            }

            var listRoomSchedule = new List<RoomSchedule>();
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Thiết lập LicenseContext
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets.First();
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var roomSchedule = new RoomSchedule
                        {
                            GiangVien = worksheet.Cells[row, 1].Text,
                            PhongHoc = worksheet.Cells[row, 2].Text,
                            Ngay = DateTime.Parse(worksheet.Cells[row, 3].Text),
                            ThoiGianBatDau = TimeSpan.Parse(worksheet.Cells[row, 4].Text),
                            ThoiGianKetThuc = TimeSpan.Parse(worksheet.Cells[row, 5].Text),
                            MonHoc = worksheet.Cells[row, 6].Text
                        };

                        // Kiểm tra ModelState cho mỗi roomSchedule
                        if (TryValidateModel(roomSchedule))
                        {
                            listRoomSchedule.Add(roomSchedule);
                        }
                        else
                        {
                            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                            {
                                Console.WriteLine(error.ErrorMessage);
                            }
                        }
                    }
                }
            }

            if (ModelState.IsValid)
            {
                foreach (var roomSchedule in listRoomSchedule)
                {
                    await _roomScheduleRepository.AddAsync(roomSchedule);
                }

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
