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
using Engine.Devices.Interfaces;
using Engine.Events;
using Engine.Objects.Entities.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Engine.Devices
{
    public class Dmouse : IDmouse
    {
        private ButtonState oldState;

        private int oldMouseState;

        //Create a new event of type EventHandler that takes Events class called MouseEvent.
        public event EventHandler<MouseEvent> newMouseInput;
        public event EventHandler<MouseScrollEvent> newMouseScroll;

        private MouseState pressed;
        //private bool Scrolled = false;

        private Vector2 direction;
        private float rotationInRadians;

        public Dmouse()
        {
            // Sets the old state to the state of the left mouse button ONLY
            oldState = Mouse.GetState().LeftButton;
            oldMouseState = Mouse.GetState().ScrollWheelValue;
        }

        /// <summary>
        /// Calculates the angle of rotation from a IEntity e to the mouse position
        /// </summary>
        /// <param name="e">IEntity to be compared against</param>
        /// <returns>float rotation</returns>
        public void getRotationFromEntity(IEntity e)
        {
            Vector2 mousePos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

            direction = Vector2.Subtract(mousePos, e.getSetLocation);
            direction.Normalize();

            /// quarter turn
            //rotationInRadians = (float)Math.PI / 2;

            rotationInRadians = (float)Math.Atan2(direction.Y, direction.X);

            if (rotationInRadians < 0)
            {
                rotationInRadians *= -1;
            }
        }

        /// <summary>
        /// Returns the value of the float rotation
        /// </summary>
        public float getRotationInRadians
        {
            get { return rotationInRadians; }
        }

        // Calls all of the listener functions for newMouseInput
        public virtual void OnNewInput(MouseState mouseRef)
        {

            //Call subscriber and assign it to a var. 
            MouseEvent eve = new MouseEvent(mouseRef);
            if (newMouseInput != null)
            {
                //Subscribe the event to this.
                newMouseInput(this, eve);
            }
        }
        
        // Adds a class function to the event publisher
        public void AddClickListener(EventHandler<MouseEvent> InputHandler)
        {

            // Add event handler:
            newMouseInput += InputHandler;
        }

        // Removes a class function from the event publisher
        public void RemoveClickListener(EventHandler<MouseEvent> InputHandler)
        {
            // Remove event handler:
            newMouseInput -= InputHandler;
        }

        /// <summary>
        /// If mouse has been scrolled, call listener functions
        /// </summary>
        /// <param name="state"></param>
        public virtual void OnNewScroll(int state)
        {
            MouseScrollEvent eve = new MouseScrollEvent(state);
            if (newMouseScroll != null)
            {
                newMouseScroll(this, eve);
            }
        }

        /// <summary>
        /// Adds a listener class
        /// </summary>
        /// <param name="Input"></param>
        public void AddScrollListener(EventHandler<MouseScrollEvent> Input)
        {
            newMouseScroll = Input;
        }

        /// <summary>
        /// Removes a listener class
        /// </summary>
        /// <param name="InputHandler"></param>
        public void RemoveScrollListener(EventHandler<MouseScrollEvent> InputHandler)
        {
            // Remove event handler:
            newMouseScroll -= InputHandler;
        }

        // If the Left Mouse Button state changes ie left clicked
        // Raise a new event
        public void Update()
        {
            ButtonState newState = Mouse.GetState().LeftButton;
            int newMouseScroll = Mouse.GetState().ScrollWheelValue;

            if (newState != ButtonState.Released)
            {
                //pressed = ButtonState.Pressed;

                OnNewInput(pressed);
            }

            if (newMouseScroll != oldMouseState)
            {
                // Scrolled = true;

                OnNewScroll(newMouseScroll);
            }

            //pressed = false;
            //Scrolled = false;

            oldMouseState = newMouseScroll;
            oldState = newState;
        }

    }
}
