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
using Microsoft.Xna.Framework;

namespace TheRiftCaller.Factories.Interfaces
{
    public interface IFactory
    {
        /// <summary>
        /// Using an array of numbers, creates entities depending on the value in the array
        /// </summary>
        /// <param name="objectArray">Array with information about which entity to create</param>
        void GenerateObjects(int[] objectArray);

        /// <summary>
        /// Create an IEntity of type Player, create an IMind of type PlayerMind, link entity and mind, set location
        /// </summary>
        void GeneratePlayer(string floor);

        /// <summary>
        /// Create an IEntity of type Player, create an IMind of type PlayerMind, link entity and mind, set location
        /// </summary>
        /// <param name="pLocation">Vector2, New location coordinates for the player entity</param>
        void GeneratePlayer(Vector2 pLocation);

        /// <summary>
        /// Create IEntities and IMinds which will represent enemy characters, link the entity and the mind
        /// </summary>
        void GenerateEnemies();
    }
}
