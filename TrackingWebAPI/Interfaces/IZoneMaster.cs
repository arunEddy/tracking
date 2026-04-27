using TrackingWebAPI.Models;

namespace TrackingWebAPI.Interfaces
{
    public interface IZoneMaster
    {
        void AddZone(ZoneMaster zone);
        void DeleteZone(int zoneId);
        void UpdateZone(int zoneId, ZoneMaster zone);
        List<ZoneMaster> GetZone();
        List<ZoneMaster> GetZone(bool type);
    }
}
