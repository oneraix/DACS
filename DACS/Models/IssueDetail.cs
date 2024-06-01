using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DACS.Models
{
    public class IssueDetail
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }

        // Foreign key for IssueCategory
        public string IssueCategoryId { get; set; }
        [ForeignKey("IssueCategoryId")]
        public IssueCategory? IssueCategory { get; set; }
    }
}
