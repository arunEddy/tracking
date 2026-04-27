using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingWebAPI.Models
{
    public class AWBStockRequest
    {
        [Key]
        public int Asrid { get; set; }
        public string? AOfficeName { get; set; }
        public DateTime? ARequestDate { get; set; }
        public string? AItemName { get; set; }
        public decimal? Quantity { get; set; }
        public DateTime? ExpectedDeliveryDate { get; set; }
        public string? Remarks { get; set; }
        public string? createdby { get; set; }
        public DateTime? createdon { get; set; } = DateTime.UtcNow;
        public string? mdfby { get; set; }
        public DateTime? mdfon { get; set; }
        public string? IsActive { get; set; }
        [Column("end_dt")]
        public string? EndDate { get; set; }
    }
}
