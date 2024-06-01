using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DACS.Models
{
    public class LoanEquipmentTicket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaPhieuMuon { get; set; } // Unique identifier for each loan ticket

        [Required]
        public string TenNguoiMuon { get; set; }

        [Required]
        public string SDT { get; set; } // Thêm thuộc tính số điện thoại

        [Required]
        [ForeignKey("LoanEquipment")] // Indicates the foreign key to LoanEquipment
        public string MaThietBiMuon { get; set; } // Foreign key to LoanEquipment

        [Required]
        public int SoLuongMuon { get; set; }

        [Required]
        public DateTime NgayMuon { get; set; }

        public DateTime? NgayTra { get; set; }



        [Required]
        [ForeignKey("Class")] // Indicates the foreign key to Class
        public string MaPhongHoc { get; set; } // Foreign key to Class

        // Navigation property to Class
        public Class? Class { get; set; }
        // Navigation property to LoanEquipment
        public LoanEquipment? LoanEquipment { get; set; }
    }
}
