using TrackingWebAPI.Models;

namespace TrackingWebAPI.Interfaces
{
    public interface IStockReturn
    {
        Task<IEnumerable<StockReturn>> GetAllStockReturn();
        Task<StockReturn> GetStockReturnById(int id);
        Task<StockReturn> CreateStockReturn(StockReturn StockReturn);
        Task<StockReturn> UpdateStockReturn(int id, StockReturn StockReturn);
        Task<StockReturn> DeleteStockReturn(int id);
    }
}
