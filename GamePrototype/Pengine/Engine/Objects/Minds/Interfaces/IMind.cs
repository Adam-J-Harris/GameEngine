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
using Engine.Objects.Entities.Interfaces;
using Microsoft.Xna.Framework;

namespace Engine.Objects.Minds.Interfaces
{
    public interface IMind
    {
        /// <summary>
        /// Set/Initialise the values of the class
        /// </summary>
        void SetValues();

        float getSetSpeed { get; set; }

        float getSetAcceleration { get; set; }

        /// <summary>
        /// Set or Return the value of the Vector2 moveVector
        /// 
        /// moveVector is where the entity will move to next
        /// </summary>
        Vector2 getSetMoveVector { get; set; }

        /// <summary>
        /// Set or Return the IEntity entity
        /// </summary>
        IEntity getSetEntity { get; set; }

        /// <summary>
        /// Set or Return the value of the Vector2 location
        /// </summary>
        Vector2 getSetLocation { get; set; }

        /// <summary>
        /// Set or Return the value of float location.X
        /// </summary>
        float getSetX { get; set; }

        /// <summary>
        /// Set or Return the value of float location.Y
        /// </summary>
        float getSetY { get; set; }

        /// <summary>
        /// Set or Return the value of string id (name and number)
        /// </summary>
        string getSetID { get; set; }

        /// <summary>
        /// Set or Return the value of the string name
        /// </summary>
        string getSetName { get; set; }

        /// <summary>
        /// Set or Return the value of the int number
        /// </summary>
        int getSetNumber { get; set; }

        IFSM getSetStateMachine { get; set; }

        void AddCollisionListener();
        void RemoveCollisionListener();

        /// <summary>
        /// Perform the update method on the mind in sub-classes override methods
        /// </summary>
        void Update();

        /// <summary>
        /// Send new location values back to the entity
        /// </summary>
        /// <param name="posRef">new location coordinates</param>
        void Update(Vector2 posRef);
    }
}
