using TrackingWebAPI.Models;

namespace TrackingWebAPI.Interfaces
{
    public interface IUrgentDeliveryAlertEntry
    {
        Task<IEnumerable<UrgentDeliveryAlertEntry>> GetAllUrgentDeliveryAlertEntry();
        Task<UrgentDeliveryAlertEntry> GetUrgentDeliveryAlertEntryById(int id);
        Task<UrgentDeliveryAlertEntry> CreateUrgentDeliveryAlertEntry(UrgentDeliveryAlertEntry UrgentDeliveryAlertEntry);
        Task<UrgentDeliveryAlertEntry> UpdateUrgentDeliveryAlertEntry(int id, UrgentDeliveryAlertEntry UrgentDeliveryAlertEntry);
        Task<UrgentDeliveryAlertEntry> DeleteUrgentDeliveryAlertEntry(int id);
    }
}
