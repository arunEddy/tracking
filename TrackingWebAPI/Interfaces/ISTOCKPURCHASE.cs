using TrackingWebAPI.Models;

namespace TrackingWebAPI.Interfaces
{
    public interface ISTOCKPURCHASE
    {
        Task<IEnumerable<StockPurchaseMaster>> GetAllStockPurchase();
        Task<StockPurchaseMaster> GetStockPurchaseById(int id);
        Task<StockPurchaseMaster> CreateStockPurchase(StockPurchaseMaster StockPurchase);
        Task<StockPurchaseMaster> UpdateStockPurchase(int id, StockPurchaseMaster StockPurchase);
        Task<StockPurchaseMaster> DeleteStockPurchase(int id);
    }
}
