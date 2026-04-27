using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingWebAPI.Models
{
    public class DeliveryBoyMasters
    {
        [Key]
        public int DBMID { get; set; }
        public string? BoyCode { get; set; }
        public string? OfficeName { get; set; }
        public string? BoyName { get; set; }
        public string MobileNo { get; set; }
        public string? EmployeeName { get; set; }
        public string? AllowMobileApplication { get; set; }
        public string SIMNo { get; set; }
        public string? createdBy { get; set; }
        public DateTime? createdDate { get; set; } = DateTime.UtcNow;
        public string? modifyedBy { get; set; }
        public DateTime? ModifyedOn { get; set; }
        public string? IsActive { get; set; }
        [Column("end_dt")]
        public string? EndDate { get; set; }

    }
}
