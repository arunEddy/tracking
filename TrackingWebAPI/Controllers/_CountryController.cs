using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;

namespace TrackingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountry _countryService;

        public CountryController(ICountry countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        public IActionResult GetCountry()
        {
            var result = _countryService.GetCountries();
            return Ok(result);
        }
        [HttpGet("GetByType")]
        public IActionResult GetByType(bool type)
        {
            var result = _countryService.GetCountries(type);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddCountry(CountryMaster country)
        {
            _countryService._AddCountry(country);
            return Ok("Country Added");
        }

        [HttpPut ("{countryId}")]
        public IActionResult UpdateCountry(int countryId, [FromBody] CountryMaster country)
        {
            _countryService._UpdateCountry(countryId, country);
            return Ok("Country Updated");
        }

        [HttpDelete("{countryId}")]
        public IActionResult DeleteCountry(int countryId)
        {
            _countryService._DeleteCountry(countryId);
            return Ok("Country Deleted");
        }

       
    }
}
