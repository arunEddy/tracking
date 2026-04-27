using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingWebAPI.Models
{
    public class BookingScanLogisticItemDetails
    {
        [Key]
        public int bsldItid { get; set; }
        public string? PartnerRefNo { get; set; }
        public string? eWayBillNumber { get; set; }
        public DateTime? EWBValidDate { get; set; }
        public string? InvoiceID { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string? Description { get; set; }
        public decimal? InvoiceAmount { get; set; }
        public decimal? CODAmount { get; set; }
        public string? createdby { get; set; }
        public DateTime? createdon { get; set; } = DateTime.UtcNow;
        public string? mdfby { get; set; }
        public DateTime? mdfon { get; set; }
        public string? IsActive { get; set; }
        [Column("end_dt")]
        public string? EndDate { get; set; }
    }
}
