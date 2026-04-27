using DALCLASS.DBContact;
using Microsoft.EntityFrameworkCore;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;

namespace TrackingWebAPI.Services
{
    public class StockIssueToEmployeeServices:IStockIssueToEmployee
    {
        private readonly ApplicationDbContext _context;

        public StockIssueToEmployeeServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.StockIssueToEmployeeMaster>> GetAllStockIssueToEmployee()
        {
            return await _context.StockIssueToEmployee
                           .Where(x => x.EndDate == null || x.EndDate == "")
                            .ToListAsync();
        }

        public async Task<Models.StockIssueToEmployeeMaster> GetStockIssueToEmployeeById(int id)
        {
            return await _context.StockIssueToEmployee
           .Where(x => x.Sitoe == id && x.EndDate == null || x.EndDate == "")
           .FirstOrDefaultAsync();
        }

        public async Task<Models.StockIssueToEmployeeMaster> CreateStockIssueToEmployee(Models.StockIssueToEmployeeMaster Stoem)
        {
            await _context.StockIssueToEmployee.AddAsync(Stoem);
            await _context.SaveChangesAsync();
            return Stoem;
        }

        public async Task<StockIssueToEmployeeMaster> UpdateStockIssueToEmployee(int id, Models.StockIssueToEmployeeMaster Stoem)
        {
            var existingStoem = await _context.StockIssueToEmployee.FindAsync(id);
            if (existingStoem != null)
            {
                existingStoem.OfficeName = Stoem.OfficeName;
                existingStoem.EmployeeName = Stoem.EmployeeName;
                existingStoem.IssueDate = Stoem.IssueDate;
                existingStoem.StartNo = Stoem.StartNo;
                existingStoem.Quantity = Stoem.Quantity;
                existingStoem.EndNo = Stoem.EndNo;
                existingStoem.mfdby = Stoem.mfdby;
                existingStoem.mfdon = DateTime.UtcNow;
                existingStoem.IsActive = Stoem.IsActive;
                await _context.SaveChangesAsync();
            }
            return existingStoem;
        }

        public async Task<StockIssueToEmployeeMaster> DeleteStockIssueToEmployee(int id)
        {
            var StockIssueToEmployee = await _context.StockIssueToEmployee.FindAsync(id);
            if (StockIssueToEmployee != null)
            {
                StockIssueToEmployee.EndDate = DateTime.Now.ToString();
                await _context.SaveChangesAsync();
            }
            return StockIssueToEmployee;
        }
    }
}
