using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Managers;
using Engine.Maps;
using Engine.Objects.Entities.Interfaces;
using Engine.Screens;
using Microsoft.Xna.Framework.Graphics;
using TheRiftCaller.Managers;
using TheRiftCaller.Objects.Entities.Statics;

namespace TheRiftCaller.Screens
{
    public class NoteScreen : AGameScreen, INoteScreen
    {
        private string path;

        private static INoteScreen instance;

        private IBigNote bn;

        private NoteScreen()
        {
            bn = new BigNote();

            EntityM.getInstance.AddToDict((IEntity)bn);
        }

        public static INoteScreen getInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NoteScreen();
                }
                return instance;
            }
        }

        public void Initialise(string id)
        {
            if (!EntityM.getInstance.getDictionary.ContainsKey("BigNote1"))
            {
                EntityM.getInstance.AddToDict((IEntity)bn);
            }

            bn.Decipher(id, DisplayM.getInstance.getCurrentScene.getThisAsGameScreen.getSetMap.getSetCurrentLevel);

            ICollidable c = (ICollidable)bn;
            c.SetUpHitbox();
        }

        /// <summary>
        /// Intial set up of the screen
        /// </summary>
        public override void Initialise()
        {
            /// What note needs to be displayed
            // currentDimension
            // currentFloor
            // currentCluster? 1 note max per cluster, to reduce confusion

            //path = "Note" + getSetMap.getCurrentFloor + DimensionM.getInstance.getDimension + getSetMap.getCluster.getMapPos;
        }

        /// <summary>
        /// Load the content which will be shown on the screen
        /// </summary>
        public override void LoadContent()
        {
            // Load the note
        }

        /// <summary>
        /// Unload the content currently on the screen
        /// </summary>
        public override void UnloadContent()
        {
            // Unload the note
        }

        /// <summary>
        /// Update the screen
        /// </summary>
        public override void Update()
        {
            // Check for player closing the note
        }
    }
}