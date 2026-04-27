using TrackingWebAPI.Models;

namespace TrackingWebAPI.Interfaces
{
    public interface IDeliveryBoyMasters
    {
        Task<IEnumerable<DeliveryBoyMasters>> GetAllDeleveryBoyMasters();
        Task<DeliveryBoyMasters> GetDeleveryBoyMastersById(int id);
        Task<DeliveryBoyMasters> CreateDeleveryBoyMasters(DeliveryBoyMasters deliveryBoyMasters);
        Task<DeliveryBoyMasters> UpdateDeleveryBoyMasters(int id, DeliveryBoyMasters deliveryBoyMasters);
        Task<DeliveryBoyMasters> DeleteDeleveryBoyMasters(int id);
    }
}
