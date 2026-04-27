using DALCLASS.DBContact;
using Microsoft.EntityFrameworkCore;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;

namespace TrackingWebAPI.Services
{
    public class CancelAWBServices:ICancelAWB
    {
        private readonly ApplicationDbContext _context;

        public CancelAWBServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.CancelAWB>> GetAllCancelAWB()
        {
            return await _context.cancelAWB
                           .Where(x => x.EndDate == null || x.EndDate == "")
                            .ToListAsync();
        }

        public async Task<Models.CancelAWB> GetCancelAWBById(int id)
        {
            return await _context.cancelAWB
           .Where(x => x.caid == id && x.EndDate == null || x.EndDate == "")
           .FirstOrDefaultAsync();
        }

        public async Task<Models.CancelAWB> CreateCancelAWB(Models.CancelAWB CancelAWB)
        {
            await _context.cancelAWB.AddAsync(CancelAWB);
            await _context.SaveChangesAsync();
            return CancelAWB;
        }

        public async Task<CancelAWB> UpdateCancelAWB(int id, Models.CancelAWB CancelAWB)
        {
            var existingCancelAWB = await _context.cancelAWB.FindAsync(id);
            if (existingCancelAWB != null)
            {
                existingCancelAWB.AWBNumber = CancelAWB.AWBNumber;
                existingCancelAWB.Reason = CancelAWB.Reason;
                existingCancelAWB.mfdby = CancelAWB.mfdby;
                existingCancelAWB.mfdon = CancelAWB.mfdon;
                existingCancelAWB.IsActive = CancelAWB.IsActive;
                await _context.SaveChangesAsync();
            }
            return existingCancelAWB;
        }

        public async Task<CancelAWB> DeleteCancelAWB(int id)
        {
            var CancelAWB = await _context.cancelAWB.FindAsync(id);
            if (CancelAWB != null)
            {
                CancelAWB.EndDate = DateTime.Now.ToString();
                await _context.SaveChangesAsync();
            }
            return CancelAWB;
        }
    }
}
