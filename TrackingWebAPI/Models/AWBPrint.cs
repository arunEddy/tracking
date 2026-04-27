using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingWebAPI.Models
{
    public class AWBPrint
    {
        [Key]
        public int apid { get; set; }
        public string? OfficeName { get; set; }
        public string? ShipperName { get; set; }
        public string? apFormat { get; set; }
        public string? apOption { get; set; }
        public DateTime? BookingDate { get; set; }
        public string? Phone { get; set; }
        public string? Remarks { get; set; }
        public DateTime? DelDate { get; set; }
        public string? Deltime { get; set; }
        public string? DeliveryRemarks { get; set; }
        public string? Status { get; set; }
        public string? createdby { get; set; }
        public DateTime? createdon { get; set; } = DateTime.UtcNow;
        public string? mdfby { get; set; }
        public DateTime? mdfon { get; set; }
        public string? IsActive { get; set; }
        [Column("end_dt")]
        public string? EndDate { get; set; }
    }
}
