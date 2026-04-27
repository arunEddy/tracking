using DALCLASS.DBContact;
using Microsoft.EntityFrameworkCore;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;

namespace TrackingWebAPI.Services
{
    public class StockIssueServices:IStockIssue
    {
        private readonly ApplicationDbContext _context;

        public StockIssueServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.StockIssue>> GetAllStockIssue()
        {
            return await _context.stockIssue
                           .Where(x => x.EndDate == null || x.EndDate == "")
                            .ToListAsync();
        }

        public async Task<Models.StockIssue> GetStockIssueById(int id)
        {
            return await _context.stockIssue
           .Where(x => x.siId == id && x.EndDate == null || x.EndDate == "")
           .FirstOrDefaultAsync();
        }

        public async Task<Models.StockIssue> CreateStockIssue(Models.StockIssue stockIssue)
        {
            await _context.stockIssue.AddAsync(stockIssue);
            await _context.SaveChangesAsync();
            return stockIssue;
        }

        public async Task<StockIssue> UpdateStockIssue(int id, Models.StockIssue stockIssue)
        {
            var existingstockIssue = await _context.stockIssue.FindAsync(id);
            if (existingstockIssue != null)
            {
                existingstockIssue.SiOfficeName = stockIssue.SiOfficeName;
                existingstockIssue.IssueDate = stockIssue.IssueDate;
                existingstockIssue.CustomerName = stockIssue.CustomerName;
                existingstockIssue.SiItemName = stockIssue.SiItemName;
                existingstockIssue.VoucherNo = stockIssue.VoucherNo;
                existingstockIssue.Quantity = stockIssue.Quantity;
                existingstockIssue.Serialized = stockIssue.Serialized;
                existingstockIssue.AWB = stockIssue.AWB;
                existingstockIssue.StartNo = stockIssue.StartNo;
                existingstockIssue.EndNo = stockIssue.EndNo;
                existingstockIssue.BalQuantity = stockIssue.BalQuantity;
                existingstockIssue.ItemPrice = stockIssue.ItemPrice;
                existingstockIssue.mdfby = stockIssue.mdfby;
                existingstockIssue.mdfon = stockIssue.mdfon;
                existingstockIssue.IsActive = stockIssue.IsActive;
                existingstockIssue.Quantity = stockIssue.Quantity;
                await _context.SaveChangesAsync();
            }
            return existingstockIssue;
        }

        public async Task<StockIssue> DeleteStockIssue(int id)
        {
            var StockIssue = await _context.stockIssue.FindAsync(id);
            if (StockIssue != null)
            {
                StockIssue.EndDate = DateTime.Now.ToString();
                await _context.SaveChangesAsync();
            }
            return StockIssue;
        }
    }
}
