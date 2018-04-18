using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Managers;
using Engine.Maps.Interfaces;
using Engine.Maps.QuadTree.Interfaces;
using Microsoft.Xna.Framework;

namespace Engine.Maps
{
    public abstract class AMap : IMap
    {
        private IQuadTree quadTree;
        private IDictionary<int, IQuadNode> hierarchicalMap = new Dictionary<int, IQuadNode>();

        private string[] textPath;
        private string mergedFilePath;

        private Rectangle mapBounds;

        private int currentLevel = 1;

        public AMap()
        {
            mapBounds = DisplayM.getInstance.getCurrentScene.getSetScreenBounds;
        }

        public virtual void SetTextMaps()
        {

        }

        /// <summary>
        /// Set or Return the value of string textPath
        /// 
        /// textPath is a TextFile located within the Game Library
        /// </summary>
        public string[] getSetMapTextPath
        {
            get { return textPath; }
            set { textPath = value; }
        }

        public string getSetMergedFilePath
        {
            get { return mergedFilePath; }
            set { mergedFilePath = value; }
        }

        public IQuadTree getSetQuadTree
        {
            get { return quadTree; }
            set { quadTree = value; }
        }

        public int getSetCurrentLevel
        {
            get { return currentLevel; }
            set { currentLevel = value; }
        }

        public virtual void RebuildTree()
        {

        }
    }
}
