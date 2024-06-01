using DACS.Models;

namespace DACS.Repositories
{
    public interface ILoanEquipmentRepository
    {
        Task<LoanEquipment> GetByIdAsync(string id);
        Task<IEnumerable<LoanEquipment>> GetAllAsync();
        Task<LoanEquipment> CreateAsync(LoanEquipment equipment);
        Task<LoanEquipment> UpdateAsync(LoanEquipment equipment);
        Task DeleteAsync(string id);
    }
}
