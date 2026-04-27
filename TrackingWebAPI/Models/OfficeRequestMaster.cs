using System.ComponentModel.DataAnnotations;

namespace TrackingWebAPI.Models
{
    public class OfficeRequestMaster
    {
        [Key]
        public int ORMID { get; set; }
        
        public string? ReferenceBy { get; set; }
        
        public string? ReferenceName { get; set; }
       
        public string? OfficeName { get; set; }
        
        public string? PlanAdopted { get; set; }
        public string? AmountPaid { get; set; }
        public string? DepositAmount  { get; set; }
        
        public string? Name { get; set; }
        
        public string? Address { get; set; }
       
        public string? Mobile { get; set; }
        public string? Phone { get; set; }
        
        public string? Pincode { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PersonalNo { get; set; }
        
        public string? EMail { get; set; }
        public string? PANNumber { get; set; }
        public string? ServiceTaxNumber { get; set; }
       
        public string? Document1 { get; set; }
        public string? Document2 { get; set; }
        public string? Document3 { get; set; }
        public string? Document4 { get; set; }
        public string? Document5 { get; set; }
        public string? Document6 { get; set; }
        public string? createdBy { get;set; }
        public DateTime? createdOn { get; set; } = DateTime.UtcNow;
        public string? status { get; set; }
        public DateTime? end_dt { get; set; }



    }
}
