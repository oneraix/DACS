using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DACS.Models;

namespace DACS.Repositories
{
    public interface IRoomScheduleRepository
    {
        Task<IEnumerable<RoomSchedule>> GetAllAsync();
        Task<RoomSchedule> GetByIdAsync(int id);
        Task<RoomSchedule> AddAsync(RoomSchedule roomSchedule);
        Task<RoomSchedule> UpdateAsync(RoomSchedule roomSchedule);
        Task DeleteAsync(int id);
    }
}
