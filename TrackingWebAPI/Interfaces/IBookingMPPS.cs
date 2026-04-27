using TrackingWebAPI.Models;

namespace TrackingWebAPI.Interfaces
{
    public interface IBookingMPPS
    {
        Task<IEnumerable<BookingMPPS>> GetAllBookingMPPS();
        Task<BookingMPPS> GetBookingMPPSById(int id);
        Task<BookingMPPS> CreateBookingMPPS(BookingMPPS BookingMPPS);
        Task<BookingMPPS> UpdateBookingMPPS(int id, BookingMPPS BookingMPPS);
        Task<BookingMPPS> DeleteBookingMPPS(int id);
    }
}
