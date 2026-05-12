using DALCLASS.DBContact;
using Microsoft.EntityFrameworkCore;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;

namespace TrackingWebAPI.Services
{
    public class ReturnToCustomerManifestingServices : IReturnToCustomerManifesting
    {
        private readonly ApplicationDbContext _context;

        public ReturnToCustomerManifestingServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.ReturnToCustomerManifesting>> GetAllReturnToCustomerManifesting()
        {
            return await _context.returnToCustomerManifesting
                           .Where(x => x.EndDate == null || x.EndDate == "")
                            .ToListAsync();
        }

        public async Task<Models.ReturnToCustomerManifesting> GetReturnToCustomerManifestingById(int id)
        {
            return await _context.returnToCustomerManifesting
           .Where(x => x.rtcmid == id && x.EndDate == null || x.EndDate == "")
           .FirstOrDefaultAsync();
        }

        public async Task<Models.ReturnToCustomerManifesting> CreateReturnToCustomerManifesting(Models.ReturnToCustomerManifesting ReturnToCustomerManifesting)
        {
            await _context.returnToCustomerManifesting.AddAsync(ReturnToCustomerManifesting);
            await _context.SaveChangesAsync();
            return ReturnToCustomerManifesting;
        }

        public async Task<ReturnToCustomerManifesting> UpdateReturnToCustomerManifesting(int id, Models.ReturnToCustomerManifesting ReturnToCustomerManifesting)
        {
            var existingdRS = await _context.returnToCustomerManifesting.FindAsync(id);
            if (existingdRS != null)
            {
                existingdRS.ShipperName = ReturnToCustomerManifesting.ShipperName;
                existingdRS.DeliveryBoy = ReturnToCustomerManifesting.DeliveryBoy;
                existingdRS.ReturnDate = ReturnToCustomerManifesting.ReturnDate;
                existingdRS.BagNumber = ReturnToCustomerManifesting.BagNumber;
                existingdRS.Remarks = ReturnToCustomerManifesting.Remarks;
                existingdRS.AWBNumber = ReturnToCustomerManifesting.AWBNumber;
                existingdRS.Reason = ReturnToCustomerManifesting.Reason;
                existingdRS.ManifestNo = ReturnToCustomerManifesting.ManifestNo;
                existingdRS.mfdby = ReturnToCustomerManifesting.mfdby;
                existingdRS.mfdon = ReturnToCustomerManifesting.mfdon;
                existingdRS.IsActive = ReturnToCustomerManifesting.IsActive;
                await _context.SaveChangesAsync();
            }
            return existingdRS;
        }

        public async Task<ReturnToCustomerManifesting> DeleteReturnToCustomerManifesting(int id)
        {
            var ReturnToCustomerManifesting = await _context.returnToCustomerManifesting.FindAsync(id);
            if (ReturnToCustomerManifesting != null)
            {
                ReturnToCustomerManifesting.EndDate = DateTime.Now.ToString();
                await _context.SaveChangesAsync();
            }
            return ReturnToCustomerManifesting;
        }
    }
}

