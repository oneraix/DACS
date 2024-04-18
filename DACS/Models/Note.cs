using System.ComponentModel.DataAnnotations;

namespace DACS.Models
{
    public class Note
    {
        [Key]
        public int MaGhiChu { get; set; }
        public DateTime NgayGhi { get; set; }
        public string NoiDung {  get; set; }
    }
}
