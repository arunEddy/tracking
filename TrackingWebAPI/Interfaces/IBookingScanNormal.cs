using TrackingWebAPI.Models;

namespace TrackingWebAPI.Interfaces
{
    public interface IBookingScanNormal
    {
        Task<IEnumerable<BookingScanNormal>> GetAllBookingScanNormal();
        Task<BookingScanNormal> GetBookingScanNormalById(int id);
        Task<BookingScanNormal> CreateBookingScanNormal(BookingScanNormal BookingScanNormal);
        Task<BookingScanNormal> UpdateBookingScanNormal(int id, BookingScanNormal BookingScanNormal);
        Task<BookingScanNormal> UpdateBookingScanNormal(int id);
    }
}
