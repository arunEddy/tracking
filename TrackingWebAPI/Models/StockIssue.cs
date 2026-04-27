using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingWebAPI.Models
{
    public class StockIssue
    {
        [Key]
        public int siId { get; set; }
        public string? SiOfficeName { get; set; }
        public DateTime? IssueDate { get; set; }
        public string? CustomerName { get; set; }
        public string? SiItemName { get; set; }
        public string? VoucherNo { get; set; }
        public decimal? Quantity { get; set; }
        public string? Serialized { get; set; }
        public string? AWB { get; set; }
        public int? StartNo { get; set; }
        public int? EndNo { get; set; }
        public decimal? BalQuantity { get; set; }
        public string? ItemPrice { get; set; }
        public string? createdby { get; set; }
        public DateTime? createdon { get; set; } = DateTime.UtcNow;
        public string? mdfby { get; set; }
        public DateTime? mdfon { get; set; }
        public string? IsActive { get; set; }
        [Column("end_dt")]
        public string? EndDate { get; set; }
    }
}
