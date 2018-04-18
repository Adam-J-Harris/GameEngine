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
using Engine.Objects.Entities.Interfaces;
using Engine.Objects.Minds.Interfaces;
using Microsoft.Xna.Framework;
using TheRiftCaller.AI.Behaviours;
using TheRiftCaller.Factories.Interfaces;
using TheRiftCaller.Objects.Entities;
using TheRiftCaller.Objects.Entities.Statics;
using TheRiftCaller.Objects.Minds;

namespace TheRiftCaller.Factories
{
    public class Factory : IFactory
    {
        private static IFactory instance;

        private IEntity tempEntity;
        private IMind tempMind;

        private Factory()
        {

        }

        // Singleton Code
        public static IFactory getInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Factory();
                }
                return instance;
            }
        }

        /// <summary>
        /// Using an array of numbers, creates entities depending on the value in the array
        /// </summary>
        /// <param name="objectArray">Array with information about which entity to create</param>
        public void GenerateObjects(int[] objectArray)
        {
            for (int a = 0; a < objectArray.Length; a++)
            {
                for (int b = 0; b < objectArray[a]; b++)
                {
                    switch (a)
                    {
                        case 0:
                            tempEntity = EntityM.getInstance.Create<Floor>();
                            break;

                        case 1:
                            tempEntity = EntityM.getInstance.Create<Wall>();
                            break;

                        case 2:
                            tempEntity = EntityM.getInstance.Create<Stair>();
                            break;

                        case 3:
                            tempEntity = EntityM.getInstance.Create<Window>();
                            break;

                        case 4:
                            tempEntity = EntityM.getInstance.Create<Handrail>();
                            break;

                        case 5:
                            tempEntity = EntityM.getInstance.Create<Door>();
                            break;

                        case 6:
                            tempEntity = EntityM.getInstance.Create<Note>();
                            break;

                        case 7:
                            tempEntity = EntityM.getInstance.Create<Key>();
                            break;

                        default:
                            Console.WriteLine("Missing Class");
                            break;
                    }

                    tempEntity.getSetNumber = b + 1;

                    
                        tempEntity.getSetID = tempEntity.getSetName + tempEntity.getSetNumber;

                    //if (!PlayerMind.inventory.ContainsKey(tempEntity.getSetID))
                    //{
                        EntityM.getInstance.AddToDict(tempEntity);
                    //}                    
                }
            }
        }

        /// <summary>
        /// Create an IEntity of type Player, create an IMind of type PlayerMind, link entity and mind, set location
        /// </summary>
        public void GeneratePlayer(string floor)
        {
            tempEntity = EntityM.getInstance.Create<Player>();
            tempEntity.getSetNumber = 1;
            tempEntity.getSetID = tempEntity.getSetName + tempEntity.getSetNumber;

            EntityM.getInstance.AddToDict(tempEntity);

            tempMind = MindM.getInstance.Create<PlayerMind>();
            tempMind.getSetNumber = 1;
            tempMind.getSetID = tempMind.getSetName + tempMind.getSetNumber;

            MindM.getInstance.AddToDict(tempMind);

            TargetM.getInstance.SetEntityToMind(EntityM.getInstance.getEntity("Player1"), MindM.getInstance.getMind("PlayerMind1"));

            Vector2 location = new Vector2();

            switch (floor)
            {
                case "TextMaps\\Floor0.txt":
                    location = new Vector2(1472, 1700);
                    break;
                case "TextMaps\\Floor1.txt":
                    // Location should be set in the FloorManager or MapManager or something
                    location = new Vector2(1700, 1700);
                    break;
                case "TextMaps\\Floor2.txt":
                    // Location should be set in the FloorManager or MapManager or something
                    location = new Vector2(1700, 1700);
                    break;
                case "TextMaps\\Floor3.txt":
                    location = new Vector2(1700, 1700);
                    break;
                case "TextMaps\\Chapel.txt":
                    break;
                default:
                    break;
            }

            EntityM.getInstance.getEntity("Player1").getSetLocation = location;
            MindM.getInstance.getMind("PlayerMind1").SetValues();

            tempMind = MindM.getInstance.Create<CameraMind>();
            tempMind.getSetNumber = 1;
            tempMind.getSetID = tempMind.getSetName + tempMind.getSetNumber;

            MindM.getInstance.AddToDict(tempMind);

            tempEntity = EntityM.getInstance.Create<Filter>();
            tempEntity.getSetNumber = 1;
            tempEntity.getSetID = tempEntity.getSetName + tempEntity.getSetNumber;

            EntityM.getInstance.AddToDict(tempEntity);

            TargetM.getInstance.SetTargetToEntity((ITargetable)MindM.getInstance.getMind("CameraMind1"), EntityM.getInstance.getEntity("Player1"));
        }

        /// <summary>
        /// Create an IEntity of type Player, create an IMind of type PlayerMind, link entity and mind, set location
        /// </summary>
        /// <param name="pLocation">Vector2, New location coordinates for the player entity</param>
        public void GeneratePlayer(Vector2 pLocation)
        {
            tempEntity = EntityM.getInstance.Create<Player>();
            tempEntity.getSetNumber = 1;
            tempEntity.getSetID = tempEntity.getSetName + tempEntity.getSetNumber;

            EntityM.getInstance.AddToDict(tempEntity);

            tempMind = MindM.getInstance.Create<PlayerMind>();
            tempMind.getSetNumber = 1;
            tempMind.getSetID = tempMind.getSetName + tempMind.getSetNumber;

            MindM.getInstance.AddToDict(tempMind);

            TargetM.getInstance.SetEntityToMind(EntityM.getInstance.getEntity("Player1"), MindM.getInstance.getMind("PlayerMind1"));

            // Location should be set in the FloorManager or MapManager or something
            EntityM.getInstance.getEntity("Player1").getSetLocation = pLocation;
            MindM.getInstance.getMind("PlayerMind1").SetValues();
        }

        /// <summary>
        /// Create IEntities and IMinds which will represent enemy characters, link the entity and the mind
        /// </summary>
        public void GenerateEnemies()
        {


            tempEntity = EntityM.getInstance.Create<Patient>();
            tempEntity.getSetNumber = 1;
            tempEntity.getSetID = tempEntity.getSetName + tempEntity.getSetNumber;

            EntityM.getInstance.AddToDict(tempEntity);

            tempMind = MindM.getInstance.Create<PatientMind>();
            tempMind.getSetNumber = 1;
            tempMind.getSetID = tempMind.getSetName + tempMind.getSetNumber;

            MindM.getInstance.AddToDict(tempMind);

            TargetM.getInstance.SetEntityToMind(EntityM.getInstance.getEntity("Patient1"), MindM.getInstance.getMind("PatientMind1"));

            Vector2 location = new Vector2(2250, 1450);
            EntityM.getInstance.getEntity("Patient1").getSetLocation = location;
            MindM.getInstance.getMind("PatientMind1").SetValues();

            tempEntity = EntityM.getInstance.Create<Ghost>();
            tempEntity.getSetNumber = 1;
            tempEntity.getSetID = tempEntity.getSetName + tempEntity.getSetNumber;

            EntityM.getInstance.AddToDict(tempEntity);

            tempMind = MindM.getInstance.Create<GhostMind>();
            tempMind.getSetNumber = 1;
            tempMind.getSetID = tempMind.getSetName + tempMind.getSetNumber;

            MindM.getInstance.AddToDict(tempMind);

            TargetM.getInstance.SetEntityToMind(EntityM.getInstance.getEntity("Ghost1"), MindM.getInstance.getMind("GhostMind1"));

            location = new Vector2(3600, 1450);
            EntityM.getInstance.getEntity("Ghost1").getSetLocation = location;
            MindM.getInstance.getMind("GhostMind1").SetValues();
        }

    }

}
