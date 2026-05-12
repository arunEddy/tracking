using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingWebAPI.Models
{
    public class NonDelivery
    {
        [Key]
        public int ndId { get; set; }
        public string? DestinationOffice { get; set; }
        public decimal? AwbNo { get; set; }
        public DateTime? NDRDate { get; set; }
        public string? NDRReason { get; set; }
        public string? Remarks { get; set; }
        public string? ShipperName { get; set; }
        public decimal? TotalNDR { get; set; }
        public decimal? PhysicalAttempt { get; set; }
        public decimal? MaxAttempt { get; set; }
        public string? createdby { get; set; }
        public DateTime? createdon { get; set; } = DateTime.UtcNow;
        public string? mfdby { get; set; }
        public DateTime? mfdon { get; set; }
        public string? IsActive { get; set; }
        [Column("end_dt")]
        public string? EndDate { get; set; }
    }
}
