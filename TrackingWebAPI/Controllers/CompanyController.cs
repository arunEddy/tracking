using Microsoft.AspNetCore.Mvc;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;
using TrackingWebAPI.Services;

namespace TrackingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CompanyController : ControllerBase
    {
        private readonly ICompanyMaster _companyService;

        public CompanyController(ICompanyMaster companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public IActionResult GetCompany()
        {
            return Ok(_companyService.GetCompanies());
        }

        [HttpPost]
        public IActionResult AddCompany(CompanyMaster _company)
        {

            _companyService._AddCompany(_company);
            return Ok("Company Added");
        }

        [HttpPut]
        public IActionResult UpdateCompany(CompanyMaster _company)
        {
            _companyService._UpdateCompany(_company);
            return Ok("Company Updated");
        }
        [HttpDelete("Id")]
        public IActionResult DeleteCompany(int id)
        {
            _companyService._DeleteCompany(id);
            return Ok("Company Deleted");
        }
    }
}