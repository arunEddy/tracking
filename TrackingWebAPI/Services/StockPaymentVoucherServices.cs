using DALCLASS.DBContact;
using Microsoft.EntityFrameworkCore;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;

namespace TrackingWebAPI.Services
{
    public class StockPaymentVoucherServices:IStockPaymentVoucher
    {
        private readonly ApplicationDbContext _context;

        public StockPaymentVoucherServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.StockPaymentVoucher>> GetAllStockPaymentVoucher()
        {
            return await _context.stockPaymentVoucher
                           .Where(x => x.EndDate == null || x.EndDate == "")
                            .ToListAsync();
        }

        public async Task<Models.StockPaymentVoucher> GetStockPaymentVoucherById(int id)
        {
            return await _context.stockPaymentVoucher
           .Where(x => x.spvId == id && x.EndDate == null || x.EndDate == "")
           .FirstOrDefaultAsync();
        }

        public async Task<Models.StockPaymentVoucher> CreateStockPaymentVoucher(Models.StockPaymentVoucher stockPaymentVouchero)
        {
            await _context.stockPaymentVoucher.AddAsync(stockPaymentVouchero);
            await _context.SaveChangesAsync();
            return stockPaymentVouchero;
        }

        public async Task<StockPaymentVoucher> UpdateStockPaymentVoucher(int id, Models.StockPaymentVoucher stockPaymentVouchero)
        {
            var existingstockPaymentVouchero = await _context.stockPaymentVoucher.FindAsync(id);
            if (existingstockPaymentVouchero != null)
            {
                existingstockPaymentVouchero.VoucherNumber = stockPaymentVouchero.VoucherNumber;
                existingstockPaymentVouchero.FranchiseName = stockPaymentVouchero.FranchiseName;
                existingstockPaymentVouchero.TotalAmount = stockPaymentVouchero.TotalAmount;
                existingstockPaymentVouchero.BankName = stockPaymentVouchero.BankName;
                existingstockPaymentVouchero.Remarks = stockPaymentVouchero.Remarks;
                existingstockPaymentVouchero.PaymentDate = stockPaymentVouchero.PaymentDate;
                existingstockPaymentVouchero.AmountPerAWB = stockPaymentVouchero.AmountPerAWB;
                existingstockPaymentVouchero.PaymentMode = stockPaymentVouchero.PaymentMode;
                existingstockPaymentVouchero.ChequeDDNo = stockPaymentVouchero.ChequeDDNo;
                existingstockPaymentVouchero.mdfby = stockPaymentVouchero.mdfby;
                existingstockPaymentVouchero.mdfon = stockPaymentVouchero.mdfon;
                existingstockPaymentVouchero.IsActive = stockPaymentVouchero.IsActive;
                existingstockPaymentVouchero.Quantity = stockPaymentVouchero.Quantity;
                await _context.SaveChangesAsync();
            }
            return existingstockPaymentVouchero;
        }

        public async Task<StockPaymentVoucher> DeleteStockPaymentVoucher(int id)
        {
            var StockPaymentVoucher = await _context.stockPaymentVoucher.FindAsync(id);
            if (StockPaymentVoucher != null)
            {
                StockPaymentVoucher.EndDate = DateTime.Now.ToString();
                await _context.SaveChangesAsync();
            }
            return StockPaymentVoucher;
        }
    }
}
