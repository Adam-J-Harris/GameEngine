using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.AI.Behaviours.Interfaces;
using Engine.Objects.Minds.Interfaces;

namespace Engine.AI.StateMachine.Interfaces
{
    public interface IFSM
    {
        void ApplyStates();

        void ApplyStates(IList<IState> list);

        IState getSetCurrentState { get; set; }
        IState getSetPreviousState { get; set; }
    }
}
