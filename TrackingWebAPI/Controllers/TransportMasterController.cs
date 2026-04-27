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
    public class TransportMasterController : ControllerBase
    {
        private readonly ITransportMaster _transportMasterService;
        private readonly ILogger<TransportMasterController> _logger;

        public TransportMasterController(ITransportMaster transportMasterService, ILogger<TransportMasterController> logger)
        {
            _transportMasterService = transportMasterService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransportMasters()
        {
            try
            {
                _logger.LogInformation("Fetching all records");
                var transportMasters = await _transportMasterService.GetTransportMasters();
                return Ok(new
                {
                    success = true,
                    data = transportMasters,
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
        public async Task<IActionResult> GetTransportMastersById(int id)
        {
            _logger.LogInformation("fetched record for ID: {id}", id);
            try
            {
                var transportMaster = await _transportMasterService.GetTransportMastersById(id);
               
                if (transportMaster == null)
                {
                    _logger.LogWarning("Record not found for ID: {id}", id);
                    return NotFound();
                }
                return Ok(new
                {
                    success = true,
                    data = transportMaster,
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
        public async Task<IActionResult> CreateTransportMasters(TrackingWebAPI.Models.TransportMaster transportMaster)
        {

            _logger.LogInformation("Creating new Create Mobile Alert Message record");
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var createdTransportMaster = await _transportMasterService.CreateTransportMasters(transportMaster);
               
                if (createdTransportMaster == null)
                {
                    _logger.LogWarning("Failed to create record");
                    return BadRequest("Failed to create record");
                }

                _logger.LogInformation("Record created successfully with ID: {id}", createdTransportMaster.TMID);
                return CreatedAtAction(nameof(GetTransportMastersById), new { id = createdTransportMaster.TMID }, createdTransportMaster);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating new Mobile Alert Message record");
                return StatusCode(500, "Internal server error");
            }
            
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransportMasters(int id, TrackingWebAPI.Models.TransportMaster transportMaster)
        {
            _logger.LogInformation("Updating record for ID: {id}", id);
            if (id != transportMaster.TMID)
            {
                _logger.LogWarning("ID mismatch: URL ID = {id}, ID = {TMID}", id, transportMaster.TMID);
                return BadRequest("ID mismatch");
            }
            try
            {

                var existingTransportMaster = await _transportMasterService.GetTransportMastersById(id);
                if (existingTransportMaster == null)
                {
                    _logger.LogWarning("Record not found for update, ID: {id}", id);
                    return NotFound();
                }
                await _transportMasterService.UpdateTransportMasters(id, transportMaster);
              
                _logger.LogInformation("Record updated successfully for ID: {id}", id);

                var result = await _transportMasterService.UpdateTransportMasters(id, transportMaster);
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

        public async Task<IActionResult> DeleteTransportMasters(int id)
        {
            _logger.LogInformation("Deleting record for ID: {id}", id);
            try
            {
                var existingTransportMaster = await _transportMasterService.GetTransportMastersById(id);
                if (existingTransportMaster == null)
                {
                    _logger.LogWarning("Record not found for deletion, ID: {id}", id);
                    return NotFound();
                }
                _logger.LogInformation("Record deleted successfully for ID: {id}", id);

             var result= await _transportMasterService.DeleteTransportMasters(id);
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
