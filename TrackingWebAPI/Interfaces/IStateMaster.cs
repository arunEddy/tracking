using System.Collections.Generic;
using TrackingWebAPI.Models;

namespace TrackingWebAPI.Interfaces
{
    public interface IStateMaster
    {
        void _AddState(StateMaster state);
        void _DeleteState(int stateId);
        void _UpdateState(int stateId,StateMaster state);
        List<StateMaster> GetStates();
        List<StateMaster> GetStates(int stateId);
    }
}
