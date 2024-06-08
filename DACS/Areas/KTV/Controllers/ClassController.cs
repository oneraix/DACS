using DACS.Models;
using DACS.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DACS.Areas.KTV.Controllers
{
    [Area("KTV")]
    [Authorize(Roles = Role.Role_KTV)]
    public class ClassController : Controller
    {
        private readonly IClassRepository _classRepository;
        private readonly IRoomCategoriesRepository _roomCategoriesRepository;

        public ClassController(IClassRepository classRepository, IRoomCategoriesRepository roomCategoriesRepository)
        {
            _classRepository = classRepository;
            _roomCategoriesRepository = roomCategoriesRepository;
        }

        public async Task<IActionResult> Index(int? tang, string maLoaiPhong)
        {
            var classes = await _classRepository.GetAllClassesAsync();

            if (tang.HasValue)
            {
                classes = classes.Where(c => c.Tang == tang.Value).ToList();
            }

            if (!string.IsNullOrEmpty(maLoaiPhong))
            {
                classes = classes.Where(c => c.MaLoaiPhong == maLoaiPhong).ToList();
            }

            ViewBag.SelectedFloor = tang;
            ViewBag.SelectedCategory = maLoaiPhong;
            ViewBag.RoomCategories = new SelectList(await _roomCategoriesRepository.GetAllRoomCategoriesAsync(), "MaLoaiPhong", "TenLoaiPhong");

            return View(classes);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.MaLoaiPhong = new SelectList(await _roomCategoriesRepository.GetAllRoomCategoriesAsync(), "MaLoaiPhong", "TenLoaiPhong");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Class classEntity)
        {
            if (ModelState.IsValid)
            {
                await _classRepository.AddClassAsync(classEntity);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MaLoaiPhong = new SelectList(await _roomCategoriesRepository.GetAllRoomCategoriesAsync(), "MaLoaiPhong", "TenLoaiPhong", classEntity.MaLoaiPhong);
            return View(classEntity);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classEntity = await _classRepository.GetClassByIdAsync(id);
            if (classEntity == null)
            {
                return NotFound();
            }
            ViewBag.MaLoaiPhong = new SelectList(await _roomCategoriesRepository.GetAllRoomCategoriesAsync(), "MaLoaiPhong", "TenLoaiPhong", classEntity.MaLoaiPhong);
            return View(classEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Class classEntity)
        {
            if (id != classEntity.MaPhongHoc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _classRepository.UpdateClassAsync(classEntity);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _classRepository.GetClassByIdAsync(id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MaLoaiPhong = new SelectList(await _roomCategoriesRepository.GetAllRoomCategoriesAsync(), "MaLoaiPhong", "TenLoaiPhong", classEntity.MaLoaiPhong);
            return View(classEntity);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classEntity = await _classRepository.GetClassByIdAsync(id);
            if (classEntity == null)
            {
                return NotFound();
            }

            return View(classEntity);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _classRepository.DeleteClassAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
