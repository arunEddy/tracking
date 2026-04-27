using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingWebAPI.Models
{
    public class ReturnBooking
    {
        [Key]
        public int rbid { get; set; }
        public string? AWBReference { get; set; }
        public string? AWBReferenceNo { get; set; }
        public string? BookingOffice { get; set; }
        public string? PickupCity { get; set; }
        public string? PickupPincode { get; set; }
        public string? ConsignorName { get; set; }
        public string? DepttPersonName { get; set; }
        public DateTime? BookingDate { get; set; }
        public DateTime? ExpDeliveryDate { get; set; }
        public string? ExpDeliveryTime { get; set; }
        public string? Pcs { get; set; }
        public decimal? ChargeWeight { get; set; }
        public decimal? MachineWeight { get; set; }
        public string? ProductName { get; set; }
        public string  ? Mode { get; set; }
        public decimal? CODAmount { get; set; }
        public string? bsName { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? Pincode { get; set; }
        public string? bsState { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public string? ODAChargeApplicable { get; set; }
        public string? ConsigneeName { get; set; }
        public string? ConsigneeAddress1 { get; set; }
        public string? ConsigneeAddress2 { get; set; }
        public string? ReturnPhone { get; set; }
        public string? ReturnMobile { get; set; }
        public string? ReturnPincode { get; set; }
        public string? ReturnCity { get; set; }
        public string? ReturnAWBNo { get; set; }
        public string? ReturnPickupCity { get; set; }
        public string? ReturnDestinationCity { get; set; }
        public string? ReturnRemarks { get; set; }
        public decimal? ReturnPieces { get; set; }
        public string? ReturnWeight { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string? ReturnTime { get; set; }
        public string? ReturnPickupPincode { get; set; }
        public string? ReturnDestinationPicode { get; set; }
        public string? ReturnProductName { get; set; }
        public string? ReturnMode { get; set; }
        public string? ReturnProductType { get; set; }
        public string? createdby { get; set; }
        public DateTime? createdon { get; set; } = DateTime.UtcNow;
        public string? mdfby { get; set; }
        public DateTime? mdfon { get; set; }
        public string? IsActive { get; set; }
        [Column("end_dt")]
        public string? EndDate { get; set; }
    }
}
