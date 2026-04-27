using DALCLASS;
using DALCLASS.DBContact;
using DALCLASS.InterfaceModal;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;
using CountryMaster = TrackingWebAPI.Models.CountryMaster;
using ICountry = TrackingWebAPI.Interfaces.ICountry;

namespace TrackingWebAPI.Services
{

    public class CountryServices : TrackingWebAPI.Interfaces.ICountry
    {
        private readonly ApplicationDbContext _context;

        public CountryServices(ApplicationDbContext context)
        {
            _context = context;
        }

       
        public List<CountryMaster> GetCountries()
        {
            // Map DALCLASS.CountryMaster to TrackingWebAPI.Models.CountryMaster
            return _context.CountryMaster
                .Select(x => new CountryMaster
                {
                    CountryId = x.CountryId,
                    CountryName = x.CountryName,
                    isInternational=x.isInternational,
                    IsActive=x.IsActive
                })
                .ToList();
        }


        public void _UpdateCountry(CountryMaster _country)
        {
            // Find the DALCLASS.CountryMaster entity by ID
            var dalCountry = _context.CountryMaster.FirstOrDefault(x => x.CountryId == _country.CountryId);
            if (dalCountry != null)
            {
                // Map properties from TrackingWebAPI.Models.CountryMaster to DALCLASS.CountryMaster
                dalCountry.CountryName = _country.CountryName;
                // Add other property mappings if needed, e.g.:
                 dalCountry.isInternational = _country.isInternational;
                dalCountry.IsActive = _country.IsActive;

                _context.CountryMaster.Update(dalCountry);
                _context.SaveChanges();
            }
        }

        public void _UpdateCountry(int countryId,CountryMaster _country)
        {
            // Find the DALCLASS.CountryMaster entity by ID
            var dalCountry = _context.CountryMaster.FirstOrDefault(x => x.CountryId == countryId);
            if (dalCountry != null)
            {
                // Map properties from TrackingWebAPI.Models.CountryMaster to DALCLASS.CountryMaster
                dalCountry.CountryName = _country.CountryName;
                // Add other property mappings if needed, e.g.:
                dalCountry.isInternational = _country.isInternational;
                dalCountry.IsActive = _country.IsActive;

                _context.CountryMaster.Update(dalCountry);
                _context.SaveChanges();
            }
        }


        public void _AddCountry(CountryMaster _country)
        {
            // Map TrackingWebAPI.Models.CountryMaster to DALCLASS.CountryMaster
            var dalCountry = new CountryMaster
            {
                CountryId = _country.CountryId,
                CountryName = _country.CountryName,
                isInternational = _country.isInternational,
                IsActive = _country.IsActive
            };
            _context.CountryMaster.Add(dalCountry);
            _context.SaveChanges();
        }

      public  void _DeleteCountry(int countryId)
        {
           var country=_context.CountryMaster.FirstOrDefault(x=>x.CountryId== countryId);
            if(country!=null)
            {
                _context.CountryMaster.Remove(country);
                _context.SaveChanges();
            }
        }

        List<DALCLASS.CountryMaster> ICountry.GetCountries()
        {
            // Return DALCLASS.CountryMaster list directly
            return _context.CountryMaster
                .Select(x => new DALCLASS.CountryMaster
                {
                    CountryId = x.CountryId,
                    CountryName = x.CountryName,
                    isInternational = x.isInternational,
                    IsActive = x.IsActive
                })
                .ToList();
        }

        List<DALCLASS.CountryMaster> ICountry.GetCountries(bool type)
        {
            // Return DALCLASS.CountryMaster list directly
            return _context.CountryMaster
                 .Where(x => x.isInternational == type)
                .Select(x => new DALCLASS.CountryMaster
                {
                    CountryId = x.CountryId,
                    CountryName = x.CountryName
                })
                .ToList();


        }



        //List<CountryMaster> GetCountries()
        //{
        //        // Map DALCLASS.CountryMaster to TrackingWebAPI.Models.CountryMaster
        //    return _context.CountryMaster
        //        .Select(x => new CountryMaster
        //        {
        //            CountryId = x.CountryId,
        //            CountryName = x.CountryName,
        //            isInternational=x.isInternational,
        //            IsActive=x.IsActive
        //        })
        //        .ToList();
        //}
    }
}
