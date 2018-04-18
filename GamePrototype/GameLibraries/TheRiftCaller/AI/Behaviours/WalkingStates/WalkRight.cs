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
    public class WalkRight : AState
    {
        public WalkRight()
        {
            getSetName = "WalkRight";
        }

        public override void RunBehaviour(IMind mind, Vector2 v)
        {
            // Right
            if (v.X == 1)
            {
                // Right + Down
                if (v.Y == 1)
                {
                    mind.getSetMoveVector = new Vector2(1, 1);
                }
                // Right + Up
                else if (v.Y == -1)
                {
                    mind.getSetMoveVector = new Vector2(1, -1);
                }
                // Right
                else
                {
                    mind.getSetMoveVector = new Vector2(1, 0);
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

            //Console.WriteLine("WalkRight " + mind.getSetID);
        }
    }
}
