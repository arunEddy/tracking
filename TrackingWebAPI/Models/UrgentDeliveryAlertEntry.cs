using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingWebAPI.Models
{
    public class UrgentDeliveryAlertEntry
    {
        [Key]
        public int udaId { get; set; }
        public string? AwbNo { get; set; }
        public string? DestinationOffice { get; set; }
        public string? ShipperName { get; set; }
        public string? ConsigneeName { get; set; }
        public string? Address { get; set; }
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
