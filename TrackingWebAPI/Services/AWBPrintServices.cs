using DALCLASS.DBContact;
using Microsoft.EntityFrameworkCore;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;

namespace TrackingWebAPI.Services
{
    public class AWBPrintServices:IAWBPrint
    {
        private readonly ApplicationDbContext _context;

        public AWBPrintServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.AWBPrint>> GetAllAWBPrint()
        {
            return await _context.aWBPrint
                           .Where(x => x.EndDate == null || x.EndDate == "")
                            .ToListAsync();
        }

        public async Task<Models.AWBPrint> GetAWBPrintById(int id)
        {
            return await _context.aWBPrint
           .Where(x => x.apid == id && x.EndDate == null || x.EndDate == "")
           .FirstOrDefaultAsync();
        }

        public async Task<Models.AWBPrint> CreateAWBPrint(Models.AWBPrint Awb)
        {
            await _context.aWBPrint.AddAsync(Awb);
            await _context.SaveChangesAsync();
            return Awb;
        }

        public async Task<AWBPrint> UpdateAWBPrint(int id, Models.AWBPrint Awb)
        {
            var existingAwb = await _context.aWBPrint.FindAsync(id);
            if (existingAwb != null)
            {
                existingAwb.OfficeName = Awb.OfficeName;
                existingAwb.ShipperName = Awb.ShipperName;
                existingAwb.apFormat = Awb.apFormat;
                existingAwb.apOption = Awb.apOption;
                existingAwb.BookingDate = Awb.BookingDate;
                existingAwb.Phone = Awb.Phone;
                existingAwb.Remarks = Awb.Remarks;
                existingAwb.DelDate = Awb.DelDate;
                existingAwb.Deltime = Awb.Deltime;
                existingAwb.DeliveryRemarks = Awb.DeliveryRemarks;
                existingAwb.Status = Awb.Status;
                existingAwb.mdfby = Awb.mdfby;
                existingAwb.mdfon = Awb.mdfon;
                existingAwb.IsActive = Awb.IsActive;
                await _context.SaveChangesAsync();
            }
            return existingAwb;
        }

        public async Task<AWBPrint> DeleteAWBPrint(int id)
        {
            var Awbprint = await _context.aWBPrint.FindAsync(id);
            if (Awbprint != null)
            {
                Awbprint.EndDate = DateTime.Now.ToString();
                await _context.SaveChangesAsync();
            }
            return Awbprint;
        }
    }
}
