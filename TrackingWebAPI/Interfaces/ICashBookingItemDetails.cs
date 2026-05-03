using TrackingWebAPI.Models;

namespace TrackingWebAPI.Interfaces
{
    public interface ICashBookingItemDetails
    {
        Task<IEnumerable<CashBookingItemDetails>> GetAllCashBookingItemDetails();
        Task<IEnumerable<CashBookingItemDetails>> GetCashBookingItemDetailsById(int id);
        Task<CashBookingItemDetails> CreateCashBookingItemDetails(CashBookingItemDetails CashBookingItemDetails);
        Task<CashBookingItemDetails> UpdateCashBookingItemDetails(int id, CashBookingItemDetails CashBookingItemDetails);
        Task<CashBookingItemDetails> DeleteCashBookingItemDetails(int id);
    }
}
