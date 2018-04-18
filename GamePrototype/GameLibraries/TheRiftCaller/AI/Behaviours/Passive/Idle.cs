/*
MIT License

Copyright (c) 2016 Duncan Baldwin & Adam Harris

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.AI;
using Engine.AI.Behaviours;
using Engine.Objects.Minds.Interfaces;
using Microsoft.Xna.Framework;
using Engine.Managers;
using TheRiftCaller.Physics;

namespace TheRiftCaller.AI.Behaviours.Passive
{
    public class Idle : AState
    {
        public Idle()
        {
            getSetPersonality = BehaviourM.getInstance.GetPersonality("Passive");
            getSetName = "Idle";
        }

        public override void RunBehaviour(IMindAI mind)
        {
            // Choose a random point nearby
            Vector2 chaseVector = new Vector2(0, 0);

            if (mind.getSetTarget != null && Vector2.Distance(mind.getSetTarget.getSetLocation, mind.getSetEntity.getSetLocation) < 250)
            {
                mind.getSetStateMachine.getSetCurrentState = BehaviourM.getInstance.GetState("ChaseEntity");
            }

            mind.getSetMoveVector = chaseVector;

            //Console.WriteLine("Idle passive... " + mind.getSetID);
        }

        public override void RunBehaviour(IMind mind, Vector2 v)
        {
            // Left
            if (v.X == -1)
            {
                mind.getSetStateMachine.getSetCurrentState = BehaviourM.getInstance.GetState("WalkLeft");
            }
            // Right
            else if (v.X == 1)
            {
                mind.getSetStateMachine.getSetCurrentState = BehaviourM.getInstance.GetState("WalkRight");
            }
            // Down
            else if (v.Y == 1)
            {
                mind.getSetStateMachine.getSetCurrentState = BehaviourM.getInstance.GetState("WalkDown");
            }
            // Up
            else if (v.Y == -1)
            {
                mind.getSetStateMachine.getSetCurrentState = BehaviourM.getInstance.GetState("WalkUp");
            }
            else
            {
                //mind.getSetMoveVector = new Vector2(0, 0);

                if (PhysicsM.getInstance.getSetInputForce > 0)
                {
                    PhysicsM.getInstance.getSetInputForce -= 1.5f;
                }
                //PhysicsM.getInstance.getSetInputForce = 0;
            }

            //Console.WriteLine("Idle passive... " + mind.getSetID);
        }

    }

}
