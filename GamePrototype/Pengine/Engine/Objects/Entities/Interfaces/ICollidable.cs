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
using Engine.CollisionDetection;
using Microsoft.Xna.Framework;

namespace Engine.Objects.Entities.Interfaces
{
    public interface ICollidable : IEntity
    {
        event EventHandler BoundsChanged;

        void RaiseBoundsChanged();

        /// <summary>
        /// Set or Return the value of Rectangle hitbox
        /// </summary>
        Rectangle getSetHitbox { get; set; }

        Rectangle getSetLeftHitbox { get; set; }
        Rectangle getSetRightHitbox { get; set; }
        Rectangle getSetTopHitbox { get; set; }
        Rectangle getSetBottomHitbox { get; set; }

        /// <summary>
        /// Set up the collidable's hitbox
        /// If the collidable needs more hitboxes they can be set up in the override method
        /// </summary>
        void SetUpHitbox();

        /// <summary>
        /// Destroy the collidable's hitbox(s)
        /// </summary>
        void DestroyHitboxes();

        void SetVertices();

        void SetAxes();

        Vector2[] getAxes { get; }

        Projection Project(Vector2 a);

        Vector2[] getSetVertices { get; }

        Vector2 getSetCenter { get; }

        void AddCollisionListener();
        void RemoveCollisionListener();
    }
}
