using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingWebAPI.Models
{
    public class CashBooking
    {
        [Key]
        public int cbid { get; set; }
        public string? BookingOffice { get; set; }
        public string? ShipperName { get; set; }
        public string? DocketType { get; set; }
        public string? AWB { get; set; }
        public DateTime? BookingDate { get; set; }
        public string? Origin { get; set; }
        public string? Destination { get; set; }
        public string? Mode { get; set; }
        public string? Product { get; set; }
        public string? Pcs { get; set; }
        public decimal? Volumetric { get; set; }
        public decimal? ChargeWeight { get; set; }
        public decimal? VolWeight { get; set; }
        public decimal? CODAmount { get; set; }
        public string? ConsignorMobile { get; set; }
        public string? ConsignorName { get; set; }
        public string? ConsignorAddress1 { get; set; }
        public string? ConsignorAddress2 { get; set; }
        public string? ConsignorCity { get; set; }
        public string? ConsignorPincode { get; set; }
        public string? ConsignorState { get; set; }
        public string? ConsigneeMobile { get; set; }
        public string? ConsigneeName { get; set; }
        public string? ConsigneeAddress1 { get; set; }
        public string? ConsigneeAddress2 { get; set; }
        public string? ConsigneeCity { get; set; }
        public string? ConsigneePincode { get; set; }
        public string? ConsigneeState { get; set; }
        public string? ConsigneeODAChargeApplicable { get; set; }
        public string? ConsigneeGSTNo { get; set; }
        public string? PaymentMode { get; set; }
        public decimal? ReceivedAmount { get; set; }
        public decimal? TariffAmount { get; set; }
        public decimal? CGST { get; set; }
        public decimal? SGST { get; set; }
        public decimal? IGST { get; set; }
        public decimal? TotalAmount { get; set; }
        public string? createdby { get; set; }
        public DateTime? createdon { get; set; } = DateTime.UtcNow;
        public string? mdfby { get; set; }
        public DateTime? mdfon { get; set; }
        public string? IsActive { get; set; }
        [Column("end_dt")]
        public string? EndDate { get; set; }
    }
}
