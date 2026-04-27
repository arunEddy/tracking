using TrackingWebAPI.Models;

namespace TrackingWebAPI.Interfaces
{
    public interface IBookingScanLogisticEdit
    {
        Task<IEnumerable<BookingScanLogisticEdit>> GetAllBookingScanLogisticEdit();
        Task<BookingScanLogisticEdit> GetBookingScanLogisticEditById(int id);
        Task<BookingScanLogisticEdit> CreateBookingScanLogisticEdit(BookingScanLogisticEdit BookingScanNormalEdit);
        Task<BookingScanLogisticEdit> UpdateBookingScanLogisticEdit(int id, BookingScanLogisticEdit BookingScanNormalEdit);
        Task<BookingScanLogisticEdit> DeleteBookingScanLogisticEdit(int id);
    }
}
