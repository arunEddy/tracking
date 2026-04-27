using TrackingWebAPI.Models;

namespace TrackingWebAPI.Interfaces
{
    public interface IStockPaymentVoucher
    {
        Task<IEnumerable<StockPaymentVoucher>> GetAllStockPaymentVoucher();
        Task<StockPaymentVoucher> GetStockPaymentVoucherById(int id);
        Task<StockPaymentVoucher> CreateStockPaymentVoucher(StockPaymentVoucher StockPaymentVoucher);
        Task<StockPaymentVoucher> UpdateStockPaymentVoucher(int id, StockPaymentVoucher StockPaymentVoucher);
        Task<StockPaymentVoucher> DeleteStockPaymentVoucher(int id);
    }
}
