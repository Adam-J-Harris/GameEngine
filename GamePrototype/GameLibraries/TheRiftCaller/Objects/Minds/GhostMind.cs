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
using Engine.Devices.Interfaces;
using Engine.Events;
using Engine.Managers;
using Engine.Maps;
using Engine.Objects.Minds;
using Microsoft.Xna.Framework;
using TheRiftCaller.Managers;
using TheRiftCaller.Objects.Minds.InterfacesAI;

namespace TheRiftCaller.Objects.Minds
{
    public class GhostMind : AMindAI, IEthereal
    {
        private Vector2 mPos;
        private float speed;

        private float distanceToTarget;

        public GhostMind()
        {
            getSetName = "GhostMind";
        }

        /// <summary>
        /// Set/Initialise the values of the class
        /// </summary>
        public override void SetValues()
        {
            base.SetValues();

            mPos = getSetEntity.getSetLocation;
            speed = 0.01f;
        }

        /// <summary>
        /// Updates the values of this class and sends information back to the getSetEntity
        /// </summary>
        public override void Update()
        {
            if (DisplayM.timer.TotalGameTime.Seconds > startTime + timeDelay)
            {
                canAction = true;
            }

            if (DimensionM.getInstance.getDimension == "P")
            {
                getSetEntity.getSetDrawMe = true;
                FindATarget();
            }
            else
            {
                getSetEntity.getSetDrawMe = false;
            }

            if (getSetTarget != null)
            {
                distanceToTarget = Vector2.Subtract(getSetEntity.getSetLocation, getSetTarget.getSetLocation).Length();

                if (distanceToTarget < 300)
                {
                    getSetMoveVector = Vector2.Subtract(getSetTarget.getSetLocation, getSetEntity.getSetLocation);
                }
                else if (distanceToTarget > 400)
                {
                    getSetMoveVector = Vector2.Zero;
                }
            }           

            getSetMoveVector.Normalize();

            mPos += getSetMoveVector * speed;

            base.Update(mPos);
        }

        private void CheckPersonality()
        {

        }

        /// <summary>
        /// Finds an IEntity target
        /// </summary>
        private void FindATarget()
        {
            getSetTarget = MindM.getInstance.getMind("PlayerMind1").getSetEntity;
        }

        /// <summary>
        /// Determines what happens if the ICollidable getSetEntity is intersecting another entity's hitbox
        /// </summary>
        /// <param name="source">.</param>
        /// <param name="data">CollisionEvent which contains the entity being collided with</param>
        public virtual void newCollision(object source, CollisionEvent data)
        {

        }
    }
}
