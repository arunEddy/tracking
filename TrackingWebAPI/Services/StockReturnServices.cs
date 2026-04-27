using DALCLASS.DBContact;
using Microsoft.EntityFrameworkCore;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;

namespace TrackingWebAPI.Services
{
    public class StockReturnServices:IStockReturn
    {
        private readonly ApplicationDbContext _context;

        public StockReturnServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.StockReturn>> GetAllStockReturn()
        {
            return await _context.stockReturn
                           .Where(x => x.EndDate == null || x.EndDate == "")
                            .ToListAsync();
        }

        public async Task<Models.StockReturn> GetStockReturnById(int id)
        {
            return await _context.stockReturn
           .Where(x => x.srid == id && x.EndDate == null || x.EndDate == "")
           .FirstOrDefaultAsync();
        }

        public async Task<Models.StockReturn> CreateStockReturn(Models.StockReturn StockReturn)
        {
            await _context.stockReturn.AddAsync(StockReturn);
            await _context.SaveChangesAsync();
            return StockReturn;
        }

        public async Task<StockReturn> UpdateStockReturn(int id, Models.StockReturn StockReturn)
        {
            var existingStockReturn = await _context.stockReturn.FindAsync(id);
            if (existingStockReturn != null)
            {
                existingStockReturn.FromOffice = StockReturn.FromOffice;
                existingStockReturn.ShipperName = StockReturn.ShipperName;
                existingStockReturn.ToOffice = StockReturn.ToOffice;
                existingStockReturn.ReturnDate = StockReturn.ReturnDate;
                existingStockReturn.srItemName = StockReturn.srItemName;
                existingStockReturn.Quantity = StockReturn.Quantity;
                existingStockReturn.StartNo = StockReturn.StartNo;
                existingStockReturn.EndNo = StockReturn.EndNo;
                existingStockReturn.BalQuantity = StockReturn.BalQuantity;
                existingStockReturn.mdfby = StockReturn.mdfby;
                existingStockReturn.mdfon = StockReturn.mdfon;
                existingStockReturn.IsActive = StockReturn.IsActive;
                await _context.SaveChangesAsync();
            }
            return existingStockReturn;
        }

        public async Task<StockReturn> DeleteStockReturn(int id)
        {
            var StockReturn = await _context.stockReturn.FindAsync(id);
            if (StockReturn != null)
            {
                StockReturn.EndDate = DateTime.Now.ToString();
                await _context.SaveChangesAsync();
            }
            return StockReturn;
        }
    }
}
