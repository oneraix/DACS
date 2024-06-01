using System.ComponentModel.DataAnnotations;

namespace DACS.Models
{
    public enum ClassStatus
    {
        [Display(Name = "Đang sử dụng")]
        Dangsudung,
        [Display(Name ="Trống")]
        Trong,
        [Display(Name ="Sửa chữa")]
        Suachua
    }
}

