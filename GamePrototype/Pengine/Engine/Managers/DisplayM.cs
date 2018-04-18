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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.AI.Pathfinding;
using Engine.AI.Pathfinding.HPA;
using Engine.Managers.Interfaces;
using Engine.Screens.Interfaces;
using Microsoft.Xna.Framework;

namespace Engine.Managers
{
    public class DisplayM : IDisplayManager
    {
        private static IDisplayManager instance = null;

        private IScreen currentScreen;

        public static GameTime timer;

        public static Stopwatch animTimer;

        private Stopwatch Stop;

        private DisplayM()
        {

        }

        /// <summary>
        /// If instance is null
        ///     Create a new DisplayM, set to instance
        /// Return instance
        /// </summary>
        public static IDisplayManager getInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DisplayM();
                }
                return instance;
            }
        }

        /// <summary>
        /// Creates a new GameScreen, set it to currentScreen
        /// </summary>
        public void NewScene(IScreen scr)
        {
            animTimer = Stopwatch.StartNew();
            currentScreen = scr;
        }

        /// <summary>
        /// Call to create a new scene
        /// Initialise the scene
        /// </summary>
        public void Initialise(IScreen scr)
        {
            NewScene(scr);
            currentScreen.Initialise();
        }

        /// <summary>
        /// Load the content of the IGameScreen currentScreen
        /// </summary>
        public void LoadContent()
        {
            currentScreen.LoadContent();
        }

        /// <summary>
        /// Unload the content of the IGameScreen currentScreen
        /// </summary>
        public void UnloadContent()
        {
            currentScreen.UnloadContent();
        }

        public void UpdateScreenTexturesHPA(List<IPathNode> path)
        {
            currentScreen.getThisAsGameScreen.UpdateTextures(path);
        }

        public void UpdateScreenTextures(IList<Point> list)
        {
            currentScreen.getThisAsGameScreen.UpdateTextures(list);
        }

        public void UpdateScreenTextures(IList<Node> nodelist)
        {
            currentScreen.getThisAsGameScreen.UpdateTextures(nodelist);
        }

        public void UpdateScreenEntranceTextures(IDictionary<Node, string> nodelist)
        {
            currentScreen.getThisAsGameScreen.UpdateEntranceTextures(nodelist);
        }

        public void ResetScreenTextures()
        {
            currentScreen.getThisAsGameScreen.ResetTextures();
        }

        /// <summary>
        /// Update the IGameScreen currentScreen
        /// </summary>
        public void Update(GameTime gt)
        {
            timer = gt;
            currentScreen.Update();
        }


        public void GetStopWatch(Stopwatch stopwatch)
        {
            Stop = stopwatch;
        }

        public TimeSpan GetStopWatchTime()
        {
            return Stop.Elapsed;
        }

        /// <summary>
        /// Returns the value of the currentScreen
        /// </summary>
        /// <returns>IGameScreen</returns>
        public IScreen getCurrentScene
        {
            get { return currentScreen; }
        }

    }

}
