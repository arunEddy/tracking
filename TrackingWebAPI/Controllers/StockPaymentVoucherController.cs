using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TrackingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockPaymentVoucherController : ControllerBase
    {
        private readonly Interfaces.IStockPaymentVoucher _stockPaymentVoucher;
        private readonly ILogger<StockPaymentVoucherController> _logger;
        public StockPaymentVoucherController(Interfaces.IStockPaymentVoucher StockOut, ILogger<StockPaymentVoucherController> logger)
        {
            _stockPaymentVoucher = StockOut;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStockPaymentVoucher()
        {
            try
            {
                _logger.LogInformation("Fetching all records");
                var stockPurchase = await _stockPaymentVoucher.GetAllStockPaymentVoucher();

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
        public async Task<IActionResult> GetStockPaymentVoucherById(int id)
        {
            _logger.LogInformation("fetched record for ID: {id}", id);
            try
            {
                var stockPurchase = await _stockPaymentVoucher.GetStockPaymentVoucherById(id);
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
        public async Task<IActionResult> CreateStockPaymentVoucher(TrackingWebAPI.Models.StockPaymentVoucher stockout)
        {

            _logger.LogInformation("Creating new Create Stock Purchase Message record");
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var stockpurchaseMaster = await _stockPaymentVoucher.CreateStockPaymentVoucher(stockout);

                if (stockpurchaseMaster == null)
                {
                    _logger.LogWarning("Failed to create record");
                    return BadRequest("Failed to create record");
                }

                _logger.LogInformation("Record created successfully with ID: {id}", stockpurchaseMaster.spvId);

                return CreatedAtAction(nameof(GetStockPaymentVoucherById), new { id = stockpurchaseMaster.spvId }, stockpurchaseMaster);

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
        public async Task<IActionResult> UpdateStockPaymentVoucher(int id, TrackingWebAPI.Models.StockPaymentVoucher stockout)
        {
            _logger.LogInformation("Updating record for ID: {id}", id);
            if (id != stockout.spvId)
            {
                _logger.LogWarning("ID mismatch: URL ID = {id}, ID = {spvId}", id, stockout.spvId);
                return BadRequest("ID mismatch");
            }
            try
            {

                var stockPurchaseMaster = await _stockPaymentVoucher.GetStockPaymentVoucherById(id);
                if (stockPurchaseMaster == null)
                {
                    _logger.LogWarning("Record not found for update, ID: {id}", id);
                    return NotFound();
                }
                _logger.LogInformation("Record updated successfully for ID: {id}", id);

                var result = await _stockPaymentVoucher.UpdateStockPaymentVoucher(id, stockout);
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
        public async Task<IActionResult> DeleteStockPaymentVoucher(int id)
        {

            _logger.LogInformation("Deleting record for ID: {id}", id);
            try
            {
                var existingstockpurchase = await _stockPaymentVoucher.GetStockPaymentVoucherById(id);
                if (existingstockpurchase == null)
                {
                    _logger.LogWarning("Record not found for deletion, ID: {id}", id);
                    return NotFound();
                }
                _logger.LogInformation("Record deleted successfully for ID: {id}", id);

                await _stockPaymentVoucher.DeleteStockPaymentVoucher(id);
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
