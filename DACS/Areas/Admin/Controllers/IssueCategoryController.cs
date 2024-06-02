using Microsoft.AspNetCore.Mvc;
using DACS.Models;
using DACS.Repositories;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace DACS.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class IssueCategoryController : Controller
    {
        private readonly IIssueCategoryRepository _issueCategoryRepository;

        public IssueCategoryController(IIssueCategoryRepository issueCategoryRepository)
        {
            _issueCategoryRepository = issueCategoryRepository;
        }

        // GET: IssueCategory
        public async Task<IActionResult> Index()
        {
            return View(await _issueCategoryRepository.GetAllAsync());
        }

        // GET: IssueCategory/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var category = await _issueCategoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // GET: IssueCategory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: IssueCategory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IssueCategoryName")] IssueCategory category)
        {
            if (ModelState.IsValid)
            {
                await _issueCategoryRepository.CreateAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: IssueCategory/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _issueCategoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: IssueCategory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IssueCategoryId,IssueCategoryName")] IssueCategory category)
        {
            if (id != category.IssueCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _issueCategoryRepository.UpdateAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: IssueCategory/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _issueCategoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: IssueCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _issueCategoryRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
