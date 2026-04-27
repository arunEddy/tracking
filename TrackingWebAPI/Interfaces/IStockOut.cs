using TrackingWebAPI.Models;

namespace TrackingWebAPI.Interfaces
{
    public interface IStockOut
    {
        Task<IEnumerable<StockOut>> GetAllStockOut();
        Task<StockOut> GetStockOutById(int id);
        Task<StockOut> CreateStockOut(StockOut StockOut);
        Task<StockOut> UpdateStockOut(int id, StockOut StockOut);
        Task<StockOut> DeleteStockOut(int id);
    }
}
