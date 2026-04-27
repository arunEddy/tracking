using DALCLASS.DBContact;
using Microsoft.EntityFrameworkCore;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;

namespace TrackingWebAPI.Services
{
    public class BookingScanNormalEditServices:IBookingScanNormalEdit
    {
        private readonly ApplicationDbContext _context;

        public BookingScanNormalEditServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.BookingScanNormalEdit>> GetAllBookingScanNormalEdit()
        {
            return await _context.bookingScanNormalEdit
                           .Where(x => x.EndDate == null || x.EndDate == "")
                            .ToListAsync();
        }

        public async Task<Models.BookingScanNormalEdit> GetBookingScanNormalEditById(int id)
        {
            return await _context.bookingScanNormalEdit
           .Where(x => x.bsneid == id && x.EndDate == null || x.EndDate == "")
           .FirstOrDefaultAsync();
        }

        public async Task<Models.BookingScanNormalEdit> CreateBookingScanNormalEdit(Models.BookingScanNormalEdit customerDataUpdateAWB)
        {
            await _context.bookingScanNormalEdit.AddAsync(customerDataUpdateAWB);
            await _context.SaveChangesAsync();
            return customerDataUpdateAWB;
        }

        public async Task<BookingScanNormalEdit> UpdateBookingScanNormalEdit(int id, Models.BookingScanNormalEdit customerDataUpdateAWB)
        {
            var existingcustomerDataUpdateAWB = await _context.bookingScanNormalEdit.FindAsync(id);
            if (existingcustomerDataUpdateAWB != null)
            {
                existingcustomerDataUpdateAWB.AWBNumber = customerDataUpdateAWB.AWBNumber;
                existingcustomerDataUpdateAWB.BookingOffice = customerDataUpdateAWB.BookingOffice;
                existingcustomerDataUpdateAWB.PickupCity = customerDataUpdateAWB.PickupCity;
                existingcustomerDataUpdateAWB.PickupPincode = customerDataUpdateAWB.PickupPincode;
                existingcustomerDataUpdateAWB.BookingDate = customerDataUpdateAWB.BookingDate;
                existingcustomerDataUpdateAWB.ManifestNo = customerDataUpdateAWB.ManifestNo;
                existingcustomerDataUpdateAWB.ManifestDate = customerDataUpdateAWB.ManifestDate;
                existingcustomerDataUpdateAWB.ActualWeight = customerDataUpdateAWB.ActualWeight;
                existingcustomerDataUpdateAWB.ChargeWeight = customerDataUpdateAWB.ChargeWeight;
                existingcustomerDataUpdateAWB.ProductName = customerDataUpdateAWB.ProductName;
                existingcustomerDataUpdateAWB.CODAmount = customerDataUpdateAWB.CODAmount;
                existingcustomerDataUpdateAWB.Mode = customerDataUpdateAWB.Mode;
                existingcustomerDataUpdateAWB.Volumetric = customerDataUpdateAWB.Volumetric;
                existingcustomerDataUpdateAWB.VolWeight = customerDataUpdateAWB.VolWeight;
                existingcustomerDataUpdateAWB.BookType = customerDataUpdateAWB.BookType;
                existingcustomerDataUpdateAWB.Content = customerDataUpdateAWB.Content;
                existingcustomerDataUpdateAWB.RefNo = customerDataUpdateAWB.RefNo;
                existingcustomerDataUpdateAWB.ProductService = customerDataUpdateAWB.ProductService;
                existingcustomerDataUpdateAWB.Name = customerDataUpdateAWB.Name;
                existingcustomerDataUpdateAWB.Address = customerDataUpdateAWB.Address;
                existingcustomerDataUpdateAWB.Destination = customerDataUpdateAWB.Destination;
                existingcustomerDataUpdateAWB.City = customerDataUpdateAWB.City;
                existingcustomerDataUpdateAWB.Pincode = customerDataUpdateAWB.Pincode;
                existingcustomerDataUpdateAWB.ODAChargeApplicable = customerDataUpdateAWB.ODAChargeApplicable;
                existingcustomerDataUpdateAWB.mdfby = customerDataUpdateAWB.mdfby;
                existingcustomerDataUpdateAWB.mdfon = customerDataUpdateAWB.mdfon;
                existingcustomerDataUpdateAWB.IsActive = customerDataUpdateAWB.IsActive;
                await _context.SaveChangesAsync();
            }
            return existingcustomerDataUpdateAWB;
        }

        public async Task<BookingScanNormalEdit> DeleteBookingScanNormalEdit(int id)
        {
            var customerDataUpdateAWB = await _context.bookingScanNormalEdit.FindAsync(id);
            if (customerDataUpdateAWB != null)
            {
                customerDataUpdateAWB.EndDate = DateTime.Now.ToString();
                await _context.SaveChangesAsync();
            }
            return customerDataUpdateAWB;
        }
    }
}
