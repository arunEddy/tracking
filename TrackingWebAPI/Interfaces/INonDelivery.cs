using TrackingWebAPI.Models;

namespace TrackingWebAPI.Interfaces
{
    public interface INonDelivery
    {
        Task<IEnumerable<NonDelivery>> GetNonDeliveries();
        Task<NonDelivery> GetNonDeliveryById(int id);
        Task<NonDelivery> CreateNonDelivery(NonDelivery nonDeliveryRS);
        Task<NonDelivery> UpdateNonDelivery(int id, NonDelivery nonDelivery);
        Task<NonDelivery> DeleteNonDelivery(int id);
    }
}
