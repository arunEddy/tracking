using DALCLASS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackingWebAPI.Interfaces;
using TrackingWebAPI.Models;
using TrackingWebAPI.Services;
using StateMaster = TrackingWebAPI.Models.StateMaster;
using StateServices = TrackingWebAPI.Services.StateServices;

namespace TrackingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly IStateMaster _stateService;

        public StateController(IStateMaster stateService)
        {
            _stateService = stateService;
        }

        [HttpGet]
        public IActionResult GetStates()
        {
            return Ok(_stateService.GetStates());
        }

        [HttpGet("{stateId}")]
        public IActionResult GetStates(int stateId)
        {
            return Ok(_stateService.GetStates(stateId));
        }

        [HttpPost]
        public IActionResult AddState(StateMaster _state)
        {

            _stateService._AddState(_state);
            return Ok("State Added");
        }

        //[HttpPut("{countryId}")]
        //public IActionResult UpdateCountry(int countryId, [FromBody] CountryMaster country)
        //{
        //    _countryService._UpdateCountry(countryId, country);
        //    return Ok("Country Updated");
        //}

        [HttpPut("{stateId}")]
        public IActionResult UpdateState(int stateId,StateMaster _state)
        {
            _stateService._UpdateState(stateId,_state);
            return Ok("State Updated");
        }
        [HttpDelete("{stateId}")]
        public IActionResult DeleteState(int stateId)
        {
            _stateService._DeleteState(stateId);
            return Ok("State Deleted");
        }
    }
}
