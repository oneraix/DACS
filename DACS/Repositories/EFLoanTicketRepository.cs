using DACS.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DACS.Repositories
{
    public class EFLoanTicketRepository : ILoanTicketRepository
    {
        private readonly ApplicationDbContext _context;

        public EFLoanTicketRepository(ApplicationDbContext context)
        {
            _context = context; // Gán giá trị của context từ constructor, không khai báo biến mới
        }

        // Lấy tất cả các phiếu mượn
        public async Task<IEnumerable<LoanEquipmentTicket>> GetAllTicketsAsync()
        {
            return await _context.LoanEquipmentTickets
                                 .Include(t => t.LoanEquipment)
                                 .Include(t => t.Class)
                                 .ToListAsync();
        }

        // Lấy một phiếu mượn dựa trên ID
        public async Task<LoanEquipmentTicket> GetTicketByIdAsync(int ticketId)
        {
            return await _context.LoanEquipmentTickets
                                 .Include(t => t.LoanEquipment)
                                 .Include(t => t.Class)
                                 .FirstOrDefaultAsync(t => t.MaPhieuMuon == ticketId);
        }

        // Tạo một phiếu mượn mới
        public async Task<LoanEquipmentTicket> CreateTicketAsync(LoanEquipmentTicket ticket)
        {
            _context.LoanEquipmentTickets.Add(ticket);
            await _context.SaveChangesAsync();
            return ticket;
        }

        // Cập nhật thông tin phiếu mượn
        public async Task UpdateTicketAsync(LoanEquipmentTicket ticket)
        {
            _context.Entry(ticket).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // Xóa một phiếu mượn dựa trên ID
        public async Task DeleteTicketAsync(int ticketId)
        {
            var ticket = await GetTicketByIdAsync(ticketId);
            if (ticket != null)
            {
                _context.LoanEquipmentTickets.Remove(ticket);
                await _context.SaveChangesAsync();
            }
        }
    }
}
