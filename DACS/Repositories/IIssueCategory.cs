using DACS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DACS.Repositories
{
    public interface IIssueCategoryRepository
    {
        Task<IEnumerable<IssueCategory>> GetAllAsync();
        Task<IssueCategory> GetByIdAsync(string id);
        Task<IssueCategory> CreateAsync(IssueCategory category);
        Task<IssueCategory> UpdateAsync(IssueCategory category);
        Task DeleteAsync(string id);
    }
}
