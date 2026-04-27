using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingWebAPI.Models
{
    public class BookingSelf
    {
        [Key]
        public int bseid { get; set; }
        public string? BookingOffice { get; set; }
        public string? CustomerName { get; set; }
        public string? DocketType { get; set; }
        public string? AWB { get; set; }
        public DateTime? BookDate { get; set; }
        public string? Origin { get; set; }
        public string? Destination { get; set; }
        public string? Mode { get; set; }
        public decimal? ActualWeight { get; set; }
        public int? Pcs { get; set; }
        public decimal? ChargeWt { get; set; }
        public decimal? Volumetric { get; set; }
        public decimal? VolWt { get; set; }
        public string? ProductName { get; set; }
        public decimal? TopayAmount { get; set; }
        public string? ProductType { get; set; }
        public string? ConsignorName { get; set; }
        public string? PickupCity { get; set; }
        public string? PickupPincode { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? ConsigneeName { get; set; }
        public string? ConsigneeAddress1 { get; set; }
        public string? ConsigneeAddress2 { get; set; }
        public string? ConsigneePhone { get; set; }
        public string? Mobile { get; set; }
        public string? ConsigneePincode { get; set; }
        public string? City { get; set; }
        public string? MasterReferenceNo { get; set; }
        public string? Remarks { get; set; }
        public string? createdby { get; set; }
        public DateTime? createdon { get; set; } = DateTime.UtcNow;
        public string? mdfby { get; set; }
        public DateTime? mdfon { get; set; }
        public string? IsActive { get; set; }
        [Column("end_dt")]
        public string? EndDate { get; set; }
    }
}
