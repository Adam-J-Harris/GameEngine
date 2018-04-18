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
using Engine.AI.StateMachine.Interfaces;
using Engine.Events;
using Engine.Managers;
using Engine.Objects.Minds;
using Microsoft.Xna.Framework;
using TheRiftCaller.AI.StateMachine;
using TheRiftCaller.Managers;
using TheRiftCaller.Objects.Minds.InterfacesAI;
using Engine.Objects.Entities.Interfaces;
using TheRiftCaller.Physics;

namespace TheRiftCaller.Objects.Minds
{
    public class PatientMind : AMindAI, IPhysical
    {
        private Vector2 mPos;

        private float speed;

        private float distanceToTarget;

        private float damage = 20;

        private float force = 1;

        private bool physicsCollide = false;

        private List<Point> mPath;

        public PatientMind()
        {
            getSetName = "PatientMind";
            //PersonalityChange(BehaviourM.getInstance.GetPersonality("Passive"));

            getSetStateMachine = new FSM();
            getSetStateMachine.getSetCurrentState = BehaviourM.getInstance.GetState("Idle");

            //CollisionM.getInstance.AddListener(newCollision);
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

            if (DimensionM.getInstance.getDimension == "R")
            {
                getSetEntity.getSetDrawMe = true;
                FindATarget();
            }
            else
            {
                getSetEntity.getSetDrawMe = false;
                RemoveTarget();
            }

            if (getSetTarget != null)
            {
                distanceToTarget = Vector2.Subtract(getSetEntity.getSetLocation, getSetTarget.getSetLocation).Length();
            }

            getSetStateMachine.getSetCurrentState.RunBehaviour(this);
            getSetMoveVector.Normalize();

            if (getSetSpeed < 3)
            {
                force += 1f;
            }

            if (force > 0 && getSetTarget == null)
            {
                force -= 1f;
            }

            // Force equals Mass multiplied by Acceleration
            getSetAcceleration += force * (float)(1 / 82.5);//* mePhysical.getSetMass;
            // Speed/Velocity equals acceleration multiplied by delta time in seconds
            getSetSpeed += getSetAcceleration; //* (float)DisplayM.timer.ElapsedGameTime.TotalSeconds;
            //getSetSpeed *= PhysicsM.damping;

            getSetAcceleration = 0;

            if (getSetSpeed < 0)
            {
                getSetSpeed = 0;
            }

            mPos += getSetMoveVector * speed;

            physicsCollide = false;

            getSetLocation = mPos;

            base.Update(mPos);
        }

        /// <summary>
        /// Finds an IEntity target
        /// </summary>
        private void FindATarget()
        {
            getSetTarget = MindM.getInstance.getMind("PlayerMind1").getSetEntity;
        }

        public override void GivePath(List<Point> path)
        {
            mPath = path;
        }

        /// <summary>
        /// Determines what happens if the ICollidable getSetEntity is intersecting another entity's hitbox
        /// </summary>
        /// <param name="source">.</param>
        /// <param name="data">CollisionEvent which contains the entity being collided with</param>
        public virtual void newCollision(object source, CollisionEvent data)
        {
            ICollidable cRef = (ICollidable)getSetEntity;

            //if (data.collidable is ISmartObject)
            //{
            //    smartObj = (ISmartObject)data.collidable;
            //    smartObjCollide = true;
            //}
            //else if (data.collidable is ISmartStructure)
            //{
            //    smartStruct = (ISmartStructure)data.collidable;
            //    smartStructCollide = true;
            //}

            if (cRef.getSetCenter.X < data.collidable.getSetCenter.X)
            {
                data.mtv.X *= -1;
            }

            if (cRef.getSetCenter.Y < data.collidable.getSetCenter.Y)
            {
                data.mtv.Y *= -1;
            }

            mPos += data.mtv * 2;

            physicsCollide = true;
            getSetSpeed *= 0.1f;
        }

        public override void AddCollisionListener()
        {
            CollisionM.getInstance.AddListener(newCollision);
        }

        public override void RemoveCollisionListener()
        {
            CollisionM.getInstance.RemoveListener(newCollision);
        }

    }

}
