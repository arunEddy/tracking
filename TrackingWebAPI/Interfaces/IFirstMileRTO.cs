using TrackingWebAPI.Models;

namespace TrackingWebAPI.Interfaces
{
    public interface IFirstMileRTO
    {
        Task<IEnumerable<FirstMileRTO>> GetfirstMileRTO();
        Task<FirstMileRTO> GetfirstMileRTOById(int id);
        Task<FirstMileRTO> CreatefirstMileRTO(FirstMileRTO firstMileRTO);
        Task<FirstMileRTO> UpdatefirstMileRTO(int id, FirstMileRTO firstMileRTO);
        Task<FirstMileRTO> DeletefirstMileRTO(int id);
    }
}
