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
using Engine.Devices;
using Engine.Devices.Interfaces;
using Engine.Managers.Interfaces;

namespace Engine.Managers
{
    public class InputM : IInputManager
    {
        private static IInputManager instance;

        private List<IDevice> devices;

        private IDevice mouseDevice;
        private IDevice keyboardDevice;

        private InputM()
        {
            devices = new List<IDevice>();

            mouseDevice = new Dmouse();
            keyboardDevice = new Dkeyboard();

            devices.Add(mouseDevice);
            devices.Add(keyboardDevice);
        }

        /// <summary>
        /// Make this class a singleton
        /// </summary>
        public static IInputManager getInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InputM();
                }
                return instance;
            }
        }
        
        /// <summary>
        /// Return the value of IDevice keyboardDevice
        /// </summary>
        public IDevice getKeyboard
        {
            get { return keyboardDevice; }
        }
        
        /// <summary>
        /// Return the value of IDevice mouseDevice
        /// </summary>
        public IDevice getMouse
        {
            get { return mouseDevice; }
        }

        /// <summary>
        /// Return the value of IDmouse mouseDevice
        /// </summary>
        public IDmouse getDMouse
        {
            get { return (IDmouse)mouseDevice; }
        }

        /// <summary>
        /// Update the devices by looping through the device list 
        /// </summary>
        public void Update()
        {
            foreach (IDevice device in devices)
            {
                device.Update();
            }
        }

    }

}
