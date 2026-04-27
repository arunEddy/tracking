using TrackingWebAPI.Models;

namespace TrackingWebAPI.Interfaces
{
    public interface IReturnBooking
    {
        Task<IEnumerable<ReturnBooking>> GetAllReturnBooking();
        Task<ReturnBooking> GetReturnBookingById(int id);
        Task<ReturnBooking> CreateReturnBooking(ReturnBooking ReturnBooking);
        Task<ReturnBooking> UpdateReturnBooking(int id, ReturnBooking ReturnBooking);
        Task<ReturnBooking> DeleteReturnBooking(int id);
    }
}
