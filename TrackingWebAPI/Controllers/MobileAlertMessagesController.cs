using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;
using TrackingWebAPI.Services;

namespace TrackingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MobileAlertMessagesController : ControllerBase
    {
        private readonly Interfaces.IMobileAlertMessages _mobileAlertMessagesService;
        private readonly ILogger<MobileAlertMessagesController> _logger;
        public MobileAlertMessagesController(Interfaces.IMobileAlertMessages mobileAlertMessagesService, ILogger<MobileAlertMessagesController> logger)
        {
            _mobileAlertMessagesService = mobileAlertMessagesService;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetMobileAlertMessages()
        {
            try
            {
                _logger.LogInformation("Fetching all records");
                var result = await _mobileAlertMessagesService.GetAllMobileAlertMessages();
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
        public async Task<IActionResult> GetMobileAlertMessagesById(int id)
        {
            _logger.LogInformation("Updating record for ID: {id}", id);
            try
            {
                var result = await _mobileAlertMessagesService.GetMobileAlertMessagesById(id);
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
        public async Task<IActionResult> CreateMobileAlertMessages(TrackingWebAPI.Models.MobileAlertMessages mobileAlertMessages)
        {
            _logger.LogInformation("Creating new Create Mobile Alert Message record");
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _mobileAlertMessagesService.CreateMobileAlertMessages(mobileAlertMessages);
                //return Ok(result);
                if (result == null)
                {
                    _logger.LogWarning("Failed to create record");
                    return BadRequest("Failed to create record");
                }

                _logger.LogInformation("Record created successfully with ID: {id}", mobileAlertMessages.MAMID);
                return CreatedAtAction(nameof(GetMobileAlertMessagesById), new { id = mobileAlertMessages.MAMID }, mobileAlertMessages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating new Mobile Alert Message record");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMobileAlertMessages(int id, TrackingWebAPI.Models.MobileAlertMessages mobileAlertMessages)
        {
            _logger.LogInformation("Updating record for ID: {id}", id);
            if (id != mobileAlertMessages.MAMID)
            {
                _logger.LogWarning("ID mismatch: URL ID = {id}, ID = {MAMID}", id, mobileAlertMessages.MAMID);
                return BadRequest("ID mismatch");
            }
            try
            {
                var existingRecord = await _mobileAlertMessagesService.GetMobileAlertMessagesById(id);
                if (existingRecord == null)
                {
                    _logger.LogWarning("Record not found for update, ID: {id}", id);
                    return NotFound();
                }
                _logger.LogInformation("Record updated successfully for ID: {id}", id);

                var result = await _mobileAlertMessagesService.UpdateMobileAlertMessages(id, mobileAlertMessages);
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
        [HttpDelete]
        public async Task<IActionResult> DeleteMobileAlertMessages(int id)
        {
            _logger.LogInformation("Deleting record for ID: {id}", id);
            try
            {
                var existingRecord = await _mobileAlertMessagesService.GetMobileAlertMessagesById(id);
                if (existingRecord == null)
                {
                    _logger.LogWarning("Record not found for deletion, ID: {id}", id);
                    return NotFound();
                }
                _logger.LogInformation("Record deleted successfully for ID: {id}", id);
               
                await _mobileAlertMessagesService.DeleteMobileAlertMessages(id);
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
