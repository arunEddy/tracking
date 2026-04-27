using TrackingWebAPI.Models;

namespace TrackingWebAPI.Interfaces
{
    public interface IVendorMaster
    {
       Task<IEnumerable<TrackingWebAPI.Models.VendorMasters>> GetVendorMasters();
        Task<TrackingWebAPI.Models.VendorMasters> GetVendorMastersById(int id);
        Task<TrackingWebAPI.Models.VendorMasters> CreateVendorMasters(TrackingWebAPI.Models.VendorMasters vendorMaster);
        Task<VendorMasters> UpdateVendorMasters(int id, TrackingWebAPI.Models.VendorMasters vendorMaster);
        Task<VendorMasters> DeleteVendorMasters(int id);
    }
}
