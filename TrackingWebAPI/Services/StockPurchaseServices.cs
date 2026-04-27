using DALCLASS.DBContact;
using Microsoft.EntityFrameworkCore;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;

namespace TrackingWebAPI.Services
{
    public class StockPurchaseServices:ISTOCKPURCHASE
    {
        private readonly ApplicationDbContext _context;

        public StockPurchaseServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.StockPurchaseMaster>> GetAllStockPurchase()
        {
            return await _context.stockPurchaseMaster
                           .Where(x => x.EndDate == null || x.EndDate == "")
                            .ToListAsync();
        }

        public async Task<Models.StockPurchaseMaster> GetStockPurchaseById(int id)
        {
            return await _context.stockPurchaseMaster
           .Where(x => x.stpId == id && x.EndDate == null || x.EndDate == "")
           .FirstOrDefaultAsync();
        }

        public async Task<Models.StockPurchaseMaster> CreateStockPurchase(Models.StockPurchaseMaster customerReBook)
        {
            await _context.stockPurchaseMaster.AddAsync(customerReBook);
            await _context.SaveChangesAsync();
            return customerReBook;
        }

        public async Task<StockPurchaseMaster> UpdateStockPurchase(int id, Models.StockPurchaseMaster customerReBook)
        {
            var existingStockPurchase = await _context.stockPurchaseMaster.FindAsync(id);
            if (existingStockPurchase != null)
            {
                existingStockPurchase.OfficeName = customerReBook.OfficeName;
                existingStockPurchase.VendorName = customerReBook.VendorName;
                existingStockPurchase.PurchaseDate = customerReBook.PurchaseDate;
                existingStockPurchase.ItemName = customerReBook.ItemName;
                existingStockPurchase.Quantity = customerReBook.Quantity;
                existingStockPurchase.PurchaseRate = customerReBook.PurchaseRate;
                existingStockPurchase.VendorRate = customerReBook.VendorRate;
                existingStockPurchase.StartNo = customerReBook.StartNo;
                existingStockPurchase.EndNo = customerReBook.EndNo;
                existingStockPurchase.BookRequired = customerReBook.BookRequired;
                existingStockPurchase.mfdby = customerReBook.mfdby;
                existingStockPurchase.IsActive = customerReBook.IsActive;
                await _context.SaveChangesAsync();
            }
            return existingStockPurchase;
        }

        public async Task<StockPurchaseMaster> DeleteStockPurchase(int id)
        {
            var customerReBook = await _context.stockPurchaseMaster.FindAsync(id);
            if (customerReBook != null)
            {
                customerReBook.EndDate = DateTime.Now.ToString();
                await _context.SaveChangesAsync();
            }
            return customerReBook;
        }
    }
}
