using TrackingWebAPI.Models;

namespace TrackingWebAPI.Interfaces
{
    public interface ICashBookingItemDetails
    {
        Task<IEnumerable<CashBookingItemDetails>> GetAllCashBookingItemDetails();
        Task<CashBookingItemDetails> GetCashBookingItemDetailsById(int id);
        Task<CashBookingItemDetails> CreateCashBookingItemDetails(CashBookingItemDetails CashBookingItemDetails);
        Task<CashBookingItemDetails> UpdateCashBookingItemDetails(int id, CashBookingItemDetails CashBookingItemDetails);
        Task<CashBookingItemDetails> DeleteCashBookingItemDetails(int id);
    }
}
