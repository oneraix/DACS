using Microsoft.AspNetCore.Mvc;
using DACS.Models;
using DACS.Repositories;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace DACS.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Role.Role_Admin)]
    public class LoanEquipmentTicketController : Controller
    {
        private readonly ILoanTicketRepository _loanTicketRepository;
        private readonly ApplicationDbContext _context;

        public LoanEquipmentTicketController(ApplicationDbContext context, ILoanTicketRepository loanTicketRepository)
        {
            _context = context;
            _loanTicketRepository = loanTicketRepository;
        }

        // GET: LoanEquipmentTicket
        public async Task<IActionResult> Index()
        {
            var tickets = await _loanTicketRepository.GetAllTicketsAsync();
            return View(tickets);
        }

        // GET: LoanEquipmentTicket/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var ticket = await _context.LoanEquipmentTickets
                .Include(t => t.LoanEquipment)
                .Include(t => t.Class)
                .FirstOrDefaultAsync(t => t.MaPhieuMuon == id);

            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: LoanEquipmentTicket/Create
        public IActionResult Create()
        {
            ViewBag.Equipments = new SelectList(_context.LoanEquipments, "MaThietBiMuon", "TenThietBiMuon");
            ViewBag.Classrooms = new SelectList(_context.Classes, "MaPhongHoc", "MaPhongHoc");
            return View();
        }

        // POST: LoanEquipmentTicket/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenNguoiMuon,SDT,MaThietBiMuon,SoLuongMuon,NgayMuon,NgayTra,MaPhongHoc")] LoanEquipmentTicket loanTicket)
        {
            // Tải các đối tượng liên quan từ cơ sở dữ liệu
            loanTicket.LoanEquipment = await _context.LoanEquipments.FirstOrDefaultAsync(e => e.MaThietBiMuon == loanTicket.MaThietBiMuon);
            loanTicket.Class = await _context.Classes.FirstOrDefaultAsync(c => c.MaPhongHoc == loanTicket.MaPhongHoc);

            // Kiểm tra xem các đối tượng liên quan có tồn tại không
            if (loanTicket.LoanEquipment == null || loanTicket.Class == null)
            {
                ModelState.AddModelError("", "The specified Equipment or Class does not exist.");
                ViewBag.Equipments = new SelectList(_context.LoanEquipments, "MaThietBiMuon", "TenThietBiMuon");
                ViewBag.Classrooms = new SelectList(_context.Classes, "MaPhongHoc", "MaPhongHoc");
                return View(loanTicket);
            }

            if (ModelState.IsValid)
            {
                _context.LoanEquipmentTickets.Add(loanTicket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // Nếu model không hợp lệ, tái tạo các SelectList để trả về form
            ViewBag.Equipments = new SelectList(_context.LoanEquipments, "MaThietBiMuon", "TenThietBiMuon", loanTicket.MaThietBiMuon);
            ViewBag.Classrooms = new SelectList(_context.Classes, "MaPhongHoc", "MaPhongHoc", loanTicket.MaPhongHoc);
            return View(loanTicket);
        }

        // GET: LoanEquipmentTicket/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var loanTicket = await _loanTicketRepository.GetTicketByIdAsync(id);
            if (loanTicket == null)
            {
                return NotFound();
            }
            ViewBag.Equipments = new SelectList(_context.LoanEquipments, "MaThietBiMuon", "TenThietBiMuon", loanTicket.MaThietBiMuon);
            ViewBag.Classrooms = new SelectList(_context.Classes, "MaPhongHoc", "MaPhongHoc", loanTicket.MaPhongHoc);
            return View(loanTicket);
        }

        // POST: LoanEquipmentTicket/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaPhieuMuon, TenNguoiMuon, SDT, MaThietBiMuon, SoLuongMuon, NgayMuon, NgayTra, MaPhongHoc")] LoanEquipmentTicket loanTicket)
        {
            if (id != loanTicket.MaPhieuMuon)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loanTicket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoanTicketExists(loanTicket.MaPhieuMuon))
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
            ViewBag.Equipments = new SelectList(_context.LoanEquipments, "MaThietBiMuon", "TenThietBiMuon", loanTicket.MaThietBiMuon);
            ViewBag.Classrooms = new SelectList(_context.Classes, "MaPhongHoc", "MaPhongHoc", loanTicket.MaPhongHoc);
            return View(loanTicket);
        }

        // GET: LoanEquipmentTicket/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var loanTicket = await _loanTicketRepository.GetTicketByIdAsync(id);
            if (loanTicket == null)
            {
                return NotFound();
            }
            return View(loanTicket);
        }

        // POST: LoanEquipmentTicket/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loanTicket = await _context.LoanEquipmentTickets.FindAsync(id);
            _context.LoanEquipmentTickets.Remove(loanTicket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoanTicketExists(int id)
        {
            return _context.LoanEquipmentTickets.Any(e => e.MaPhieuMuon == id);
        }
    }
}
