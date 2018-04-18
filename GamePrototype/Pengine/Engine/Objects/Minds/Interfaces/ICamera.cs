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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.Objects.Minds.Interfaces
{
    public interface ICamera : ITargetable
    {
        Rectangle getSetBounds { get; set; }

        /// <summary>
        /// Sets or Returns the value of Matrix transform
        /// </summary>
        Matrix getSetTransform { get; set; }

        /// <summary>
        /// Sets or Returns the value of Viewport view
        /// </summary>
        Viewport getSetView { get; set; }

        /// <summary>
        /// Sets or Returns the value of Vector2 center
        /// </summary>
        Vector2 getSetCenter { get; set; }

        /// <summary>
        /// Sets or Returns the value of float center.X
        /// </summary>
        float getSetCenterX { get; set; }

        /// <summary>
        /// Sets or Returns the value of float center.Y
        /// </summary>
        float getSetCenterY { get; set; }

        /// <summary>
        /// Sets or Returns the value of float zoom
        /// </summary>
        float getSetZoom { get; set; }

        /// <summary>
        /// Sets or Returns the value of float rotation
        /// </summary>
        float getSetRotation { get; set; }

        /// <summary>
        /// Updates the camera
        /// </summary>
        /// <param name="gameTime">Current gametime</param>
        void Update(GameTime gameTime);

        /// <summary>
        /// Draws the camera using spritebatch
        /// </summary>
        /// <param name="spriteBatch">New value of spritebatch</param>
        void Draw(SpriteBatch spriteBatch);

        /// <summary>
        /// Set the value of Viewport view
        /// </summary>
        /// <param name="newView">Viewport</param>
        void SetViewport(Viewport newView);
    }
}
