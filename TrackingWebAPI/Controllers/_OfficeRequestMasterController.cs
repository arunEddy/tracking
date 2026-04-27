using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TrackingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class _OfficeRequestMasterController : ControllerBase
    {
        private readonly IOfficeRequestMaster _officeRequestMasterService;
        private readonly ILogger<_OfficeRequestMasterController> _logger;
        public _OfficeRequestMasterController(IOfficeRequestMaster officeRequestMasterService,ILogger<_OfficeRequestMasterController> logger)
        {
            _officeRequestMasterService = officeRequestMasterService;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOfficeRequestMaster()
        {
            try
            {
                _logger.LogInformation("Fetching all records");
               
                var result = await _officeRequestMasterService.GetAllOfficeRequestMaster();
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
        public async Task<IActionResult> GetOfficeRequestMasterById(int id)
        {
            _logger.LogInformation("Fetching record for ID: {id}", id);
            try
            {
                var result = await _officeRequestMasterService.GetOfficeRequestMasterById(id);

                if(result == null)
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
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error while fetching record for ID: {id}", id);
                return StatusCode(500, "Internal server error");
            }
          
        }
        [HttpPost]
        public async Task<IActionResult> CreateOfficeRequestMaster(TrackingWebAPI.Models.OfficeRequestMaster officeRequestMaster)
        {
            _logger.LogInformation("Creating new directorship record");
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _officeRequestMasterService.CreateOfficeRequestMaster(officeRequestMaster);
                if (result == null)
                {
                    _logger.LogWarning("Failed to create record");
                    return BadRequest("Failed to create record");
                }

                _logger.LogInformation("Record created successfully with ID: {id}", result.ORMID);
                return CreatedAtAction(nameof(CreateOfficeRequestMaster), new { id = result.ORMID }, result);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error while creating record");
                return StatusCode(500, "Internal server error");
            }
            
        }
        [HttpPut("{id}")]   
        public async Task<IActionResult> UpdateOfficeRequestMaster(int id, TrackingWebAPI.Models.OfficeRequestMaster officeRequestMaster)
        {
            _logger.LogInformation("Updating record for ID: {id}", id);
            if (id != officeRequestMaster.ORMID)
            {
                _logger.LogWarning("ID mismatch: URL ID = {id}, ID = {ORMID}", id, officeRequestMaster.ORMID);
                return BadRequest("ID mismatch");
            }
            try
            {
                var result = await _officeRequestMasterService.UpdateOfficeRequestMaster(id, officeRequestMaster);
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
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error while updating record for ID: {id}", id);
                return StatusCode(500, "Internal server error");
            }
           
        }
        [HttpDelete("{id}")]    
        public async Task<IActionResult> DeleteOfficeRequestMaster(int id)
        {
            _logger.LogInformation("Deleting record for ID: {id}", id);
            try
            {
              var deleted = await _officeRequestMasterService.DeleteOfficeRequestMaster(id);
               

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
