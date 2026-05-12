using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingWebAPI.Models
{
    public class FirstMileRTO
    {
        [Key]
        public int rtoid { get; set; }

        public string? EntryOffice { get; set; }
        public string? AWBNumber { get; set; }
        public DateTime? RTODate { get; set; }
        public string? RTOReason { get; set; }

        public string? createdby { get; set; }

        public DateTime? createdon { get; set; } = DateTime.Now;

        public string? mfdby { get; set; }

        public DateTime? mfdon { get; set; }

        public string? IsActive { get; set; }

        [Column("end_dt")]
        public string? EndDate { get; set; }
    }
}