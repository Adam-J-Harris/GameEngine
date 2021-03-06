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
using Engine.AI.Behaviours.Interfaces;
using Engine.AI.StateMachine.Interfaces;
using Engine.Managers;
using Engine.Objects.Minds.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.AI.StateMachine
{
    public abstract class AFSM : IFSM
    {
        private IDictionary<string, IState> states;

        private IState currentState;
        private IState previousState;

        public AFSM()
        {
            states = new Dictionary<string, IState>();
        }

        public void ApplyStates()
        {
            states = BehaviourM.getInstance.getDictionary;
        }

        public void ApplyStates(IList<IState> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                states.Add(list[i].getSetName, list[i]);
            }
        }

        public IState getSetCurrentState
        {
            get { return currentState; }
            set { currentState = value; }
        }

        public IState getSetPreviousState
        {
            get { return previousState; }
            set { previousState = value; }
        }
    }
}
