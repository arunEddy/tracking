using DALCLASS.DBContact;
using Microsoft.EntityFrameworkCore;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;

namespace TrackingWebAPI.Services
{
    public class AWBStockRequestServices:IAWBStockRequest
    {
        private readonly ApplicationDbContext _context;

        public AWBStockRequestServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.AWBStockRequest>> GetAllStockRequest()
        {
            return await _context.aWBStockRequest
                           .Where(x => x.EndDate == null || x.EndDate == "")
                            .ToListAsync();
        }

        public async Task<Models.AWBStockRequest> GetStockRequestById(int id)
        {
            return await _context.aWBStockRequest
           .Where(x => x.Asrid == id && x.EndDate == null || x.EndDate == "")
           .FirstOrDefaultAsync();
        }

        public async Task<Models.AWBStockRequest> CreateStockRequest(Models.AWBStockRequest aWBStockRequest)
        {
            await _context.aWBStockRequest.AddAsync(aWBStockRequest);
            await _context.SaveChangesAsync();
            return aWBStockRequest;
        }

        public async Task<AWBStockRequest> UpdateStockRequest(int id, Models.AWBStockRequest aWBStockRequest)
        {
            var existingaWBStockRequest = await _context.aWBStockRequest.FindAsync(id);
            if (existingaWBStockRequest != null)
            {
                existingaWBStockRequest.AOfficeName = aWBStockRequest.AOfficeName;
                existingaWBStockRequest.ARequestDate = aWBStockRequest.ARequestDate;
                existingaWBStockRequest.AItemName = aWBStockRequest.AItemName;
                existingaWBStockRequest.Quantity = aWBStockRequest.Quantity;
                existingaWBStockRequest.ExpectedDeliveryDate = aWBStockRequest.ExpectedDeliveryDate;
                existingaWBStockRequest.Remarks = aWBStockRequest.Remarks;
                existingaWBStockRequest.mdfby = aWBStockRequest.mdfby;
                existingaWBStockRequest.mdfon = aWBStockRequest.mdfon;
                existingaWBStockRequest.IsActive = aWBStockRequest.IsActive;
                await _context.SaveChangesAsync();
            }
            return existingaWBStockRequest;
        }

        public async Task<AWBStockRequest> DeleteStockRequest(int id)
        {
            var customerDataUpdateAWB = await _context.aWBStockRequest.FindAsync(id);
            if (customerDataUpdateAWB != null)
            {
                customerDataUpdateAWB.EndDate = DateTime.Now.ToString();
                await _context.SaveChangesAsync();
            }
            return customerDataUpdateAWB;
        }
    }
}
