using TrackingWebAPI.Models;

namespace TrackingWebAPI.Interfaces
{
    public interface ICustomerDataUpdateAWB
    {
        Task<IEnumerable<CustomerDataUpdateAWB>> GetAllCustomerDataUpdateAWB();
        Task<CustomerDataUpdateAWB> GetCustomerDataUpdateAWBById(int id);
        Task<CustomerDataUpdateAWB> CreateCustomerDataUpdateAWB(CustomerDataUpdateAWB CustomerDataUpdateAWB);
        Task<CustomerDataUpdateAWB> UpdateCustomerDataUpdateAWB(int id, CustomerDataUpdateAWB CustomerDataUpdateAWB);
        Task<CustomerDataUpdateAWB> DeleteCustomerDataUpdateAWB(int id);
    }
}
