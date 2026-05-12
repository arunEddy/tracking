using TrackingWebAPI.Models;

namespace TrackingWebAPI.Interfaces
{
    public interface IReturnToCustomerManifesting
    {
        Task<IEnumerable<ReturnToCustomerManifesting>> GetAllReturnToCustomerManifesting();
        Task<ReturnToCustomerManifesting> GetReturnToCustomerManifestingById(int id);
        Task<ReturnToCustomerManifesting> CreateReturnToCustomerManifesting(ReturnToCustomerManifesting returnToCustomerManifesting);
        Task<ReturnToCustomerManifesting> UpdateReturnToCustomerManifesting(int id, ReturnToCustomerManifesting returnToCustomerManifesting);
        Task<ReturnToCustomerManifesting> DeleteReturnToCustomerManifesting(int id);
    }
}
