using DALCLASS.DBContact;
using Microsoft.EntityFrameworkCore;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;

namespace TrackingWebAPI.Services
{
    public class BookingScanLogisticEditServices:IBookingScanLogisticEdit
    {
        private readonly ApplicationDbContext _context;

        public BookingScanLogisticEditServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.BookingScanLogisticEdit>> GetAllBookingScanLogisticEdit()
        {
            return await _context.bookingScanLogisticEdit
                           .Where(x => x.EndDate == null || x.EndDate == "")
                            .ToListAsync();
        }

        public async Task<Models.BookingScanLogisticEdit> GetBookingScanLogisticEditById(int id)
        {
            return await _context.bookingScanLogisticEdit
           .Where(x => x.bsldid == id && x.EndDate == null || x.EndDate == "")
           .FirstOrDefaultAsync();
        }

        public async Task<Models.BookingScanLogisticEdit> CreateBookingScanLogisticEdit(Models.BookingScanLogisticEdit customerDataUpdateAWB)
        {
            await _context.bookingScanLogisticEdit.AddAsync(customerDataUpdateAWB);
            await _context.SaveChangesAsync();
            return customerDataUpdateAWB;
        }

        public async Task<BookingScanLogisticEdit> UpdateBookingScanLogisticEdit(int id, Models.BookingScanLogisticEdit customerDataUpdateAWB)
        {
            var existingcustomerDataUpdateAWB = await _context.bookingScanLogisticEdit.FindAsync(id);
            if (existingcustomerDataUpdateAWB != null)
            {
                existingcustomerDataUpdateAWB.AWB = customerDataUpdateAWB.AWB;
                existingcustomerDataUpdateAWB.BookingDate = customerDataUpdateAWB.BookingDate;
                existingcustomerDataUpdateAWB.CustomerName = customerDataUpdateAWB.CustomerName;
                existingcustomerDataUpdateAWB.Mode = customerDataUpdateAWB.Mode;
                existingcustomerDataUpdateAWB.Pcs = customerDataUpdateAWB.Pcs;
                existingcustomerDataUpdateAWB.VolWt = customerDataUpdateAWB.VolWt;
                existingcustomerDataUpdateAWB.ChargeWt = customerDataUpdateAWB.ChargeWt;
                existingcustomerDataUpdateAWB.ProductName = customerDataUpdateAWB.ProductName;
                existingcustomerDataUpdateAWB.TopayAmount = customerDataUpdateAWB.TopayAmount;
                existingcustomerDataUpdateAWB.ProductType = customerDataUpdateAWB.ProductType;
                existingcustomerDataUpdateAWB.ConsignorName = customerDataUpdateAWB.ConsignorName;
                existingcustomerDataUpdateAWB.PickupCity = customerDataUpdateAWB.PickupCity;
                existingcustomerDataUpdateAWB.PickupPincode = customerDataUpdateAWB.PickupPincode;
                existingcustomerDataUpdateAWB.Address1 = customerDataUpdateAWB.Address1;
                existingcustomerDataUpdateAWB.Address2 = customerDataUpdateAWB.Address2;
                existingcustomerDataUpdateAWB.Destination = customerDataUpdateAWB.Destination;
                existingcustomerDataUpdateAWB.City = customerDataUpdateAWB.City;
                existingcustomerDataUpdateAWB.Pincode = customerDataUpdateAWB.Pincode;
                existingcustomerDataUpdateAWB.Name = customerDataUpdateAWB.Name;
                existingcustomerDataUpdateAWB.ConsigneeAddress1 = customerDataUpdateAWB.ConsigneeAddress1;
                existingcustomerDataUpdateAWB.ConsigneeAddress2 = customerDataUpdateAWB.ConsigneeAddress2;
                existingcustomerDataUpdateAWB.State = customerDataUpdateAWB.State;
                existingcustomerDataUpdateAWB.Phone = customerDataUpdateAWB.Phone;
                existingcustomerDataUpdateAWB.Mobile = customerDataUpdateAWB.Mobile;
                existingcustomerDataUpdateAWB.ODAChargeApplicable = customerDataUpdateAWB.ODAChargeApplicable;
                existingcustomerDataUpdateAWB.mdfby = customerDataUpdateAWB.mdfby;
                existingcustomerDataUpdateAWB.mdfon = customerDataUpdateAWB.mdfon;
                existingcustomerDataUpdateAWB.IsActive = customerDataUpdateAWB.IsActive;
                await _context.SaveChangesAsync();
            }
            return existingcustomerDataUpdateAWB;
        }

        public async Task<BookingScanLogisticEdit> DeleteBookingScanLogisticEdit(int id)
        {
            var customerDataUpdateAWB = await _context.bookingScanLogisticEdit.FindAsync(id);
            if (customerDataUpdateAWB != null)
            {
                customerDataUpdateAWB.EndDate = DateTime.Now.ToString();
                await _context.SaveChangesAsync();
            }
            return customerDataUpdateAWB;
        }
    }
}
