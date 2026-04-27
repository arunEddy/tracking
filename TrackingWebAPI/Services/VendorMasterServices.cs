using DALCLASS.DBContact;
using Microsoft.EntityFrameworkCore;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;

namespace TrackingWebAPI.Services
{
    public class VendorMasterServices : IVendorMaster
    {
        private readonly ApplicationDbContext _context;

        public VendorMasterServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VendorMasters>> GetVendorMasters()
        {
            return await _context.VendorMasters
           .Where(x => x.EndDate == null || x.EndDate == "")
            .ToListAsync();
        }
        public async Task<VendorMasters> GetVendorMastersById(int id)
        {
            return await _context.VendorMasters
            .Where(x => x.VMID == id && x.EndDate == null || x.EndDate == "")
            .FirstOrDefaultAsync();
        }

        public async Task<VendorMasters> CreateVendorMasters(TrackingWebAPI.Models.VendorMasters vendorMaster)
        {
            await _context.VendorMasters.AddAsync(vendorMaster);
            await _context.SaveChangesAsync();
            return vendorMaster;
        }

        public async Task<VendorMasters> UpdateVendorMasters(int id,VendorMasters vendorMaster)
        {
            var existingVendorMaster = await _context.VendorMasters.FindAsync(id);
            if (existingVendorMaster != null)
            {
                existingVendorMaster.OfficeName = vendorMaster.OfficeName;
                existingVendorMaster.VendorType = vendorMaster.VendorType;
                existingVendorMaster.VendorCode = vendorMaster.VendorCode;
                existingVendorMaster.VendorName = vendorMaster.VendorName;
                existingVendorMaster.GSTIN = vendorMaster.GSTIN;
                existingVendorMaster.Name = vendorMaster.Name;
                existingVendorMaster.Address = vendorMaster.Address;
                existingVendorMaster.Mobile = vendorMaster.Mobile;
                existingVendorMaster.Phone = vendorMaster.Phone;
                existingVendorMaster.EMail = vendorMaster.EMail;
                existingVendorMaster.Pincode = vendorMaster.Pincode;
                existingVendorMaster.City = vendorMaster.City;
                existingVendorMaster.State = vendorMaster.State;
                existingVendorMaster.modifyedBy = vendorMaster.modifyedBy;
                //existingVendorMaster.ModifyedOn = vendorMaster.ModifyedOn;
                existingVendorMaster.IsActive = vendorMaster.IsActive;
                await _context.SaveChangesAsync();
            }
            return existingVendorMaster;
        }

        public async Task<VendorMasters> DeleteVendorMasters(int id)
        {
            var vendorMaster = await _context.VendorMasters.FindAsync(id);
            if (vendorMaster != null)
            {
                vendorMaster.EndDate = DateTime.Now.ToString();
                await _context.SaveChangesAsync();
            }
            return vendorMaster;
        }
    }

}
