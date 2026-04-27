using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingWebAPI.Models
{
    public class BULKVIRTUALBOOKING
    {
        [Key]
        public int bvbid { get; set; }
        public string? RequestNo { get; set; }
        public string? BookingOffice { get; set; }
        public string? ProductName { get; set; }
        public string? Mode { get; set; }
        public string? eWayBillNumber { get; set; }
        public DateTime? PickupBookingDate { get; set; }
        public string? PickupBookingTime { get; set; }
        public string? DefaultPCS { get; set; }
        public string? DefaultWeight { get; set; }
        public string? bvbFormat { get; set; }
        public string? bvbformatfile { get; set; }
        public string? createdby { get; set; }
        public DateTime? createdon { get; set; } = DateTime.UtcNow;
        public string? mdfby { get; set; }
        public DateTime? mdfon { get; set; }
        public string? IsActive { get; set; }
        [Column("end_dt")]
        public string? EndDate { get; set; }
    }
}
