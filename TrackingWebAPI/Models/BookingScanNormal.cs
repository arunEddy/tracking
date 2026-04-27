using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingWebAPI.Models
{
    public class BookingScanNormal
    {
        [Key]
        public int bsnid { get; set; }
        public string? BookingOffice { get; set; }
        public string? PickupCity { get; set; }
        public string? PickupPincode { get; set; }
        public DateTime? BookingDate { get; set; }
        public int? ManifestNo { get; set; }
        public DateTime? ManifestDate { get; set; }
        public string? CreditLimit { get; set; }
        public string? AWB { get; set; }
        public string? CustomerName { get; set; }
        public string? Mode { get; set; }
        public string? Pcs { get; set; }
        public decimal? ActualWeight { get; set; }
        public string? Volumetric { get; set; }
        public decimal? VolWt { get; set; }
        public decimal? ChargeWt { get; set; }
        public string? ProductName { get; set; }
        public decimal? CODAmount { get; set; }
        public string? BookType { get; set; }
        public string? Content { get; set; }
        public string? RefNo { get; set; }
        public string? ProductService { get; set; }
        public string? bsnName { get; set; }
        public string? bsnAddress { get; set; }
        public string? City { get; set; }
        public string? Pincode { get; set; }
        public string? ODAChargeApplicable { get; set; }
        public string? createdby { get; set; }
        public DateTime? createdon { get; set; } = DateTime.UtcNow;
        public string? mdfby { get; set; }
        public DateTime? mdfon { get; set; }
        public string? IsActive { get; set; }
        [Column("end_dt")]
        public string? EndDate { get; set; }
    }
}
