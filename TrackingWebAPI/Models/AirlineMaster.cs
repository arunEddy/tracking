using System.ComponentModel.DataAnnotations;

namespace TrackingWebAPI.Models
{
    public class AirlineMaster
    {
        [Key]
        public int AIRID { get; set; }

        public string? AirlineCode { get; set; }
        public string? AirlineName { get; set; }
        public decimal?  Slab1 { get; set; }
        public decimal?  Slab2 { get; set; }
        public decimal?  Slab3 { get; set; }
        public decimal?  Slab4 { get; set; }
        public decimal? Slab5 { get; set; }
        public string? createdBy { get; set; }
        public DateTime? createdDate { get; set; } = DateTime.UtcNow;
        public string? mfdby { get; set; }
        public DateTime? mfdon { get; set; }
        public string? IsActive { get; set; }
        public DateTime? end_dt { get; set; }
    }
}
