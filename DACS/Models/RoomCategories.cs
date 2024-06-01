using System.ComponentModel.DataAnnotations;

namespace DACS.Models
{
    public class RoomCategories
    {
        [Key]
        public string MaLoaiPhong {  get; set; }
        public string TenLoaiPhong { get; set; }

        public List<Class>? Classes { get; set; }
    }
}
