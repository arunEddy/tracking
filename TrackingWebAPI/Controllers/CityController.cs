using DALCLASS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;
using TrackingWebAPI.Services;
using CityMaster = TrackingWebAPI.Models.CityMaster;
using cityServices = TrackingWebAPI.Services.CityServices;

namespace TrackingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityMaster _cityService;

        public CityController(ICityMaster cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        public IActionResult GetCitys()
        {
            return Ok(_cityService.GetCity());
        }

        [HttpGet("{cityId}")]
        public IActionResult GetCitys(int cityId)
        {
            return Ok(_cityService.GetCity(cityId));
        }

        [HttpPost]
        public IActionResult AddCity(CityMaster _city)
        {

            _cityService._AddCity(_city);
            return Ok("City Added");
        }

        //[HttpPut("{countryId}")]
        //public IActionResult UpdateCountry(int countryId, [FromBody] CountryMaster country)
        //{
        //    _countryService._UpdateCountry(countryId, country);
        //    return Ok("Country Updated");
        //}

        [HttpPut("{cityId}")]
        public IActionResult UpdateCity(int cityId, CityMaster _city)
        {
            _cityService._UpdateCity(cityId, _city);
            return Ok("City Updated");
        }
        [HttpDelete("{cityId}")]
        public IActionResult DeleteCity(int cityId)
        {
            _cityService._DeleteCity(cityId);
            return Ok("City Deleted");
        }
    }
}
