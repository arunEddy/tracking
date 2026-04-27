using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingWebAPI.Models
{
    public class CashBookingItemDetails
    {
        [Key]
        public int cbIId { get; set; }
        public int? InvoiceID { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string? Description { get; set; }
        public decimal? Amount { get; set; }
        public string? eWayBillNumber { get; set; }
        public DateTime? EWBDate { get; set; }
        public int? cbId { get; set; }
        public string? createdby { get; set; }
        public DateTime? createdon { get; set; } = DateTime.UtcNow;
        public string? mdfby { get; set; }
        public DateTime? mdfon { get; set; }
        public string? IsActive { get; set; }
        [Column("end_dt")]
        public string? EndDate { get; set; }
    }
}
