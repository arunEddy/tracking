using DALCLASS.DBContact;
using Microsoft.EntityFrameworkCore;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;

namespace TrackingWebAPI.Services
{
    public class AirlineMasterServices : IAirlineMaster
    {
        private readonly ApplicationDbContext _context;


        public AirlineMasterServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AirlineMaster>> GetAllAirlineMaster()
        {
            return await _context.AirlineMasters
      .Where(x => x.end_dt == null)
       .ToListAsync();
        }
        public async Task<AirlineMaster> GetAirlineMasterById(int id)
        {
            return await _context.AirlineMasters.
                Where(x=>x.end_dt==null && x.AIRID==id).FirstOrDefaultAsync();

        }

        public async Task<AirlineMaster> CreateAirlineMaster(TrackingWebAPI.Models.AirlineMaster airlineMaster)
        {
            await _context.AirlineMasters.AddAsync(airlineMaster);
            await _context.SaveChangesAsync();
            return airlineMaster;
        }

        public async Task<AirlineMaster> UpdateAirlineMaster(int id, AirlineMaster airlineMaster)
        {
            var existingAirlineMaster = await _context.AirlineMasters.FindAsync(id);
            if (existingAirlineMaster != null)
            {
                existingAirlineMaster.AirlineCode = airlineMaster.AirlineCode;
                existingAirlineMaster.AirlineName = airlineMaster.AirlineName;
                existingAirlineMaster.Slab1 = airlineMaster.Slab1;
                existingAirlineMaster.Slab2 = airlineMaster.Slab2;
                existingAirlineMaster.Slab3 = airlineMaster.Slab3;
                existingAirlineMaster.Slab4 = airlineMaster.Slab4;
                existingAirlineMaster.Slab5 = airlineMaster.Slab5;
                existingAirlineMaster.createdBy = airlineMaster.createdBy;
                existingAirlineMaster.IsActive = airlineMaster.IsActive;
                await _context.SaveChangesAsync();
            }
            return existingAirlineMaster;
        }

        public async Task<AirlineMaster> DeleteAirlineMaster(int id)
        {

            var airlineMaster = await _context.AirlineMasters.FindAsync(id);
            if (airlineMaster != null)
            {
                airlineMaster.end_dt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            return airlineMaster;
        }
    }

}
