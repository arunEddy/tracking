using TrackingWebAPI.Models;

namespace TrackingWebAPI.Interfaces
{
    public interface ITransportMaster
    {
        Task<IEnumerable<TransportMaster>> GetTransportMasters();
        Task<TransportMaster> GetTransportMastersById(int id);
        Task<TransportMaster> CreateTransportMasters(TransportMaster airlineMaster);
        Task<TransportMaster> UpdateTransportMasters(int id, TransportMaster airlineMaster);
        Task<TransportMaster> DeleteTransportMasters(int id);
    }
}
