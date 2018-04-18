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
using Engine.Maps;
using Engine.Objects.Entities.Interfaces;
using Microsoft.Xna.Framework;

namespace Engine.Objects.Entities
{
    public class ASmartStructure : AStructure, ISmartStructure
    {
        private Rectangle interactHitbox;

        public ASmartStructure()
        {

        }

        /// <summary>
        /// Set up the collidable's hitbox
        /// If the collidable needs more hitboxes they can be set up in the override method
        /// </summary>
        public override void SetUpHitbox()
        {
            // Makes sure the texture is set
            //localTexture = getSetTexture;

            // Initialise the hitbox to the entitys starting coordinates and using it's height and width create a rectangle
            getSetHitbox = new Rectangle((int)getSetLocation.X, (int)getSetLocation.Y, getSetTexture.Width, getSetTexture.Height);

            // Larger than original hitbox so player doesn't have to appear to be colliding with SmartStructure
            getSetInteractHitbox = new Rectangle((int)getSetLocation.X - 5, (int)getSetLocation.Y - 5, getSetTexture.Width + 10, getSetTexture.Height + 10);
        }

        /// <summary>
        /// Perform Smart method
        /// Differentiate between classes by using override method
        /// </summary>
        public virtual void Interact()
        {
            //Map.getInstance.UpdatePositions(Map.getInstance.getCluster.getMapPos);
        }

        /// <summary>
        /// Sets or Returns the value of the Rectangle interactHitbox
        /// </summary>
        public Rectangle getSetInteractHitbox
        {
            get { return interactHitbox; }
            set { interactHitbox = value; }
        }
    }
}
