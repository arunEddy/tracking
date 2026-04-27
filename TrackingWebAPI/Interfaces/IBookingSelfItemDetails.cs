using TrackingWebAPI.Models;

namespace TrackingWebAPI.Interfaces
{
    public interface IBookingSelfItemDetails
    {
        Task<IEnumerable<BookingSelfItemDetails>> GetAllBookingSelfItemDetails();
        Task<BookingSelfItemDetails> GetBookingSelfItemDetailsId(int id);
        Task<BookingSelfItemDetails> CreateBookingSelfItemDetails(BookingSelfItemDetails BookingSelfItemDetails);
        Task<BookingSelfItemDetails> UpdateBookingSelfItemDetails(int id, BookingSelfItemDetails BookingSelfItemDetails);
        Task<BookingSelfItemDetails> DeleteBookingSelfItemDetails(int id);
    }
}
