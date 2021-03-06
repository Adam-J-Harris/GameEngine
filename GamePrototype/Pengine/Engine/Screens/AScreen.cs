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
using Engine.AI.Pathfinding;
using Engine.Screens.Interfaces;
using Microsoft.Xna.Framework;

namespace Engine.Screens
{
    public abstract class AScreen : IScreen
    {
        private Rectangle screenBounds;

        public AScreen()
        {

        }

        public Rectangle getSetScreenBounds
        {
            get { return screenBounds; }
            set { screenBounds = value; }
        }

        public IGameScreen getThisAsGameScreen
        {
            get { return (IGameScreen)this; }
        }

        /// <summary>
        /// Intial set up of the screen
        /// </summary>
        public virtual void Initialise()
        {

        }

        /// <summary>
        /// Load the content which will be shown on the screen
        /// </summary>
        public virtual void LoadContent()
        {

        }

        /// <summary>
        /// Unload the content currently on the screen
        /// </summary>
        public virtual void UnloadContent()
        {

        }

        /// <summary>
        /// Update the screen
        /// </summary>
        public virtual void Update()
        {

        }

        

        

    }

}
