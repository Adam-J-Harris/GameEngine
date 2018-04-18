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
    public class WalkDown : AState
    {
        public WalkDown()
        {
            getSetName = "WalkDown";
        }

        public override void RunBehaviour(IMind mind, Vector2 v)
        {
            // Down
            if (v.Y == 1)
            {
                // Down + Right
                if (v.X == 1)
                {
                    mind.getSetMoveVector = new Vector2(1, 1);
                }
                // Down + Left
                else if (v.X == -1)
                {
                    mind.getSetMoveVector = new Vector2(-1, 1);
                }
                // Down
                else
                {
                    mind.getSetMoveVector = new Vector2(0, 1);
                }

                if (mind.getSetSpeed < 3)
                {
                    PhysicsM.getInstance.getSetInputForce += 1f;
                }
            }
            // Up
            else if (v.Y == -1)
            {
                mind.getSetStateMachine.getSetPreviousState = this;
                mind.getSetStateMachine.getSetCurrentState = BehaviourM.getInstance.GetState("WalkUp");
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
            else
            {
                mind.getSetStateMachine.getSetPreviousState = this;
                mind.getSetStateMachine.getSetCurrentState = BehaviourM.getInstance.GetState("Idle");
            }

            //Console.WriteLine("WalkDown " + mind.getSetID);
        }
    }
}
