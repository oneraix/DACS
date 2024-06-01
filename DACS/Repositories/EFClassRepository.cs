using DACS.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public async Task<IEnumerable<Class>> GetAllClassesAsync()
        {
            return await _context.Classes.Include(c => c.RoomCategory).ToListAsync();
        }

        public async Task<Class> GetClassByIdAsync(string maPhongHoc)
        {
            return await _context.Classes.Include(c => c.RoomCategory)
                                         .FirstOrDefaultAsync(c => c.MaPhongHoc == maPhongHoc);
        }

        public async Task AddClassAsync(Class classEntity)
        {
            _context.Classes.Add(classEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateClassAsync(Class classEntity)
        {
            _context.Entry(classEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteClassAsync(string maPhongHoc)
        {
            var classEntity = await _context.Classes.FindAsync(maPhongHoc);
            if (classEntity != null)
            {
                _context.Classes.Remove(classEntity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
