using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DACS.Models
{
    public class Class
    {
        [Key]
        public string MaPhongHoc { get; set; }
        public string SoPhong { get; set; }
        public int Tang { get; set; }
        public string? GhiChu { get; set; }

        // Foreign Key for RoomCategory
        public string MaLoaiPhong { get; set; }

        // Navigation property
        [ForeignKey("MaLoaiPhong")]
        public RoomCategories? RoomCategory { get; set; }
    }
}
