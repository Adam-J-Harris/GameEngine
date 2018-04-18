using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.AI.Behaviours;
using Engine.Managers;
using Engine.Objects.Minds.Interfaces;
using Microsoft.Xna.Framework;
using TheRiftCaller.Physics;

namespace TheRiftCaller.AI.Behaviours.WalkingStates
{
    public class WalkUp : AState
    {
        public WalkUp()
        {
            getSetName = "WalkUp";
        }

        public override void RunBehaviour(IMind mind, Vector2 v)
        {
            // Up
            if (v.Y == -1)
            {
                // Up + Right
                if (v.X == 1)
                {
                    mind.getSetMoveVector = new Vector2(1, -1);
                }
                // Up + Left
                else if (v.X == -1)
                {
                    mind.getSetMoveVector = new Vector2(-1, -1);
                }
                // Up
                else
                {
                    mind.getSetMoveVector = new Vector2(0, -1);
                }

                if (mind.getSetSpeed < 3)
                {
                    PhysicsM.getInstance.getSetInputForce += 1f;
                }
            }
            // Left
            else if (v.X == -1)
            {
                mind.getSetStateMachine.getSetPreviousState = this;
                mind.getSetStateMachine.getSetCurrentState = BehaviourM.getInstance.GetState("WalkLeft");
            }
            // Right
            else if (v.X == 1)
            {
                mind.getSetStateMachine.getSetPreviousState = this;
                mind.getSetStateMachine.getSetCurrentState = BehaviourM.getInstance.GetState("WalkRight");
            }
            // Down
            else if (v.Y == 1)
            {
                mind.getSetStateMachine.getSetPreviousState = this;
                mind.getSetStateMachine.getSetCurrentState = BehaviourM.getInstance.GetState("WalkDown");
            }
            else
            {
                mind.getSetStateMachine.getSetPreviousState = this;
                mind.getSetStateMachine.getSetCurrentState = BehaviourM.getInstance.GetState("Idle");
            }

            //Console.WriteLine("WalkUp " + mind.getSetID);
        }
    }
}