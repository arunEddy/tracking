using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingWebAPI.Models
{
    public class VendorMasters
    {
        [Key]
        public int VMID { get; set; }
        public string? OfficeName { get; set; }
        public string? VendorType { get; set; }
        public string? VendorCode { get; set; }
        public string? VendorName { get; set; }
        public string? GSTIN { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Mobile { get; set; }
        public string? Phone { get; set; }
        public string? EMail { get; set; }
        public string? Pincode { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? createdBy { get; set; }
        public DateTime? createdDate { get; set; } = DateTime.UtcNow;
        public string? modifyedBy { get; set; }
        public DateTime? ModifyedOn  { get; set; }
        public string? IsActive { get; set; }
        [Column("end_dt")]
        public string? EndDate { get; set; }
    }
}
