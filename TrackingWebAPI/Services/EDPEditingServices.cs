using DALCLASS.DBContact;
using Microsoft.EntityFrameworkCore;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;

namespace TrackingWebAPI.Services
{
    public class EDPEditingServices:IEDPEditing
    {
        private readonly ApplicationDbContext _context;

        public EDPEditingServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.EDPEditing>> GetAllEDPEditing()
        {
            return await _context.eDPEditing
                           .Where(x => x.EndDate == null || x.EndDate == "")
                            .ToListAsync();
        }

        public async Task<Models.EDPEditing> GetEDPEditingById(int id)
        {
            return await _context.eDPEditing
           .Where(x => x.edpId == id && x.EndDate == null || x.EndDate == "")
           .FirstOrDefaultAsync();
        }

        public async Task<Models.EDPEditing> CreateEDPEditing(Models.EDPEditing customerDataUpdateAWB)
        {
            await _context.eDPEditing.AddAsync(customerDataUpdateAWB);
            await _context.SaveChangesAsync();
            return customerDataUpdateAWB;
        }

        public async Task<EDPEditing> UpdateEDPEditing(int id, Models.EDPEditing customerDataUpdateAWB)
        {
            var existingcustomerDataUpdateAWB = await _context.eDPEditing.FindAsync(id);
            if (existingcustomerDataUpdateAWB != null)
            {
                existingcustomerDataUpdateAWB.AwbNo = customerDataUpdateAWB.AwbNo;
                existingcustomerDataUpdateAWB.VAWB = customerDataUpdateAWB.VAWB;
                existingcustomerDataUpdateAWB.ShipperName = customerDataUpdateAWB.ShipperName;
                existingcustomerDataUpdateAWB.ConsigneeName = customerDataUpdateAWB.ConsigneeName;
                existingcustomerDataUpdateAWB.ProductName = customerDataUpdateAWB.ProductName;
                existingcustomerDataUpdateAWB.CODAmount = customerDataUpdateAWB.CODAmount;
                existingcustomerDataUpdateAWB.RiskValue = customerDataUpdateAWB.RiskValue;
                existingcustomerDataUpdateAWB.GoodsValue = customerDataUpdateAWB.GoodsValue;
                existingcustomerDataUpdateAWB.Contents = customerDataUpdateAWB.Contents;
                existingcustomerDataUpdateAWB.Remarks = customerDataUpdateAWB.Remarks;
                existingcustomerDataUpdateAWB.mdfby = customerDataUpdateAWB.mdfby;
                existingcustomerDataUpdateAWB.mdfon = customerDataUpdateAWB.mdfon;
                existingcustomerDataUpdateAWB.IsActive = customerDataUpdateAWB.IsActive;
                await _context.SaveChangesAsync();
            }
            return existingcustomerDataUpdateAWB;
        }

        public async Task<EDPEditing> DeleteEDPEditing(int id)
        {
            var customerDataUpdateAWB = await _context.eDPEditing.FindAsync(id);
            if (customerDataUpdateAWB != null)
            {
                customerDataUpdateAWB.EndDate = DateTime.Now.ToString();
                await _context.SaveChangesAsync();
            }
            return customerDataUpdateAWB;
        }
    }
}
