using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingWebAPI.Models
{
    public class StockTransfer
    {
        [Key]
        public int stid { get; set; }
        public DateTime? TransferDate { get; set; }
        public string? FromOffice { get; set; }
        public string? ToOffice { get; set; }
        public string? ItemName { get; set; }
        public decimal? Quantity { get; set; }
        public int? StartNo { get; set; }
        public int? EndNo { get; set; }
        public decimal? BalQuantity { get; set; }
        public string? createdby { get; set; }
        public DateTime? createdon { get; set; } = DateTime.UtcNow;
        public string? mfdby { get; set; }
        public DateTime? mfdon { get; set; }
        public string? IsActive { get; set; }
        [Column("end_dt")]
        public string? EndDate { get; set; }
    }
}
