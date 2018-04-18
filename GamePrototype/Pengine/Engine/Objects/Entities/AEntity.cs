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
using Engine.Maps;
using Engine.Maps.Interfaces;
using Engine.Objects.Entities.Interfaces;
using Engine.Objects.Minds.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.Objects.Entities
{
    public abstract class AEntity : IEntity
    {
        private Texture2D texture;
        private Vector2 location;
        private Vector2 clusterPos;
        private Point pointLocation;
        private Color color = Color.AntiqueWhite;

        private string id;
        private string name;
        private int number;

        private IMind mind;

        private bool drawMe = true;

        private bool walkable;

        public AEntity()
        {
            
        }
        
        /// <summary>
        /// Set or Return the value of Texture2D texture
        /// </summary>
        public Texture2D getSetTexture
        {
            get { return texture; }
            set { texture = value; }
        }
        
        /// <summary>
        /// Set or Return the Vector2 location
        /// </summary>
        public Vector2 getSetLocation
        {
            get { return location; }
            set { location = value; }
        }

        /// <summary>
        /// Sets or Returns the value of the Point pointLocation
        /// </summary>
        public Point getSetPointLocation
        {
            get { return pointLocation; }
            set { pointLocation = value; }
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

        /// <summary>
        /// Set or Return the value of IMind mind
        /// </summary>
        public IMind getSetMind
        {
            get { return mind; }
            set { mind = value; }
        }

        /// <summary>
        /// Set or Return the value of bool drawMe
        /// </summary>
        public bool getSetDrawMe
        {
            get { return drawMe; }
            set { drawMe = value; }
        }
        
        public bool getSetWalkable
        {
            get { return walkable; }
            set { walkable = value; }
        }

        /// <summary>
        /// Update the position of the entity
        /// </summary>
        /// <param name="locationRef">Vector2, location coordinates gathered from the mind</param>
        public void Update(Vector2 pLocation)
        {
            // Reset data and assign new values
            location = pLocation;
            pointLocation = new Point((int)Math.Truncate((location.X + 32) / 64), (int)Math.Truncate((location.Y + 32) / 64));
        }
        
        /// <summary>
        /// Uses SpriteBatch to draw the entity
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch, allows the drawing  of the entity</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            color = Color.AntiqueWhite;

            //new Rectangle((int)location.X, (int)location.Y, 50, 50),
            
            spriteBatch.Draw(texture, location, color);
        }

    }

}
