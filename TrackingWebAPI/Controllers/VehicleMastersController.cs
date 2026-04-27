using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;
using TrackingWebAPI.Services;

namespace TrackingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleMastersController : ControllerBase
    {
        private readonly Interfaces.IVehicleMasters _vehicleMastersService;
        private readonly ILogger<VehicleMastersController> _logger;
        public VehicleMastersController(Interfaces.IVehicleMasters vehicleMastersService, ILogger<VehicleMastersController> logger  )
        {
            _vehicleMastersService = vehicleMastersService;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetVehicleMasters()
        {
            try
            {
                _logger.LogInformation("Fetching all records");
                var vehicleMasters = await _vehicleMastersService.GetVehicleMasters();
               
                return Ok(new
                {
                    success = true,
                    data = vehicleMasters,
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
        public async Task<IActionResult> GetVehicleMastersById(int id)
        {
            _logger.LogInformation("fetched record for ID: {id}", id);
            try
            {
                var vehicleMaster = await _vehicleMastersService.GetVehicleMastersById(id);
                if (vehicleMaster == null)
                {
                    _logger.LogWarning("Record not found for ID: {id}", id);
                    return NotFound();
                }
                return Ok(new
                {
                    success = true,
                    data = vehicleMaster,
                    message = $"Data fetched successfully for ID {id}"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching record for ID: {id}", id);
                return StatusCode(500, "Internal server error");
            }
          
        }
        [HttpPost]
        public async Task<IActionResult> CreateVehicleMasters(TrackingWebAPI.Models.VehicleMasters vehicleMaster)
        {
            _logger.LogInformation("Creating new Create Mobile Alert Message record");
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var createdVehicleMaster = await _vehicleMastersService.CreateVehicleMasters(vehicleMaster);
                
                if (createdVehicleMaster == null)
                {
                    _logger.LogWarning("Failed to create record");
                    return BadRequest("Failed to create record");
                }

                _logger.LogInformation("Record created successfully with ID: {id}", createdVehicleMaster.VEMID);
                return CreatedAtAction(nameof(GetVehicleMastersById), new { id = createdVehicleMaster.VEMID }, createdVehicleMaster);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating new Mobile Alert Message record");
                return StatusCode(500, "Internal server error");
            }
           
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicleMasters(int id, TrackingWebAPI.Models.VehicleMasters vehicleMaster)
        {
            _logger.LogInformation("Updating record for ID: {id}", id);
            if (id != vehicleMaster.VEMID)
            {
                _logger.LogWarning("ID mismatch: URL ID = {id}, ID = {VEMID}", id, vehicleMaster.VEMID);
                return BadRequest("ID mismatch");
            }
            try
            {

                var existingVehicleMaster = await _vehicleMastersService.GetVehicleMastersById(id);
                if (existingVehicleMaster == null)
                {
                    _logger.LogWarning("Record not found for update, ID: {id}", id);
                    return NotFound();
                }
                _logger.LogInformation("Record updated successfully for ID: {id}", id);

                var result = await _vehicleMastersService.UpdateVehicleMasters(id, vehicleMaster);
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
        public async Task<IActionResult> DeleteVehicleMasters(int id)
        {
            _logger.LogInformation("Deleting record for ID: {id}", id);
            try
            {
                var existingVehicleMaster = await _vehicleMastersService.GetVehicleMastersById(id);
                if (existingVehicleMaster == null)
                {
                    _logger.LogWarning("Record not found for deletion, ID: {id}", id);
                    return NotFound();
                }
                _logger.LogInformation("Record deleted successfully for ID: {id}", id);

                await _vehicleMastersService.DeleteVehicleMasters(id);
                return Ok("Mobile Alert Messages Deleted");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting record for ID: {id}", id);
                return StatusCode(500, "Internal server error");
            }
            
         
        }
    }
}
