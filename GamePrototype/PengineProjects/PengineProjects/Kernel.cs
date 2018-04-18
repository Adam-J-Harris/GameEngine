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
﻿namespace PengineProjects
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Kernel
    {
        private static Kernel pGameInstance;

        private Kernel()
        {

        }

        // Code to check if there is an instance already created of Game1
        // Singleton Code
        public static Kernel GameInstance
        {
            get
            {
                // If there is no current instance
                if (pGameInstance == null)
                {
                    // Create a new instance
                    pGameInstance = new Kernel();
                }
                // Return GameInstance
                return pGameInstance;
            }
        }

        // Begin the game, using Run();
        public void StartInitialise()
        {
            // Run() awayyyy
            //SceneMSandbox.getInstance.Run();
            SceneMTheRiftCaller.getInstance.Run();
            //SceneMComputingProject.getInstance.Run();
        }
    }
}