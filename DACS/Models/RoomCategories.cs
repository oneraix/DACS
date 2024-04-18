using System.ComponentModel.DataAnnotations;

namespace DACS.Models
{
    public class RoomCategories
    {
        [Key]
        public int MaLoaiPhong {  get; set; }
        public string TenLoaiPhong { get; set; }

        public List<Class> classes { get; set; }
    }
}
