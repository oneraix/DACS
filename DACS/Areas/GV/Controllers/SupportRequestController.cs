using Microsoft.AspNetCore.Mvc;
using DACS.Models;
using DACS.Repositories;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace DACS.Areas.GV.Controllers
{
    [Area("GV")]
    [Authorize(Roles = Role.Role_GV)]
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


        public async Task<IActionResult> Index(string searchTerm, bool? isResolved)
        {
            var userId = _userManager.GetUserId(User);
            var supportRequests = await _supportRepository.GetAllAsync();

            // Chỉ lấy các yêu cầu hỗ trợ của người dùng hiện tại
            supportRequests = supportRequests.Where(sr => sr.UserId == userId).ToList();

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


        public async Task<IActionResult> Details(int id)
        {
            var supportRequest = await _supportRepository.GetByIdAsync(id);

            if (supportRequest == null || supportRequest.UserId != _userManager.GetUserId(User))
            {
                return NotFound();
            }

            return View(supportRequest);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var supportRequest = await _supportRepository.GetByIdAsync(id);

            if (supportRequest == null || supportRequest.UserId != _userManager.GetUserId(User))
            {
                return NotFound();
            }

            ViewBag.Classes = _context.Classes.ToList();
            ViewBag.IssueCategories = _context.IssueCategories.ToList();
            ViewBag.IssueDetails = _context.IssueDetails.ToList();
            return View(supportRequest);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RequestId,Description,ReportDate,IsResolved,MaPhongHoc,IssueCategoryId,IssueDetailId,UserId")] SupportRequest supportRequest)
        {
            if (id != supportRequest.RequestId || supportRequest.UserId != _userManager.GetUserId(User))
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

            ViewBag.Classes = _context.Classes.ToList();
            ViewBag.IssueCategories = _context.IssueCategories.ToList();
            ViewBag.IssueDetails = _context.IssueDetails.ToList();
            return View(supportRequest);
        }

        private async Task<bool> SupportRequestExists(int id)
        {
            var supportRequest = await _supportRepository.GetByIdAsync(id);
            return supportRequest != null;
        }
    }
}
