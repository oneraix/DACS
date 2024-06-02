using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DACS.Models
{
    public class SupportRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RequestId { get; set; }
        public string Description { get; set; }
        public DateTime ReportDate { get; set; } = DateTime.Now;
        public bool IsResolved { get; set; }

        // Khóa ngoại cho Class
        public string MaPhongHoc { get; set; }
        [ForeignKey("MaPhongHoc")]
        public Class? Class { get; set; }

        // Khóa ngoại cho IssueCategory
        public int IssueCategoryId { get; set; }
        [ForeignKey("IssueCategoryId")]
        public IssueCategory? IssueCategory { get; set; }

        // Khóa ngoại cho IssueDetail
        public int IssueDetailId { get; set; }
        [ForeignKey("IssueDetailId")]
        public IssueDetail? IssueDetail { get; set; }

        // Khóa ngoại cho ApplicationUser
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser? ApplicationUser { get; set; }
    }

}
