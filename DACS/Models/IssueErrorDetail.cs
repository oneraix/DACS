namespace DACS.Models
{
    public class IssueErrorDetail
    {
        public int IssueReportId { get; set; } // ID của báo cáo sự cố
        public int ErrorDetailId { get; set; } // ID của chi tiết lỗi
        public IssueReport IssueReport { get; set; } // Báo cáo sự cố liên quan
        public ErrorDetail ErrorDetail { get; set; } // Chi tiết lỗi liên quan
    }

}
