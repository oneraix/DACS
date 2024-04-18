using System.Collections.Generic;
using System.Threading.Tasks;
using DACS.Models;

namespace DACS.Repositories
{
    public interface IClassRepository
    {
        Task<IEnumerable<Class>> GetAllAsync();
        Task<Class> GetByIdAsync(string MaPhongHoc);
        Task AddAsync(Class @class); // Sử dụng @class thay vì class vì class là từ khóa của ngôn ngữ
        Task UpdateAsync(Class @class);
        Task DeleteAsync(string MaPhongHoc);
        Task<IEnumerable<Class>> SearchAsync(string keyword);
    }
}
