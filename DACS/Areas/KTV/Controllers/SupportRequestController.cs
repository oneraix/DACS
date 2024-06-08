using Microsoft.AspNetCore.Mvc;
using DACS.Models;
using DACS.Repositories;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace DACS.Areas.KTV.Controllers
{
    [Area("KTV")]
    [Authorize(Roles = Role.Role_KTV)]
    public class SupportRequestController : Controller
    {
        private readonly ISupportRequestRepository _supportRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public SupportRequestController(ISupportRequestRepository supportRepository, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _supportRepository = supportRepository;
            _userManager = userManager;
            _context = context;
        }

        // GET: SupportRequest
        public async Task<IActionResult> Index(string searchTerm, bool? isResolved)
        {
            var supportRequests = await _supportRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                supportRequests = supportRequests.Where(sr => sr.Description.Contains(searchTerm)).ToList();
            }

            if (isResolved.HasValue)
            {
                supportRequests = supportRequests.Where(sr => sr.IsResolved == isResolved.Value).ToList();
            }

            ViewBag.SearchTerm = searchTerm;
            ViewBag.IsResolved = isResolved;

            return View(supportRequests);
        }

        // GET: SupportRequest/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var supportRequest = await _supportRepository.GetByIdAsync(id);

            if (supportRequest == null)
            {
                return NotFound();
            }

            return View(supportRequest);
        }

        // GET: SupportRequest/Create
        public IActionResult Create()
        {
            ViewBag.Classes = _context.Classes.ToList();
            ViewBag.IssueCategories = _context.IssueCategories.ToList();
            ViewBag.IssueDetails = _context.IssueDetails.ToList();
            ViewBag.UserId = _userManager.GetUserId(User); // Lấy UserId của người dùng đang đăng nhập
            return View();
        }

        // POST: SupportRequest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description,MaPhongHoc,IssueCategoryId,IssueDetailId,UserId")] SupportRequest supportRequest)
        {
            supportRequest.IsResolved = false;
            supportRequest.UserId = _userManager.GetUserId(User); // Lấy UserId của người dùng đang đăng nhập
            supportRequest.ReportDate = DateTime.Now; // Thiết lập ngày báo cáo hiện tại

            if (ModelState.IsValid)
            {
                await _supportRepository.AddAsync(supportRequest);
                await _supportRepository.SaveAsync();
                return RedirectToAction(nameof(Index));
            }

            // Log thông tin lỗi nếu ModelState không hợp lệ
            foreach (var modelState in ViewData.ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }

            ViewBag.Classes = _context.Classes.ToList();
            ViewBag.IssueCategories = _context.IssueCategories.ToList();
            ViewBag.IssueDetails = _context.IssueDetails.ToList();
            ViewBag.UserId = _userManager.GetUserId(User); // Lấy UserId của người dùng đăng nhập
            return View(supportRequest);
        }

        // GET: SupportRequest/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var supportRequest = await _supportRepository.GetByIdAsync(id);

            if (supportRequest == null)
            {
                return NotFound();
            }

            return View(supportRequest);
        }

        // POST: SupportRequest/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RequestId,Description,ReportDate,IsResolved,MaPhongHoc,IssueCategoryId,IssueDetailId,UserId")] SupportRequest supportRequest)
        {
            if (id != supportRequest.RequestId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _supportRepository.UpdateAsync(supportRequest);
                    await _supportRepository.SaveAsync();
                }
                catch (Exception)
                {
                    if (!await SupportRequestExists(supportRequest.RequestId))
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
            return View(supportRequest);
        }

        // GET: SupportRequest/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var supportRequest = await _supportRepository.GetByIdAsync(id);

            if (supportRequest == null)
            {
                return NotFound();
            }

            return View(supportRequest);
        }

        // POST: SupportRequest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _supportRepository.DeleteAsync(id);
            await _supportRepository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> SupportRequestExists(int id)
        {
            var supportRequest = await _supportRepository.GetByIdAsync(id);
            return supportRequest != null;
        }
    }
}
