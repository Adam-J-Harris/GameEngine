using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Objects.Entities;
using Engine.Managers;
using System.Diagnostics;
using Engine.Screens.Interfaces;
using TheRiftCaller.Screens;

namespace TheRiftCaller.Objects.Entities.Statics
{
    public class Note : ASmartObject
    {
        public bool collected = false;
        public Note()
        {
            getSetName = "Note";
            getSetTexture = ImageM.getInstance.getAsset("Note");

            Debug.WriteLine(getSetID);
        }

        public override void Interact()
        {
            base.Interact();

            
            Console.WriteLine("Using the Note " + getSetID);

            //getSetTexture = ImageM.getInstance.getAsset("NoteFullSizeTemplate");

            NoteScreen.getInstance.Initialise(getSetID);

            collected = true;
        }
    }
}
