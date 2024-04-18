using DACS.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DACS.Repositories
{
    public class EFClassRepository : IClassRepository
    {
        private readonly ApplicationDbContext _context;

        public EFClassRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Class>> GetAllAsync()
        {
            return await _context.Classes.ToListAsync();
        }

        public async Task<Class> GetByIdAsync(string MaPhongHoc)
        {
            return await _context.Classes.FirstOrDefaultAsync(p => p.MaPhongHoc == MaPhongHoc);
        }

        public async Task AddAsync(Class @class)
        {
            _context.Classes.Add(@class);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Class @class)
        {
            _context.Classes.Update(@class);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string MaPhongHoc)
        {
            var classToRemove = await _context.Classes.FindAsync(MaPhongHoc);
            if (classToRemove != null)
            {
                _context.Classes.Remove(classToRemove);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Class>> SearchAsync(string keyword)
        {
            return await _context.Classes
                .Where(p => p.MaPhongHoc.Contains(keyword) || p.GhiChu.Contains(keyword))
                .ToListAsync();
        }
    }
}
