using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingWebAPI.Models
{
    public class ReturnToCustomerManifesting
    {
        [Key]
        public int rtcmid { get; set; }

        public string? ShipperName { get; set; }

        public string? DeliveryBoy { get; set; }

        public DateTime? ReturnDate { get; set; }

        public string? BagNumber { get; set; }

        public string? Remarks { get; set; }

        public string? AWBNumber { get; set; }

        public string? Reason { get; set; }

        public string? ManifestNo { get; set; }

        public string? createdby { get; set; }

        public DateTime? createdon { get; set; } = DateTime.UtcNow;

        public string? mfdby { get; set; }

        public DateTime? mfdon { get; set; }

        public string? IsActive { get; set; }

        [Column("end_dt")]
        public string? EndDate { get; set; }
    }
}