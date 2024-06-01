using System.ComponentModel.DataAnnotations;

namespace DACS.Models
{
    public class LoanEquipment
    {
        [Key]
        public string MaThietBiMuon { get; set; }
        public string TenThietBiMuon { get; set; }
        public int SoLuong { get; set; }

        // Collection to hold associated LoanEquipmentTickets
        public ICollection<LoanEquipmentTicket>? LoanEquipmentTickets { get; set; }
    }
}

