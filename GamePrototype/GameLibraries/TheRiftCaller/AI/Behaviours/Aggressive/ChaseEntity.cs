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
using TheRiftCaller.Managers;
using Engine.AI.Pathfinding.Astar;
using Engine.AI.Pathfinding;

namespace TheRiftCaller.AI.Behaviours.Aggressive
{
    public class ChaseEntity : AState
    {
        private Vector2 chaseVector;
        private List<Point> path = new List<Point>();
        private int nodeNumber;

        private bool once = false;
        private bool newAstar = true;
        private bool rayHit = false;

        public ChaseEntity()
        {
            getSetPersonality = BehaviourM.getInstance.GetPersonality("Aggressive");
            getSetName = "ChaseEntity";
        }

        public override void RunBehaviour(IMindAI mind)
        {
            // Cast ray to target
            // if false
            // then use vectors
            // else use A*

            if (!rayHit && !RayM.getInstance.CastRayToPlayer(mind.getSetLocation))
            {
                rayHit = true;
            }
            else
            {
                rayHit = false;
            }

            if (rayHit)
            {
                if (path.Count > 0)
                {
                    path.Clear();
                    nodeNumber = 0;
                }

                chaseVector = Vector2.Subtract(mind.getSetTarget.getSetLocation, mind.getSetEntity.getSetLocation);
            }
            else
            {
                if (path.Count == 0)
                {
                    // Can't Reach The Player Entity

                    //Console.WriteLine("Using A* - ChaseEntity");
                    //Point start = new Point((int)mind.getSetLocation.X / 64, (int)mind.getSetLocation.Y / 64);
                    //Point end = new Point((int)mind.getSetTarget.getSetLocation.X / 64, (int)mind.getSetTarget.getSetLocation.Y / 64);
                    Point start = mind.getSetEntity.getSetPointLocation;
                    Point end = mind.getSetTarget.getSetPointLocation;
                    SearchParameters sp = new SearchParameters(start, end);

                    APathfinder ap = new APathfinder(sp);
                    path = ap.FindPath();
                    ap.ResetNodes();
                    nodeNumber = 0;

                    once = false;
                    //newAstar = false;
                }
                else
                {
                    //Point current = new Point((int)mind.getSetEntity.getSetPointLocation.X, (int)mind.getSetEntity.getSetPointLocation.Y);
                    if (nodeNumber >= path.Count)
                    {
                        path.Clear();
                        newAstar = true;
                    }
                    else
                    {
                        if (mind.getSetEntity.getSetPointLocation == path[nodeNumber])
                        {
                            nodeNumber++;

                            once = false;


                        }
                        else if (!once)
                        {
                            Vector2 pathNode = new Vector2((path[nodeNumber].X * 64) - 20, (path[nodeNumber].Y * 64) - 20);
                            //Vector2 direction = mind.getSetEntity.getSetLocation - pathNode;
                            chaseVector = pathNode - mind.getSetEntity.getSetLocation;
                            //chaseVector.Normalize();

                            

                            once = true;
                        }
                    }

                    //chaseVector = Vector2.Subtract(mind.getSetTarget.getSetLocation, mind.getSetEntity.getSetLocation);

                    

                    //Console.WriteLine("ChaseEntity aggressive... " + mind.getSetID);
                }
            }

            if (mind.getSetTarget != null && Vector2.Distance(mind.getSetTarget.getSetLocation, mind.getSetEntity.getSetLocation) < 50)
            {
                mind.getSetStateMachine.getSetCurrentState = BehaviourM.getInstance.GetState("Attack");
            }
            else if (mind.getSetTarget == null || Vector2.Distance(mind.getSetTarget.getSetLocation, mind.getSetEntity.getSetLocation) > 100)
            {
                mind.getSetStateMachine.getSetCurrentState = BehaviourM.getInstance.GetState("Idle");
            }

            mind.getSetMoveVector = chaseVector;
        }

        private void CheckSwitchState(Vector2 v)
        {

        }

    }

}
