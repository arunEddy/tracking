using TrackingWebAPI.Models;

namespace TrackingWebAPI.Interfaces
{
    public interface IEDPEditing
    {
        Task<IEnumerable<EDPEditing>> GetAllEDPEditing();
        Task<EDPEditing> GetEDPEditingById(int id);
        Task<EDPEditing> CreateEDPEditing(EDPEditing EDPEditing);
        Task<EDPEditing> UpdateEDPEditing(int id, EDPEditing EDPEditing);
        Task<EDPEditing> DeleteEDPEditing(int id);
    }
}
