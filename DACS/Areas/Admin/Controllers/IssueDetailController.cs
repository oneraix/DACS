using Microsoft.AspNetCore.Mvc;
using DACS.Models;
using DACS.Repositories;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DACS.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class IssueDetailController : Controller
    {
        private readonly IIssueDetailRepository _issueDetailRepository;
        private readonly IIssueCategoryRepository _issueCategoryRepository;

        public IssueDetailController(IIssueDetailRepository issueDetailRepository, IIssueCategoryRepository issueCategoryRepository)
        {
            _issueDetailRepository = issueDetailRepository;
            _issueCategoryRepository = issueCategoryRepository;
        }

        // GET: IssueDetail
        public async Task<IActionResult> Index()
        {
            return View(await _issueDetailRepository.GetAllAsync());
        }

        // GET: IssueDetail/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var detail = await _issueDetailRepository.GetByIdAsync(id);
            if (detail == null)
            {
                return NotFound();
            }
            return View(detail);
        }

        // GET: IssueDetail/Create
        public async Task<IActionResult> Create()
        {
            ViewData["IssueCategoryId"] = new SelectList(await _issueCategoryRepository.GetAllAsync(), "IssueCategoryId", "IssueCategoryName");
            return View();
        }

        // POST: IssueDetail/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description,IssueCategoryId")] IssueDetail detail)
        {
            if (ModelState.IsValid)
            {
                await _issueDetailRepository.CreateAsync(detail);
                return RedirectToAction(nameof(Index));
            }
            ViewData["IssueCategoryId"] = new SelectList(await _issueCategoryRepository.GetAllAsync(), "IssueCategoryId", "IssueCategoryName", detail.IssueCategoryId);
            return View(detail);
        }

        // GET: IssueDetail/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var detail = await _issueDetailRepository.GetByIdAsync(id);
            if (detail == null)
            {
                return NotFound();
            }
            ViewData["IssueCategoryId"] = new SelectList(await _issueCategoryRepository.GetAllAsync(), "IssueCategoryId", "IssueCategoryName", detail.IssueCategoryId);
            return View(detail);
        }

        // POST: IssueDetail/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,IssueCategoryId")] IssueDetail detail)
        {
            if (id != detail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _issueDetailRepository.UpdateAsync(detail);
                return RedirectToAction(nameof(Index));
            }
            ViewData["IssueCategoryId"] = new SelectList(await _issueCategoryRepository.GetAllAsync(), "IssueCategoryId", "IssueCategoryName", detail.IssueCategoryId);
            return View(detail);
        }

        // GET: IssueDetail/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var detail = await _issueDetailRepository.GetByIdAsync(id);
            if (detail == null)
            {
                return NotFound();
            }
            return View(detail);
        }

        // POST: IssueDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _issueDetailRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
