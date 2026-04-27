using TrackingWebAPI.Models;

namespace TrackingWebAPI.Interfaces
{
    public interface ICancelAWB
    {
        Task<IEnumerable<CancelAWB>> GetAllCancelAWB();
        Task<CancelAWB> GetCancelAWBById(int id);
        Task<CancelAWB> CreateCancelAWB(CancelAWB CancelAWB);
        Task<CancelAWB> UpdateCancelAWB(int id, CancelAWB CancelAWB);
        Task<CancelAWB> DeleteCancelAWB(int id);
    }
}
