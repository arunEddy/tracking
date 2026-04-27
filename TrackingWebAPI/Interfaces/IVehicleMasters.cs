using TrackingWebAPI.Models;

namespace TrackingWebAPI.Interfaces
{
    public interface IVehicleMasters
    {
        Task<IEnumerable<VehicleMasters>> GetVehicleMasters();
        Task<VehicleMasters> GetVehicleMastersById(int id);
        Task<VehicleMasters> CreateVehicleMasters(VehicleMasters vehicleMasters);
        Task<VehicleMasters> UpdateVehicleMasters(int id, VehicleMasters vehicleMasters);
        Task<VehicleMasters> DeleteVehicleMasters(int id);
    }
}
