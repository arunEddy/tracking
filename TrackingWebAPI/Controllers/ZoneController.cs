using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;

namespace TrackingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ZoneController : ControllerBase
    {
        private readonly IZoneMaster _zoneService;

        public ZoneController(IZoneMaster ZoneService)
        {
            _zoneService = ZoneService;
        }

        [HttpGet]
        public IActionResult GetZone()
        {
            var result = _zoneService.GetZone();
            return Ok(result);
        }
        [HttpGet("GetByType")]
        public IActionResult GetByType(bool type)
        {
            var result = _zoneService.GetZone(type);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddZone(ZoneMaster zone)
        {
            _zoneService.AddZone(zone);
            return Ok("Zone Added");
        }

        [HttpPut("{zoneId}")]
        public IActionResult UpdateZone(int zoneId, [FromBody] ZoneMaster zone)
        {
            _zoneService.UpdateZone(zoneId, zone);
            return Ok("Zone Updated");
        }

        [HttpDelete("{zoneId}")]
        public IActionResult DeleteZone(int zoneId)
        {
            _zoneService.DeleteZone(zoneId);
            return Ok("Zone Deleted");
        }


    }
}
