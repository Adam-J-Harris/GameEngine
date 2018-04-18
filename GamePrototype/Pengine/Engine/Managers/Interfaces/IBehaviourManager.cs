/*
MIT License

Copyright (c) 2016 Duncan Baldwin

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
using Engine.AI;
using Engine.AI.Behaviours.Interfaces;

namespace Engine.Managers.Interfaces
{
    public interface IBehaviourManager
    {
        /// <summary>
        /// Using generic class 'T', creates a behaviour of type IBehaviour
        /// </summary>
        /// <typeparam name="T">IBehaviour or sub-class, the class type representing the IBehaviour value</typeparam>
        void Create<T>() where T : IState, new();

        void CreatePersonality<T>() where T : IPersonality, new();

        /// <summary>
        /// Adds an IBehaviour b to the Dictionary behaves using b.getSetPersonality and b
        /// </summary>
        /// <param name="b">IBehaviour to be added</param>
        void AddToDictionary(IState b);

        /// <summary>
        /// Returns the value of the IDictionary behaves
        /// </summary>
        IDictionary<string, IState> getDictionary { get; }

        IPersonality GetPersonality(string key);

        IState GetState(string key);

        void UnloadPersonalities();

        /// <summary>
        /// Returns a list of all behaviours which have the Personality type of mindP
        /// </summary>
        /// <param name="mindP">The personality state the mind is currently in</param>
        /// <returns></returns>
        IList<IState> getAvailableBehaviours(IPersonality mindP);
    }
}
