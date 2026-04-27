using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALCLASS.InterfaceModal
{
   public  interface ICountry
    {
        void _AddCountry(CountryMaster _country);
        void _DeleteCountry(int countryId);
        //string _GetCountry(CountryMaster _country);
        void _UpdateCountry(CountryMaster _country);
        List<CountryMaster> GetCountries();

    }
}
