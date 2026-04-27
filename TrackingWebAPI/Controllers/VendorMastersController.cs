using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackingWebAPI.Services;

namespace TrackingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorMastersController : ControllerBase
    {
        private readonly Interfaces.IVendorMaster _vendorMasterService;
        private readonly ILogger<VendorMastersController> _logger;
        public VendorMastersController(Interfaces.IVendorMaster vendorMasterService, ILogger<VendorMastersController> logger)
        {
            _vendorMasterService = vendorMasterService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetVendorMasters()
        {
            try
            {
                _logger.LogInformation("Fetching all records");
                var vendorMasters = await _vendorMasterService.GetVendorMasters();

                return Ok(new
                {
                    success = true,
                    data = vendorMasters,
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
        public async Task<IActionResult> GetVendorMastersById(int id)
        {
            _logger.LogInformation("fetched record for ID: {id}", id);
            try
            {
                var vendorMaster = await _vendorMasterService.GetVendorMastersById(id);
                if (vendorMaster == null)
                {
                    _logger.LogWarning("Record not found for ID: {id}", id);
                    return NotFound();
                }
                return Ok(new
                {
                    success = true,
                    data = vendorMaster,
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
        public async Task<IActionResult> CreateVendorMasters(TrackingWebAPI.Models.VendorMasters vendorMaster)
        {

            _logger.LogInformation("Creating new Create Mobile Alert Message record");
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var createdVendorMaster = await _vendorMasterService.CreateVendorMasters(vendorMaster);

                if (createdVendorMaster == null)
                {
                    _logger.LogWarning("Failed to create record");
                    return BadRequest("Failed to create record");
                }

                _logger.LogInformation("Record created successfully with ID: {id}", vendorMaster.VMID);
              
                return CreatedAtAction(nameof(GetVendorMastersById), new { id = createdVendorMaster.VMID }, createdVendorMaster);
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating new Vendor Master Details record");
               

                var error = ex.InnerException?.Message ?? ex.Message;
                Console.WriteLine("ERROR: " + error);

                return StatusCode(500, error);
            }
            
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVendorMasters(int id, TrackingWebAPI.Models.VendorMasters vendorMaster)
        {
            _logger.LogInformation("Updating record for ID: {id}", id);
            if (id != vendorMaster.VMID)
            {
                _logger.LogWarning("ID mismatch: URL ID = {id}, ID = {VMID}", id, vendorMaster.VMID);
                return BadRequest("ID mismatch");
            }
            try
            {

                var existingVendorMaster = await _vendorMasterService.GetVendorMastersById(id);
                if (existingVendorMaster == null)
                {
                    _logger.LogWarning("Record not found for update, ID: {id}", id);
                    return NotFound();
                }
                _logger.LogInformation("Record updated successfully for ID: {id}", id);

               var result= await _vendorMasterService.UpdateVendorMasters(id, vendorMaster);
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
        public async Task<IActionResult> DeleteVendorMasters(int id)
        {

            _logger.LogInformation("Deleting record for ID: {id}", id);
            try
            {
                var existingVendorMaster = await _vendorMasterService.GetVendorMastersById(id);
                if (existingVendorMaster == null)
                {
                    _logger.LogWarning("Record not found for deletion, ID: {id}", id);
                    return NotFound();
                }
                _logger.LogInformation("Record deleted successfully for ID: {id}", id);

                await _vendorMasterService.DeleteVendorMasters(id);
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
