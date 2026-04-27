using TrackingWebAPI.Models;

namespace TrackingWebAPI.Interfaces
{
    public interface IStockIssue
    {
        Task<IEnumerable<StockIssue>> GetAllStockIssue();
        Task<StockIssue> GetStockIssueById(int id);
        Task<StockIssue> CreateStockIssue(StockIssue StockIssue);
        Task<StockIssue> UpdateStockIssue(int id, StockIssue StockIssue);
        Task<StockIssue> DeleteStockIssue(int id);
    }
}
