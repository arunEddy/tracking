using TrackingWebAPI.Models;

namespace TrackingWebAPI.Interfaces
{
    public interface ICityMaster
    {
        void _AddCity(CityMaster city);
        void _DeleteCity(int cityId);
        void _UpdateCity(int cityId, CityMaster city);
        List<CityMaster> GetCity();
        List<CityMaster> GetCity(int cityId);
    }
}
