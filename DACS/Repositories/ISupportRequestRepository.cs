using DACS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DACS.Repositories
{
    public interface ISupportRequestRepository
    {
        Task<IEnumerable<SupportRequest>> GetAllAsync();
        Task<SupportRequest> GetByIdAsync(int requestId);
        Task AddAsync(SupportRequest supportRequest);
        Task UpdateAsync(SupportRequest supportRequest);
        Task DeleteAsync(int requestId);
        Task SaveAsync();
    }
}

