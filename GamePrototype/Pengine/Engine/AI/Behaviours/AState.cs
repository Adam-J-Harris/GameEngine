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
using Engine.AI.Behaviours.Interfaces;
using Engine.Objects.Entities.Interfaces;
using Engine.Objects.Minds.Interfaces;
using Microsoft.Xna.Framework;

namespace Engine.AI.Behaviours
{
    public class AState : IState
    {
        private IPersonality p;

        private string name;

        public AState()
        {

        }

        /// <summary>
        /// Set or Return the value of the Personality p
        /// </summary>
        public IPersonality getSetPersonality
        {
            get { return p; }
            set { p = value; }
        }

        public string getSetName
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Runs the behaviour
        /// </summary>
        public virtual void RunBehaviour(IMindAI mind)
        {

        }

        public virtual void RunBehaviour(IMind mind, Vector2 v)
        {
            ICollidable mePhysical = (ICollidable)mind.getSetEntity;
            
        }
    }
}
