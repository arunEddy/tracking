using DALCLASS.DBContact;
using Microsoft.EntityFrameworkCore;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;

namespace TrackingWebAPI.Services
{
    public class BookingScanLogisticItemDetailsServices: IBookingScanLogisticItemDetails
    {
        private readonly ApplicationDbContext _context;

        public BookingScanLogisticItemDetailsServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.BookingScanLogisticItemDetails>> GetAllBookingScanLogisticItemDetails()
        {
            return await _context.bookingScanLogisticItemDetails
                           .Where(x => x.EndDate == null || x.EndDate == "")
                            .ToListAsync();
        }

        public async Task<Models.BookingScanLogisticItemDetails> GetBookingScanLogisticItemDetailsById(int id)
        {
            return await _context.bookingScanLogisticItemDetails
           .Where(x => x.bsldItid == id && x.EndDate == null || x.EndDate == "")
           .FirstOrDefaultAsync();
        }

        public async Task<Models.BookingScanLogisticItemDetails> CreateBookingScanLogisticItemDetails(Models.BookingScanLogisticItemDetails customerDataUpdateAWB)
        {
            await _context.bookingScanLogisticItemDetails.AddAsync(customerDataUpdateAWB);
            await _context.SaveChangesAsync();
            return customerDataUpdateAWB;
        }

        public async Task<BookingScanLogisticItemDetails> UpdateBookingScanLogisticItemDetails(int id, Models.BookingScanLogisticItemDetails customerDataUpdateAWB)
        {
            var existingcustomerDataUpdateAWB = await _context.bookingScanLogisticItemDetails.FindAsync(id);
            if (existingcustomerDataUpdateAWB != null)
            {
                existingcustomerDataUpdateAWB.PartnerRefNo = customerDataUpdateAWB.PartnerRefNo;
                existingcustomerDataUpdateAWB.eWayBillNumber = customerDataUpdateAWB.eWayBillNumber;
                existingcustomerDataUpdateAWB.EWBValidDate = customerDataUpdateAWB.EWBValidDate;
                existingcustomerDataUpdateAWB.InvoiceID = customerDataUpdateAWB.InvoiceID;
                existingcustomerDataUpdateAWB.InvoiceDate = customerDataUpdateAWB.InvoiceDate;
                existingcustomerDataUpdateAWB.Description = customerDataUpdateAWB.Description;
                existingcustomerDataUpdateAWB.InvoiceAmount = customerDataUpdateAWB.InvoiceAmount;
                existingcustomerDataUpdateAWB.CODAmount = customerDataUpdateAWB.CODAmount;
                existingcustomerDataUpdateAWB.mdfby = customerDataUpdateAWB.mdfby;
                existingcustomerDataUpdateAWB.mdfon = customerDataUpdateAWB.mdfon;
                existingcustomerDataUpdateAWB.IsActive = customerDataUpdateAWB.IsActive;
                await _context.SaveChangesAsync();
            }
            return existingcustomerDataUpdateAWB;
        }

        public async Task<BookingScanLogisticItemDetails> DeleteBookingScanLogisticItemDetails(int id)
        {
            var customerDataUpdateAWB = await _context.bookingScanLogisticItemDetails.FindAsync(id);
            if (customerDataUpdateAWB != null)
            {
                customerDataUpdateAWB.EndDate = DateTime.Now.ToString();
                await _context.SaveChangesAsync();
            }
            return customerDataUpdateAWB;
        }
    }
}
