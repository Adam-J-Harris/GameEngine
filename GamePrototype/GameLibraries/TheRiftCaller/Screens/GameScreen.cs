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
using Engine.Objects.Entities.Interfaces;
using Engine.Screens;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using TheRiftCaller.AI.Behaviours;
using TheRiftCaller.Factories;
using TheRiftCaller.Managers;
using TheRiftCaller.Objects.Entities.Statics;
using TheRiftCaller.Maps.Quadtrees;
using TheRiftCaller.Maps;
using Microsoft.Xna.Framework;
using Engine.AI.Managers;

namespace TheRiftCaller.Screens
{
    public class GameScreen : AGameScreen
    {
        private IList<string> toRemove;
        private bool staticsOnly;

        private Song backgroundMusic;

        private bool firstRun = true;
        private bool firstRunD = true;

        public GameScreen(int pWidth, int pHeight)
        {
            getSetScreenBounds = new Rectangle(-32, -32, pWidth, pHeight);
            toRemove = new List<string> { "Door", "Floor", "Handrail", "Key", "Stair", "Wall", "Window", "Note" };
        }

        /// <summary>
        /// Intial set up of the screen
        /// </summary>
        public override void Initialise()
        {
            
            if (firstRunD)
            {
                getSetMap = new Map();
                getSetMap.getSetMergedFilePath = "TextMaps\\Floor1.txt";
                firstRunD = false;
            }
            getSetMap.SetTextMaps();
            getSetMap.getSetQuadTree = new QuadTree(getSetScreenBounds);
        }

        private void SwitchTextPath()
        {
            getSetMap.SetTextMaps();
        }

        /// <summary>
        /// Load the content which will be shown on the screen
        /// </summary>
        public override void LoadContent()
         {
            SwitchTextPath();

            LevelM.getInstance.SettingLocationsFromFile(getSetMap.getSetMapTextPath);
            //LevelM.getInstance.SettingLocationsTest(Map.getInstance.getSetMergedFilePath);

            if (firstRun)
            {
                GameBehaviour gb = new GameBehaviour();
                Factory.getInstance.GeneratePlayer(getSetMap.getSetMergedFilePath);
                Factory.getInstance.GenerateEnemies();
                firstRun = false;
            }

            backgroundMusic = SoundM.getInstance.getSong("Creeping_Death");

            //MediaPlayer.Play(backgroundMusic);
            //MediaPlayer.Volume = 0.3f;

            foreach (IEntity e in EntityM.getInstance.getDictionary.Values)
            {
                if (e is ICollidable)
                {
                    ICollidable c = (ICollidable)e;
                    c.SetUpHitbox();
                }
            }

            RebuildTree();

            IDictionary<string, IEntity> removal = new Dictionary<string, IEntity>();

            foreach (IEntity e in EntityM.getInstance.getDictionary.Values)
            {
                if (InventoryM.getInstance.inv.ContainsKey(e.getSetID))
                {
                    removal.Add(e.getSetID, e);
                }
            }

            foreach (IEntity e in removal.Values)
            {
                RemoveAnObject(e);
            }
        }

        /// <summary>
        /// Rebuilds the QuadTree
        /// </summary>
        public override void RebuildTree()
        {
            //getSetMap.getSetQuadTree.ClearQuadTree();
            getSetMap.getSetQuadTree.SetUpQuadTree();
            
        }

        /// <summary>
        /// Removes an IEntity type from the quadtree and from the entity list
        /// </summary>
        /// <param name="ent"></param>
        public override void RemoveAnObject(IEntity ent)
        {
            getSetMap.getSetQuadTree.Remove(ent);

            NodeM.getInstance.SetTransitionNode(NodeM.getInstance.GetNode(ent.getSetPointLocation));

            CollisionM.getInstance.DeleteCollidable((ICollidable)ent);

            EntityM.getInstance.Remove(ent.getSetID);

            //RebuildTree();
        }

        /// <summary>
        /// Unload the content currently on the screen
        /// </summary>
        public override void UnloadContent()
        {
            EntityM.getInstance.UnloadEntities(toRemove, staticsOnly);
            MindM.getInstance.UnloadMinds();
            BehaviourM.getInstance.UnloadPersonalities();

            Console.WriteLine(getSetMap.getSetMergedFilePath);
        }

        public override void ChangeFloor()
        {
            getSetMap.getSetQuadTree = new QuadTree(getSetScreenBounds);

            staticsOnly = false;
            UnloadContent();

            Initialise();

            firstRun = true;

            LoadContent();

            Console.WriteLine("DispM Change Floor " + getSetMap.getSetMergedFilePath);
        }

        /// <summary>
        /// Update the screen
        /// </summary>
        public override void Update()
        {
            getSetMap.getSetQuadTree.Update();

            if (DimensionM.getInstance.getSetSwitcher)
            {
                DimensionM.getInstance.getSetSwitcher = false;

                //staticsOnly = false;

                //UnloadContent();
                //LoadContent();
            }
        }

    }

}
