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
using Engine.Managers;
using Engine.Objects.Minds.Interfaces;
using Microsoft.Xna.Framework;

namespace TheRiftCaller.AI.Behaviours.Aggressive
{
    public class Attack : AState
    {
        private Vector2 chaseVector;

        public Attack()
        {
            getSetPersonality = BehaviourM.getInstance.GetPersonality("Aggressive");
            getSetName = "Attack";
        }

        public override void RunBehaviour(IMindAI mind)
        {
            chaseVector = Vector2.Subtract(mind.getSetTarget.getSetLocation, mind.getSetEntity.getSetLocation);
            mind.getSetMoveVector = new Vector2(0, 0);

            if (Vector2.Distance(mind.getSetTarget.getSetLocation, mind.getSetEntity.getSetLocation) > 60)
            {
                mind.getSetStateMachine.getSetCurrentState = BehaviourM.getInstance.GetState("ChaseEntity");
            }

            //Console.WriteLine("Attack " + mind.getSetID);
        }
    }
}
