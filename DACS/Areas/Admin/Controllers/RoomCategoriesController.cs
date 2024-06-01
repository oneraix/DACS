using DACS.Models;
using DACS.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DACS.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Role.Role_Admin)]
    public class RoomCategoriesController : Controller
    {
        private readonly IRoomCategoriesRepository _roomCategoriesRepository;

        public RoomCategoriesController(IRoomCategoriesRepository roomCategoriesRepository)
        {
            _roomCategoriesRepository = roomCategoriesRepository;
        }

        public async Task<IActionResult> Index()
        {
            var roomCategories = await _roomCategoriesRepository.GetAllRoomCategoriesAsync();
            return View(roomCategories);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomCategory = await _roomCategoriesRepository.GetRoomCategoryByIdAsync(id);
            if (roomCategory == null)
            {
                return NotFound();
            }

            return View(roomCategory);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoomCategories roomCategory)
        {
            if (ModelState.IsValid)
            {
                await _roomCategoriesRepository.AddRoomCategoryAsync(roomCategory);
                return RedirectToAction(nameof(Index));
            }
            return View(roomCategory);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomCategory = await _roomCategoriesRepository.GetRoomCategoryByIdAsync(id);
            if (roomCategory == null)
            {
                return NotFound();
            }

            return View(roomCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, RoomCategories roomCategory)
        {
            if (id != roomCategory.MaLoaiPhong)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _roomCategoriesRepository.UpdateRoomCategoryAsync(roomCategory);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _roomCategoriesRepository.GetRoomCategoryByIdAsync(id) == null)
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
            return View(roomCategory);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomCategory = await _roomCategoriesRepository.GetRoomCategoryByIdAsync(id);
            if (roomCategory == null)
            {
                return NotFound();
            }

            return View(roomCategory);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _roomCategoriesRepository.DeleteRoomCategoryAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
