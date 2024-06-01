using DACS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DACS.Repositories
{
    public interface IRoomCategoriesRepository
    {
        Task<IEnumerable<RoomCategories>> GetAllRoomCategoriesAsync();
        Task<RoomCategories> GetRoomCategoryByIdAsync(string maLoaiPhong);
        Task AddRoomCategoryAsync(RoomCategories roomCategory);
        Task UpdateRoomCategoryAsync(RoomCategories roomCategory);
        Task DeleteRoomCategoryAsync(string maLoaiPhong);
    }
}