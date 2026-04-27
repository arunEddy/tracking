using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingWebAPI.Models
{
    public class MobileAlertMessages
    {
        [Key]
        public int MAMID { get; set; }
        public string? OfficeName { get; set; }
        public string? BoyName { get; set; }
        public string? MessageType { get; set; }
        public string? TextMessage { get; set; }
        public string? UploadImage { get; set; }
        public string createdBy { get; set; }
        public DateTime? createdDate { get; set; } = DateTime.UtcNow;
        public string? modifyedBy { get; set; }
        public DateTime? ModifyedOn { get; set; }
        public string? IsActive { get; set; }
        [Column("end_dt")]
        public string? EndDate { get; set; }
    }
}
