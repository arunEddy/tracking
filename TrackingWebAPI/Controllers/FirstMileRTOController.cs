using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TrackingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FirstMileRTOController : ControllerBase
    {
        private readonly Interfaces.IFirstMileRTO _FirstMileRTO;
        private readonly ILogger<FirstMileRTOController> _logger;
        public FirstMileRTOController(Interfaces.IFirstMileRTO FirstMileRTO, ILogger<FirstMileRTOController> logger)
        {
            _FirstMileRTO = FirstMileRTO;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetfirstMileRTO()
        {
            try
            {
                _logger.LogInformation("Fetching all records");
                var nonDeliveries = await _FirstMileRTO.GetfirstMileRTO();

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
        public async Task<IActionResult> GetfirstMileRTOById(int id)
        {
            _logger.LogInformation("fetched record for ID: {id}", id);
            try
            {
                var nonDeliveries = await _FirstMileRTO.GetfirstMileRTOById(id);
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
        public async Task<IActionResult> CreatefirstMileRTO(TrackingWebAPI.Models.FirstMileRTO firstMileRTO)
        {

            _logger.LogInformation("Creating new Create Stock Purchase Message record");
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var nonDeliveriesMaster = await _FirstMileRTO.CreatefirstMileRTO(firstMileRTO);

                if (nonDeliveriesMaster == null)
                {
                    _logger.LogWarning("Failed to create record");
                    return BadRequest("Failed to create record");
                }

                _logger.LogInformation("Record created successfully with ID: {id}", nonDeliveriesMaster.rtoid);

                return CreatedAtAction(nameof(GetfirstMileRTOById), new { id = nonDeliveriesMaster.rtoid }, nonDeliveriesMaster);

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
        public async Task<IActionResult> UpdatUpdatefirstMileRTOeDRS(int id, TrackingWebAPI.Models.FirstMileRTO firstMileRTO)
        {
            _logger.LogInformation("Updating record for ID: {id}", id);
            if (id != firstMileRTO.rtoid)
            {
                _logger.LogWarning("ID mismatch: URL ID = {id}, ID = {ndId}", id, firstMileRTO.rtoid);
                return BadRequest("ID mismatch");
            }
            try
            {

                var nonDeliveriesMaster = await _FirstMileRTO.GetfirstMileRTOById(id);
                if (nonDeliveriesMaster == null)
                {
                    _logger.LogWarning("Record not found for update, ID: {id}", id);
                    return NotFound();
                }
                _logger.LogInformation("Record updated successfully for ID: {id}", id);

                var result = await _FirstMileRTO.UpdatefirstMileRTO(id, firstMileRTO);
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
        public async Task<IActionResult> DeletefirstMileRTO(int id)
        {

            _logger.LogInformation("Deleting record for ID: {id}", id);
            try
            {
                var existingnonDeliveries = await _FirstMileRTO.GetfirstMileRTOById(id);
                if (existingnonDeliveries == null)
                {
                    _logger.LogWarning("Record not found for deletion, ID: {id}", id);
                    return NotFound();
                }
                _logger.LogInformation("Record deleted successfully for ID: {id}", id);

                await _FirstMileRTO.DeletefirstMileRTO(id);
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