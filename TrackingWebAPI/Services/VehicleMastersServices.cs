using DALCLASS.DBContact;
using Microsoft.EntityFrameworkCore;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;

namespace TrackingWebAPI.Services
{
    public class VehicleMastersServices : IVehicleMasters
    {
        private readonly ApplicationDbContext _context;

        public VehicleMastersServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VehicleMasters>> GetVehicleMasters()
        {
            return await _context.vehicleMasters
      .Where(x => x.EndDate == null)
       .ToListAsync();
        }

        public async Task<VehicleMasters> GetVehicleMastersById(int id)
        {
            return await _context.vehicleMasters
            .Where(x => x.VEMID == id && x.EndDate == null)
            .FirstOrDefaultAsync();
        }
        public async Task<TrackingWebAPI.Models.VehicleMasters> CreateVehicleMasters(VehicleMasters vehicleMaster)
        {
            await _context.vehicleMasters.AddAsync(vehicleMaster);
            await _context.SaveChangesAsync();
            return vehicleMaster;
        }
        public async Task<VehicleMasters> UpdateVehicleMasters(int id, TrackingWebAPI.Models.VehicleMasters vehicleMaster)
        {
            var existingVehicleMaster = await _context.vehicleMasters.FindAsync(id);
            if (existingVehicleMaster != null)
            {
                existingVehicleMaster.VehicleType = vehicleMaster.VehicleType;
                existingVehicleMaster.RunningType = vehicleMaster.RunningType;
                existingVehicleMaster.VehicleNo = vehicleMaster.VehicleNo;
                existingVehicleMaster.ReportingTime = vehicleMaster.ReportingTime;
                existingVehicleMaster.MakeModel = vehicleMaster.MakeModel;
                existingVehicleMaster.Capacity = vehicleMaster.Capacity;
                existingVehicleMaster.OriginOffice = vehicleMaster.OriginOffice;
                existingVehicleMaster.DestinationOffice = vehicleMaster.DestinationOffice;
                existingVehicleMaster.VendorName = vehicleMaster.VendorName;
                existingVehicleMaster.TrackingDeviceID = vehicleMaster.TrackingDeviceID;
                existingVehicleMaster.modifyedBy = vehicleMaster.modifyedBy;
                existingVehicleMaster.ModifyedOn = vehicleMaster.ModifyedOn;
                existingVehicleMaster.IsActive = vehicleMaster.IsActive;
                await _context.SaveChangesAsync();
            }
            return existingVehicleMaster;
        }
        public async Task<VehicleMasters> DeleteVehicleMasters(int id)
        {
            var vehicleMaster = await _context.vehicleMasters.FindAsync(id);
            if (vehicleMaster != null)
            {
                vehicleMaster.EndDate = DateTime.Now.ToString();
                await _context.SaveChangesAsync();
            }
            return vehicleMaster;
        }
    }
}
