using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DACS.Models
{
    public class IssueDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Description { get; set; }

        // Foreign key for IssueCategory
        public int IssueCategoryId { get; set; }
        [ForeignKey("IssueCategoryId")]
        public IssueCategory? IssueCategory { get; set; }
    }
}
