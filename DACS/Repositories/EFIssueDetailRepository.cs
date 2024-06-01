using DACS.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DACS.Repositories
{
    public class EFIssueDetailRepository : IIssueDetailRepository
    {
        private readonly ApplicationDbContext _context;

        public EFIssueDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<IssueDetail>> GetAllAsync()
        {
            return await _context.IssueDetails
                .Include(d => d.IssueCategory)
                .ToListAsync();
        }

        public async Task<IssueDetail> GetByIdAsync(int id)
        {
            return await _context.IssueDetails
                .Include(d => d.IssueCategory)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IssueDetail> CreateAsync(IssueDetail detail)
        {
            _context.IssueDetails.Add(detail);
            await _context.SaveChangesAsync();
            return detail;
        }

        public async Task<IssueDetail> UpdateAsync(IssueDetail detail)
        {
            _context.IssueDetails.Update(detail);
            await _context.SaveChangesAsync();
            return detail;
        }

        public async Task DeleteAsync(int id)
        {
            var detail = await _context.IssueDetails.FindAsync(id);
            if (detail != null)
            {
                _context.IssueDetails.Remove(detail);
                await _context.SaveChangesAsync();
            }
        }
    }
}
