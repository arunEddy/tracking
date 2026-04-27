using DALCLASS;
using DALCLASS.DBContact;
using DALCLASS.InterfaceModal;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;
using IStateMaster = TrackingWebAPI.Interfaces.IStateMaster;
using StateMaster = TrackingWebAPI.Models.StateMaster;

namespace TrackingWebAPI.Services
{
    public class StateServices : IStateMaster
    {
        private readonly ApplicationDbContext _context;

        public StateServices(ApplicationDbContext context)
        {
            _context = context;
        }



        public List<StateMaster> GetStates()
        {
            // Map DALCLASS.CountryMaster to TrackingWebAPI.Models.CountryMaster
            //StateId, CountryId, StateType, StateCode, StateName, GSTIN, IsActive
            //return _context.StateMaster
            //    .Select(x => new StateMaster
            //    {
            //        StateId = x.StateId,
            //        CountryId = x.CountryId,
            //        StateType = x.StateType,
            //        StateCode = x.StateCode,
            //        StateName = x.StateName,
            //        GSTIN = x.GSTIN,
            //        IsActive = x.IsActive
            //    })
            //    .ToList();
            return _context.StateMaster
               .Join(_context.CountryMaster,
                   s => s.CountryId,
                   c => c.CountryId,
                   (s, c) => new StateMaster
                   {
                       StateId = s.StateId,
                       CountryId = s.CountryId,
                       StateType = s.StateType,
                       StateCode = s.StateCode,
                       StateName = s.StateName,
                      // GSTIN = s.GSTIN,
                       IsActive = s.IsActive,
                       isInternational = c.isInternational,
                       CountryName = c.CountryName
                       // Add additional properties if needed
                   })
               .ToList();
        }


        public List<StateMaster> GetStates(int stateId)
        {
            var state = (from s in _context.StateMaster
                         join c in _context.CountryMaster
                         on s.CountryId equals c.CountryId
                         where s.StateId == stateId
                         select new StateMaster
                         {
                             StateId = s.StateId,
                             CountryId = s.CountryId,
                            CountryName = c.CountryName,
                             isInternational = c.isInternational,
                             StateType = s.StateType,
                             StateCode = s.StateCode,
                             StateName = s.StateName,
                           //  GSTIN = s.GSTIN,
                             IsActive = s.IsActive
                         }).FirstOrDefault();

            return state != null ? new List<StateMaster> { state } : new List<StateMaster>();
        }


        public List<StateMaster> GetStatesWithCountry()
        {
            return _context.StateMaster
                .Join(_context.CountryMaster,
                    s => s.CountryId,
                    c => c.CountryId,
                    (s, c) => new StateMaster
                    {
                        StateId = s.StateId,
                        CountryId = s.CountryId,
                        StateType = s.StateType,
                        StateCode = s.StateCode,
                        StateName = s.StateName,
                       // GSTIN = s.GSTIN,
                        IsActive = s.IsActive,
                        isInternational = c.isInternational,
                        CountryName = c.CountryName
                        // Add additional properties if needed
                    })
                .ToList();
        }
        public void _UpdateState(int stateId, StateMaster _state)
        {
            // Find the DALCLASS.CountryMaster entity by ID
            var dalState = _context.StateMaster.FirstOrDefault(x => x.StateId == stateId);
            if (dalState != null)
            {
                // Map properties from TrackingWebAPI.Models.CountryMaster to DALCLASS.CountryMaster
                dalState.CountryId = _state.CountryId;
                dalState.StateType = _state.StateType;
                dalState.StateCode = _state.StateCode;
                dalState.StateName = _state.StateName;
              //  dalState.GSTIN = _state.GSTIN;
                dalState.IsActive = _state.IsActive;

                _context.StateMaster.Update(dalState);
                _context.SaveChanges();
            }
        }

        public void _AddState(StateMaster _state)
        {
            // Map TrackingWebAPI.Models.CountryMaster to DALCLASS.CountryMaster
            var dalState = new StateMaster
            {
                CountryId = _state.CountryId,
                StateType = _state.StateType,
                StateCode = _state.StateCode,
                StateName = _state.StateName,
                //GSTIN = _state.GSTIN,
                IsActive = _state.IsActive
            };

            _context.StateMaster.Add(dalState);
            _context.SaveChanges();
        }

        public void _DeleteState(int stateId)
        {
           var state = _context.StateMaster.FirstOrDefault(x => x.StateId == stateId);
           
            if (state != null)
            {
                _context.StateMaster.Remove(state);
                _context.SaveChanges();
            }
        }



        //List<StateMaster> GetStates()
        //{
        //    // Map DALCLASS.CountryMaster to TrackingWebAPI.Models.CountryMaster
        //    return _context.CountryMaster
        //        .Select(x => new CountryMaster
        //        {
        //            CountryId = x.CountryId,
        //            CountryName = x.CountryName,
        //            CountryType = x.CountryType,
        //            IsActive = x.IsActive
        //        })
        //        .ToList();
        //}
        //}
    }
}