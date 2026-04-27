using DALCLASS.DBContact;
using Microsoft.EntityFrameworkCore;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;

namespace TrackingWebAPI.Services
{
    public class DeliveryBoyMastersServices:IDeliveryBoyMasters
    {
        private readonly ApplicationDbContext _context;

        public DeliveryBoyMastersServices(ApplicationDbContext deliveryBoyMastersRepository)
        {
            _context = deliveryBoyMastersRepository;
        }

        public async Task<IEnumerable<DeliveryBoyMasters>> GetAllDeleveryBoyMasters()
        {
            return await _context.deliveryBoyMasters.Where(x=>x.EndDate==null || x.EndDate=="").ToListAsync();

           
        }
        public async Task<DeliveryBoyMasters> GetDeleveryBoyMastersById(int id)
        {
            return await _context.deliveryBoyMasters.Where(x=>x.DBMID==id && x.EndDate== null || x.EndDate == "").FirstOrDefaultAsync();

        }

        public async Task<DeliveryBoyMasters> CreateDeleveryBoyMasters(TrackingWebAPI.Models.DeliveryBoyMasters airlineMaster)
        {
            await _context.deliveryBoyMasters.AddAsync(airlineMaster);
            await _context.SaveChangesAsync();
            return airlineMaster;
        }

        public async Task<DeliveryBoyMasters> UpdateDeleveryBoyMasters(int id, DeliveryBoyMasters airlineMaster)
        {
            var existingdeliveryBoyMasters = await _context.deliveryBoyMasters.FindAsync(id);
            if (existingdeliveryBoyMasters != null)
            {
                existingdeliveryBoyMasters.OfficeName = airlineMaster.OfficeName;
                existingdeliveryBoyMasters.BoyCode = airlineMaster.BoyCode;
                existingdeliveryBoyMasters.BoyName = airlineMaster.BoyName;
                existingdeliveryBoyMasters.MobileNo = airlineMaster.MobileNo;
                existingdeliveryBoyMasters.EmployeeName = airlineMaster.EmployeeName;
                existingdeliveryBoyMasters.AllowMobileApplication = airlineMaster.AllowMobileApplication;
                existingdeliveryBoyMasters.SIMNo = airlineMaster.SIMNo;
                existingdeliveryBoyMasters.modifyedBy = airlineMaster.modifyedBy;
                existingdeliveryBoyMasters.ModifyedOn = airlineMaster.ModifyedOn;
                existingdeliveryBoyMasters.IsActive = airlineMaster.IsActive;
                await _context.SaveChangesAsync();
            }
            return existingdeliveryBoyMasters;
        }

        public async Task<DeliveryBoyMasters> DeleteDeleveryBoyMasters(int id)
        {
            var deliveryBoyMasters = await _context.deliveryBoyMasters.FindAsync(id);
            if (deliveryBoyMasters != null)
            {
                deliveryBoyMasters.EndDate = DateTime.Now.ToString();
                await _context.SaveChangesAsync();
            }
            return deliveryBoyMasters;
        }
    }
}
