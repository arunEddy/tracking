using DALCLASS.DBContact;
using Microsoft.EntityFrameworkCore;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;

namespace TrackingWebAPI.Services
{
    public class UrgentDeliveryAlertEntryServices:IUrgentDeliveryAlertEntry
    {
        private readonly ApplicationDbContext _context;

        public UrgentDeliveryAlertEntryServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.UrgentDeliveryAlertEntry>> GetAllUrgentDeliveryAlertEntry()
        {
            return await _context.urgentDeliveryAlertEntry
                           .Where(x => x.EndDate == null || x.EndDate == "")
                            .ToListAsync();
        }

        public async Task<Models.UrgentDeliveryAlertEntry> GetUrgentDeliveryAlertEntryById(int id)
        {
            return await _context.urgentDeliveryAlertEntry
           .Where(x => x.udaId == id && x.EndDate == null || x.EndDate == "")
           .FirstOrDefaultAsync();
        }

        public async Task<Models.UrgentDeliveryAlertEntry> CreateUrgentDeliveryAlertEntry(Models.UrgentDeliveryAlertEntry Awb)
        {
            await _context.urgentDeliveryAlertEntry.AddAsync(Awb);
            await _context.SaveChangesAsync();
            return Awb;
        }

        public async Task<UrgentDeliveryAlertEntry> UpdateUrgentDeliveryAlertEntry(int id, Models.UrgentDeliveryAlertEntry urgentDeliveryAlertEntry)
        {
            var existingurgentDeliveryAlertEntry = await _context.urgentDeliveryAlertEntry.FindAsync(id);
            if (existingurgentDeliveryAlertEntry != null)
            {
                existingurgentDeliveryAlertEntry.AwbNo = urgentDeliveryAlertEntry.AwbNo;
                existingurgentDeliveryAlertEntry.DestinationOffice = urgentDeliveryAlertEntry.DestinationOffice;
                existingurgentDeliveryAlertEntry.ShipperName = urgentDeliveryAlertEntry.ShipperName;
                existingurgentDeliveryAlertEntry.ConsigneeName = urgentDeliveryAlertEntry.ConsigneeName;
                existingurgentDeliveryAlertEntry.Address = urgentDeliveryAlertEntry.Address;
                existingurgentDeliveryAlertEntry.Phone = urgentDeliveryAlertEntry.Phone;
                existingurgentDeliveryAlertEntry.Remarks = urgentDeliveryAlertEntry.Remarks;
                existingurgentDeliveryAlertEntry.DelDate = urgentDeliveryAlertEntry.DelDate;
                existingurgentDeliveryAlertEntry.Deltime = urgentDeliveryAlertEntry.Deltime;
                existingurgentDeliveryAlertEntry.DeliveryRemarks = urgentDeliveryAlertEntry.DeliveryRemarks;
                existingurgentDeliveryAlertEntry.Status = urgentDeliveryAlertEntry.Status;
                existingurgentDeliveryAlertEntry.mdfby = urgentDeliveryAlertEntry.mdfby;
                existingurgentDeliveryAlertEntry.mdfon = urgentDeliveryAlertEntry.mdfon;
                existingurgentDeliveryAlertEntry.IsActive = urgentDeliveryAlertEntry.IsActive;
                await _context.SaveChangesAsync();
            }
            return existingurgentDeliveryAlertEntry;
        }

        public async Task<UrgentDeliveryAlertEntry> DeleteUrgentDeliveryAlertEntry(int id)
        {
            var urgentDeliveryAlertEntry = await _context.urgentDeliveryAlertEntry.FindAsync(id);
            if (urgentDeliveryAlertEntry != null)
            {
                urgentDeliveryAlertEntry.EndDate = DateTime.Now.ToString();
                await _context.SaveChangesAsync();
            }
            return urgentDeliveryAlertEntry;
        }
    }
}
