using System.ComponentModel.DataAnnotations;

namespace TrackingWebAPI.Models
{
    public class ZoneMaster
    {
        [Key]
        public int ZoneId { get; set; }
        [Required]
        public string? ZoneName { get; set; }
        public bool isInternational { get; set; }
        public bool IsActive { get; set; }

    }
}
