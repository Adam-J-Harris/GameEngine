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
using Engine.Objects.Minds.Interfaces;

namespace Engine.Managers.Interfaces
{
    public interface IMindManager
    {
        /// <summary>
        /// Using generic class T, create a mind of type T
        /// </summary>
        /// <typeparam name="T">IMind or subclass of IMind</typeparam>
        IMind Create<T>() where T : IMind, new();

        /// <summary>
        /// Adds a type IMind to the dictionary
        /// </summary>
        /// <param name="mind">IMind, the value of the entry</param>
        void AddToDict(IMind mind);

        /// <summary>
        /// Adds a type IMind to the dictionary
        /// </summary>
        /// <param name="id">string, the key of the entry</param>
        /// <param name="mind">IMind, the value of the entry</param>
        void AddToDict(string id, IMind mind);

        /// <summary>
        /// Returns the value of the dictionary
        /// </summary>
        IDictionary<string, IMind> getDictionary { get; }

        /// <summary>
        /// Using the string type, Returns a value from the mind with the key type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IMind getMind(string type);

        /// <summary>
        /// Removes a key-value from the Dictionary
        /// </summary>
        /// <param name="type">key of value to be removed</param>
        void Remove(string type);

        void UnloadMinds();

        /// <summary>
        /// Update each mind in the dictionary
        /// </summary>
        void Update();
    }
}
