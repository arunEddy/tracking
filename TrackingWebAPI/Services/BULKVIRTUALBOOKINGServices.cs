using DALCLASS.DBContact;
using Microsoft.EntityFrameworkCore;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;

namespace TrackingWebAPI.Services
{
    public class BULKVIRTUALBOOKINGServices:IBULKVIRTUALBOOKING
    {
        private readonly ApplicationDbContext _context;

        public BULKVIRTUALBOOKINGServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.BULKVIRTUALBOOKING>> GetAllBULKVIRTUALBOOKING()
        {
            return await _context.bULKVIRTUALBOOKING
                           .Where(x => x.EndDate == null || x.EndDate == "")
                            .ToListAsync();
        }

        public async Task<Models.BULKVIRTUALBOOKING> GetBULKVIRTUALBOOKINGById(int id)
        {
            return await _context.bULKVIRTUALBOOKING
           .Where(x => x.bvbid == id && x.EndDate == null || x.EndDate == "")
           .FirstOrDefaultAsync();
        }

        public async Task<Models.BULKVIRTUALBOOKING> CreateBULKVIRTUALBOOKINGDetails(Models.BULKVIRTUALBOOKING customerDataUpdateAWB)
        {
            await _context.bULKVIRTUALBOOKING.AddAsync(customerDataUpdateAWB);
            await _context.SaveChangesAsync();
            return customerDataUpdateAWB;
        }

        public async Task<BULKVIRTUALBOOKING> UpdateBULKVIRTUALBOOKINGDetails(int id, Models.BULKVIRTUALBOOKING customerDataUpdateAWB)
        {
            var existingcustomerDataUpdateAWB = await _context.bULKVIRTUALBOOKING.FindAsync(id);
            if (existingcustomerDataUpdateAWB != null)
            {
                existingcustomerDataUpdateAWB.RequestNo = customerDataUpdateAWB.RequestNo;
                existingcustomerDataUpdateAWB.BookingOffice = customerDataUpdateAWB.BookingOffice;
                existingcustomerDataUpdateAWB.ProductName = customerDataUpdateAWB.ProductName;
                existingcustomerDataUpdateAWB.Mode = customerDataUpdateAWB.Mode;
                existingcustomerDataUpdateAWB.eWayBillNumber = customerDataUpdateAWB.eWayBillNumber;
                existingcustomerDataUpdateAWB.PickupBookingDate = customerDataUpdateAWB.PickupBookingDate;
                existingcustomerDataUpdateAWB.PickupBookingTime = customerDataUpdateAWB.PickupBookingTime;
                existingcustomerDataUpdateAWB.DefaultPCS = customerDataUpdateAWB.DefaultPCS;
                existingcustomerDataUpdateAWB.DefaultWeight = customerDataUpdateAWB.DefaultWeight;
                existingcustomerDataUpdateAWB.bvbFormat = customerDataUpdateAWB.bvbFormat;
                existingcustomerDataUpdateAWB.bvbformatfile = customerDataUpdateAWB.bvbformatfile;
                existingcustomerDataUpdateAWB.mdfby = customerDataUpdateAWB.mdfby;
                existingcustomerDataUpdateAWB.mdfon = customerDataUpdateAWB.mdfon;
                existingcustomerDataUpdateAWB.IsActive = customerDataUpdateAWB.IsActive;
                await _context.SaveChangesAsync();
            }
            return existingcustomerDataUpdateAWB;
        }

        public async Task<BULKVIRTUALBOOKING> DeleteBULKVIRTUALBOOKINGDetails(int id)
        {
            var customerDataUpdateAWB = await _context.bULKVIRTUALBOOKING.FindAsync(id);
            if (customerDataUpdateAWB != null)
            {
                customerDataUpdateAWB.EndDate = DateTime.Now.ToString();
                await _context.SaveChangesAsync();
            }
            return customerDataUpdateAWB;
        }
    }
}
