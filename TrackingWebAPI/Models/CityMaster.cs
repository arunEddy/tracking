using System.ComponentModel.DataAnnotations;

namespace TrackingWebAPI.Models
{
    public class CityMaster
    {
        //[CityId], [ZoneName], [StateName], [CityCode], [CityName], [IsMetroCity], [CountryType], [IsActive], [ModifiedBy], [ModifiedDate], [CreatedBy], [CreatedDate]
        [Key]
        public int CityId { get; set; }
        public int ZoneId { get; set; }
        public int StateId { get; set; }
        public int CountryId { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public bool IsMetroCity { get; set; }
        public bool isInternational { get; set; }
        public bool IsActive { get; set; }
        public int ModifiedBy { get; set; } 
        public DateTime ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string CountryName { get; internal set; }
        public string ZoneName { get; internal set; }
        public string StateName { get; internal set; }


        //public string ZoneName { get; set; }
        //   public string StateName { get; set; }
        //public string CountryCode { get; set; }
        // public string CountryName { get; set; }
        //public string StateCode { get; set; } = "Unknown";

    }
}
