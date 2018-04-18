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
using Engine.Managers.Interfaces;
using Engine.Objects.Minds.Interfaces;

namespace Engine.Managers
{
    public class BehaviourM : IBehaviourManager
    {
        private static IBehaviourManager instance = null;

        private IDictionary<string, IPersonality> personalities;
        private IPersonality personality;

        private IDictionary<string, IState> states;
        private IState state;

        private BehaviourM()
        {
            personalities = new Dictionary<string, IPersonality>();
            states = new Dictionary<string, IState>();
        }

        public static IBehaviourManager getInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BehaviourM();
                }
                return instance;
            }
        }

        /// <summary>
        /// Using generic class 'T', creates a behaviour of type IBehaviour
        /// </summary>
        /// <typeparam name="T">IBehaviour or sub-class, the class type representing the IBehaviour value</typeparam>
        public void Create<T>() where T : IState, new()
        {
            state = new T();

            AddToDictionary(state);
        }

        public void CreatePersonality<T>() where T : IPersonality, new()
        {
            personality = new T();

            AddToDictionary(personality);
        }

        /// <summary>
        /// Adds an IBehaviour b to the Dictionary behaves using b.getSetPersonality and b
        /// </summary>
        /// <param name="b">IBehaviour to be added</param>
        public void AddToDictionary(IState b)
        {
            states.Add(b.getSetName, b);
        }

        public void AddToDictionary(IPersonality p)
        {
            personalities.Add(p.getSetName, p);
        }

        /// <summary>
        /// Returns the value of the IDictionary behaves
        /// </summary>
        public IDictionary<string, IState> getDictionary
        {
            get { return states; }
        }

        public IDictionary<string, IPersonality> getPersonalityDictionary
        {
            get { return personalities; }
        }

        public IPersonality GetPersonality(string key)
        {
            return personalities[key];
        }

        public IState GetState(string key)
        {
            return states[key];
        }

        /// <summary>
        /// Returns a list of all behaviours which have the Personality type of mindP
        /// </summary>
        /// <param name="mindP">The personality state the mind is currently in</param>
        /// <returns></returns>
        public IList<IState> getAvailableBehaviours(IPersonality mindP)
        {
            IList<IState> list = new List<IState>();

            //foreach (KeyValuePair<IState, IPersonality> p in behaves)
            //{
            //    if (p.Value == mindP)
            //    {
            //        list.Add(p.Key);
            //    }
            //}

            return list;
        }

        public void UnloadPersonalities()
        {
            personalities.Clear();
            states.Clear();
        }

        /// <summary>
        /// Uses the passed minds state machine to switch the current and previous state which is then applied to the mind
        /// </summary>
        /// <param name="mind">IMind which has changed states</param>
        /// <param name="fromState">The current state of the mind</param>
        /// <param name="toState">The next state of the mind</param>
        public void StateSwitcher(IMind mind, IState fromState, IState toState)
        {
            mind.getSetStateMachine.getSetPreviousState = fromState;
            mind.getSetStateMachine.getSetCurrentState = toState;
        }

    }

}
