using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;
using TrackingWebAPI.Services;

namespace TrackingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirlineMasterController : ControllerBase
    {
        private readonly IAirlineMaster _airlineMasterService;
        private readonly ILogger<AirlineMasterController> _logger;
        public AirlineMasterController(IAirlineMaster airlineMasterService, ILogger<AirlineMasterController> logger)
        {
            _airlineMasterService = airlineMasterService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAirlineMaster()
        {
            try
            {
                _logger.LogInformation("Fetching all records");
                var airlineMasters = await _airlineMasterService.GetAllAirlineMaster();
                return Ok(new
                {
                    success = true,
                    data = airlineMasters,
                    message = "Data fetched successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all records");
                return StatusCode(500, "Internal server error");
            }

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAirlineMasterById(int id)
        {
            _logger.LogInformation("Updating record for ID: {id}", id);
            try
            {
                var airlineMaster = await _airlineMasterService.GetAirlineMasterById(id);
                if (airlineMaster == null)
                {
                    _logger.LogWarning("Record not found for ID: {id}", id);
                    return NotFound();
                }
                return Ok(new
                {
                    success = true,
                    data = airlineMaster,
                    message = $"Data fetched successfully for ID {id}"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating record for ID: {id}", id);
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpPost]
        public async Task<IActionResult> CreateAirlineMaster(TrackingWebAPI.Models.AirlineMaster airlineMaster)
        {
            _logger.LogInformation("Creating new directorship record");
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _airlineMasterService.CreateAirlineMaster(airlineMaster);
                if (result == null)
                {
                    _logger.LogWarning("Failed to create record");
                    return BadRequest("Failed to create record");
                }

                _logger.LogInformation("Record created successfully with ID: {id}", result.AIRID);
                return CreatedAtAction(nameof(GetAllAirlineMaster), new { id = result.AIRID }, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating new record");
                return StatusCode(500, "Internal server error");
            }


        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAirlineMaster(int id, TrackingWebAPI.Models.AirlineMaster airlineMaster)
        {
            _logger.LogInformation("Updating record for ID: {id}", id);
            if (id != airlineMaster.AIRID)
            {
                _logger.LogWarning("ID mismatch: URL ID = {id}, ID = {AIRID}", id, airlineMaster.AIRID);
                return BadRequest("ID mismatch");
            }
            try
            {
                var result = await _airlineMasterService.UpdateAirlineMaster(id, airlineMaster);
                if (result == null)
                {
                    _logger.LogWarning("Record not found for update, ID: {id}", id);
                    return NotFound();
                }
                _logger.LogInformation("Record updated successfully for ID: {id}", id);


                return Ok(new
                {
                    success = true,
                    data = result,
                    message = "Data Updated Sucessfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating record for ID: {id}", id);
                return StatusCode(500, "Internal server error");
            }

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAirlineMaster(int id)
        {
            _logger.LogInformation("Deleting record for ID: {id}", id);
            try
            {
                var deleted = await _airlineMasterService.DeleteAirlineMaster(id);
                if (deleted == null)
                {
                    _logger.LogWarning("Record not found for deletion, ID: {id}", id);
                    return NotFound();
                }
                _logger.LogInformation("Record deleted successfully for ID: {id}", id);

                return Ok("Office Request Master Deleted");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting record for ID: {id}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
