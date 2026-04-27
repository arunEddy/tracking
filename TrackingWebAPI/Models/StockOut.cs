using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingWebAPI.Models
{
    public class StockOut
    {
        [Key]
        public int soid { get; set; }
        public string? SOfficeName { get; set; }
        public DateTime? SOutDate { get; set; }
        public string? SItemName { get; set; }
        public decimal? SQuantity { get; set; }
        public string? Serialized { get; set; }
        public string? AWB { get; set; }
        public int? StartNo { get; set; }
        public int? EndNo { get; set; }
        public decimal? BalQuantity { get; set; }
        public string? createdby { get; set; }
        public DateTime? createdon { get; set; } = DateTime.UtcNow;
        public string? mdfby { get; set; }
        public DateTime? mdfon { get; set; }
        public string? IsActive { get; set; }
        [Column("end_dt")]
        public string? EndDate { get; set; }
    }
}
