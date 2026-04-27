using TrackingWebAPI.Models;

namespace TrackingWebAPI.Interfaces
{
    public interface IBookingScanLogisticItemDetails
    {
        Task<IEnumerable<BookingScanLogisticItemDetails>> GetAllBookingScanLogisticItemDetails();
        Task<BookingScanLogisticItemDetails> GetBookingScanLogisticItemDetailsById(int id);
        Task<BookingScanLogisticItemDetails> CreateBookingScanLogisticItemDetails(BookingScanLogisticItemDetails BookingScanLogisticItemDetails);
        Task<BookingScanLogisticItemDetails> UpdateBookingScanLogisticItemDetails(int id, BookingScanLogisticItemDetails BookingScanLogisticItemDetails);
        Task<BookingScanLogisticItemDetails> DeleteBookingScanLogisticItemDetails(int id);
    }
}
