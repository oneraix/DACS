using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DACS.Models;
using DACS.Repositories;

namespace DACS.Controllers
{
    public class LoanEquipmentController : Controller
    {
        private readonly ILoanEquipmentRepository _equipmentRepository;

        public LoanEquipmentController(ILoanEquipmentRepository equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;
        }

        // GET: LoanEquipment
        public async Task<IActionResult> Index()
        {
            return View(await _equipmentRepository.GetAllAsync());
        }

        // GET: LoanEquipment/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var equipment = await _equipmentRepository.GetByIdAsync(id);
            if (equipment == null)
            {
                return NotFound();
            }

            return View(equipment);
        }

        // GET: LoanEquipment/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LoanEquipment/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("MaThietBiMuon, TenThietBiMuon, SoLuong")] LoanEquipment equipment)
        {
            if (ModelState.IsValid)
            {
                await _equipmentRepository.CreateAsync(equipment);
                return RedirectToAction(nameof(Index));
            }
            return View(equipment);
        }

        // GET: LoanEquipment/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var equipment = await _equipmentRepository.GetByIdAsync(id);
            if (equipment == null)
            {
                return NotFound();
            }

            return View(equipment);
        }

        // POST: LoanEquipment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaThietBiMuon,TenThietBiMuon,SoLuong")] LoanEquipment equipment)
        {
            if (id != equipment.MaThietBiMuon)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _equipmentRepository.UpdateAsync(equipment);
                }
                catch
                {
                    if (!await EquipmentExists(equipment.MaThietBiMuon))
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
            return View(equipment);
        }

        // GET: LoanEquipment/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var equipment = await _equipmentRepository.GetByIdAsync(id);
            if (equipment == null)
            {
                return NotFound();
            }

            return View(equipment);
        }

        // POST: LoanEquipment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _equipmentRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> EquipmentExists(string id)
        {
            return await _equipmentRepository.GetByIdAsync(id) != null;
        }
    }
}
