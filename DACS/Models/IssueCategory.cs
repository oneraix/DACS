using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DACS.Models
{
    public class IssueCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IssueCategoryId { get; set; }
        public string IssueCategoryName { get; set; }

        // Collection to hold associated IssueDetails
        public ICollection<IssueDetail>? IssueDetails { get; set; }
    }
}
