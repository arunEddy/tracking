using DALCLASS.DBContact;
using Microsoft.EntityFrameworkCore;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;

namespace TrackingWebAPI.Services
{
    public class FirstMileRTOServices:IFirstMileRTO
    {
        private readonly ApplicationDbContext _context;

        public FirstMileRTOServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.FirstMileRTO>> GetfirstMileRTO()
        {
            return await _context.firstMileRTO
                           .Where(x => x.EndDate == null || x.EndDate == "")
                            .ToListAsync();
        }

        public async Task<Models.FirstMileRTO> GetfirstMileRTOById(int id)
        {
            return await _context.firstMileRTO
           .Where(x => x.rtoid == id && x.EndDate == null || x.EndDate == "")
           .FirstOrDefaultAsync();
        }

        public async Task<Models.FirstMileRTO> CreatefirstMileRTO(Models.FirstMileRTO FirstMileRTO)
        {
            await _context.firstMileRTO.AddAsync(FirstMileRTO);
            await _context.SaveChangesAsync();
            return FirstMileRTO;
        }

        public async Task<FirstMileRTO> UpdatefirstMileRTO(int id, Models.FirstMileRTO FirstMileRTO)
        {
            var existingdRS = await _context.firstMileRTO.FindAsync(id);
            if (existingdRS != null)
            {
                
                existingdRS.AWBNumber = FirstMileRTO.AWBNumber;
                existingdRS.RTODate = FirstMileRTO.RTODate;
                existingdRS.RTOReason = FirstMileRTO.RTOReason;
                existingdRS.mfdby = FirstMileRTO.mfdby;
                existingdRS.mfdon = FirstMileRTO.mfdon;
                existingdRS.IsActive = FirstMileRTO.IsActive;
                await _context.SaveChangesAsync();
            }
            return existingdRS;
        }

        public async Task<FirstMileRTO> DeletefirstMileRTO(int id)
        {
            var FirstMileRTO = await _context.firstMileRTO.FindAsync(id);
            if (FirstMileRTO != null)
            {
                FirstMileRTO.EndDate = DateTime.Now.ToString();
                await _context.SaveChangesAsync();
            }
            return FirstMileRTO;
        }
    }
}
