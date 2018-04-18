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
using Engine.Objects.Entities.Interfaces;
using Microsoft.Xna.Framework;

namespace Engine.Objects.Entities
{
    public abstract class ACollidable : AEntity, ICollidable
    {
        private Rectangle hitbox;

        private Rectangle leftHitbox;
        private Rectangle rightHitbox;
        private Rectangle topHitbox;
        private Rectangle bottomHitbox;

        private Vector2[] vertices;
        private Vector2 center;
        private Vector2[] axes;

        public event EventHandler BoundsChanged;

        public ACollidable()
        {

        }

        public virtual void SetVertices()
        {
            vertices = new Vector2[4];
            vertices[0] = getSetLocation;
            vertices[1] = new Vector2(getSetLocation.X + getSetTexture.Width, getSetLocation.Y);
            vertices[2] = new Vector2(getSetLocation.X + getSetTexture.Width, getSetLocation.Y + getSetTexture.Height);
            vertices[3] = new Vector2(getSetLocation.X, getSetLocation.Y + getSetTexture.Height);

            center = new Vector2(getSetLocation.X + (getSetTexture.Width / 2), getSetLocation.Y + (getSetTexture.Height / 2));
        }

        public Vector2[] getSetVertices
        {
            get { return vertices; }
            set { vertices = value; }
        }

        public Vector2 getSetCenter
        {
            get { return center; }
            set { center = value; }
        }

        public void SetAxes()
        {
            axes = new Vector2[2];
            Vector2 pointOne = vertices[0];

            for (int i = 0; i < axes.Length; i++)
            {
                if (i + 1 >= axes.Length)
                {
                    axes[i] = new Vector2(vertices[vertices.Count() - 1].X - pointOne.X, vertices[vertices.Count() - 1].Y - pointOne.Y);
                }
                else
                {
                    axes[i] = new Vector2(vertices[i + 1].X - vertices[i].X, vertices[i + 1].Y - vertices[i].Y);
                }

                axes[i].Normalize();
            }
        }

        public Vector2[] getAxes
        {
            get { return axes; }
        }

        public Projection Project(Vector2 a)
        {
            Projection p = new Projection(a, this);
            
            return p;
        }

        /// <summary>
        /// Set or Return the value of Rectangle hitbox
        /// </summary>
        public Rectangle getSetHitbox
        {
            get { return hitbox; }
            set { hitbox = value; }
        }

        public Rectangle getSetLeftHitbox
        {
            get { return leftHitbox; }
            set { leftHitbox = value; }
        }
        public Rectangle getSetRightHitbox
        {
            get { return rightHitbox; }
            set { rightHitbox = value; }
        }
        public Rectangle getSetTopHitbox
        {
            get { return topHitbox; }
            set { topHitbox = value; }
        }
        public Rectangle getSetBottomHitbox
        {
            get { return bottomHitbox; }
            set { bottomHitbox = value; }
        }

        public void RaiseBoundsChanged()
        {
            EventHandler handler = BoundsChanged;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
                
        }

        public void AddCollisionListener()
        {
            getSetMind.AddCollisionListener();
        }

        public void RemoveCollisionListener()
        {
            getSetMind.RemoveCollisionListener();
        }

        /// <summary>
        /// Set up the collidable's hitbox
        /// If the collidable needs more hitboxes they can be set up in the override method
        /// </summary>
        public virtual void SetUpHitbox()
        {
            // Initialise the hitbox to the entities starting coordinates and using it's height and width create a rectangle
            hitbox = new Rectangle((int)getSetLocation.X, (int)getSetLocation.Y, getSetTexture.Width, getSetTexture.Height);
        }

        /// <summary>
        /// Destroy the collidable's hitbox(s)
        /// </summary>
        public virtual void DestroyHitboxes()
        {

        }

    }
}
