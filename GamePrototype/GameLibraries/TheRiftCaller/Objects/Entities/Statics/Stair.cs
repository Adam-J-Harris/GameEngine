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
using Engine.Objects.Entities;
using Engine.Objects.Entities.Interfaces;
using TheRiftCaller.Maps;
using System.Diagnostics;

namespace TheRiftCaller.Objects.Entities.Statics
{
    public class Stair : ASmartStructure
    {
        public string getSetStairType;
        public int currentLevel;

        public Stair()
        {
            getSetName = "Stair";
            getSetTexture = ImageM.getInstance.getAsset("Stair");

        
        }

        public override void Interact()
        {
            base.Interact();

            Console.WriteLine("Using the stairs: " + getSetID);

            if (getSetStairType == "Up")
            {
                DisplayM.getInstance.getCurrentScene.getThisAsGameScreen.getSetMap.getSetCurrentLevel += 1;
            }
            else if (getSetStairType == "Down")
            {
                DisplayM.getInstance.getCurrentScene.getThisAsGameScreen.getSetMap.getSetCurrentLevel -= 1;
            }

            currentLevel = DisplayM.getInstance.getCurrentScene.getThisAsGameScreen.getSetMap.getSetCurrentLevel;

            string newLevel = "TextMaps\\Floor" + currentLevel + ".txt";

            DisplayM.getInstance.getCurrentScene.getThisAsGameScreen.getSetMap.getSetMergedFilePath = newLevel;

            Console.WriteLine("Stair Up " + currentLevel);

            // Trigger Map floor change or something

            DisplayM.getInstance.getCurrentScene.getThisAsGameScreen.ChangeFloor();

            Debug.WriteLine(currentLevel);
        }

    }

}
