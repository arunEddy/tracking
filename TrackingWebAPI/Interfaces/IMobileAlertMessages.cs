using TrackingWebAPI.Models;

namespace TrackingWebAPI.Interfaces
{
    public interface IMobileAlertMessages
    {
        Task<IEnumerable<MobileAlertMessages>> GetAllMobileAlertMessages();
        Task<MobileAlertMessages> GetMobileAlertMessagesById(int id);
        Task<MobileAlertMessages> CreateMobileAlertMessages(MobileAlertMessages mobileAlertMessages);
        Task<MobileAlertMessages> UpdateMobileAlertMessages(int id, MobileAlertMessages mobileAlertMessages);
        Task<MobileAlertMessages> DeleteMobileAlertMessages(int id);
    }
}
