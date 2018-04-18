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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Managers;
using Engine.Maps;
using Microsoft.Xna.Framework;
using TheRiftCaller.Factories;
using TheRiftCaller.Managers.Interfaces;
using Engine.AI.Managers;
using TheRiftCaller.Objects.Entities.Statics;

namespace TheRiftCaller.Managers
{
    public class LevelM : ILevelManager
    {
        private static ILevelManager instance = null;

        private string text;

        private int height;
        private string[] lines;

        private int width;
        private string[] chars;

        private Vector2 size;

        private int[,] textmap;
        private bool[,] boolMap;

        private int heightOffset = 64;
        private int widthOffset = 64;
        private int offset = 64;

        // Numbers to send to the factory
        private int floorNo;
        private int wallNo;
        private int stairNo;
        private int windowNo;
        private int handrailNo;
        private int doorNo;
        private int noteNo;
        private int keyNo;

        // Numbers which find the entities from the entityDictionary in EntityM
        private int floorNumber;
        private int wallNumber;
        private int stairNumber;
        private int windowNumber;
        private int handrailNumber;
        private int doorNumber;
        private int noteNumber;
        private int keyNumber;

        private int[] objectsArray;

        private Vector2 location;

        private LevelM()
        {

        }

        public static ILevelManager getInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LevelM();
                }
                return instance;
            }
        }

        /// <summary>
        /// Reset all the numbers 
        /// Used when switching maps or screens
        /// </summary>
        public void ResetNumbers(bool b)
        {
            if (b)
            {
                floorNo = 0;
                wallNo = 0;
                stairNo = 0;
                windowNo = 0;
                handrailNo = 0;
                doorNo = 0;
                noteNo = 0;
                keyNo = 0;
            }

            floorNumber = 0;
            wallNumber = 0;
            stairNumber = 0;
            windowNumber = 0;
            handrailNumber = 0;
            doorNumber = 0;
            noteNumber = 0;
            keyNumber = 0;
        }

        public Vector2 getSetSize
        {
            get { return size; }
        }

        /// <summary>
        /// Applies value to each element in the int[,] textMap
        /// Applies value to each element in the bool[,] boolMap
        /// </summary>
        /// <param name="textPath">Directory where the .txt file is located</param>
        public void SettingLocationsFromFile(string[] lines)
        {
            //text = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), textPath));

            width = 128;
            height = 64;

            textmap = new int[width, height];
            boolMap = new bool[width, height];

            ResetNumbers(true);

            //lines = textPath.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            for (int j = 0; j < lines.Length; j++)
            {
                chars = lines[j].Split(new string[] { "," }, StringSplitOptions.None);

                for (int i = 0; i < chars.Length; i++)
                {
                    textmap[i, j] = short.Parse(chars[i]);

                    switch (chars[i])
                    {
                        case "0":
                            floorNo++;
                            break;

                        case "1":
                            wallNo++;
                            break;

                        case "2":
                            stairNo++;
                            break;

                        case "3":
                            windowNo++;
                            break;

                        case "4":
                            handrailNo++;
                            break;

                        case "5":
                            doorNo++;
                            floorNo++;
                            break;

                        case "6":
                            stairNo++;
                            break;

                        case "7":
                            noteNo++;
                            floorNo++;
                            break;

                        case "8":
                            keyNo++;
                            floorNo++;
                            break;

                        default:
                            //Console.WriteLine("Missing information");
                            break;
                    }
                }
            }

            size = new Vector2(width, height);

            objectsArray = new int[] { floorNo, wallNo, stairNo, windowNo, handrailNo, doorNo, noteNo, keyNo };
            Factory.getInstance.GenerateObjects(objectsArray);

            ResetNumbers(false);

            heightOffset = 0;

            // 18 squares
            for (int y = 0; heightOffset < (height * offset); y++)
            {
                widthOffset = 0;

                // 32 squares
                for (int x = 0; widthOffset < (width * offset); x++)
                {
                    if (textmap[x, y].ToString() == "0")
                    // inside floor
                    {
                        boolMap[x, y] = true;
                        location = new Vector2(widthOffset - 32, heightOffset - 32);
                        floorNumber++;
                        EntityM.getInstance.getEntity("Floor" + floorNumber).getSetLocation = location;
                        EntityM.getInstance.getEntity("Floor" + floorNumber).getSetPointLocation = new Point(x, y);
                    }
                    else if (textmap[x, y].ToString() == "9")
                    // outside floor
                    {
                        boolMap[x, y] = false;

                    }
                    else if (textmap[x, y].ToString() == "1")
                    // Wall
                    {
                        boolMap[x, y] = false;
                        location = new Vector2(widthOffset - 32, heightOffset - 32);
                        wallNumber++;
                        EntityM.getInstance.getEntity("Wall" + wallNumber).getSetLocation = location;
                        EntityM.getInstance.getEntity("Wall" + wallNumber).getSetPointLocation = new Point(x, y);
                    }
                    else if (textmap[x, y].ToString() == "2")
                    // Stair
                    {
                        boolMap[x, y] = false;
                        location = new Vector2(widthOffset - 32, heightOffset - 32);
                        stairNumber++;
                        EntityM.getInstance.getEntity("Stair" + stairNumber).getSetLocation = location;
                        EntityM.getInstance.getEntity("Stair" + stairNumber).getSetPointLocation = new Point(x, y);
                        Stair tempent = (Stair)EntityM.getInstance.getEntity("Stair" + stairNumber);
                        tempent.getSetStairType = "Up";
                    }
                 
                    else if (textmap[x, y].ToString() == "3")
                    // Window
                    {
                        boolMap[x, y] = false;
                        location = new Vector2(widthOffset - 32, heightOffset - 32);
                        windowNumber++;
                        EntityM.getInstance.getEntity("Window" + windowNumber).getSetLocation = location;
                        EntityM.getInstance.getEntity("Window" + windowNumber).getSetPointLocation = new Point(x, y);
                    }
                    else if (textmap[x, y].ToString() == "4")
                    // Handrail
                    {
                        boolMap[x, y] = false;
                        location = new Vector2(widthOffset - 32, heightOffset - 32);
                        handrailNumber++;
                        EntityM.getInstance.getEntity("Handrail" + handrailNumber).getSetLocation = location;
                        EntityM.getInstance.getEntity("Handrail" + handrailNumber).getSetPointLocation = new Point(x, y);
                    }
                    else if (textmap[x, y].ToString() == "5")
                    // Door + floor
                    {
                        boolMap[x, y] = false;
                        location = new Vector2(widthOffset - 32, heightOffset - 32);
                        doorNumber++;
                        EntityM.getInstance.getEntity("Door" + doorNumber).getSetLocation = location;
                        EntityM.getInstance.getEntity("Door" + doorNumber).getSetPointLocation = new Point(x, y);

                        floorNumber++;
                        EntityM.getInstance.getEntity("Floor" + floorNumber).getSetLocation = location;
                        EntityM.getInstance.getEntity("Floor" + floorNumber).getSetPointLocation = new Point(x, y);
                    }
                    else if (textmap[x, y].ToString() == "6")
                    // Stair
                    {
                        boolMap[x, y] = false;
                        location = new Vector2(widthOffset - 32, heightOffset - 32);
                        stairNumber++;
                        EntityM.getInstance.getEntity("Stair" + stairNumber).getSetLocation = location;
                        EntityM.getInstance.getEntity("Stair" + stairNumber).getSetPointLocation = new Point(x, y);
                        Stair tempent = (Stair)EntityM.getInstance.getEntity("Stair" + stairNumber);
                        tempent.getSetStairType = "Down";
                    }
                    else if (textmap[x, y].ToString() == "7")
                    // note + floor
                    {
                        boolMap[x, y] = true;
                        location = new Vector2(widthOffset - 32, heightOffset - 32);
                        noteNumber++;
                        EntityM.getInstance.getEntity("Note" + noteNumber).getSetLocation = location;
                        EntityM.getInstance.getEntity("Note" + noteNumber).getSetPointLocation = new Point(x, y);

                        floorNumber++;
                        EntityM.getInstance.getEntity("Floor" + floorNumber).getSetLocation = location;
                        EntityM.getInstance.getEntity("Floor" + floorNumber).getSetPointLocation = new Point(x, y);
                    }
                    else if (textmap[x, y].ToString() == "8")
                    // Key + floor
                    {
                        boolMap[x, y] = true;
                        location = new Vector2(widthOffset - 32, heightOffset - 32);
                        keyNumber++;
                        EntityM.getInstance.getEntity("Key" + keyNumber).getSetLocation = location;
                        EntityM.getInstance.getEntity("Key" + keyNumber).getSetPointLocation = new Point(x, y);

                        floorNumber++;
                        EntityM.getInstance.getEntity("Floor" + floorNumber).getSetLocation = location;
                        EntityM.getInstance.getEntity("Floor" + floorNumber).getSetPointLocation = new Point(x, y);
                    }
                    else
                    {
                        Console.WriteLine("Missing class information");
                    }

                    widthOffset += 64;
                }
                heightOffset += 64;
            }
            NodeM.getInstance.InitialiseNodes(boolMap);
        }

        public bool[,] getBoolMap
        {
            get { return boolMap; }
        }

    }

}