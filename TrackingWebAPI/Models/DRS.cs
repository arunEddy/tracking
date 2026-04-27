using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingWebAPI.Models
{
    public class DRS
    {
        [Key]
        public int drsid { get; set; }
        public string? DRSMadeFor { get; set; }
        public string? DeliveryOffice { get; set; }
        public string? ConnectionNumber { get; set; }
        public string? DeliveryBoy { get; set; }
        public DateTime? VehicleNumber { get; set; }
        public string? DeliveryDate { get; set; }
        public string? DRSNo { get; set; }
        public string? AllowMobileApplication { get; set; }
        public string? MobileNo { get; set; }
        public string? SimNo { get; set; }
        public string? AwbNo { get; set; }
        public string? ConsigneeName { get; set; }
        public string? createdby { get; set; }
        public DateTime? createdon { get; set; } = DateTime.UtcNow;
        public string? mdfby { get; set; }
        public DateTime? mdfon { get; set; }
        public string? IsActive { get; set; }
        [Column("end_dt")]
        public string? EndDate { get; set; }
    }
}
