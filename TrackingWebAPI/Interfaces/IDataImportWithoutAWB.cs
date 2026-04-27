using TrackingWebAPI.Models;

namespace TrackingWebAPI.Interfaces
{
    public interface IDataImportWithoutAWB
    {
        Task<IEnumerable<DataImportWithoutAWB>> GetAllDataImportWithoutAWB();
        Task<DataImportWithoutAWB> GetDataImportWithoutAWBById(int id);
        Task<DataImportWithoutAWB> CreateDataImportWithoutAWB(DataImportWithoutAWB DataImportWithoutAWB);
        Task<DataImportWithoutAWB> UpdateDataImportWithoutAWB(int id, DataImportWithoutAWB DataImportWithoutAWB);
        Task<DataImportWithoutAWB> DeleteDataImportWithoutAWB(int id);
    }
}
