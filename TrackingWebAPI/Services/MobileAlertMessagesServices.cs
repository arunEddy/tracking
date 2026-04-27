using DALCLASS.DBContact;
using Microsoft.EntityFrameworkCore;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;

namespace TrackingWebAPI.Services
{
    public class MobileAlertMessagesServices : IMobileAlertMessages
    {

        private readonly ApplicationDbContext _context;

        public MobileAlertMessagesServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MobileAlertMessages>> GetAllMobileAlertMessages()
        {
            return await _context.mobileAlertMessages
       .Where(x => x.EndDate == null)
        .ToListAsync();
        }
        public async Task<MobileAlertMessages> GetMobileAlertMessagesById(int id)
        {
            return await _context.mobileAlertMessages
           .Where(x => x.MAMID == id && x.EndDate == null)
           .FirstOrDefaultAsync();
        }
        public async Task<MobileAlertMessages> CreateMobileAlertMessages(MobileAlertMessages mobileAlertMessages)
        {
            await _context.mobileAlertMessages.AddAsync(mobileAlertMessages);
            await _context.SaveChangesAsync();
            return mobileAlertMessages;
        }
        public async Task<MobileAlertMessages> UpdateMobileAlertMessages(int id, MobileAlertMessages mobileAlertMessages)
        {
            var existingMobileAlertMessages = await _context.mobileAlertMessages.FindAsync(id);
            if (existingMobileAlertMessages != null)
            {
                existingMobileAlertMessages.OfficeName = mobileAlertMessages.OfficeName;
                existingMobileAlertMessages.BoyName = mobileAlertMessages.BoyName;
                existingMobileAlertMessages.MessageType = mobileAlertMessages.MessageType;
                existingMobileAlertMessages.TextMessage = mobileAlertMessages.TextMessage;
                existingMobileAlertMessages.UploadImage = mobileAlertMessages.UploadImage;
                existingMobileAlertMessages.modifyedBy = mobileAlertMessages.modifyedBy;
                existingMobileAlertMessages.ModifyedOn = mobileAlertMessages.ModifyedOn;
                existingMobileAlertMessages.IsActive = mobileAlertMessages.IsActive;
                await _context.SaveChangesAsync();
            }
            return existingMobileAlertMessages;
        }

        public async Task<MobileAlertMessages> DeleteMobileAlertMessages(int id)
        {
            var mobileAlertMessages = await _context.mobileAlertMessages.FindAsync(id);
            if (mobileAlertMessages != null)
            {
                mobileAlertMessages.EndDate = DateTime.Now.ToString();
                await _context.SaveChangesAsync();
            }
            return mobileAlertMessages;
        }
    }

}