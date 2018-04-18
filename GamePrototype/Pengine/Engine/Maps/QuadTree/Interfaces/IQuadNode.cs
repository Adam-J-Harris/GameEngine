/*
MIT License

Copyright (c) 2016 Duncan Baldwin

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
using Engine.Objects.Entities.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.Maps.QuadTree.Interfaces
{
    public interface IQuadNode
    {
        /// <summary>
        /// Splits the current node into 4 child nodes
        /// </summary>
        void Split();

        /// <summary>
        /// Checks if movable objects are still in the QuadNode
        /// </summary>
        /// <returns>true for yes, false for no</returns>
        bool CheckContentsBounds();

        /// <summary>
        /// Get the IDictionary containing all of the contents
        /// </summary>
        IDictionary<string, IEntity> getContents { get; }

        /// <summary>
        /// Get the value of the int mContents.Count
        /// </summary>
        int getContentsCount { get; }

        /// <summary>
        /// Get the IDictionary<int, IQuadNode> mChildNodes
        /// </summary>
        IDictionary<int, IQuadNode> getChildNodes { get; }

        /// <summary>
        /// Get or Set the Rectangle mBounds
        /// </summary>
        Rectangle getBounds { get; }

        /// <summary>
        /// Get or Set the value of the IQuadNode mParentQuadNode
        /// </summary>
        IQuadNode getSetParent { get; set; }

        /// <summary>
        /// Get or Set the value of the int nodeID
        /// </summary>
        int getSetID { get; set; }

        void Draw(SpriteBatch sb, Rectangle rectangleToDraw, int thicknessOfBorder, Color borderColor);

        void AddToNodePopulation(Node n);
        void SortBorderNodes();

        IDictionary<Node, string> getTransitionNodes { get; }
        IDictionary<Point, Node> getNodePopulation { get; }
    }
}
