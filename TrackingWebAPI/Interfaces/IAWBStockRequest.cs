using TrackingWebAPI.Models;

namespace TrackingWebAPI.Interfaces
{
    public interface IAWBStockRequest
    {
        Task<IEnumerable<AWBStockRequest>> GetAllStockRequest();
        Task<AWBStockRequest> GetStockRequestById(int id);
        Task<AWBStockRequest> CreateStockRequest(AWBStockRequest StockRequest);
        Task<AWBStockRequest> UpdateStockRequest(int id, AWBStockRequest StockRequest);
        Task<AWBStockRequest> DeleteStockRequest(int id);
    }
}
