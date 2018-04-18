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
    public class WalkLeft : AState
    {
        public WalkLeft()
        {
            getSetName = "WalkLeft";
        }

        public override void RunBehaviour(IMind mind, Vector2 v)
        {
            // Left
            if (v.X == -1)
            {
                // Left + Down
                if (v.Y == 1)
                {
                    mind.getSetMoveVector = new Vector2(-1, 1);
                }
                // Left + Up
                else if (v.Y == -1)
                {
                    mind.getSetMoveVector = new Vector2(-1, -1);
                }
                // Left
                else
                {
                    mind.getSetMoveVector = new Vector2(-1, 0);
                }

                if (mind.getSetSpeed < 3)
                {
                    PhysicsM.getInstance.getSetInputForce += 1f;
                }
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
            // Up
            else if (v.Y == -1)
            {
                mind.getSetStateMachine.getSetPreviousState = this;
                mind.getSetStateMachine.getSetCurrentState = BehaviourM.getInstance.GetState("WalkUp");
            }
            else
            {
                mind.getSetStateMachine.getSetPreviousState = this;
                mind.getSetStateMachine.getSetCurrentState = BehaviourM.getInstance.GetState("Idle");
            }

            //Console.WriteLine("WalkLeft " + mind.getSetID);
        }
    }
}
