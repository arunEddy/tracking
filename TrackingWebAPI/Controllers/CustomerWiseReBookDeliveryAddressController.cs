using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;

namespace TrackingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerWiseReBookDeliveryAddressController : ControllerBase
    {
        private readonly Interfaces.ICustomerWiseReBookDeliveryAddress _customerWiseReBookDeliveryAddressService;
        private readonly ILogger<CustomerWiseReBookDeliveryAddressController> _logger;
        public CustomerWiseReBookDeliveryAddressController
            (Interfaces.ICustomerWiseReBookDeliveryAddress customerWiseReBookDeliveryAddressService, ILogger<CustomerWiseReBookDeliveryAddressController> logger)
        {
            _customerWiseReBookDeliveryAddressService = customerWiseReBookDeliveryAddressService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomerWiseReBookDeliveryAddress()
        {
            _logger.LogInformation("Fetching all records");
            try
            {
                var result = await _customerWiseReBookDeliveryAddressService.GetAllCustomerReBook();
                return Ok(new
                {
                    success = true,
                    data = result,
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
        public async Task<IActionResult> GetCustomerWiseReBookDeliveryAddressById(int id)
        {
            _logger.LogInformation("Updating record for ID: {id}", id);
            try
            {
                var result = await _customerWiseReBookDeliveryAddressService.GetCustomerReBookById(id);
                if (result == null)
                {
                    _logger.LogWarning("Record not found for ID: {id}", id);
                    return NotFound();
                }
                return Ok(new
                {
                    success = true,
                    data = result,
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
        public async Task<IActionResult> CreateCustomerWiseReBookDeliveryAddress(TrackingWebAPI.Models.CustomerWiseReBookDeliveryAddress customerWiseReBookDeliveryAddress)
        {
            _logger.LogInformation("Creating new directorship record");
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _customerWiseReBookDeliveryAddressService.CreateCustomerReBook(customerWiseReBookDeliveryAddress);
                if (result == null)
                {
                    _logger.LogWarning("Failed to create record");
                    return BadRequest("Failed to create record");
                }

                _logger.LogInformation("Record created successfully with ID: {id}", customerWiseReBookDeliveryAddress.CWBID);
                return CreatedAtAction(nameof(GetCustomerWiseReBookDeliveryAddressById), new { id = customerWiseReBookDeliveryAddress.CWBID }, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating new record");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomerWiseReBookDeliveryAddress(int id, TrackingWebAPI.Models.CustomerWiseReBookDeliveryAddress customerWiseReBookDeliveryAddress)
        {

            _logger.LogInformation("Updating record for ID: {id}", id);
            if (id != customerWiseReBookDeliveryAddress.CWBID)
            {
                _logger.LogWarning("ID mismatch: URL ID = {id}, ID = {CWBID}", id, customerWiseReBookDeliveryAddress.CWBID);
                return BadRequest("ID mismatch");
            }
            try
            {
                var existingRecord = await _customerWiseReBookDeliveryAddressService.GetCustomerReBookById(id);
                if (existingRecord == null)
                {
                    _logger.LogWarning("Record not found for update, ID: {id}", id);
                    return NotFound();
                }
                _logger.LogInformation("Record updated successfully for ID: {id}", id);

                var result = await _customerWiseReBookDeliveryAddressService.UpdateCustomerReBook(id, customerWiseReBookDeliveryAddress);
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
        public async Task<IActionResult> DeleteCustomerWiseReBookDeliveryAddress(int id)
        {
            _logger.LogInformation("Deleting record for ID: {id}", id);
            try
            {
                var existingRecord = await _customerWiseReBookDeliveryAddressService.GetCustomerReBookById(id);
                if (existingRecord == null)
                {
                    _logger.LogWarning("Record not found for deletion, ID: {id}", id);
                    return NotFound();
                }
               
                _logger.LogInformation("Record deleted successfully for ID: {id}", id);
                await _customerWiseReBookDeliveryAddressService.DeleteCustomerReBook(id);
                return Ok("Record deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting record for ID: {id}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
