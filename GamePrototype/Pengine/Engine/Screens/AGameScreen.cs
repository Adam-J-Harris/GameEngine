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
using Engine.AI.Pathfinding;
using Engine.AI.Pathfinding.HPA;
using Engine.Maps.Interfaces;
using Engine.Maps.QuadTree.Interfaces;
using Engine.Screens.Interfaces;
using Microsoft.Xna.Framework;
using Engine.Objects.Entities.Interfaces;

namespace Engine.Screens
{
    public abstract class AGameScreen : AScreen, IGameScreen
    {
        private IMap map;

        public AGameScreen()
        {

        }

        public IMap getSetMap
        {
            get { return map; }
            set { map = value; }
        }

        public virtual void RebuildTree()
        {

        }

        /// <summary>
        /// Removes an IEntity type from the quadtree and from the entity list
        /// </summary>
        /// <param name="ent"></param>
        public virtual void RemoveAnObject(IEntity ent)
        {

        }

        public virtual void UpdateTextures(IList<Point> list)
        {

        }

        public virtual void UpdateTextures(List<IPathNode> list)
        {

        }

        public virtual void UpdateTextures(IList<Node> list)
        {

        }

        public virtual void UpdateEntranceTextures(IDictionary<Node, string> nodelist)
        {

        }

        public virtual void ChangeFloor()
        {

        }

        public virtual void ResetTextures()
        {

        }
    }
}
