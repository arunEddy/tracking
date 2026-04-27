using TrackingWebAPI.Models;

namespace TrackingWebAPI.Interfaces
{
    public interface IAWBPrint
    {
        Task<IEnumerable<AWBPrint>> GetAllAWBPrint();
        Task<AWBPrint> GetAWBPrintById(int id);
        Task<AWBPrint> CreateAWBPrint(AWBPrint AWBPrint);
        Task<AWBPrint> UpdateAWBPrint(int id, AWBPrint AWBPrint);
        Task<AWBPrint> DeleteAWBPrint(int id);
    }
}
