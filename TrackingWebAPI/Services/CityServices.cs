using DALCLASS.DBContact;
using System.Text;
using System.Threading.Tasks;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;
using ICityMaster = TrackingWebAPI.Interfaces.ICityMaster;
using CityMaster = TrackingWebAPI.Models.CityMaster;
namespace TrackingWebAPI.Services
{
    public class CityServices : ICityMaster
    {
        private readonly ApplicationDbContext _context;

        public CityServices(ApplicationDbContext context)
        {
            _context = context;
        }



        public List<CityMaster> GetCity()
        {
            var result =
        from c in _context.CityMaster
        join co in _context.CountryMaster
            on c.CountryId equals co.CountryId
        join s in _context.CityMaster
            on c.CityId equals s.CityId
        join z in _context.ZoneMaster
            on c.ZoneId equals z.ZoneId
        select new CityMaster
        {
            CityCode = c.CityCode,
            CityName = c.CityName,
            isInternational = c.isInternational,
            CountryName = co.CountryName,
            ZoneName = z.ZoneName,
           
            IsActive = c.IsActive,
            IsMetroCity = c.IsMetroCity
        };

            return result.ToList();
                    }


        public List<CityMaster> GetCity(int cityId)
        {
            var result =
        (from c in _context.CityMaster
        join co in _context.CountryMaster
            on c.CountryId equals co.CountryId
        join s in _context.CityMaster
            on c.CityId equals s.CityId
        join z in _context.ZoneMaster
            on c.ZoneId equals z.ZoneId
        where c.CityId == cityId
        select new CityMaster
        {
            CityCode = c.CityCode,
            CityName = c.CityName,
            isInternational = c.isInternational,
            CountryName = co.CountryName,
            ZoneName = z.ZoneName,
            
            IsActive = c.IsActive,
            IsMetroCity = c.IsMetroCity
        }).FirstOrDefault();
            

            return result != null ? new List<CityMaster> { result } : new List<CityMaster>();
        }


       
        public void _UpdateCity(int cityId, CityMaster _city)
        {
            // Find the DALCLASS.CountryMaster entity by ID
            var dalCity = _context.CityMaster.FirstOrDefault(x => x.CityId == cityId);
            if (dalCity != null)
            {
                // Map properties from TrackingWebAPI.Models.CountryMaster to DALCLASS.CountryMaster
                dalCity.CountryId = _city.CountryId;
                dalCity.isInternational = _city.isInternational;
                dalCity.CityCode = _city.CityCode;
                dalCity.CityName = _city.CityName;
                dalCity.IsMetroCity = _city.IsMetroCity;    
                dalCity.CountryName = _city.CountryName;
                dalCity.ZoneName = _city.ZoneName;
                dalCity.StateName = _city.StateName;

                dalCity.IsActive = _city.IsActive;

                _context.CityMaster.Update(dalCity);
                _context.SaveChanges();
            }
        }

        public void _AddCity(CityMaster _city)
        {
            // Map TrackingWebAPI.Models.CountryMaster to DALCLASS.CountryMaster
            var dalCity = new CityMaster
            {
                CountryId = _city.CountryId,
             isInternational = _city.isInternational,
             CityId = _city.CityId,
             StateId = _city.StateId,
             StateName = _city.StateName,
                ZoneId = _city.ZoneId,
                ZoneName = _city.ZoneName,
                CityCode = _city.CityCode,
                CityName = _city.CityName,
                IsMetroCity = _city.IsMetroCity,

                IsActive = _city.IsActive
            };

            _context.CityMaster.Add(dalCity);
            _context.SaveChanges();
        }

        public void _DeleteCity(int cityId)
        {
            var state = _context.CityMaster.FirstOrDefault(x => x.CityId == cityId);

            if (state != null)
            {
                _context.CityMaster.Remove(state);
                _context.SaveChanges();
            }
        }



    }
}