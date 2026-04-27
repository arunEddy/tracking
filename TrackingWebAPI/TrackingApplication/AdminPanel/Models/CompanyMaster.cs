using System.ComponentModel.DataAnnotations;

namespace TrackingWebAPI.Models
{
    public class CompanyMaster
    {
        [Key]
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Gst { get; set; }
        public int OfficeSize { get; set; }
        public int EmployeeSize { get; set; }
        public string RegistrationNo { get; set; }
        public string RegistrationDate { get; set; }
        public string ServiceTaxCategory { get; set; }
        public string PANNo { get; set; }
        public string CINNo { get; set; }
        public bool DefaultCompany { get; set; }
        public bool GSTAPIAllowed { get; set; }
        public bool EWaybillAPIAllowed { get; set; }
        public string CompanyLogo { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string ContactPersonName { get; set; }
        public string ContactPersonPhone { get; set; }
        public string ContactPersonMobile { get; set; }
        public string ContactPersonEmail { get; set; }
        public bool IsActive { get; set; }

        //  [CompanyId]
        //,[CompanyName]
        //,[Gst]
        //,[OfficeSize]
        //,[EmployeeSize]
        //,[RegistrationNo]
        //,[RegistrationDate]
        //,[ServiceTaxCategory]
        //,[PANNo]
        //,[CINNo]
        //,[DefaultCompany]
        //,[GSTAPIAllowed]
        //,[EWaybillAPIAllowed]
        //,[CompanyLogo]
        //,[Address]
        //,[Pincode]
        //,[City]
        //,[State]
        //,[Phone]
        //,[Fax]
        //,[Email]
        //,[Website]
        //,[ContactPersonName]
        //,[ContactPersonPhone]
        //,[ContactPersonMobile]
        //,[ContactPersonEmail]
        //,[IsActive]

    }
}
