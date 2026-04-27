using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingWebAPI.Models
{
    public class StockPaymentVoucher
    {
        [Key]
        public int spvId { get; set; }
        public string? VoucherNumber { get; set; }
        public string? FranchiseName { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? TotalAmount { get; set; }
        public string? BankName { get; set; }
        public string? Remarks { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal? AmountPerAWB { get; set; }
        public string? PaymentMode { get; set; }
        public string? ChequeDDNo { get; set; }
        public string? createdby { get; set; }
        public DateTime? createdon { get; set; } = DateTime.UtcNow;
        public string? mdfby { get; set; }
        public DateTime? mdfon { get; set; }
        public string? IsActive { get; set; }
        [Column("end_dt")]
        public string? EndDate { get; set; }
    }
}
