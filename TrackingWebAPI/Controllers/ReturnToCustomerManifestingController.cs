using DALCLASS.DBContact;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrackingWebAPI.Models;

namespace TrackingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReturnToCustomerManifestingController : ControllerBase
    {

        private readonly Interfaces.IReturnToCustomerManifesting _returntocustomermanifesting;
        private readonly ILogger<ReturnToCustomerManifesting> _logger;
        public ReturnToCustomerManifestingController(Interfaces.IReturnToCustomerManifesting returntocustomermanifesting, ILogger<ReturnToCustomerManifesting> logger)
        {
            _returntocustomermanifesting = returntocustomermanifesting;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReturnToCustomerManifesting()
        {
            try
            {
                _logger.LogInformation("Fetching all records");
                var nonDeliveries = await _returntocustomermanifesting.GetAllReturnToCustomerManifesting();

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
        public async Task<IActionResult> GetReturnToCustomerManifestingById(int id)
        {
            _logger.LogInformation("fetched record for ID: {id}", id);
            try
            {
                var nonDeliveries = await _returntocustomermanifesting.GetReturnToCustomerManifestingById(id);
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
        public async Task<IActionResult> CreateReturnToCustomerManifesting(TrackingWebAPI.Models.ReturnToCustomerManifesting returnToCustomerManifesting)
        {

            _logger.LogInformation("Creating new Create Stock Purchase Message record");
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var nonDeliveriesMaster = await _returntocustomermanifesting.CreateReturnToCustomerManifesting(returnToCustomerManifesting);

                if (nonDeliveriesMaster == null)
                {
                    _logger.LogWarning("Failed to create record");
                    return BadRequest("Failed to create record");
                }

                _logger.LogInformation("Record created successfully with ID: {id}", nonDeliveriesMaster.rtcmid);

                return CreatedAtAction(nameof(GetReturnToCustomerManifestingById), new { id = nonDeliveriesMaster.rtcmid }, nonDeliveriesMaster);

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
        public async Task<IActionResult> UpdateReturnToCustomerManifesting(int id, TrackingWebAPI.Models.ReturnToCustomerManifesting returnToCustomerManifesting)
        {
            _logger.LogInformation("Updating record for ID: {id}", id);
            if (id != returnToCustomerManifesting.rtcmid)
            {
                _logger.LogWarning("ID mismatch: URL ID = {id}, ID = {ndId}", id, returnToCustomerManifesting.rtcmid);
                return BadRequest("ID mismatch");
            }
            try
            {

                var nonDeliveriesMaster = await _returntocustomermanifesting.GetReturnToCustomerManifestingById(id);
                if (nonDeliveriesMaster == null)
                {
                    _logger.LogWarning("Record not found for update, ID: {id}", id);
                    return NotFound();
                }
                _logger.LogInformation("Record updated successfully for ID: {id}", id);

                var result = await _returntocustomermanifesting.UpdateReturnToCustomerManifesting(id, returnToCustomerManifesting);
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
        public async Task<IActionResult> DeleteReturnToCustomerManifesting(int id)
        {

            _logger.LogInformation("Deleting record for ID: {id}", id);
            try
            {
                var existingnonDeliveries = await _returntocustomermanifesting.GetReturnToCustomerManifestingById(id);
                if (existingnonDeliveries == null)
                {
                    _logger.LogWarning("Record not found for deletion, ID: {id}", id);
                    return NotFound();
                }
                _logger.LogInformation("Record deleted successfully for ID: {id}", id);

                await _returntocustomermanifesting.DeleteReturnToCustomerManifesting(id);
                return Ok("Return to customer manifesting Deleted");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting record for ID: {id}", id);
                return StatusCode(500, "Internal server error");
            }

        }
    }
}