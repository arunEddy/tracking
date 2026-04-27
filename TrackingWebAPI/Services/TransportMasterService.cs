using DALCLASS.DBContact;
using Microsoft.EntityFrameworkCore;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;

namespace TrackingWebAPI.Services
{

    public class TransportMasterService : ITransportMaster
    {
        private readonly ApplicationDbContext _context;

        public TransportMasterService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TransportMaster>> GetTransportMasters()
        {
            //return await _context.TransportMasters.ToListAsync();
            return await _context.TransportMasters
            .Where(x => x.EndDate == null)
             .ToListAsync();
        }

        public async Task<TransportMaster> GetTransportMastersById(int id)
        {
            //return await _context.TransportMasters.FindAsync(id);
            return await _context.TransportMasters
            .Where(x => x.TMID == id && x.EndDate == null)
            .FirstOrDefaultAsync();
        }

        public async Task<TransportMaster> CreateTransportMasters(TransportMaster transportMaster)
        {
            await _context.TransportMasters.AddAsync(transportMaster);
            await _context.SaveChangesAsync();
            return transportMaster;
        }

        public async Task<TransportMaster> UpdateTransportMasters(int id, TransportMaster transportMaster)
        {
            var existingTransportMaster = await _context.TransportMasters.FindAsync(id);
            if (existingTransportMaster != null)
            {
               
                existingTransportMaster.OriginOffice = transportMaster.OriginOffice;
                existingTransportMaster.DestinationOffice = transportMaster.DestinationOffice;
                existingTransportMaster.ModeType = transportMaster.ModeType;
                existingTransportMaster.AirlineName = transportMaster.AirlineName;
                existingTransportMaster.ModeName = transportMaster.ModeName;
                existingTransportMaster.ServiceType  = transportMaster.ServiceType;
                existingTransportMaster.DepartureTime = transportMaster.DepartureTime;
                existingTransportMaster.ArrivalTime = transportMaster.ArrivalTime;
                existingTransportMaster.ExpectedTransitDay = transportMaster.ExpectedTransitDay;
                existingTransportMaster.ExpReachingTimeatHub = transportMaster.ExpReachingTimeatHub;
                existingTransportMaster.IsActive = transportMaster.IsActive;
                existingTransportMaster.modifiedBy = transportMaster.modifiedBy;
                existingTransportMaster.modifieddate = transportMaster.modifieddate;
                await _context.SaveChangesAsync();
            }
            return existingTransportMaster;
        }

        public async Task<TransportMaster> DeleteTransportMasters(int id)
        {
            var transportMaster = await _context.TransportMasters.FindAsync(id);
            if (transportMaster != null)
            {
                transportMaster.EndDate = DateTime.Now.ToString();   
                await _context.SaveChangesAsync();
               
            }
            return transportMaster;
        }
    }
}
