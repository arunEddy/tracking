using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingWebAPI.Models
{
    public class StockReturn
    {
        [Key]
        public int srid { get; set; }
        public string? FromOffice { get; set; }
        public string? ShipperName { get; set; }
        public string? ToOffice { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string? srItemName { get; set; }
        public decimal? Quantity { get; set; }
        public int? StartNo { get; set; }
        public int? EndNo { get; set; }
        public int? BalQuantity { get; set; }
        public string? createdby { get; set; }
        public DateTime? createdon { get; set; } = DateTime.UtcNow;
        public string? mdfby { get; set; }
        public DateTime? mdfon { get; set; }
        public string? IsActive { get; set; }
        [Column("end_dt")]
        public string? EndDate { get; set; }
    }
}
