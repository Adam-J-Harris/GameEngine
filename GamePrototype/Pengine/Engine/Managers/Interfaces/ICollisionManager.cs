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
using Engine.Events;
using Engine.Objects.Entities.Interfaces;
using Microsoft.Xna.Framework;

namespace Engine.Managers.Interfaces
{
    public interface ICollisionManager
    {
        /// <summary>
        /// Calls all of the listeners collision event function
        /// </summary>
        /// <param name="collideRef">THe colliding object reference</param>
        void OnNewCollision(ICollidable collideRef, Vector2 mtv);

        /// <summary>
        /// Starts a class having an event function called when triggered
        /// </summary>
        /// <param name="handler">the collision event reference</param>
        void AddListener(EventHandler<CollisionEvent> handler);

        /// <summary>
        /// Stop a class from having the collision event function called
        /// </summary>
        /// <param name="handler">The collision event reference</param>
        void RemoveListener(EventHandler<CollisionEvent> handler);

        /// <summary>
        /// Set up all hitboxes of entities which are in the same map cluster as the player character
        /// </summary>
        /// <param name="d"></param>
        void SetUpStaticHitboxes(IDictionary<string, ICollidable> d);

        /// <summary>
        /// Gets rid of static hitboxes which are no longer in the same cluster as the player character
        /// </summary>
        void ResetStaticHitboxes();

        void DeleteCollidable(ICollidable c);

        void SetUpMovingEntities();

        IDictionary<string, ICollidable> getSetDictionary { get; set; }

        /// <summary>
        /// Update hitboxes of non-structure Entities, check for rectangle intersections
        /// </summary>
        void Update();
        bool CheckRayCollision(Rectangle tempR);
    }
}
