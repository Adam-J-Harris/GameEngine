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
using Engine.Devices.Interfaces;
using Engine.Events;
using Engine.Managers;
using Engine.Objects.Minds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheRiftCaller.Objects.Minds
{
    class CameraMind : ACamera
    {
        private bool xTransform = true;
        private bool yTransform = true;

        private int firstRun = 0;

        private float stayHere;

        private IDmouse mouseDevice;
        private float oldMouseScrollVal;
        private float mouseScrollVal;
        

        public CameraMind()
        {
            getSetName = "CameraMind";

            mouseDevice = (IDmouse)InputM.getInstance.getMouse;
            mouseDevice.AddScrollListener(this.newMouseScroll);
        }

        public virtual void newMouseScroll(object source, MouseScrollEvent mouseData)
        {
            oldMouseScrollVal = mouseScrollVal;
            mouseScrollVal = mouseData.mouseScroll / 100;

            if (mouseScrollVal > oldMouseScrollVal)
            {
                getSetZoom += 0.1f;
            }
            else if (mouseScrollVal < oldMouseScrollVal)
            {
                getSetZoom -= 0.1f;
            };
        }

        public override void SetViewport(Viewport newView)
        {
            getSetView = newView;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

        }

        public override void Update()
        {
            EntityM.getInstance.getEntity("Filter1").getSetLocation = new Vector2(getSetCenter.X - (getSetView.Width / 2), getSetCenter.Y - (getSetView.Height / 2));
            //EntityM.getInstance.getEntity("Filter").getSetLocation.Y = ;

            getSetBounds = new Rectangle((int)(getSetCenter.X - (getSetView.Width / 2)), (int)(getSetCenter.Y - (getSetView.Height / 2)), 1600, 900);

            if (xTransform && yTransform)
            {
                Transformer();
            }
            else if (xTransform && !yTransform)
            {
                TransformerX();
            }
            else if (!xTransform && yTransform)
            {
                TransformerY();
            }

            //if ((getSetTarget.getSetX) <= 0 + ((getSetView.Width / 2) / 1.5) ||
            //        (getSetTarget.getSetX) + (getSetTarget.getSetTexture.Width) >= 5000 - ((getSetView.Width / 2) / 1.5))
            //{
            //    xTransform = false;
            //}
            //else
            //{
            //    xTransform = true;
            //}

            //if ((getSetTarget.getSetY) <= 0 + ((getSetView.Height / 2) / 1.5) ||
            //        (getSetTarget.getSetY) + (getSetTarget.getSetTexture.Height) >= 2500 - ((getSetView.Height / 2) / 1.5))
            //{
            //    yTransform = false;
            //}
            //else
            //{
            //    yTransform = true;
            //}
        }

        private void Transformer()
        {
            firstRun = 0;

            getSetCenter = new Vector2(getSetTarget.getSetLocation.X + (getSetTarget.getSetTexture.Width / 4),
                                        getSetTarget.getSetLocation.Y + (getSetTarget.getSetTexture.Height / 4));

            getSetTransform = Matrix.CreateTranslation(new Vector3(-getSetCenter.X, -getSetCenter.Y, 0)) *
                                Matrix.CreateRotationZ(getSetRotation) *
                                Matrix.CreateScale(getSetZoom, getSetZoom, 0) *
                                Matrix.CreateTranslation(new Vector3(getSetView.Width / 2, getSetView.Height / 2, 0));
        }

        private void TransformerX()
        {
            if (firstRun == 0)
            {
                stayHere = getSetTarget.getSetLocation.Y + (getSetTarget.getSetTexture.Height / 4);
                firstRun = 1;
            }

            getSetCenter = new Vector2(getSetTarget.getSetLocation.X + (getSetTarget.getSetTexture.Width / 4),
                                        stayHere + (getSetTarget.getSetTexture.Height / 4));

            getSetTransform = Matrix.CreateTranslation(new Vector3(-getSetCenter.X, -stayHere, 0)) *
                                Matrix.CreateRotationZ(getSetRotation) *
                                Matrix.CreateScale(getSetZoom, getSetZoom, 0) *
                                Matrix.CreateTranslation(new Vector3(getSetView.Width / 2, getSetView.Height / 2, 0));
        }

        private void TransformerY()
        {
            if (firstRun == 0)
            {
                stayHere = getSetTarget.getSetLocation.X + (getSetTarget.getSetTexture.Width / 4);
                firstRun = 1;
            }

            getSetCenter = new Vector2(getSetTarget.getSetLocation.X + (getSetTarget.getSetTexture.Width / 4),
                                        getSetTarget.getSetLocation.Y + (getSetTarget.getSetTexture.Height / 4));

            getSetTransform = Matrix.CreateTranslation(new Vector3(-stayHere, -getSetCenter.Y, 0)) *
                                Matrix.CreateRotationZ(getSetRotation) *
                                Matrix.CreateScale(getSetZoom, getSetZoom, 0) *
                                Matrix.CreateTranslation(new Vector3(getSetView.Width / 2, getSetView.Height / 2, 0));
        }
    }
}