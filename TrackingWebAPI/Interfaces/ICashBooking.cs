using TrackingWebAPI.Models;

namespace TrackingWebAPI.Interfaces
{
    public interface ICashBooking
    {
        Task<IEnumerable<CashBooking>> GetAllCashBooking();
        Task<CashBooking> GetCashBookingById(int id);
        Task<CashBooking> CreateCashBooking(CashBooking CashBooking);
        Task<CashBooking> UpdateCashBooking(int id, CashBooking CashBooking);
        Task<CashBooking> DeleteCashBooking(int id);
    }
}
