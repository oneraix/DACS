using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DACS.Models
{
    public class IssueCategory
    {
        [Key]
        public string IssueCategoryId { get; set; }
        public string IssueCategoryName { get; set; }

        // Collection to hold associated IssueDetails
        public ICollection<IssueDetail> IssueDetails { get; set; }
    }
}
