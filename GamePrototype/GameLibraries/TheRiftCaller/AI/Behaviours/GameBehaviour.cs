using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Managers;
using TheRiftCaller.AI.Behaviours.Aggressive;
using TheRiftCaller.AI.Behaviours.Defensive;
using TheRiftCaller.AI.Behaviours.Passive;
using Engine.AI.Behaviours.Interfaces;
using Engine.AI;
using Engine.AI.StateMachine;
using TheRiftCaller.AI.Behaviours.WalkingStates;

namespace TheRiftCaller.AI.Behaviours
{
    public class GameBehaviour
    {
        public GameBehaviour()
        {
            BehaviourM.getInstance.CreatePersonality<AggPersonality>();
            BehaviourM.getInstance.CreatePersonality<PasPersonality>();
            BehaviourM.getInstance.CreatePersonality<DefPersonality>();

            BehaviourM.getInstance.Create<Attack>();
            BehaviourM.getInstance.Create<ChaseEntity>();

            BehaviourM.getInstance.Create<Retreat>();

            BehaviourM.getInstance.Create<Idle>();
            BehaviourM.getInstance.Create<MoveToEntity>();
            BehaviourM.getInstance.Create<UseSmartEntity>();

            BehaviourM.getInstance.Create<WalkRight>();
            BehaviourM.getInstance.Create<WalkLeft>();
            BehaviourM.getInstance.Create<WalkUp>();
            BehaviourM.getInstance.Create<WalkDown>();
        }
    }
}
