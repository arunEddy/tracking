using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingWebAPI.Models
{
    public class DataImportWithoutAWB
    {
        [Key]
        public int eimwId { get; set; }
        public string? OfficeName { get; set; }
        public string? ShipperName { get; set; }
        public DateTime? BookingDate { get; set; }
        public string? ConsigneeName { get; set; }
        public string? Shipperfile { get; set; }
        public string? createdby { get; set; }
        public DateTime? createdon { get; set; } = DateTime.UtcNow;
        public string? mdfby { get; set; }
        public DateTime? mdfon { get; set; }
        public string? IsActive { get; set; }
        [Column("end_dt")]
        public string? EndDate { get; set; }
    }
}
