using DALCLASS.DBContact;
using DALCLASS;
using DALCLASS.InterfaceModal;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;
using ZonrMaster = TrackingWebAPI.Models.ZoneMaster;
using IZoneMaster = TrackingWebAPI.Interfaces.IZoneMaster;

namespace TrackingWebAPI.Services
{
    public class ZoneServices  : TrackingWebAPI.Interfaces.IZoneMaster
    {
        private readonly ApplicationDbContext _context;

        public ZoneServices(ApplicationDbContext context)
        {
            _context = context;
        }

       
        public List<ZoneMaster> GetZone()
        {
            // Map DALCLASS.ZoneMaster to TrackingWebAPI.Models.ZoneMaster
            return _context.ZoneMaster
                .Select(x => new ZoneMaster
                {
                    ZoneId = x.ZoneId,
                    ZoneName = x.ZoneName,
                    isInternational=x.isInternational,
                    IsActive=x.IsActive
                })
                .ToList();
        }


        public void UpdateZone(ZoneMaster _zone)
        {
            // Find the DALCLASS.ZoneMaster entity by ID
            var dalZone = _context.ZoneMaster.FirstOrDefault(x => x.ZoneId == _zone.ZoneId);
            if (dalZone != null)
            {
                // Map properties from TrackingWebAPI.Models.ZoneMaster to DALCLASS.ZoneMaster
                dalZone.ZoneName = _zone.ZoneName;
                // Add other property mappings if needed, e.g.:
                 dalZone.isInternational = _zone.isInternational;
                dalZone.IsActive = _zone.IsActive;

                _context.ZoneMaster.Update(dalZone);
                _context.SaveChanges();
            }
        }

        public void UpdateZone(int zoneId,ZoneMaster _zone)
        {
            // Find the DALCLASS.ZoneMaster entity by ID
            var dalZone = _context.ZoneMaster.FirstOrDefault(x => x.ZoneId == zoneId);
            if (dalZone != null)
            {
                // Map properties from TrackingWebAPI.Models.ZoneMaster to DALCLASS.ZoneMaster
                dalZone.ZoneName = _zone.ZoneName;
                // Add other property mappings if needed, e.g.:
                dalZone.isInternational = _zone.isInternational;
                dalZone.IsActive = _zone.IsActive;

                _context.ZoneMaster.Update(dalZone);
                _context.SaveChanges();
            }
        }


        public void AddZone(ZoneMaster _zone)
        {
            // Map TrackingWebAPI.Models.ZoneMaster to DALCLASS.ZoneMaster
            var dalZone = new ZoneMaster
            {
                ZoneId = _zone.ZoneId,
                ZoneName = _zone.ZoneName,
                isInternational = _zone.isInternational,
                IsActive = _zone.IsActive
            };
            _context.ZoneMaster.Add(dalZone);
            _context.SaveChanges();
        }

      public  void DeleteZone(int zoneId)
        {
           var zone=_context.ZoneMaster.FirstOrDefault(x=>x.ZoneId== zoneId);
            if(zone!=null)
            {
                _context.ZoneMaster.Remove(zone);
                _context.SaveChanges();
            }
        }

        List<ZoneMaster> IZoneMaster.GetZone()
        {
            // Return DALCLASS.ZoneMaster list directly
            return _context.ZoneMaster
                .Select(x => new ZoneMaster
                {
                    ZoneId = x.ZoneId,
                    ZoneName = x.ZoneName,
                    isInternational=x.isInternational,
                    IsActive = x.IsActive
                })
                .ToList();

        }

        List<ZoneMaster> IZoneMaster.GetZone(bool type)
        {
            // Return DALCLASS.ZoneMaster list directly
            return _context.ZoneMaster
                 .Where(x => x.isInternational == type)
                .Select(x => new ZoneMaster
                {
                    ZoneId = x.ZoneId,
                    ZoneName = x.ZoneName,
                    isInternational = x.isInternational,
                    IsActive = x.IsActive
                })
                .ToList();


        }



        //List<ZoneMaster> GetCountries()
        //{
        //        // Map DALCLASS.ZoneMaster to TrackingWebAPI.Models.ZoneMaster
        //    return _context.ZoneMaster
        //        .Select(x => new ZoneMaster
        //        {
        //            ZoneId = x.ZoneId,
        //            ZoneName = x.ZoneName,
        //            isInternational=x.isInternational,
        //            IsActive=x.IsActive
        //        })
        //        .ToList();
        //}
    }
}
