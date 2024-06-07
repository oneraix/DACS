using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DACS.Models
{
    public class RoomSchedule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        public string? MonHoc { get; set; }

        public string? GiangVien { get; set; }

        [Required]
        public string PhongHoc { get; set; }
        [ForeignKey("PhongHoc")]
        public Class? Class { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime Ngay { get; set; }

        [Required]
        [Column(TypeName = "time")]
        public TimeSpan ThoiGianBatDau { get; set; }

        [Required]
        [Column(TypeName = "time")]
        public TimeSpan ThoiGianKetThuc { get; set; }
    }
}

