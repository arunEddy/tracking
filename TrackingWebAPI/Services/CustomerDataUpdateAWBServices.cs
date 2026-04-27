using DALCLASS.DBContact;
using Microsoft.EntityFrameworkCore;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;

namespace TrackingWebAPI.Services
{
    public class CustomerDataUpdateAWBServices:ICustomerDataUpdateAWB
    {
        private readonly ApplicationDbContext _context;

        public CustomerDataUpdateAWBServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.CustomerDataUpdateAWB>> GetAllCustomerDataUpdateAWB()
        {
            return await _context.customerDataUpdateAWB
                           .Where(x => x.EndDate == null || x.EndDate == "")
                            .ToListAsync();
        }

        public async Task<Models.CustomerDataUpdateAWB> GetCustomerDataUpdateAWBById(int id)
        {
            return await _context.customerDataUpdateAWB
           .Where(x => x.cduid == id && x.EndDate == null || x.EndDate == "")
           .FirstOrDefaultAsync();
        }

        public async Task<Models.CustomerDataUpdateAWB> CreateCustomerDataUpdateAWB(Models.CustomerDataUpdateAWB customerDataUpdateAWB)
        {
            await _context.customerDataUpdateAWB.AddAsync(customerDataUpdateAWB);
            await _context.SaveChangesAsync();
            return customerDataUpdateAWB;
        }

        public async Task<CustomerDataUpdateAWB> UpdateCustomerDataUpdateAWB(int id, Models.CustomerDataUpdateAWB customerDataUpdateAWB)
        {
            var existingcustomerDataUpdateAWB = await _context.customerDataUpdateAWB.FindAsync(id);
            if (existingcustomerDataUpdateAWB != null)
            {
                existingcustomerDataUpdateAWB.OfficeName = customerDataUpdateAWB.OfficeName;
                existingcustomerDataUpdateAWB.PickupCity = customerDataUpdateAWB.PickupCity;
                existingcustomerDataUpdateAWB.PickupPincode = customerDataUpdateAWB.PickupPincode;
                existingcustomerDataUpdateAWB.ShipperName = customerDataUpdateAWB.ShipperName;
                existingcustomerDataUpdateAWB.BookingDate = customerDataUpdateAWB.BookingDate;
                existingcustomerDataUpdateAWB.ReferenceNo = customerDataUpdateAWB.ReferenceNo;
                existingcustomerDataUpdateAWB.AWBNo = customerDataUpdateAWB.AWBNo;
                existingcustomerDataUpdateAWB.mdfby = customerDataUpdateAWB.mdfby;
                existingcustomerDataUpdateAWB.mdfon = customerDataUpdateAWB.mdfon;
                existingcustomerDataUpdateAWB.IsActive = customerDataUpdateAWB.IsActive;
                await _context.SaveChangesAsync();
            }
            return existingcustomerDataUpdateAWB;
        }

        public async Task<CustomerDataUpdateAWB> DeleteCustomerDataUpdateAWB(int id)
        {
            var customerDataUpdateAWB = await _context.customerDataUpdateAWB.FindAsync(id);
            if (customerDataUpdateAWB != null)
            {
                customerDataUpdateAWB.EndDate = DateTime.Now.ToString();
                await _context.SaveChangesAsync();
            }
            return customerDataUpdateAWB;
        }
    }
}
