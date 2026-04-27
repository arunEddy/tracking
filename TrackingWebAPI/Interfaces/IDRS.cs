using TrackingWebAPI.Models;

namespace TrackingWebAPI.Interfaces
{
    public interface IDRS
    {
        Task<IEnumerable<DRS>> GetAllDRS();
        Task<DRS> GetDRSById(int id);
        Task<DRS> CreateDRS(DRS DRS);
        Task<DRS> UpdateDRS(int id, DRS DRS);
        Task<DRS> DeleteDRS(int id);
    }
}
