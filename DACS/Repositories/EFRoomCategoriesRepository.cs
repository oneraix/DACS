using DACS.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DACS.Repositories
{
    public class EFRoomCategoriesRepository : IRoomCategoriesRepository
    {
        private readonly ApplicationDbContext _context;
        public EFRoomCategoriesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RoomCategories>> GetAllRoomCategoriesAsync()
        {
            return await _context.RoomCategories.Include(rc => rc.Classes).ToListAsync();
        }

        public async Task<RoomCategories> GetRoomCategoryByIdAsync(string maLoaiPhong)
        {
            return await _context.RoomCategories.Include(rc => rc.Classes)
                                                .FirstOrDefaultAsync(rc => rc.MaLoaiPhong == maLoaiPhong);
        }

        public async Task AddRoomCategoryAsync(RoomCategories roomCategory)
        {
            _context.RoomCategories.Add(roomCategory);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRoomCategoryAsync(RoomCategories roomCategory)
        {
            _context.Entry(roomCategory).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRoomCategoryAsync(string maLoaiPhong)
        {
            var roomCategory = await _context.RoomCategories.FindAsync(maLoaiPhong);
            if (roomCategory != null)
            {
                _context.RoomCategories.Remove(roomCategory);
                await _context.SaveChangesAsync();
            }
        }
    }
}