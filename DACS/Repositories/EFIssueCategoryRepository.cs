
using DACS.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DACS.Repositories
{
    public class EFIssueCategoryRepository : IIssueCategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public EFIssueCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<IssueCategory>> GetAllAsync()
        {
            return await _context.IssueCategories.ToListAsync();
        }

        public async Task<IssueCategory> GetByIdAsync(string id)
        {
            return await _context.IssueCategories.FindAsync(id);
        }

        public async Task<IssueCategory> CreateAsync(IssueCategory category)
        {
            _context.IssueCategories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<IssueCategory> UpdateAsync(IssueCategory category)
        {
            _context.IssueCategories.Update(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task DeleteAsync(string id)
        {
            var category = await _context.IssueCategories.FindAsync(id);
            if (category != null)
            {
                _context.IssueCategories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}
