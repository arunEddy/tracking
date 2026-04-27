using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;

namespace TrackingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryBoyMastersController : ControllerBase
    {
        private readonly Interfaces.IDeliveryBoyMasters _deliveryBoyMastersService;
        private readonly ILogger<DeliveryBoyMastersController> _logger;
        public DeliveryBoyMastersController(Interfaces.IDeliveryBoyMasters deliveryBoyMastersService, ILogger<DeliveryBoyMastersController> logger)
        {
            _deliveryBoyMastersService = deliveryBoyMastersService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetDeliveryBoyMasters()
        {

            try
            {
                _logger.LogInformation("Fetching all records");
                var deliveryBoyMasters = await _deliveryBoyMastersService.GetAllDeleveryBoyMasters();
                return Ok(new
                {
                    success = true,
                    data = deliveryBoyMasters,
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
        public async Task<IActionResult> GetDeliveryBoyMastersById(int id)
        {
            _logger.LogInformation("Updating record for ID: {id}", id);
            try
            {
                var deliveryBoyMaster = await _deliveryBoyMastersService.GetDeleveryBoyMastersById(id);
                if (deliveryBoyMaster == null)
                {
                    _logger.LogWarning("Record not found for ID: {id}", id);
                    return NotFound();
                }
                return Ok(new
                {
                    success = true,
                    data = deliveryBoyMaster,
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
        public async Task<IActionResult> CreateDeliveryBoyMasters(TrackingWebAPI.Models.DeliveryBoyMasters deliveryBoyMaster)
        {
            _logger.LogInformation("Creating new directorship record");
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var createdDeliveryBoyMaster = await _deliveryBoyMastersService.CreateDeleveryBoyMasters(deliveryBoyMaster);
                if (createdDeliveryBoyMaster == null)
                {
                    _logger.LogWarning("Failed to create record");
                    return BadRequest("Failed to create record");
                }

                _logger.LogInformation("Record created successfully with ID: {id}", createdDeliveryBoyMaster.DBMID);
                return CreatedAtAction(nameof(DeliveryBoyMasters), new { id = createdDeliveryBoyMaster.DBMID }, createdDeliveryBoyMaster);
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating record");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDeleveryBoyMasters(int id, TrackingWebAPI.Models.DeliveryBoyMasters deliveryBoyMaster)
        {
            _logger.LogInformation("Updating record for ID: {id}", id);
            if (id != deliveryBoyMaster.DBMID)
            {
                _logger.LogWarning("ID mismatch: URL ID = {id}, ID = {DBMID}", id, deliveryBoyMaster.DBMID);
                return BadRequest("ID mismatch");
            }
            try
            {
                var existingDeliveryBoyMaster = await _deliveryBoyMastersService.GetDeleveryBoyMastersById(id);
                if (existingDeliveryBoyMaster == null)
                {
                    _logger.LogWarning("Record not found for update, ID: {id}", id);
                    return NotFound();
                }
                _logger.LogInformation("Record updated successfully for ID: {id}", id);
                var result = await _deliveryBoyMastersService.UpdateDeleveryBoyMasters(id, deliveryBoyMaster);
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
        public async Task<IActionResult> DeleteDeliveryBoyMasters(int id)
        {
            _logger.LogInformation("Deleting record for ID: {id}", id);
            try
            {
                var existingDeliveryBoyMaster = await _deliveryBoyMastersService.GetDeleveryBoyMastersById(id);
                if (existingDeliveryBoyMaster == null)
                {
                    _logger.LogWarning("Record not found for deletion, ID: {id}", id);
                    return NotFound();
                }

                _logger.LogInformation("Record deleted successfully for ID: {id}", id);
                await _deliveryBoyMastersService.DeleteDeleveryBoyMasters(id);
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
