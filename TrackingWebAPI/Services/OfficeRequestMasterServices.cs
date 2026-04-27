using DALCLASS.DBContact;
using Microsoft.EntityFrameworkCore;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;

namespace TrackingWebAPI.Services
{
    public class OfficeRequestMasterServices:IOfficeRequestMaster
    {
        private readonly ApplicationDbContext _context;

        public OfficeRequestMasterServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<OfficeRequestMaster>> GetAllOfficeRequestMaster()
        {
           return await _context.OfficeRequestMasters.Where(x=>x.end_dt==null).ToListAsync();
        }

        public async Task<OfficeRequestMaster> GetOfficeRequestMasterById(int id)
        {
            return await _context.OfficeRequestMasters
                        .FirstOrDefaultAsync(x => x.ORMID == id && x.end_dt==null);
        }
        public async Task<OfficeRequestMaster> CreateOfficeRequestMaster(OfficeRequestMaster officeRequestMaster)
        {
            await _context.OfficeRequestMasters.AddAsync(officeRequestMaster);
            await _context.SaveChangesAsync();
            return officeRequestMaster;
        }

        public async Task<OfficeRequestMaster> UpdateOfficeRequestMaster(int id, OfficeRequestMaster officeRequestMaster)
        {
            var existingData = await _context.OfficeRequestMasters
                                     .FirstOrDefaultAsync(x => x.ORMID == id);



            if (existingData == null)
            {
                throw new Exception("Record not found");
            }

         
            existingData.ReferenceBy = officeRequestMaster.ReferenceBy;
            existingData.ReferenceName = officeRequestMaster.ReferenceName;
            existingData.OfficeName = officeRequestMaster.OfficeName;
            existingData.PlanAdopted = officeRequestMaster.PlanAdopted;
            existingData.AmountPaid = officeRequestMaster.AmountPaid;
            existingData.DepositAmount = officeRequestMaster.DepositAmount;
            existingData.Name = officeRequestMaster.Name;
            existingData.Address = officeRequestMaster.Address;
            existingData.Mobile = officeRequestMaster.Mobile;
            existingData.Phone = officeRequestMaster.Phone;
            existingData.Pincode = officeRequestMaster.Pincode;
            existingData.City = officeRequestMaster.City;
            existingData.State = officeRequestMaster.State;
            existingData.PersonalNo = officeRequestMaster.PersonalNo;
            existingData.EMail = officeRequestMaster.EMail;
            existingData.PANNumber = officeRequestMaster.PANNumber;
            existingData.Document1 = officeRequestMaster.Document1;
            existingData.Document2 = officeRequestMaster.Document2;
            existingData.Document3 = officeRequestMaster.Document3;
            existingData.Document4 = officeRequestMaster.Document4;
            existingData.Document5 = officeRequestMaster.Document5;
            existingData.Document6 = officeRequestMaster.Document6;
            existingData.createdBy = officeRequestMaster.createdBy;
            existingData.status = officeRequestMaster.status;



            _context.OfficeRequestMasters.Update(existingData);
            await _context.SaveChangesAsync();

            return existingData;
        }
       
        public async Task<OfficeRequestMaster> DeleteOfficeRequestMaster(int id)
        {
            var data = await _context.OfficeRequestMasters
                           .FirstOrDefaultAsync(x => x.ORMID == id);
            if (data != null)
            {
                data.end_dt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            return data;
        }

       
    }

}
