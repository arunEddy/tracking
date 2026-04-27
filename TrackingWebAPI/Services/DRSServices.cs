using DALCLASS.DBContact;
using Microsoft.EntityFrameworkCore;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;

namespace TrackingWebAPI.Services
{
    public class DRSServices:IDRS
    {
        private readonly ApplicationDbContext _context;

        public DRSServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.DRS>> GetAllDRS()
        {
            return await _context.dRS
                           .Where(x => x.EndDate == null || x.EndDate == "")
                            .ToListAsync();
        }

        public async Task<Models.DRS> GetDRSById(int id)
        {
            return await _context.dRS
           .Where(x => x.drsid == id && x.EndDate == null || x.EndDate == "")
           .FirstOrDefaultAsync();
        }

        public async Task<Models.DRS> CreateDRS(Models.DRS dRS)
        {
            await _context.dRS.AddAsync(dRS);
            await _context.SaveChangesAsync();
            return dRS;
        }

        public async Task<DRS> UpdateDRS(int id, Models.DRS dRS)
        {
            var existingdRS = await _context.dRS.FindAsync(id);
            if (existingdRS != null)
            {
                existingdRS.DRSMadeFor = dRS.DRSMadeFor;
                existingdRS.DeliveryOffice = dRS.DeliveryOffice;
                existingdRS.ConnectionNumber = dRS.ConnectionNumber;
                existingdRS.DeliveryBoy = dRS.DeliveryBoy;
                existingdRS.VehicleNumber = dRS.VehicleNumber;
                existingdRS.DeliveryDate = dRS.DeliveryDate;
                existingdRS.DRSNo = dRS.DRSNo;
                existingdRS.AllowMobileApplication = dRS.AllowMobileApplication;
                existingdRS.MobileNo = dRS.MobileNo;
                existingdRS.SimNo = dRS.SimNo;
                existingdRS.AwbNo = dRS.AwbNo;
                existingdRS.ConsigneeName = dRS.ConsigneeName;
                existingdRS.mdfby = dRS.mdfby;
                existingdRS.mdfon = dRS.mdfon;
                existingdRS.IsActive = dRS.IsActive;
                await _context.SaveChangesAsync();
            }
            return existingdRS;
        }

        public async Task<DRS> DeleteDRS(int id)
        {
            var DRS = await _context.dRS.FindAsync(id);
            if (DRS != null)
            {
                DRS.EndDate = DateTime.Now.ToString();
                await _context.SaveChangesAsync();
            }
            return DRS;
        }
    }
}
