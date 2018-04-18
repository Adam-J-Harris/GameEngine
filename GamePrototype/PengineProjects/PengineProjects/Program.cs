﻿using System;

namespace PengineProjects
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Kernel.GameInstance.StartInitialise();
        }
    }
#endif
}
