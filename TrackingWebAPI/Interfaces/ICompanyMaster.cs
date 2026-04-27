using TrackingWebAPI.Models;

namespace TrackingWebAPI.Interfaces
{
    public interface ICompanyMaster
    {
        void _AddCompany(CompanyMaster _company);
        void _DeleteCompany(int companyid);
        //string _GetCountry(CountryMaster _country);
        void _UpdateCompany(CompanyMaster company);
        List<CompanyMaster> GetCompanies();
    }
}
