using System.ComponentModel.DataAnnotations;

namespace DACS.Models
{
    public class Class
    {
        [Key] // Data Annotation để chỉ định MaPhongHoc là khóa chính
        public string MaPhongHoc { get; set; }
        public string SoPhong { get; set; }
        public int Tang { get; set; }
        public ClassStatus TrangThai { get; set; }
        public string? GhiChu { get; set; }
    }
}
