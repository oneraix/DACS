using DACS.Models;
using DACS.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class EFLoanEquipmentRepository : ILoanEquipmentRepository
{
    private readonly ApplicationDbContext _context;

    public EFLoanEquipmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<LoanEquipment>> GetAllAsync()
    {
        return await _context.LoanEquipments.ToListAsync();
    }

    public async Task<LoanEquipment> GetByIdAsync(string id)
    {
        return await _context.LoanEquipments
            .FirstOrDefaultAsync(e => e.MaThietBiMuon == id);
    }

    public async Task<LoanEquipment> CreateAsync(LoanEquipment equipment)
    {
        _context.Add(equipment);
        await _context.SaveChangesAsync();
        return equipment; // Trả về đối tượng sau khi thêm
    }

    public async Task<LoanEquipment> UpdateAsync(LoanEquipment equipment)
    {
        _context.Update(equipment);
        await _context.SaveChangesAsync();
        return equipment; // Trả về đối tượng sau khi cập nhật
    }


    public async Task DeleteAsync(string id)
    {
        var equipment = await GetByIdAsync(id);
        if (equipment != null)
        {
            _context.LoanEquipments.Remove(equipment);
            await _context.SaveChangesAsync();
        }
    }
}
