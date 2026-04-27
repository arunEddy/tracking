using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingWebAPI.Models
{
    public class CustomerWiseReBookDeliveryAddress
    {
        [Key]
        public int CWBID { get; set; }
        public string? ShipperName { get; set; }
        public string? ReBookConsigneeName { get; set; }
        public string? DeliveryAddress { get; set; }
        public string? Pincode { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? createdBy { get; set; }
        public DateTime? createdDate     { get; set; } = DateTime.UtcNow;
        public string? modifyedBy { get; set; }
        public DateTime? ModifyedOn { get; set; }
        public string? IsActive { get; set; }
        [Column("end_dt")]
        public string? EndDate { get; set; }
    }
}
