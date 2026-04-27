using DALCLASS.DBContact;
using Microsoft.EntityFrameworkCore;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;

namespace TrackingWebAPI.Services
{
    public class BookingScanNormalServices:IBookingScanNormal
    {
        private readonly ApplicationDbContext _context;

        public BookingScanNormalServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.BookingScanNormal>> GetAllBookingScanNormal()
        {
            return await _context.bookingScanNormal
                           .Where(x => x.EndDate == null || x.EndDate == "")
                            .ToListAsync();
        }

        public async Task<Models.BookingScanNormal> GetBookingScanNormalById(int id)
        {
            return await _context.bookingScanNormal
           .Where(x => x.bsnid == id && x.EndDate == null || x.EndDate == "")
           .FirstOrDefaultAsync();
        }

        public async Task<Models.BookingScanNormal> CreateBookingScanNormal(Models.BookingScanNormal customerDataUpdateAWB)
        {
            await _context.bookingScanNormal.AddAsync(customerDataUpdateAWB);
            await _context.SaveChangesAsync();
            return customerDataUpdateAWB;
        }

        public async Task<BookingScanNormal> UpdateBookingScanNormal(int id, Models.BookingScanNormal customerDataUpdateAWB)
        {
            var existingcustomerDataUpdateAWB = await _context.bookingScanNormal.FindAsync(id);
            if (existingcustomerDataUpdateAWB != null)
            {
                existingcustomerDataUpdateAWB.BookingOffice = customerDataUpdateAWB.BookingOffice;
                existingcustomerDataUpdateAWB.PickupCity = customerDataUpdateAWB.PickupCity;
                existingcustomerDataUpdateAWB.PickupPincode = customerDataUpdateAWB.PickupPincode;
                existingcustomerDataUpdateAWB.BookingDate = customerDataUpdateAWB.BookingDate;
                existingcustomerDataUpdateAWB.ManifestNo = customerDataUpdateAWB.ManifestNo;
                existingcustomerDataUpdateAWB.ManifestDate = customerDataUpdateAWB.ManifestDate;
                existingcustomerDataUpdateAWB.CreditLimit = customerDataUpdateAWB.CreditLimit;
                existingcustomerDataUpdateAWB.AWB = customerDataUpdateAWB.AWB;
                existingcustomerDataUpdateAWB.CustomerName = customerDataUpdateAWB.CustomerName;
                existingcustomerDataUpdateAWB.Mode = customerDataUpdateAWB.Mode;
                existingcustomerDataUpdateAWB.Pcs = customerDataUpdateAWB.Pcs;
                existingcustomerDataUpdateAWB.ActualWeight = customerDataUpdateAWB.ActualWeight;
                existingcustomerDataUpdateAWB.Volumetric = customerDataUpdateAWB.Volumetric;
                existingcustomerDataUpdateAWB.VolWt = customerDataUpdateAWB.VolWt;
                existingcustomerDataUpdateAWB.ChargeWt = customerDataUpdateAWB.ChargeWt;
                existingcustomerDataUpdateAWB.ProductName = customerDataUpdateAWB.ProductName;
                existingcustomerDataUpdateAWB.CODAmount = customerDataUpdateAWB.CODAmount;
                existingcustomerDataUpdateAWB.BookType = customerDataUpdateAWB.BookType;
                existingcustomerDataUpdateAWB.Content = customerDataUpdateAWB.Content;
                existingcustomerDataUpdateAWB.RefNo = customerDataUpdateAWB.RefNo;
                existingcustomerDataUpdateAWB.ProductService = customerDataUpdateAWB.ProductService;
                existingcustomerDataUpdateAWB.bsnName = customerDataUpdateAWB.bsnName;
                existingcustomerDataUpdateAWB.bsnAddress = customerDataUpdateAWB.bsnAddress;
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

        public async Task<BookingScanNormal> UpdateBookingScanNormal(int id)
        {
            var customerDataUpdateAWB = await _context.bookingScanNormal.FindAsync(id);
            if (customerDataUpdateAWB != null)
            {
                customerDataUpdateAWB.EndDate = DateTime.Now.ToString();
                await _context.SaveChangesAsync();
            }
            return customerDataUpdateAWB;
        }
    }
}
