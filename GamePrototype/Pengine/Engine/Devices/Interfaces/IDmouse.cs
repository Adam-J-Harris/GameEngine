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
using Engine.Events;
using Engine.Objects.Entities.Interfaces;

namespace Engine.Devices.Interfaces
{
    public interface IDmouse : IDevice
    {
        /// <summary>
        /// Calculates the angle of rotation from a IEntity e to the mouse position
        /// </summary>
        /// <param name="e">IEntity to be compared against</param>
        void getRotationFromEntity(IEntity e);

        /// <summary>
        /// Returns the value of the float rotation
        /// </summary>
        float getRotationInRadians { get; }

        // Adds a class function to the event publisher
        void AddClickListener(EventHandler<MouseEvent> InputHandler);

        // Removes a class function from the event publisher
        void RemoveClickListener(EventHandler<MouseEvent> InputHandler);

        /// <summary>
        /// Adds a listener class
        /// </summary>
        /// <param name="Input"></param>
        void AddScrollListener(EventHandler<MouseScrollEvent> Input);

        /// <summary>
        /// Removes a listener class
        /// </summary>
        /// <param name="InputHandler"></param>
        void RemoveScrollListener(EventHandler<MouseScrollEvent> InputHandler);
    }
}
