using DACS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DACS.Repositories
{
    public interface IClassRepository
    {
        Task<IEnumerable<Class>> GetAllClassesAsync();
        Task<Class> GetClassByIdAsync(string maPhongHoc);
        Task AddClassAsync(Class classEntity);
        Task UpdateClassAsync(Class classEntity);
        Task DeleteClassAsync(string maPhongHoc);
    }
}
/*namespace DACS.Repositories
{
    public interface IClassRepository
    {
        Task<IEnumerable<Class>> GetAllAsync();
        Task<Class> GetByIdAsync(string MaPhongHoc);
        Task AddAsync(Class @class); // Sử dụng @class thay vì class vì class là từ khóa của ngôn ngữ
        Task UpdateAsync(Class @class);
        Task DeleteAsync(string MaPhongHoc);
        Task<IEnumerable<Class>> SearchAsync(string keyword);
        Task<IEnumerable<Class>> GetByFloorAsync(int Tang);
    }
}*/
