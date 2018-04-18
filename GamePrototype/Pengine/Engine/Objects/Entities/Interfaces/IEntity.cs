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
using Engine.Maps.Interfaces;
using Engine.Objects.Minds.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.Objects.Entities.Interfaces
{
    public interface IEntity
    {
        /// <summary>
        /// Set or Return the value of Texture2D texture
        /// </summary>
        Texture2D getSetTexture { get; set; }

        /// <summary>
        /// Set or Return the Vector2 location
        /// </summary>
        Vector2 getSetLocation { get; set; }

        /// <summary>
        /// Sets or Returns the value of the Point pointLocation
        /// </summary>
        Point getSetPointLocation { get; set; }

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

        /// <summary>
        /// Set or Return the value of IMind mind
        /// </summary>
        IMind getSetMind { get; set; }

        /// <summary>
        /// Set or Return the value of bool drawMe
        /// </summary>
        bool getSetDrawMe { get; set; }

        bool getSetWalkable { get; set; }

        /// <summary>
        /// Update the position of the entity
        /// </summary>
        /// <param name="locationRef">Vector2, location coordinates gathered from the mind</param>
        void Update(Vector2 pLocation);

        /// <summary>
        /// Uses SpriteBatch to draw the entity
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch, allows the drawing  of the entity</param>
        void Draw(SpriteBatch spriteBatch);
    }
}
