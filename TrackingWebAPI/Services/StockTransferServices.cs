using DALCLASS.DBContact;
using Microsoft.EntityFrameworkCore;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;

namespace TrackingWebAPI.Services
{
    public class StockTransferServices: IStockTransfer
    {
        private readonly ApplicationDbContext _context;

        public StockTransferServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.StockTransfer>> GetAllStockTransfer()
        {
            return await _context.stockTransfer
                           .Where(x => x.EndDate == null || x.EndDate == "")
                            .ToListAsync();
        }

        public async Task<Models.StockTransfer> GetStockTransferById(int id)
        {
            return await _context.stockTransfer
           .Where(x => x.stid == id && x.EndDate == null || x.EndDate == "")
           .FirstOrDefaultAsync();
        }

        public async Task<Models.StockTransfer> CreateStockTransfer(Models.StockTransfer StockTransfer)
        {
            await _context.stockTransfer.AddAsync(StockTransfer);
            await _context.SaveChangesAsync();
            return StockTransfer;
        }

        public async Task<StockTransfer> UpdateStockTransfer(int id, Models.StockTransfer StockTransfer)
        {
            var existingStockTransfer = await _context.stockTransfer.FindAsync(id);
            if (existingStockTransfer != null)
            {
                existingStockTransfer.TransferDate = StockTransfer.TransferDate;
                existingStockTransfer.FromOffice = StockTransfer.FromOffice;
                existingStockTransfer.ToOffice = StockTransfer.ToOffice;
                existingStockTransfer.ItemName = StockTransfer.ItemName;
                existingStockTransfer.Quantity = StockTransfer.Quantity;
                existingStockTransfer.StartNo = StockTransfer.StartNo;
                existingStockTransfer.EndNo = StockTransfer.EndNo;
                existingStockTransfer.BalQuantity = StockTransfer.BalQuantity;
                existingStockTransfer.mfdby = StockTransfer.mfdby;
                existingStockTransfer.mfdon = StockTransfer.mfdon;
                existingStockTransfer.IsActive = StockTransfer.IsActive;
                await _context.SaveChangesAsync();
            }
            return existingStockTransfer;
        }

        public async Task<StockTransfer> DeleteStockTransfer(int id)
        {
            var StockTransfer = await _context.stockTransfer.FindAsync(id);
            if (StockTransfer != null)
            {
                StockTransfer.EndDate = DateTime.Now.ToString();
                await _context.SaveChangesAsync();
            }
            return StockTransfer;
        }
    }
}
