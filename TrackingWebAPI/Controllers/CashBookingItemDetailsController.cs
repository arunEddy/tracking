using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TrackingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CashBookingItemDetailsController : ControllerBase
    {
        private readonly Interfaces.ICashBookingItemDetails _cashbookingItemDetails;
        private readonly ILogger<CashBookingItemDetailsController> _logger;
        public CashBookingItemDetailsController(Interfaces.ICashBookingItemDetails stockRequest, ILogger<CashBookingItemDetailsController> logger)
        {
            _cashbookingItemDetails = stockRequest;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCashBookingItemDetails()
        {
            try
            {
                _logger.LogInformation("Fetching all records");
                var stockPurchase = await _cashbookingItemDetails.GetAllCashBookingItemDetails();

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
        public async Task<IActionResult> GetCashBookingItemDetailsById(int id)
        {
            _logger.LogInformation("fetched record for ID: {id}", id);
            try
            {
                var stockPurchase = await _cashbookingItemDetails.GetCashBookingItemDetailsById(id);
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
        public async Task<IActionResult> CreateCashBookingItemDetails(TrackingWebAPI.Models.CashBookingItemDetails stockout)
        {

            _logger.LogInformation("Creating new Create Stock Purchase Message record");
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var stockpurchaseMaster = await _cashbookingItemDetails.CreateCashBookingItemDetails(stockout);

                if (stockpurchaseMaster == null)
                {
                    _logger.LogWarning("Failed to create record");
                    return BadRequest("Failed to create record");
                }

                _logger.LogInformation("Record created successfully with ID: {id}", stockpurchaseMaster.cbIId);

                return CreatedAtAction(nameof(GetCashBookingItemDetailsById), new { id = stockpurchaseMaster.cbIId }, stockpurchaseMaster);

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
        public async Task<IActionResult> UpdateCashBookingItemDetails(int id, TrackingWebAPI.Models.CashBookingItemDetails stockout)
        {
            _logger.LogInformation("Updating record for ID: {id}", id);
            if (id != stockout.cbIId)
            {
                _logger.LogWarning("ID mismatch: URL ID = {id}, ID = {cbIId}", id, stockout.cbIId);
                return BadRequest("ID mismatch");
            }
            try
            {

                var stockPurchaseMaster = await _cashbookingItemDetails.GetCashBookingItemDetailsById(id);
                if (stockPurchaseMaster == null)
                {
                    _logger.LogWarning("Record not found for update, ID: {id}", id);
                    return NotFound();
                }
                _logger.LogInformation("Record updated successfully for ID: {id}", id);

                var result = await _cashbookingItemDetails.UpdateCashBookingItemDetails(id, stockout);
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
        public async Task<IActionResult> DeleteCashBookingItemDetails(int id)
        {

            _logger.LogInformation("Deleting record for ID: {id}", id);
            try
            {
                var existingstockpurchase = await _cashbookingItemDetails.GetCashBookingItemDetailsById(id);
                if (existingstockpurchase == null)
                {
                    _logger.LogWarning("Record not found for deletion, ID: {id}", id);
                    return NotFound();
                }
                _logger.LogInformation("Record deleted successfully for ID: {id}", id);

                await _cashbookingItemDetails.DeleteCashBookingItemDetails(id);
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
