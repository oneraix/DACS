
using DACS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DACS.Repositories
{
    public interface ILoanTicketRepository
    {
        Task<IEnumerable<LoanEquipmentTicket>> GetAllTicketsAsync();
        Task<LoanEquipmentTicket> GetTicketByIdAsync(int ticketId);
        Task<LoanEquipmentTicket> CreateTicketAsync(LoanEquipmentTicket ticket);
        Task UpdateTicketAsync(LoanEquipmentTicket ticket);
        Task DeleteTicketAsync(int ticketId);
    }
}
