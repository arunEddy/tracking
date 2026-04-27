using TrackingWebAPI.Models;

namespace TrackingWebAPI.Interfaces
{
    public interface IStockIssueToEmployee
    {
        Task<IEnumerable<StockIssueToEmployeeMaster>> GetAllStockIssueToEmployee();
        Task<StockIssueToEmployeeMaster> GetStockIssueToEmployeeById(int id);
        Task<StockIssueToEmployeeMaster> CreateStockIssueToEmployee(StockIssueToEmployeeMaster SITEmployee);
        Task<StockIssueToEmployeeMaster> UpdateStockIssueToEmployee(int id, StockIssueToEmployeeMaster SITEmployee);
        Task<StockIssueToEmployeeMaster> DeleteStockIssueToEmployee(int id);
    }
}
