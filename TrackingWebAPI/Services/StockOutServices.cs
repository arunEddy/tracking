using DALCLASS.DBContact;
using Microsoft.EntityFrameworkCore;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;

namespace TrackingWebAPI.Services
{
    public class StockOutServices:IStockOut
    {
        private readonly ApplicationDbContext _context;

        public StockOutServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.StockOut>> GetAllStockOut()
        {
            return await _context.stockOut
                           .Where(x => x.EndDate == null || x.EndDate == "")
                            .ToListAsync();
        }

        public async Task<Models.StockOut> GetStockOutById(int id)
        {
            return await _context.stockOut
           .Where(x => x.soid == id && x.EndDate == null || x.EndDate == "")
           .FirstOrDefaultAsync();
        }

        public async Task<Models.StockOut> CreateStockOut(Models.StockOut stockOut)
        {
            await _context.stockOut.AddAsync(stockOut);
            await _context.SaveChangesAsync();
            return stockOut;
        }

        public async Task<StockOut> UpdateStockOut(int id, Models.StockOut stockOut)
        {
            var existingstockOut = await _context.stockOut.FindAsync(id);
            if (existingstockOut != null)
            {
                existingstockOut.SOfficeName = stockOut.SOfficeName;
                existingstockOut.SOutDate = stockOut.SOutDate;
                existingstockOut.SItemName = stockOut.SItemName;
                existingstockOut.SQuantity = stockOut.SQuantity;
                existingstockOut.Serialized = stockOut.Serialized;
                existingstockOut.AWB = stockOut.AWB;
                existingstockOut.StartNo = stockOut.StartNo;
                existingstockOut.EndNo = stockOut.EndNo;
                existingstockOut.BalQuantity = stockOut.BalQuantity;
                existingstockOut.mdfby = stockOut.mdfby;
                existingstockOut.mdfon = stockOut.mdfon;
                existingstockOut.IsActive = stockOut.IsActive;
                await _context.SaveChangesAsync();
            }
            return existingstockOut;
        }

        public async Task<StockOut> DeleteStockOut(int id)
        {
            var stockOut = await _context.stockOut.FindAsync(id);
            if (stockOut != null)
            {
                stockOut.EndDate = DateTime.Now.ToString();
                await _context.SaveChangesAsync();
            }
            return stockOut;
        }
    }
}
