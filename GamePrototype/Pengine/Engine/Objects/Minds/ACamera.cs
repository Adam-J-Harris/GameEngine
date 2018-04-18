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
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Objects.Minds.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.Objects.Minds
{
    public abstract class ACamera : ATargetable, ICamera
    {
        private Matrix transform;
        private Viewport view;

        private Vector2 center;
        private float rotation = 0.0f;
        private float zoom = 1.5f;

        private Rectangle mBounds;

        public ACamera()
        {

        }

        public Rectangle getSetBounds
        {
            get { return mBounds; }
            set { mBounds = value; }
        }

        /// <summary>
        /// Sets or Returns the value of Matrix transform
        /// </summary>
        public Matrix getSetTransform
        {
            get { return transform; }
            set { transform = value; }
        }

        /// <summary>
        /// Sets or Returns the value of Viewport view
        /// </summary>
        public Viewport getSetView
        {
            get { return view; }
            set { view = value; }
        }

        /// <summary>
        /// Sets or Returns the value of Vector2 center
        /// </summary>
        public Vector2 getSetCenter
        {
            get { return center; }
            set { center = value; }
        }

        /// <summary>
        /// Sets or Returns the value of float center.X
        /// </summary>
        public float getSetCenterX
        {
            get { return center.X; }
            set { center.X = value; }
        }

        /// <summary>
        /// Sets or Returns the value of float center.Y
        /// </summary>
        public float getSetCenterY
        {
            get { return center.Y; }
            set { center.Y = value; }
        }

        /// <summary>
        /// Sets or Returns the value of float zoom
        /// </summary>
        public float getSetZoom
        {
            get { return zoom; }
            set { zoom = value; if (zoom < 0.1f) zoom = 0.1f; }
        }

        /// <summary>
        /// Sets or Returns the value of float rotation
        /// </summary>
        public float getSetRotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        /// <summary>
        /// Updates the camera
        /// </summary>
        /// <param name="gameTime">Current gametime</param>
        public virtual void Update(GameTime gameTime)
        {

        }

        /// <summary>
        /// Draws the camera using spritebatch
        /// </summary>
        /// <param name="spriteBatch">New value of spritebatch</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

        /// <summary>
        /// Set the value of Viewport view
        /// </summary>
        /// <param name="newView">Viewport</param>
        public virtual void SetViewport(Viewport newView)
        {

        }

    }

}
