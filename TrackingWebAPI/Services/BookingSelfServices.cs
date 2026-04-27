using DALCLASS.DBContact;
using Microsoft.EntityFrameworkCore;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;

namespace TrackingWebAPI.Services
{
    public class BookingSelfServices:IBookingSelf
    {
        private readonly ApplicationDbContext _context;

        public BookingSelfServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.BookingSelf>> GetAllBookingSelf()
        {
            return await _context.bookingSelf
                           .Where(x => x.EndDate == null || x.EndDate == "")
                            .ToListAsync();
        }

        public async Task<Models.BookingSelf> GetBookingSelfById(int id)
        {
            return await _context.bookingSelf
           .Where(x => x.bseid == id && x.EndDate == null || x.EndDate == "")
           .FirstOrDefaultAsync();
        }

        public async Task<Models.BookingSelf> CreateBookingSelf(Models.BookingSelf customerDataUpdateAWB)
        {
            await _context.bookingSelf.AddAsync(customerDataUpdateAWB);
            await _context.SaveChangesAsync();
            return customerDataUpdateAWB;
        }

        public async Task<BookingSelf> UpdateBookingSelf(int id, Models.BookingSelf customerDataUpdateAWB)
        {
            var existingcustomerDataUpdateAWB = await _context.bookingSelf.FindAsync(id);
            if (existingcustomerDataUpdateAWB != null)
            {
                existingcustomerDataUpdateAWB.BookingOffice = customerDataUpdateAWB.BookingOffice;
                existingcustomerDataUpdateAWB.CustomerName = customerDataUpdateAWB.CustomerName;
                existingcustomerDataUpdateAWB.DocketType = customerDataUpdateAWB.DocketType;
                existingcustomerDataUpdateAWB.AWB = customerDataUpdateAWB.AWB;
                existingcustomerDataUpdateAWB.BookDate = customerDataUpdateAWB.BookDate;
                existingcustomerDataUpdateAWB.Origin = customerDataUpdateAWB.Origin;
                existingcustomerDataUpdateAWB.Destination = customerDataUpdateAWB.Destination;
                existingcustomerDataUpdateAWB.Mode = customerDataUpdateAWB.Mode;
                existingcustomerDataUpdateAWB.ActualWeight = customerDataUpdateAWB.ActualWeight;
                existingcustomerDataUpdateAWB.Pcs = customerDataUpdateAWB.Pcs;
                existingcustomerDataUpdateAWB.ChargeWt = customerDataUpdateAWB.ChargeWt;
                existingcustomerDataUpdateAWB.Volumetric = customerDataUpdateAWB.Volumetric;
                existingcustomerDataUpdateAWB.VolWt = customerDataUpdateAWB.VolWt;
                existingcustomerDataUpdateAWB.ProductName = customerDataUpdateAWB.ProductName;
                existingcustomerDataUpdateAWB.TopayAmount = customerDataUpdateAWB.TopayAmount;
                existingcustomerDataUpdateAWB.ProductType = customerDataUpdateAWB.ProductType;
                existingcustomerDataUpdateAWB.ConsignorName = customerDataUpdateAWB.ConsignorName;
                existingcustomerDataUpdateAWB.PickupCity = customerDataUpdateAWB.PickupCity;
                existingcustomerDataUpdateAWB.PickupPincode = customerDataUpdateAWB.PickupPincode;
                existingcustomerDataUpdateAWB.Address1 = customerDataUpdateAWB.Address1;
                existingcustomerDataUpdateAWB.Address2 = customerDataUpdateAWB.Address2;
                existingcustomerDataUpdateAWB.ConsigneeName = customerDataUpdateAWB.ConsigneeName;
                existingcustomerDataUpdateAWB.ConsigneeAddress1 = customerDataUpdateAWB.ConsigneeAddress1;
                existingcustomerDataUpdateAWB.ConsigneeAddress2 = customerDataUpdateAWB.ConsigneeAddress2;
                existingcustomerDataUpdateAWB.ConsigneePhone = customerDataUpdateAWB.ConsigneePhone;
                existingcustomerDataUpdateAWB.Mobile = customerDataUpdateAWB.Mobile;
                existingcustomerDataUpdateAWB.ConsigneePincode = customerDataUpdateAWB.ConsigneePincode;
                existingcustomerDataUpdateAWB.City = customerDataUpdateAWB.City;
                existingcustomerDataUpdateAWB.MasterReferenceNo = customerDataUpdateAWB.MasterReferenceNo;
                existingcustomerDataUpdateAWB.Remarks = customerDataUpdateAWB.Remarks;
                existingcustomerDataUpdateAWB.mdfby = customerDataUpdateAWB.mdfby;
                existingcustomerDataUpdateAWB.mdfon = customerDataUpdateAWB.mdfon;
                existingcustomerDataUpdateAWB.IsActive = customerDataUpdateAWB.IsActive;
                await _context.SaveChangesAsync();
            }
            return existingcustomerDataUpdateAWB;
        }

        public async Task<BookingSelf> DeleteBookingSelf(int id)
        {
            var customerDataUpdateAWB = await _context.bookingSelf.FindAsync(id);
            if (customerDataUpdateAWB != null)
            {
                customerDataUpdateAWB.EndDate = DateTime.Now.ToString();
                await _context.SaveChangesAsync();
            }
            return customerDataUpdateAWB;
        }
    }
}
