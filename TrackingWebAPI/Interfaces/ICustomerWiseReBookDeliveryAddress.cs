using TrackingWebAPI.Models;

namespace TrackingWebAPI.Interfaces
{
    public interface ICustomerWiseReBookDeliveryAddress
    {
        Task<IEnumerable<CustomerWiseReBookDeliveryAddress>> GetAllCustomerReBook();
        Task<CustomerWiseReBookDeliveryAddress> GetCustomerReBookById(int id);
        Task<CustomerWiseReBookDeliveryAddress> CreateCustomerReBook(CustomerWiseReBookDeliveryAddress CustomerReBook);
        Task<CustomerWiseReBookDeliveryAddress> UpdateCustomerReBook(int id, CustomerWiseReBookDeliveryAddress CustomerReBook);
        Task<CustomerWiseReBookDeliveryAddress> DeleteCustomerReBook(int id);
    }
}
