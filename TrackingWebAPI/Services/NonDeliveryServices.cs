using DALCLASS.DBContact;
using Microsoft.EntityFrameworkCore;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;

namespace TrackingWebAPI.Services
{
    public class NonDeliveryServices:INonDelivery
    {
        private readonly ApplicationDbContext _context;

        public NonDeliveryServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.NonDelivery>> GetNonDeliveries()
        {
            return await _context.nonDelivery
                           .Where(x => x.EndDate == null || x.EndDate == "")
                            .ToListAsync();
        }

        public async Task<Models.NonDelivery> GetNonDeliveryById(int id)
        {
            return await _context.nonDelivery
           .Where(x => x.ndId == id && x.EndDate == null || x.EndDate == "")
           .FirstOrDefaultAsync();
        }

        public async Task<Models.NonDelivery> CreateNonDelivery(Models.NonDelivery nonDelivery)
        {
            await _context.nonDelivery.AddAsync(nonDelivery);
            await _context.SaveChangesAsync();
            return nonDelivery;
        }

        public async Task<NonDelivery> UpdateNonDelivery(int id, Models.NonDelivery nonDelivery)
        {
            var existingdRS = await _context.nonDelivery.FindAsync(id);
            if (existingdRS != null)
            {
                existingdRS.DestinationOffice = nonDelivery.DestinationOffice;
                existingdRS.AwbNo = nonDelivery.AwbNo;
                existingdRS.NDRDate = nonDelivery.NDRDate;
                existingdRS.NDRReason = nonDelivery.NDRReason;
                existingdRS.Remarks = nonDelivery.Remarks;
                existingdRS.ShipperName = nonDelivery.ShipperName;
                existingdRS.TotalNDR = nonDelivery.TotalNDR;
                existingdRS.PhysicalAttempt = nonDelivery.PhysicalAttempt;
                existingdRS.MaxAttempt = nonDelivery.MaxAttempt;
                existingdRS.mfdby = nonDelivery.mfdby;
                existingdRS.mfdon = nonDelivery.mfdon;
                existingdRS.IsActive = nonDelivery.IsActive;
                await _context.SaveChangesAsync();
            }
            return existingdRS;
        }

        public async Task<NonDelivery> DeleteNonDelivery(int id)
        {
            var nonDelivery = await _context.nonDelivery.FindAsync(id);
            if (nonDelivery != null)
            {
                nonDelivery.EndDate = DateTime.Now.ToString();
                await _context.SaveChangesAsync();
            }
            return nonDelivery;
        }
    }
}
