using DALCLASS.DBContact;
using DALCLASS.InterfaceModal;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALCLASS.Services
{

    public class CountryServices : ICountry
    {
        private readonly ApplicationDbContext _context;

        public CountryServices(ApplicationDbContext context)
        {
            _context = context;
        }

       
        public List<CountryMaster> GetCountries()
        {
            return _context.CountryMaster.ToList();
        }


        public void _UpdateCountry(CountryMaster _country)
        {
            _context.CountryMaster.Update(_country);
            _context.SaveChanges();
        }

       public void _AddCountry(CountryMaster _country)
        {
            _context.CountryMaster.Add(_country);
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

        

        List<CountryMaster> ICountry.GetCountries()
        {
            return _context.CountryMaster.ToList();
        }
    }
}
