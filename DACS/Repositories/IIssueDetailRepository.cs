using DACS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DACS.Repositories
{
    public interface IIssueDetailRepository
    {
        Task<IEnumerable<IssueDetail>> GetAllAsync();
        Task<IssueDetail> GetByIdAsync(int id);
        Task<IssueDetail> CreateAsync(IssueDetail detail);
        Task<IssueDetail> UpdateAsync(IssueDetail detail);
        Task DeleteAsync(int id);
    }
}
