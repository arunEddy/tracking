using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingWebAPI.Models
{
    public class TransportMaster
    {
        [Key]
        public int TMID { get; set; }
        public string? OriginOffice { get; set; }
        public string? DestinationOffice { get; set; }
        public string? ModeType { get; set; }
        public string? AirlineName { get; set; }
        public string? ModeName { get; set; }
        public string?    ServiceType { get; set; }
        public string? DepartureTime { get; set; }
        public string? ArrivalTime { get; set; }
        public string? ExpectedTransitDay { get; set; }
        public string? ExpReachingTimeatHub { get; set; }
        public string? IsActive { get; set; }
        public string? modifiedBy { get; set; }
        public DateTime? modifieddate { get; set; }
        public string? createdBy { get; set; }
        public DateTime? createdDate { get; set; } = DateTime.UtcNow;
        [Column("end_dt")]
        public string? EndDate { get; set; }

    }   
}
