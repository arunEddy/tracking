using DALCLASS.DBContact;
using Microsoft.EntityFrameworkCore;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;

namespace TrackingWebAPI.Services
{
    public class CashBookingItemDetailsServices:ICashBookingItemDetails
    {
        private readonly ApplicationDbContext _context;

        public CashBookingItemDetailsServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.CashBookingItemDetails>> GetAllCashBookingItemDetails()
        {
            return await _context.cashBookingItemDetails
                           .Where(x => x.EndDate == null || x.EndDate == "")
                            .ToListAsync();
        }

        public async Task<Models.CashBookingItemDetails> GetCashBookingItemDetailsById(int id)
        {
            return await _context.cashBookingItemDetails
           .Where(x => x.cbIId == id && x.EndDate == null || x.EndDate == "")
           .FirstOrDefaultAsync();
        }

        public async Task<Models.CashBookingItemDetails> CreateCashBookingItemDetails(Models.CashBookingItemDetails customerDataUpdateAWB)
        {
            await _context.cashBookingItemDetails.AddAsync(customerDataUpdateAWB);
            await _context.SaveChangesAsync();
            return customerDataUpdateAWB;
        }

        public async Task<CashBookingItemDetails> UpdateCashBookingItemDetails(int id, Models.CashBookingItemDetails customerDataUpdateAWB)
        {
            var existingcustomerDataUpdateAWB = await _context.cashBookingItemDetails.FindAsync(id);
            if (existingcustomerDataUpdateAWB != null)
            {
                existingcustomerDataUpdateAWB.InvoiceID = customerDataUpdateAWB.InvoiceID;
                existingcustomerDataUpdateAWB.InvoiceDate = customerDataUpdateAWB.InvoiceDate;
                existingcustomerDataUpdateAWB.Description = customerDataUpdateAWB.Description;
                existingcustomerDataUpdateAWB.Amount = customerDataUpdateAWB.Amount;
                existingcustomerDataUpdateAWB.eWayBillNumber = customerDataUpdateAWB.eWayBillNumber;
                existingcustomerDataUpdateAWB.EWBDate = customerDataUpdateAWB.EWBDate;
                existingcustomerDataUpdateAWB.cbId = customerDataUpdateAWB.cbId;
                existingcustomerDataUpdateAWB.mdfby = customerDataUpdateAWB.mdfby;
                existingcustomerDataUpdateAWB.mdfon = customerDataUpdateAWB.mdfon;
                existingcustomerDataUpdateAWB.IsActive = customerDataUpdateAWB.IsActive;
                await _context.SaveChangesAsync();
            }
            return existingcustomerDataUpdateAWB;
        }

        public async Task<CashBookingItemDetails> DeleteCashBookingItemDetails(int id)
        {
            var customerDataUpdateAWB = await _context.cashBookingItemDetails.FindAsync(id);
            if (customerDataUpdateAWB != null)
            {
                customerDataUpdateAWB.EndDate = DateTime.Now.ToString();
                await _context.SaveChangesAsync();
            }
            return customerDataUpdateAWB;
        }
    }
}
