using DALCLASS.DBContact;
using Microsoft.EntityFrameworkCore;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;

namespace TrackingWebAPI.Services
{
    public class BookingSelfItemDetailsServices:IBookingSelfItemDetails
    {
        private readonly ApplicationDbContext _context;

        public BookingSelfItemDetailsServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.BookingSelfItemDetails>> GetAllBookingSelfItemDetails()
        {
            return await _context.bookingSelfItemDetails
                           .Where(x => x.EndDate == null || x.EndDate == "")
                            .ToListAsync();
        }

        public async Task<Models.BookingSelfItemDetails> GetBookingSelfItemDetailsId(int id)
        {
            return await _context.bookingSelfItemDetails
           .Where(x => x.btdId == id && x.EndDate == null || x.EndDate == "")
           .FirstOrDefaultAsync();
        }

        public async Task<Models.BookingSelfItemDetails> CreateBookingSelfItemDetails(Models.BookingSelfItemDetails customerDataUpdateAWB)
        {
            await _context.bookingSelfItemDetails.AddAsync(customerDataUpdateAWB);
            await _context.SaveChangesAsync();
            return customerDataUpdateAWB;
        }

        public async Task<BookingSelfItemDetails> UpdateBookingSelfItemDetails(int id, Models.BookingSelfItemDetails customerDataUpdateAWB)
        {
            var existingcustomerDataUpdateAWB = await _context.bookingSelfItemDetails.FindAsync(id);
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

        public async Task<BookingSelfItemDetails> DeleteBookingSelfItemDetails(int id)
        {
            var customerDataUpdateAWB = await _context.bookingSelfItemDetails.FindAsync(id);
            if (customerDataUpdateAWB != null)
            {
                customerDataUpdateAWB.EndDate = DateTime.Now.ToString();
                await _context.SaveChangesAsync();
            }
            return customerDataUpdateAWB;
        }
    }
}
