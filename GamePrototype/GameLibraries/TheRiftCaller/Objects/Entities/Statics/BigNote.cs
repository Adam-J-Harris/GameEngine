using Engine.Objects.Entities;
using Engine.Objects.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Engine.Managers;
using Microsoft.Xna.Framework;

namespace TheRiftCaller.Objects.Entities.Statics
{
    public class BigNote : ASmartObject, IBigNote
    {
        private int floor;
        private string noteId;

        public BigNote()
        {
            getSetID = "BigNote1";

            //getSetTexture = "BigNote" + id;

        }

        public void Decipher(string id, int floorId)
        {
            floor = floorId;
            noteId = id;

            if (floor == 0) // lower ground floor
            {
                switch (noteId)
                {
                    case "Note1":
                        Debug.WriteLine("I found some drinks down the spiral staircase near one of the side rooms, yumyum");
                        getSetTexture = ImageM.getInstance.getAsset("Floor0Note1");
                        break;
                    case "Note2":
                        Debug.WriteLine("Wilhelm Roentgen created the X-ray machine in 1895");
                        getSetTexture = ImageM.getInstance.getAsset("Floor0Note2");
                        break;
                    case "Note3":
                        Debug.WriteLine("Look at the awesome xray images! Wow");
                        getSetTexture = ImageM.getInstance.getAsset("Floor0Note3");
                        break;
                    default:
                        break;
                }
            }
            else if (floor == 1) // ground floor
            {
                switch (noteId)
                {
                    case "Note1":
                        Debug.WriteLine("The chapel is up ahead, be careful of his long hairy arms!!");
                        getSetTexture = ImageM.getInstance.getAsset("NoteFullSize1");
                        break;
                    case "Note2":
                        Debug.WriteLine("Postcard from black pool, Dance card");
                        getSetTexture = ImageM.getInstance.getAsset("Floor1Note2");
                        break;
                    case "Note3":
                        Debug.WriteLine("Events so far in the game");
                        getSetTexture = ImageM.getInstance.getAsset("NoteFullSize3");
                        break;
                    case "Note4":
                        Debug.WriteLine("Charles Wheeley Lea Ward, £10k");
                        getSetTexture = ImageM.getInstance.getAsset("NoteFullSize4");
                        break;
                    default:
                        break;
                }
            }
            else if (floor == 2) // first floor
            {
                switch (noteId)
                {
                    case "Note1":
                        Debug.WriteLine("Ganderton Ward");
                        getSetTexture = ImageM.getInstance.getAsset("Floor2Note1");
                        break;
                    case "Note2":
                        Debug.WriteLine("Operation room, built because no room");
                        getSetTexture = ImageM.getInstance.getAsset("Floor2Note2");
                        break;
                    default:
                        break;
                }
            }

            getSetLocation = new Vector2(EntityM.getInstance.getEntity("Player1").getSetLocation.X - 200, EntityM.getInstance.getEntity("Player1").getSetLocation.Y - 200);


        }
    }
}
