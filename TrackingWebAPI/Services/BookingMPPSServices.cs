using DALCLASS.DBContact;
using Microsoft.EntityFrameworkCore;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;

namespace TrackingWebAPI.Services
{
    public class BookingMPPSServices:IBookingMPPS
    {
        private readonly ApplicationDbContext _context;

        public BookingMPPSServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.BookingMPPS>> GetAllBookingMPPS()
        {
            return await _context.bookingMPPS
                           .Where(x => x.EndDate == null || x.EndDate == "")
                            .ToListAsync();
        }

        public async Task<Models.BookingMPPS> GetBookingMPPSById(int id)
        {
            return await _context.bookingMPPS
           .Where(x => x.bmppsId == id && x.EndDate == null || x.EndDate == "")
           .FirstOrDefaultAsync();
        }

        public async Task<Models.BookingMPPS> CreateBookingMPPS(Models.BookingMPPS bookingMPPS)
        {
            await _context.bookingMPPS.AddAsync(bookingMPPS);
            await _context.SaveChangesAsync();
            return bookingMPPS;
        }

        public async Task<BookingMPPS> UpdateBookingMPPS(int id, Models.BookingMPPS bookingMPPS)
        {
            var existingbookingMPPS = await _context.bookingMPPS.FindAsync(id);
            if (existingbookingMPPS != null)
            {
                existingbookingMPPS.AWBNumber = bookingMPPS.AWBNumber;
                existingbookingMPPS.bmpFormat = bookingMPPS.bmpFormat;
                existingbookingMPPS.AWBNo = bookingMPPS.AWBNo;
                existingbookingMPPS.mdfby = bookingMPPS.mdfby;
                existingbookingMPPS.mdfon = bookingMPPS.mdfon;
                existingbookingMPPS.IsActive = bookingMPPS.IsActive;
                await _context.SaveChangesAsync();
            }
            return existingbookingMPPS;
        }

        public async Task<BookingMPPS> DeleteBookingMPPS(int id)
        {
            var existingbookingMPPS = await _context.bookingMPPS.FindAsync(id);
            if (existingbookingMPPS != null)
            {
                existingbookingMPPS.EndDate = DateTime.Now.ToString();
                await _context.SaveChangesAsync();
            }
            return existingbookingMPPS;
        }
    }
}
