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
using Engine.AI.StateMachine.Interfaces;
using Engine.Managers;
using Engine.Maps.Interfaces;
using Engine.Objects.Entities.Interfaces;
using Engine.Objects.Minds.Interfaces;
using Microsoft.Xna.Framework;

namespace Engine.Objects.Minds
{
    public abstract class AMind : IMind
    {
        private IEntity entity;
        private Vector2 location;

        private string id;
        private string name;
        private int number;

        protected float startTime;
        protected float timeDelay;
        protected bool canAction = true;

        private Vector2 moveVector;
        private float speed;
        private float acceleration;

        private IFSM fsm;

        public AMind()
        {
            
        }

        /// <summary>
        /// Set/Initialise the values of the class
        /// </summary>
        public virtual void SetValues()
        {

        }

        public float getSetSpeed
        {
            get { return speed; }
            set { speed = value; }
        }

        public float getSetAcceleration
        {
            get { return acceleration; }
            set { acceleration = value; }
        }

        /// <summary>
        /// Set or Return the value of the Vector2 moveVector
        /// 
        /// moveVector is where the entity will move to next
        /// </summary>
        public Vector2 getSetMoveVector
        {
            get { return moveVector; }
            set { moveVector = value; }
        }

        /// <summary>
        /// Set or Return the IEntity entity
        /// </summary>
        public IEntity getSetEntity
        {
            get { return entity; }
            set { entity = value; }
        }

        /// <summary>
        /// Set or Return the value of the Vector2 location
        /// </summary>
        public Vector2 getSetLocation
        {
            get { return location; }
            set { location = value; }
        }

        /// <summary>
        /// Set or Return the value of float location.X
        /// </summary>
        public float getSetX
        {
            get { return location.X; }
            set { location.X = value; }
        }

        /// <summary>
        /// Set or Return the value of float location.Y
        /// </summary>
        public float getSetY
        {
            get { return location.Y; }
            set { location.Y = value; }
        }

        /// <summary>
        /// Set or Return the value of string id
        /// </summary>
        public string getSetID
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Set or Return the value of the string name
        /// </summary>
        public string getSetName
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Set or Return the value of the int number
        /// </summary>
        public int getSetNumber
        {
            get { return number; }
            set { number = value; }
        }

        public IFSM getSetStateMachine
        {
            get { return fsm; }
            set { fsm = value; }
        }

        /// <summary>
        /// Prevents the player from completing the same action more than once at a time
        /// </summary>
        /// <param name="delay">float which equals the amount of time in seconds</param>
        protected void StartDelay(float delay)
        {
            canAction = false;

            timeDelay = delay;

            startTime = DisplayM.timer.TotalGameTime.Seconds;
        }

        public virtual void AddCollisionListener()
        {

        }

        public virtual void RemoveCollisionListener()
        {

        }

        /// <summary>
        /// Perform the update method on the mind in sub-classes override methods
        /// </summary>
        public virtual void Update()
        {

        }

        /// <summary>
        /// Send new location values back to the entity
        /// </summary>
        /// <param name="posRef">new location coordinates</param>
        public void Update(Vector2 posRef)
        {
            entity.Update(posRef);
        }

    }

}
