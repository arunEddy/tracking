using DALCLASS.DBContact;
using Microsoft.EntityFrameworkCore;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;

namespace TrackingWebAPI.Services
{
    public class CustomerWiseReBookDeliveryAddressServices:ICustomerWiseReBookDeliveryAddress
    {
        private readonly ApplicationDbContext _context;

        public CustomerWiseReBookDeliveryAddressServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.CustomerWiseReBookDeliveryAddress>> GetAllCustomerReBook()
        {
            return await _context.customerWiseReBookDeliveryAddress
                           .Where(x => x.EndDate == null || x.EndDate == "")
                            .ToListAsync();
        }

        public async Task<Models.CustomerWiseReBookDeliveryAddress> GetCustomerReBookById(int id)
        {
            return await _context.customerWiseReBookDeliveryAddress
           .Where(x => x.CWBID == id && x.EndDate == null || x.EndDate=="")
           .FirstOrDefaultAsync();
        }

        public async Task<Models.CustomerWiseReBookDeliveryAddress> CreateCustomerReBook(Models.CustomerWiseReBookDeliveryAddress customerReBook)
        {
            await _context.customerWiseReBookDeliveryAddress.AddAsync(customerReBook);
            await _context.SaveChangesAsync();
            return customerReBook;
        }

        public async Task<CustomerWiseReBookDeliveryAddress> UpdateCustomerReBook(int id, Models.CustomerWiseReBookDeliveryAddress customerReBook)
        {
            var existingCustomerReBook = await _context.customerWiseReBookDeliveryAddress.FindAsync(id);
            if (existingCustomerReBook != null)
            {
                existingCustomerReBook.ShipperName = customerReBook.ShipperName;
                existingCustomerReBook.ReBookConsigneeName = customerReBook.ReBookConsigneeName;
                existingCustomerReBook.DeliveryAddress = customerReBook.DeliveryAddress;
                existingCustomerReBook.Pincode = customerReBook.Pincode;
                existingCustomerReBook.City = customerReBook.City;
                existingCustomerReBook.State = customerReBook.State;
                existingCustomerReBook.modifyedBy = customerReBook.modifyedBy;
                existingCustomerReBook.ModifyedOn = customerReBook.ModifyedOn;
                existingCustomerReBook.IsActive = customerReBook.IsActive;
                await _context.SaveChangesAsync();
            }
            return existingCustomerReBook;
        }   

       public async Task<CustomerWiseReBookDeliveryAddress> DeleteCustomerReBook(int id)
        {
            var customerReBook = await _context.customerWiseReBookDeliveryAddress.FindAsync(id);
            if (customerReBook != null)
            {
                customerReBook.EndDate = DateTime.Now.ToString();
                await _context.SaveChangesAsync();
            }
            return customerReBook;
        }
    }
}
