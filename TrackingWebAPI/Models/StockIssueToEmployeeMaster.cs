using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingWebAPI.Models
{
    public class StockIssueToEmployeeMaster
    {
        [Key]
        public int Sitoe { get; set; }
        public string? OfficeName { get; set; }
        public string? EmployeeName { get; set; }
        public DateTime? IssueDate { get; set; }
        public int? StartNo { get; set; }
        public int? Quantity { get; set; }
        public int? EndNo { get; set; }
        public string? createdby { get; set; }
        public DateTime? createdon { get; set; }= DateTime.UtcNow;
        public string? mfdby { get; set; }
        public DateTime? mfdon { get; set; }
        public string? IsActive { get; set; }
        [Column("end_dt")]
        public string? EndDate { get; set; }
       
    }
}
