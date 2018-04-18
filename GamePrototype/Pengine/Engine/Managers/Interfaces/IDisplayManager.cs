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
using Engine.Screens.Interfaces;
using Microsoft.Xna.Framework;

namespace Engine.Managers.Interfaces
{
    public interface IDisplayManager
    {
        /// <summary>
        /// Creates a new GameScreen, set it to currentScreen
        /// </summary>
        void NewScene(IScreen scr);

        /// <summary>
        /// Call to create a new scene
        /// Initialise the scene
        /// </summary>
        void Initialise(IScreen scr);

        /// <summary>
        /// Load the content of the IGameScreen currentScreen
        /// </summary>
        void LoadContent();

        /// <summary>
        /// Unload the content of the IGameScreen currentScreen
        /// </summary>
        void UnloadContent();

        /// <summary>
        /// Update the IGameScreen currentScreen
        /// </summary>
        void Update(GameTime gt);

        void UpdateScreenTexturesHPA(List<IPathNode> path);
        void UpdateScreenTextures(IList<Point> list);
        void UpdateScreenTextures(IList<Node> list);
        void UpdateScreenEntranceTextures(IDictionary<Node, string> nodelist);

        void ResetScreenTextures();


        void GetStopWatch(Stopwatch stopwatch);

        TimeSpan GetStopWatchTime();

        /// <summary>
        /// Returns the value of the currentScreen
        /// </summary>
        /// <returns>IScreen</returns>
        IScreen getCurrentScene { get; }
    }
}
