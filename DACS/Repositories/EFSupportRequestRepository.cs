using DACS.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DACS.Repositories
{
    public class EFSupportRequestRepository : ISupportRequestRepository
    {
        private readonly ApplicationDbContext _context;

        public EFSupportRequestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SupportRequest>> GetAllAsync()
        {
            return await _context.SupportRequests
                .Include(s => s.Class)
                .Include(s => s.IssueCategory)
                .Include(s => s.IssueDetail)
                .Include(s => s.ApplicationUser)
                .ToListAsync();
        }

        public async Task<SupportRequest> GetByIdAsync(int requestId)
        {
            return await _context.SupportRequests
                .Include(s => s.Class)
                .Include(s => s.IssueCategory)
                .Include(s => s.IssueDetail)
                .Include(s => s.ApplicationUser)
                .FirstOrDefaultAsync(s => s.RequestId == requestId);
        }

        public async Task AddAsync(SupportRequest supportRequest)
        {
            await _context.SupportRequests.AddAsync(supportRequest);
        }

        public async Task UpdateAsync(SupportRequest supportRequest)
        {
            _context.SupportRequests.Update(supportRequest);
        }

        public async Task DeleteAsync(int requestId)
        {
            var supportRequest = await _context.SupportRequests.FindAsync(requestId);
            if (supportRequest != null)
            {
                _context.SupportRequests.Remove(supportRequest);
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}