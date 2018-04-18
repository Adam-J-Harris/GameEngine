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
using Engine.Managers;
using Engine.Objects.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace TheRiftCaller.Objects.Entities
{
    public class Player : ACollidable
    {
        private Rectangle srcRect;
        private double _elapsedFrameTime;

        private float y = 0;
        private float x = 0;

        private bool reset = true;

        private SoundEffect footstep1;
        private SoundEffect footstep2;

        double IntervelTimer;

        private bool soundplaying = false;

        public Player()
        {
            getSetName = "Player";
            getSetTexture = ImageM.getInstance.getAsset("PlayerSprites");

            MediaPlayer.Volume = 0.3f;
            footstep1 = SoundM.getInstance.getSoundEffect("Footsteps1");
            footstep2 = SoundM.getInstance.getSoundEffect("Footsteps2");
        }

        public override void SetUpHitbox()
        {
            getSetHitbox = new Rectangle((int)getSetLocation.X - 2, (int)getSetLocation.Y - 2, (getSetTexture.Width / 4) + 4, (getSetTexture.Height / 4) + 4);
        }

        public override void SetVertices()
        {
            getSetVertices = new Vector2[4];
            getSetVertices[0] = getSetLocation;
            getSetVertices[1] = new Vector2(getSetLocation.X + 40, getSetLocation.Y);
            getSetVertices[2] = new Vector2(getSetLocation.X + 40, getSetLocation.Y + 40);
            getSetVertices[3] = new Vector2(getSetLocation.X, getSetLocation.Y + 40);

            getSetCenter = new Vector2(getSetLocation.X + 20, getSetLocation.Y + 20);
        }

        public void animCheckTimer()
        {
            if (reset)
            {
                IntervelTimer = DisplayM.animTimer.Elapsed.TotalSeconds + 0.5;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _elapsedFrameTime += DisplayM.timer.ElapsedGameTime.TotalSeconds;

            //base.Draw(spriteBatch);
            if (getSetMind.getSetStateMachine.getSetCurrentState.getSetName == "WalkRight")
            {
                y = 0;
                srcRect = new Rectangle((int)Animation() * 40, (int)y * 40, 40, 40);
            }
            else if (getSetMind.getSetStateMachine.getSetCurrentState.getSetName == "WalkLeft")
            {
                y = 1;
                srcRect = new Rectangle((int)Animation() * 40, (int)y * 40, 40, 40);
            }
            else if (getSetMind.getSetStateMachine.getSetCurrentState.getSetName == "WalkUp")
            {
                y = 2;
                srcRect = new Rectangle((int)Animation() * 40, (int)y * 40, 40, 40);
            }
            else if (getSetMind.getSetStateMachine.getSetCurrentState.getSetName == "WalkDown")
            {
                y = 3;
                srcRect = new Rectangle((int)Animation() * 40, (int)y * 40, 40, 40);
            }
            else
            {
                x = 0;
                y = 0;
                srcRect = new Rectangle((int)x, (int)y, 40, 40);
            }

            //spriteBatch.Draw(getSetTexture, getSetLocation, new Rectangle((int)getSetLocation.X, (int)getSetLocation.Y, 50, 50), Color.AntiqueWhite);
            spriteBatch.Draw(getSetTexture, new Rectangle((int)getSetLocation.X, (int)getSetLocation.Y, 40, 40), srcRect, Color.AntiqueWhite);
        }

        private float Animation()
        {


            animCheckTimer();

            if (DisplayM.animTimer.Elapsed.TotalSeconds >= IntervelTimer)
            {

                if (soundplaying == false)
                {

                    soundplaying = true;
                    footstep1.Play();

                    //SoundM.getInstance.getSoundEffect("Footsteps1");
                }
                else
                {

                    footstep2.Play();
                }

                if (x >= 3)
                {
                    x = 0;
                }
                else
                {
                    x++;
                }

                reset = true;
            }
            else
            {
                reset = false;
            }

            soundplaying = false;

            return x;
        }
    }
}
