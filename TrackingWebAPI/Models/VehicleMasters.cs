using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingWebAPI.Models
{
    public class VehicleMasters
    {
        [Key]
        public int VEMID { get; set; }
        public string? VehicleType { get; set; }
        public string? RunningType { get; set; }
        public string? VehicleNo { get; set; }
        public string? ReportingTime { get; set; }
        public string? MakeModel { get; set; }
        public string? Capacity { get; set; }
        public string? OriginOffice { get; set; }
        public string? DestinationOffice { get; set; }
        public string? VendorName { get; set; }
        public string? TrackingDeviceID { get; set; }
        public string? createdBy { get; set; }
        public DateTime? createdDate { get; set; } = DateTime.UtcNow;
        public string? modifyedBy { get; set; }
        public DateTime? ModifyedOn { get; set; }
        public string? IsActive { get; set; }
        [Column("end_dt")]
        public string? EndDate { get; set; }

    }
}
