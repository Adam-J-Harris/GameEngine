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
using Engine.Managers.Interfaces;
using Engine.Objects.Minds.Interfaces;

namespace Engine.Managers
{
    public class MindM : IMindManager
    {
        private static IMindManager instance;

        private IMind mind;

        private int mindNumber = 0;
        private int playerMindNumber = 0;
        private int cameraMindNumber = 0;
        private int ghostMindNumber = 0;
        private int patientMindNumber = 0;

        private IDictionary<string, IMind> mindDict;

        private MindM()
        {
            mindDict = new Dictionary<string, IMind>();
        }

        public static IMindManager getInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MindM();
                }
                return instance;
            }
        }

        /// <summary>
        /// Using generic class T, create a mind of type T
        /// </summary>
        /// <typeparam name="T">IMind or subclass of IMind</typeparam>
        public IMind Create<T>() where T : IMind, new()
        {
            mind = new T();
            mindNumber++;

            //if (mind.getSetName == "PlayerMind")
            //{
            //    playerMindNumber++;
            //    mind.getSetNumber = playerMindNumber;
            //}
            //else if (mind.getSetName == "CameraMind")
            //{
            //    cameraMindNumber++;
            //    mind.getSetNumber = cameraMindNumber;
            //}
            //else if (mind.getSetName == "GhostMind")
            //{
            //    ghostMindNumber++;
            //    mind.getSetNumber = ghostMindNumber;
            //}
            //else if (mind.getSetName == "PatientMind")
            //{
            //    patientMindNumber++;
            //    mind.getSetNumber = patientMindNumber;
            //}

            //mind.getSetID = mind.getSetName + mind.getSetNumber;

            //AddToDict(mind.getSetID, mind);

            return mind;
        }

        /// <summary>
        /// Adds a type IMind to the dictionary
        /// </summary>
        /// <param name="mind">IMind, the value of the entry</param>
        public void AddToDict(IMind mind)
        {
            mindDict.Add(mind.getSetID, mind);
        }

        /// <summary>
        /// Adds a type IMind to the dictionary
        /// </summary>
        /// <param name="id">string, the key of the entry</param>
        /// <param name="mind">IMind, the value of the entry</param>
        public void AddToDict(string id, IMind mind)
        {
            mindDict.Add(id, mind);
        }

        public IDictionary<string, IMind> getDictionary
        {
            get { return mindDict; }
        }

        /// <summary>
        /// Using the string type, Returns a value from the mind with the key type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IMind getMind(string type)
        {
            IMind mind = null;

            // Search using the key string 
            if (mindDict.ContainsKey(type))
            {
                // Get the value from the key
                mind = mindDict[type];
            }

            return mind;
        }

        /// <summary>
        /// Removes a key-value from the Dictionary
        /// </summary>
        /// <param name="type">key of value to be removed</param>
        public void Remove(string type)
        {
            // Search using the key string 
            if (mindDict.ContainsKey(type))
            {
                // Get the value from the key
                mind = mindDict[type];
            }

            mind = null;

            GC.Collect();
        }

        /// <summary>
        /// Removes all items from the dictionary but keeps camera and player
        /// </summary>
        public void UnloadMinds()
        {
            //IMind camera = getMind("CameraMind1");
            //IMind player = getMind("PlayerMind1");

            mindDict.Clear();
            mindNumber = 0;

            //mindDict.Add(player.getSetID, player);
            //mindDict.Add(camera.getSetID, camera);

            //mindNumber = 2;
        }

        /// <summary>
        /// Update each mind in the dictionary
        /// </summary>
        public void Update()
        {
            foreach (KeyValuePair<string, IMind> mind in mindDict)
            {
                mind.Value.Update();
            }
        }

    }

}
