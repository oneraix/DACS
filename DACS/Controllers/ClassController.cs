using DACS.Models;
using DACS.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DACS.Controllers
{
    public class ClassController : Controller
    {
        private readonly IClassRepository _classRepository;
        public ClassController(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        public async Task<IActionResult> Index(int? tang)
        {
            IEnumerable<Class> classes;
            if (tang.HasValue)
            {
                classes = await _classRepository.GetByFloorAsync(tang.Value);
            }
            else
            {
                classes = await _classRepository.GetAllAsync();
            }

            ViewBag.SelectedFloor = tang; // Gán giá trị của biến tang vào ViewBag
            return View(classes);
        }

        public async Task<IActionResult> Details(string id)
        {
            var @class = await _classRepository.GetByIdAsync(id);
            if (@class == null)
            {
                return NotFound(); // Return 404 Not Found if class is not found
            }
            return View("Details",@class);
        }

        // Action method for displaying the form to create a new class
        public IActionResult Create()
        {
            return View();
        }

        // Action method for handling the creation of a new class
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Class @class)
        {
            if (ModelState.IsValid)
            {
                await _classRepository.AddAsync(@class);
                return RedirectToAction(nameof(Index));
            }
            return View(@class);
        }

        // Action method for displaying the form to edit a class
        public async Task<IActionResult> Edit(string id)
        {
            var @class = await _classRepository.GetByIdAsync(id);
            if (@class == null)
            {
                return NotFound(); // Return 404 Not Found if class is not found
            }
            return View(@class);
        }

        // Action method for handling the editing of a class
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Class @class)
        {
            if (id != @class.MaPhongHoc)
            {
                return NotFound(); // Return 404 Not Found if class ID in the URL doesn't match class ID in the model
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _classRepository.UpdateAsync(@class);
                }
                catch
                {
                    return NotFound(); // Return 404 Not Found if class is not found
                }
                return RedirectToAction(nameof(Index));
            }
            return View(@class);
        }

        // Action method for displaying the form to delete a class
        public async Task<IActionResult> Delete(string id)
        {
            var @class = await _classRepository.GetByIdAsync(id);
            if (@class == null)
            {
                return NotFound(); // Return 404 Not Found if class is not found
            }
            return View(@class);
        }

        // Action method for handling the deletion of a class
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var @class = await _classRepository.GetByIdAsync(id);
            if (@class == null)
            {
                return NotFound(); // Return 404 Not Found if class is not found
            }
            await _classRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
