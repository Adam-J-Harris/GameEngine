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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Engine.Devices
{
    public class Dkeyboard : IDkeyboard
    {
        private KeyboardState oldState;
        private KeyboardState noPressedState;

        //Create a new event of type EventHandler that takes Events class called KeyboardEvent
        // For handling input from a keyboard device
        public event EventHandler<KeyboardEvent> newKeyboardInput;

        public event EventHandler<KeyUpEvent> newKeyUp;
        private Keys keyUp;

        // An array of keys
        // Gets updated with new keys when the user presses on the keyboard during the simulation
        private Keys[] keysArray;

        public Dkeyboard()
        {
            // Setting the old state of the keyboard so it can be compared
            oldState = Keyboard.GetState();
            noPressedState = Keyboard.GetState();

        }

        public virtual void OnKeyUp(Keys k)
        {
            KeyUpEvent kue = new KeyUpEvent(k);

            newKeyUp(this, kue);
        }

        // Calls all of the listener functions for newKeyboardInput
        public virtual void OnNewInput(Keys[] keyRef)
        {

            //Call subscriber and assign it to a var. 
            KeyboardEvent eve = new KeyboardEvent(keyRef);

            //Subscribe the event to this.
            newKeyboardInput(this, eve);
        }

        // Adds a class function to the event publisher
        public void AddKeyUpListener(EventHandler<KeyUpEvent> InputHandler)
        {

            // Add event handler:
            newKeyUp += InputHandler;
        }

        // Removes a class function from the event publisher
        public void RemoveKeyUpListener(EventHandler<KeyUpEvent> InputHandler)
        {
            // Remove event handler:
            newKeyUp -= InputHandler;
        }

        // Adds a class function to the event publisher
        public void AddListener(EventHandler<KeyboardEvent> InputHandler)
        {

            // Add event handler:
            newKeyboardInput += InputHandler;
        }

        // Removes a class function from the event publisher
        public void RemoveListener(EventHandler<KeyboardEvent> InputHandler)
        {
            // Remove event handler:
            newKeyboardInput -= InputHandler;
        }

        // If the Keyboard state changes ie some keys are pressed
        // Raise a new event
        public void Update()
        {
            KeyboardState newstate = Keyboard.GetState();

            if (newstate != noPressedState)
            {
                // Update the keys array with new keys
                keysArray = Keyboard.GetState().GetPressedKeys();

                if (newKeyboardInput != null)
                {
                    // Call the event function
                    OnNewInput(keysArray);
                }

                if (Keyboard.GetState().IsKeyUp(Keys.Space))
                {
                    keyUp = Keys.Space;

                    if (newKeyUp != null)
                    {
                        OnKeyUp(keyUp);
                    }
                }
            }
            else
            {
                //if (Keyboard.GetState().IsKeyUp(Keys.Space))
                //{
                //    if (newKeyUp != null)
                //    {
                //        OnKeyUp(Keys.Space);
                //    }
                //}
            }

            

            // Reassign the oldstate
            oldState = newstate;
        }

        // returns an int called direction
        public Vector2 GetKeyboardInputDirection(PlayerIndex playerIndex)
        {
            Vector2 direction = new Vector2(0, 0);

            KeyboardState keyboardState = Keyboard.GetState();

            // If the "W" key is pressed the related object will move upwards
            if (keyboardState.IsKeyDown(Keys.W))
            {
                direction.Y = -1;
            }
            // If the "S" key is pressed the related object will move downwards                
            else if (keyboardState.IsKeyDown(Keys.S))
            {
                direction.Y = 1;
            }

            // If the "W" key is pressed the related object will move upwards
            if (keyboardState.IsKeyDown(Keys.A))
            {
                direction.X = -1;
            }
            // If the "S" key is pressed the related object will move downwards                
            else if (keyboardState.IsKeyDown(Keys.D))
            {
                direction.X = 1;
            }

            return direction;
        }

    }
}
