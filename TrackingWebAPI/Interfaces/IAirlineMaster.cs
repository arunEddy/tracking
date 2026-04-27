using TrackingWebAPI.Models;

namespace TrackingWebAPI.Interfaces
{
    public interface IAirlineMaster
    {
        Task<IEnumerable<AirlineMaster>> GetAllAirlineMaster();
        Task<AirlineMaster> GetAirlineMasterById(int id);
        Task<AirlineMaster> CreateAirlineMaster(AirlineMaster airlineMaster);
        Task<AirlineMaster> UpdateAirlineMaster(int id, AirlineMaster airlineMaster);
        Task<AirlineMaster> DeleteAirlineMaster(int id);
    }
}
