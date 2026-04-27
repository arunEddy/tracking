using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingWebAPI.Models
{
    public class EDPEditing
    {
        [Key]
        public int edpId { get; set; }
        public string? AwbNo { get; set; }
        public string? VAWB { get; set; }
        public string? ShipperName { get; set; }
        public string? ConsigneeName { get; set; }
        public string? ProductName { get; set; }
        public decimal? CODAmount { get; set; }
        public string? RiskValue { get; set; }
        public string? GoodsValue { get; set; }
        public string? Contents { get; set; }
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
