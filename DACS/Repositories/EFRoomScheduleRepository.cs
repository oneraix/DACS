using DACS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DACS.Repositories
{
    public class EFRoomScheduleRepository : IRoomScheduleRepository
    {
        private readonly ApplicationDbContext _context;

        public EFRoomScheduleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RoomSchedule>> GetAllAsync()
        {
            return await _context.RoomSchedules.ToListAsync();
        }

        public async Task<RoomSchedule> GetByIdAsync(int id)
        {
            return await _context.RoomSchedules.FindAsync(id);
        }

        public async Task<RoomSchedule> AddAsync(RoomSchedule roomSchedule)
        {
            _context.RoomSchedules.Add(roomSchedule);
            await _context.SaveChangesAsync();
            return roomSchedule;
        }

        public async Task<RoomSchedule> UpdateAsync(RoomSchedule roomSchedule)
        {
            _context.Entry(roomSchedule).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return roomSchedule;
        }

        public async Task DeleteAsync(int id)
        {
            var roomSchedule = await _context.RoomSchedules.FindAsync(id);
            if (roomSchedule != null)
            {
                _context.RoomSchedules.Remove(roomSchedule);
                await _context.SaveChangesAsync();
            }
        }
    }
}
