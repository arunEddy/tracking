using System.ComponentModel.DataAnnotations;

namespace TrackingWebAPI.Models
{
    public class StateMaster
    {
        //StateId, CountryId, StateType, StateCode, StateName, GSTIN, IsActive

        [Key]
        public int StateId { get; set; }
        [Required]
        public int? CountryId { get; set; }
        public string? StateType { get; set; }


        public string? StateCode { get; set; }
        public string StateName { get; set; }
        //public string GSTIN { get; set; }

        public bool IsActive { get; set; }
        //public string EWayBillGSTNumber { get; internal set; }
        //public decimal? EWayBillIntraStateLimit { get; internal set; }
        public bool isInternational { get; set; }
        public string? CountryName { get; set; }
    }
}
