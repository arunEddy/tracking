using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TrackingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NonDeliveryController : ControllerBase
    {
        private readonly Interfaces.INonDelivery _nonDelivery;
        private readonly ILogger<NonDeliveryController> _logger;
        public NonDeliveryController(Interfaces.INonDelivery nonDelivery, ILogger<NonDeliveryController> logger)
        {
            _nonDelivery = nonDelivery;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDRS()
        {
            try
            {
                _logger.LogInformation("Fetching all records");
                var nonDeliveries = await _nonDelivery.GetNonDeliveries();

                return Ok(new
                {
                    success = true,
                    data = nonDeliveries,
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
        public async Task<IActionResult> GetDRSById(int id)
        {
            _logger.LogInformation("fetched record for ID: {id}", id);
            try
            {
                var nonDeliveries = await _nonDelivery.GetNonDeliveryById(id);
                if (nonDeliveries == null)
                {
                    _logger.LogWarning("Record not found for ID: {id}", id);
                    return NotFound();
                }
                return Ok(new
                {
                    success = true,
                    data = nonDeliveries,
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
        public async Task<IActionResult> CreateDRS(TrackingWebAPI.Models.NonDelivery nonDeliveries)
        {

            _logger.LogInformation("Creating new Create Stock Purchase Message record");
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var nonDeliveriesMaster = await _nonDelivery.CreateNonDelivery(nonDeliveries);

                if (nonDeliveriesMaster == null)
                {
                    _logger.LogWarning("Failed to create record");
                    return BadRequest("Failed to create record");
                }

                _logger.LogInformation("Record created successfully with ID: {id}", nonDeliveriesMaster.ndId);

                return CreatedAtAction(nameof(GetDRSById), new { id = nonDeliveriesMaster.ndId }, nonDeliveriesMaster);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating new  Stock Purchase Details record");


                var error = ex.InnerException?.Message ?? ex.Message;
                Console.WriteLine("ERROR: " + error);

                return StatusCode(500, error);
            }

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDRS(int id, TrackingWebAPI.Models.NonDelivery nonDeliveries)
        {
            _logger.LogInformation("Updating record for ID: {id}", id);
            if (id != nonDeliveries.ndId)
            {
                _logger.LogWarning("ID mismatch: URL ID = {id}, ID = {ndId}", id, nonDeliveries.ndId);
                return BadRequest("ID mismatch");
            }
            try
            {

                var nonDeliveriesMaster = await _nonDelivery.GetNonDeliveryById(id);
                if (nonDeliveriesMaster == null)
                {
                    _logger.LogWarning("Record not found for update, ID: {id}", id);
                    return NotFound();
                }
                _logger.LogInformation("Record updated successfully for ID: {id}", id);

                var result = await _nonDelivery.UpdateNonDelivery(id, nonDeliveries);
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
        public async Task<IActionResult> DeleteDRS(int id)
        {

            _logger.LogInformation("Deleting record for ID: {id}", id);
            try
            {
                var existingnonDeliveries = await _nonDelivery.GetNonDeliveryById(id);
                if (existingnonDeliveries == null)
                {
                    _logger.LogWarning("Record not found for deletion, ID: {id}", id);
                    return NotFound();
                }
                _logger.LogInformation("Record deleted successfully for ID: {id}", id);

                await _nonDelivery.DeleteNonDelivery(id);
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