using DALCLASS;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//CountryMaster=TrackingWebAPI.Models.CountryMaster;

namespace TrackingWebAPI.Interfaces
{
   public  interface ICountry
    {
        void _AddCountry(TrackingWebAPI.Models.CountryMaster _country);
        void _DeleteCountry(int countryId);
        //string _GetCountry(CountryMaster _country);
        void _UpdateCountry(int countryId,TrackingWebAPI.Models.CountryMaster _country);
        List<CountryMaster> GetCountries();

        List<CountryMaster> GetCountries(bool type);

    }
}
