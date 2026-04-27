using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TrackingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BULKVIRTUALBOOKINGController : ControllerBase
    {
        private readonly Interfaces.IBULKVIRTUALBOOKING _bulkvitualbooking;
        private readonly ILogger<BULKVIRTUALBOOKINGController> _logger;
        public BULKVIRTUALBOOKINGController(Interfaces.IBULKVIRTUALBOOKING stockRequest, ILogger<BULKVIRTUALBOOKINGController> logger)
        {
            _bulkvitualbooking = stockRequest;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBULKVIRTUALBOOKING()
        {
            try
            {
                _logger.LogInformation("Fetching all records");
                var stockPurchase = await _bulkvitualbooking.GetAllBULKVIRTUALBOOKING();

                return Ok(new
                {
                    success = true,
                    data = stockPurchase,
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
        public async Task<IActionResult> GetBULKVIRTUALBOOKINGById(int id)
        {
            _logger.LogInformation("fetched record for ID: {id}", id);
            try
            {
                var stockPurchase = await _bulkvitualbooking.GetBULKVIRTUALBOOKINGById(id);
                if (stockPurchase == null)
                {
                    _logger.LogWarning("Record not found for ID: {id}", id);
                    return NotFound();
                }
                return Ok(new
                {
                    success = true,
                    data = stockPurchase,
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
        public async Task<IActionResult> CreateBULKVIRTUALBOOKINGDetails(TrackingWebAPI.Models.BULKVIRTUALBOOKING stockout)
        {

            _logger.LogInformation("Creating new Create Stock Purchase Message record");
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var stockpurchaseMaster = await _bulkvitualbooking.CreateBULKVIRTUALBOOKINGDetails(stockout);

                if (stockpurchaseMaster == null)
                {
                    _logger.LogWarning("Failed to create record");
                    return BadRequest("Failed to create record");
                }

                _logger.LogInformation("Record created successfully with ID: {id}", stockpurchaseMaster.bvbid);

                return CreatedAtAction(nameof(GetBULKVIRTUALBOOKINGById), new { id = stockpurchaseMaster.bvbid }, stockpurchaseMaster);

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
        public async Task<IActionResult> UpdateBULKVIRTUALBOOKINGDetails(int id, TrackingWebAPI.Models.BULKVIRTUALBOOKING stockout)
        {
            _logger.LogInformation("Updating record for ID: {id}", id);
            if (id != stockout.bvbid)
            {
                _logger.LogWarning("ID mismatch: URL ID = {id}, ID = {bvbid}", id, stockout.bvbid);
                return BadRequest("ID mismatch");
            }
            try
            {

                var stockPurchaseMaster = await _bulkvitualbooking.GetBULKVIRTUALBOOKINGById(id);
                if (stockPurchaseMaster == null)
                {
                    _logger.LogWarning("Record not found for update, ID: {id}", id);
                    return NotFound();
                }
                _logger.LogInformation("Record updated successfully for ID: {id}", id);

                var result = await _bulkvitualbooking.UpdateBULKVIRTUALBOOKINGDetails(id, stockout);
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
        public async Task<IActionResult> DeleteBULKVIRTUALBOOKINGDetails(int id)
        {

            _logger.LogInformation("Deleting record for ID: {id}", id);
            try
            {
                var existingstockpurchase = await _bulkvitualbooking.GetBULKVIRTUALBOOKINGById(id);
                if (existingstockpurchase == null)
                {
                    _logger.LogWarning("Record not found for deletion, ID: {id}", id);
                    return NotFound();
                }
                _logger.LogInformation("Record deleted successfully for ID: {id}", id);

                await _bulkvitualbooking.DeleteBULKVIRTUALBOOKINGDetails(id);
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
