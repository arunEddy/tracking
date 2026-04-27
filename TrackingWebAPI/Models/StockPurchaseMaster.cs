using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingWebAPI.Models
{
    public class StockPurchaseMaster
    {
        [Key]
        public int stpId { get; set; }
        public string? OfficeName { get; set; }
        public string? VendorName { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public string? ItemName { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? PurchaseRate { get; set; }
        public decimal? VendorRate { get; set; }
        public string?  StartNo { get; set; }
        public string? EndNo { get; set; }
        public string? BookRequired { get; set; }
        public string? createdby { get; set; }
        public DateTime? createdon { get; set; } = DateTime.UtcNow;
        public string? mfdby { get; set; }
        public DateTime? mfdon { get; set; }
        public string?   IsActive { get; set; }
        [Column("end_dt")]
        public string? EndDate { get; set; }

    }
}
