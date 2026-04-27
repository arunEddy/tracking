using TrackingWebAPI.Models;

namespace TrackingWebAPI.Interfaces
{
    public interface IOfficeRequestMaster
    {
        Task<IEnumerable<OfficeRequestMaster>> GetAllOfficeRequestMaster();
        Task<OfficeRequestMaster>GetOfficeRequestMasterById(int id);
        Task<OfficeRequestMaster>CreateOfficeRequestMaster(OfficeRequestMaster officeRequestMaster);
        Task<OfficeRequestMaster> UpdateOfficeRequestMaster(int id,OfficeRequestMaster officeRequestMaster);
        Task<OfficeRequestMaster> DeleteOfficeRequestMaster(int id);



    }
}
