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
using Engine.Managers;
using Engine.Maps;
using TheRiftCaller.Managers.Interfaces;

namespace TheRiftCaller.Managers
{
    public class DimensionM : IDimensionManager
    {
        private static IDimensionManager instance = null;

        private string dimension = "R";
        private bool switcher = false;

        private DimensionM()
        {

        }

        public static IDimensionManager getInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DimensionM();
                }
                return instance;
            }
        }

        /// <summary>
        /// Changes the value of the string dimension
        /// </summary>
        public void ChangeDimension()
        {            
            if (dimension == "R")
            {
                dimension = "P";
            }
            else
            {
                dimension = "R";
            }

            //switcher = true;

            EntityM.getInstance.getEntity("Filter1").getSetTexture = ImageM.getInstance.getAsset(dimension + "FilterLight");
        }

        /// <summary>
        /// Return the value of the string dimension
        /// </summary>
        public string getDimension
        {
            get { return dimension; }
        }

        /// <summary>
        /// Set or Return the value of the bool switcher
        /// 
        /// Switcher becomes true when the dimension has been changed
        /// </summary>
        public bool getSetSwitcher
        {
            get { return switcher; }
            set { switcher = value; }
        }

    }

}
