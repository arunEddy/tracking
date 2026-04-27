using DALCLASS.DBContact;
using System.ComponentModel.Design;
using System.Net;
using System.Numerics;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;

namespace TrackingWebAPI.Services
{
    public class CompanyServices : ICompanyMaster
    {
        
            private readonly ApplicationDbContext _context;

            public CompanyServices(ApplicationDbContext context)
            {
                _context = context;
            }


            public List<CompanyMaster> GetCompanies()
            {
                // Map DALCLASS.CountryMaster to TrackingWebAPI.Models.CountryMaster
                return _context.CompanyMaster
                    .Select(x => new CompanyMaster
                    {
                       Address=x.Address,
                          CINNo=x.CINNo,
                            City=x.City,
                                CompanyId=x.CompanyId,
                                    CompanyLogo=x.CompanyLogo,
                                    CompanyName=x.CompanyName,
                                        ContactPersonEmail=x.ContactPersonEmail,
                                        ContactPersonMobile=x.ContactPersonMobile,
                                            ContactPersonName=x.ContactPersonName,
                                            ContactPersonPhone=x.ContactPersonPhone,
                                                DefaultCompany=x.DefaultCompany,
                                                Email=x.Email,
                                                    EmployeeSize=x.EmployeeSize,
                                                    Fax=x.Fax,
                                                        Gst=x.Gst,
                                                        GSTAPIAllowed=x.GSTAPIAllowed,
                                                            OfficeSize=x.OfficeSize,
                                                            PANNo=x.PANNo,
                                                                Phone=x.Phone,
                                                                Pincode=x.Pincode,
                                                                    RegistrationDate=x.RegistrationDate,
                                                                    RegistrationNo=x.RegistrationNo,
                                                                        ServiceTaxCategory=x.ServiceTaxCategory,
                                                                        Website=x.Website,
                        IsActive = x.IsActive
                    })
                    .ToList();
            }


            public void _UpdateCompany(CompanyMaster _company)
            {
                // Find the DALCLASS.CountryMaster entity by ID
                var dalCompany = _context.CompanyMaster.FirstOrDefault(x => x.CompanyId == _company.CompanyId);
                if (dalCompany != null)
                {
                // Map properties from TrackingWebAPI.Models.CountryMaster to DALCLASS.CountryMaster
                dalCompany.CompanyLogo = _company.CompanyLogo;
                dalCompany.CompanyName = _company.CompanyName;
                dalCompany.Gst = _company.Gst;
                dalCompany.OfficeSize = _company.OfficeSize;
                dalCompany.EmployeeSize = _company.EmployeeSize;
                dalCompany.RegistrationNo = _company.RegistrationNo;
                dalCompany.RegistrationDate = _company.RegistrationDate;
                dalCompany.ServiceTaxCategory = _company.ServiceTaxCategory;
                dalCompany.PANNo = _company.PANNo;
                dalCompany.CINNo = _company.CINNo;
                dalCompany.DefaultCompany = _company.DefaultCompany;
                dalCompany.Address = _company.Address;
                dalCompany.Pincode = _company.Pincode;
                dalCompany.City = _company.City;
                dalCompany.State = _company.State;
                dalCompany.Phone = _company.Phone;
                dalCompany.Fax = _company.Fax;
                dalCompany.Email = _company.Email;
                dalCompany.Website = _company.Website;
                dalCompany.ContactPersonName = _company.ContactPersonName;
                dalCompany.ContactPersonPhone = _company.ContactPersonPhone;
                dalCompany.ContactPersonMobile = _company.ContactPersonMobile;
                dalCompany.ContactPersonEmail = _company.ContactPersonEmail;
                dalCompany.GSTAPIAllowed = _company.GSTAPIAllowed;
                dalCompany.EWaybillAPIAllowed = _company.EWaybillAPIAllowed;
               
                  


               
                dalCompany.IsActive = _company.IsActive;

                    _context.CompanyMaster.Update(dalCompany);
                    _context.SaveChanges();
                }
            }

            public void _AddCompany(CompanyMaster _company)
            {
                // Map TrackingWebAPI.Models.CountryMaster to DALCLASS.CountryMaster
                var dalCompany = new CompanyMaster
                {
                    CompanyId = _company.CompanyId,
                    CompanyName = _company.CompanyName,
                    Gst = _company.Gst,
                    OfficeSize = _company.OfficeSize,
                    EmployeeSize = _company.EmployeeSize,
                    RegistrationNo = _company.RegistrationNo,
                    RegistrationDate = _company.RegistrationDate,
                    ServiceTaxCategory = _company.ServiceTaxCategory,
                    PANNo = _company.PANNo,
                    CINNo = _company.CINNo,
                    DefaultCompany = _company.DefaultCompany,
                    GSTAPIAllowed = _company.GSTAPIAllowed,
                    EWaybillAPIAllowed = _company.EWaybillAPIAllowed,
                    CompanyLogo = _company.CompanyLogo,
                    Address = _company.Address,
                    Pincode = _company.Pincode,
                    City = _company.City,
                    State = _company.State,
                    Phone = _company.Phone,
                    Fax = _company.Fax,
                    Email = _company.Email,
                    Website = _company.Website,
                    ContactPersonName = _company.ContactPersonName,
                    ContactPersonPhone = _company.ContactPersonPhone,
                    ContactPersonMobile = _company.ContactPersonMobile,
                    ContactPersonEmail = _company.ContactPersonEmail,
                    IsActive = _company.IsActive

                };
                _context.CompanyMaster.Add(dalCompany);
                _context.SaveChanges();
            }

            public void _DeleteCompany(int companyid)
            {
                var company = _context.CompanyMaster.FirstOrDefault(x => x.CompanyId == companyid);
                if (company != null)
                {
                    _context.CompanyMaster.Remove(company);
                    _context.SaveChanges();
                }
            }



        }
    }

