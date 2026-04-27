using DALCLASS.DBContact;
using Microsoft.EntityFrameworkCore;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;

namespace TrackingWebAPI.Services
{
    public class DataImportWithoutAWBServices:IDataImportWithoutAWB
    {
        private readonly ApplicationDbContext _context;

        public DataImportWithoutAWBServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.DataImportWithoutAWB>> GetAllDataImportWithoutAWB()
        {
            return await _context.dataImportWithoutAWB
                           .Where(x => x.EndDate == null || x.EndDate == "")
                            .ToListAsync();
        }

        public async Task<Models.DataImportWithoutAWB> GetDataImportWithoutAWBById(int id)
        {
            return await _context.dataImportWithoutAWB
           .Where(x => x.eimwId == id && x.EndDate == null || x.EndDate == "")
           .FirstOrDefaultAsync();
        }

        public async Task<Models.DataImportWithoutAWB> CreateDataImportWithoutAWB(Models.DataImportWithoutAWB dataImportWithoutAWB)
        {
            await _context.dataImportWithoutAWB.AddAsync(dataImportWithoutAWB);
            await _context.SaveChangesAsync();
            return dataImportWithoutAWB;
        }

        public async Task<DataImportWithoutAWB> UpdateDataImportWithoutAWB(int id, Models.DataImportWithoutAWB dataImportWithoutAWB)
        {
            var existingdataImportWithoutAWB = await _context.dataImportWithoutAWB.FindAsync(id);
            if (existingdataImportWithoutAWB != null)
            {
                existingdataImportWithoutAWB.OfficeName = dataImportWithoutAWB.OfficeName;
                existingdataImportWithoutAWB.ShipperName = dataImportWithoutAWB.ShipperName;
                existingdataImportWithoutAWB.BookingDate = dataImportWithoutAWB.BookingDate;
                existingdataImportWithoutAWB.ConsigneeName = dataImportWithoutAWB.ConsigneeName;
                existingdataImportWithoutAWB.Shipperfile = dataImportWithoutAWB.Shipperfile;
                existingdataImportWithoutAWB.mdfby = dataImportWithoutAWB.mdfby;
                existingdataImportWithoutAWB.mdfon = dataImportWithoutAWB.mdfon;
                existingdataImportWithoutAWB.IsActive = dataImportWithoutAWB.IsActive;
                await _context.SaveChangesAsync();
            }
            return existingdataImportWithoutAWB;
        }

        public async Task<DataImportWithoutAWB> DeleteDataImportWithoutAWB(int id)
        {
            var customerDataUpdateAWB = await _context.dataImportWithoutAWB.FindAsync(id);
            if (customerDataUpdateAWB != null)
            {
                customerDataUpdateAWB.EndDate = DateTime.Now.ToString();
                await _context.SaveChangesAsync();
            }
            return customerDataUpdateAWB;
        }
    }
}
