using TrackingWebAPI.Models;

namespace TrackingWebAPI.Interfaces
{
    public interface IStockTransfer
    {
        Task<IEnumerable<StockTransfer>> GetAllStockTransfer();
        Task<StockTransfer> GetStockTransferById(int id);
        Task<StockTransfer> CreateStockTransfer(StockTransfer StockTransfer);
        Task<StockTransfer> UpdateStockTransfer(int id, StockTransfer StockTransfer);
        Task<StockTransfer> DeleteStockTransfer(int id);
    }
}
