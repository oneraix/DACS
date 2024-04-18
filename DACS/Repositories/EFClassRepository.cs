using DACS.Models;
using Microsoft.EntityFrameworkCore;

namespace DACS.Repositories
{
    public class EFClassRepository:IClassRepository
    {
        private readonly ApplicationDbContext _context;
        public EFClassRepository(ApplicationDbContext context) {  _context = context; }

        public async Task<IEnumerable<Class>> GetAllAsync()
        {
            // return await _context.Products.ToListAsync();
            return await _context.Classes.Include(p => p.TrangThai) // Include thông tin về category
        .ToListAsync();
        }

        public async Task<Class> GetByIdAsync(string MaPhongHoc)
        {
            // return await _context.Products.FindAsync(id);
            // lấy thông tin kèm theo category
            return await _context.Classes.Include(p => p.TrangThai).FirstOrDefaultAsync(p => p.MaPhongHoc == MaPhongHoc);
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
            var product = await _context.Classes.FindAsync(MaPhongHoc);
            _context.Classes.Remove(product);
            await _context.SaveChangesAsync();
        }

        //tìm kiếm
        public async Task<IEnumerable<Class>> SearchAsync(string keyword)
        {
            // Sử dụng LINQ để thực hiện tìm kiếm sản phẩm có tên hoặc mô tả chứa từ khóa
            return await _context.Classes
                .Include(p => p.TrangThai) // Include thông tin về category
                .Where(p => p.MaPhongHoc.Contains(keyword) || p.GhiChu.Contains(keyword))
                .ToListAsync();
        }
    }
}
