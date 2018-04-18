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

namespace Engine.Managers.Interfaces
{
    public interface IInputManager
    {
        /// <summary>
        /// Return the value of IDevice keyboardDevice
        /// </summary>
        IDevice getKeyboard { get; }

        /// <summary>
        /// Return the value of IDevice mouseDevice
        /// </summary>
        IDevice getMouse { get; }

        /// <summary>
        /// Return the value of IDmouse mouseDevice
        /// </summary>
        IDmouse getDMouse { get; }

        /// <summary>
        /// Update the devices by looping through the device list 
        /// </summary>
        void Update();
    }
}
