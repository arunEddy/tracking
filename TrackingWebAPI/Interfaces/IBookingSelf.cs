using TrackingWebAPI.Models;

namespace TrackingWebAPI.Interfaces
{
    public interface IBookingSelf
    {
        Task<IEnumerable<BookingSelf>> GetAllBookingSelf();
        Task<BookingSelf> GetBookingSelfById(int id);
        Task<BookingSelf> CreateBookingSelf(BookingSelf BookingSelf);
        Task<BookingSelf> UpdateBookingSelf(int id, BookingSelf BookingSelf);
        Task<BookingSelf> DeleteBookingSelf(int id);
    }
}
