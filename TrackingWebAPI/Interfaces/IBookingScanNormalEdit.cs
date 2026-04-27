using TrackingWebAPI.Models;

namespace TrackingWebAPI.Interfaces
{
    public interface IBookingScanNormalEdit
    {
        Task<IEnumerable<BookingScanNormalEdit>> GetAllBookingScanNormalEdit();
        Task<BookingScanNormalEdit> GetBookingScanNormalEditById(int id);
        Task<BookingScanNormalEdit> CreateBookingScanNormalEdit(BookingScanNormalEdit BookingScanNormalEdit);
        Task<BookingScanNormalEdit> UpdateBookingScanNormalEdit(int id, BookingScanNormalEdit BookingScanNormalEdit);
        Task<BookingScanNormalEdit> DeleteBookingScanNormalEdit(int id);
    }
}
