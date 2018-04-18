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
using Engine.Maps.Interfaces;
using Engine.Maps.QuadTree.Interfaces;
//using System.Drawing;
using Microsoft.Xna.Framework;

namespace Engine.AI.Pathfinding
{
    public class Node
    {
        private Node parentNode;
        private Node grandParentNode;

        private string id;

        private Point location;
        private NodeState state;

        private float g;
        private float h;
        private float f;

        private bool walkable;

        private bool transitionalNode = false;
        private Node transitionalToQuadNode;
        private IQuadNode quadnode;

        /// <summary>
        /// Constructor initialises all fields to standard values
        /// </summary>
        /// <param name="x">X-value of the node's location</param>
        /// <param name="y">Y-value of the node's location</param>
        /// <param name="isWalkable">If the node can be walked on or not</param>
        public Node(int x, int y, bool isWalkable)
        {
            this.location = new Point(x, y);
            this.state = NodeState.untested;
            this.walkable = isWalkable;
            this.h = 0;
            this.g = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}, {1}: {2}", this.location.X, this.location.Y, this.state);
        }

        /// <summary>
        /// Calculates the value of the float h : heuristic
        /// </summary>
        /// <param name="location"></param>
        /// <param name="otherLocation"></param>
        /// <returns></returns>
        public float GetTraversalCost(Point location, Point otherLocation)
        {
            if (otherLocation == null)
            {
                otherLocation = new Point(0, 0);
            }
            float deltaX = otherLocation.X - location.X;
            float deltaY = otherLocation.Y - location.Y;
            return (float)Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
        }

        /// <summary>
        /// Set or Return the value of Node parentNode; Set the value of float g
        /// </summary>
        public Node getSetParentNode
        {
            get { return parentNode; }
            set
            {
                parentNode = value;

                if (parentNode != null)
                {
                    g = parentNode.getSetG + GetTraversalCost(location, parentNode.getSetPointLocation);
                }

            }
        }

        public Node getSetGrandParentNode
        {
            get { return grandParentNode; }
            set
            {
                grandParentNode = value;

                if (grandParentNode != null)
                {
                    g = grandParentNode.getSetG + GetTraversalCost(location, grandParentNode.getSetPointLocation);
                }

            }
        }

        public string getSetID
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Set or Return the value of Point location
        /// </summary>
        public Point getSetPointLocation
        {
            get { return location; }
            set { location = value; }
        }

        /// <summary>
        /// Set or Return the value of the NodeState state
        /// </summary>
        public NodeState getSetState
        {
            get { return state; }
            set { state = value; }
        }

        /// <summary>
        /// Set or Return the value of the float g
        /// </summary>
        public float getSetG
        {
            get { return g; }
            set { g = value; }
        }

        /// <summary>
        /// Set or Return the value of the float h
        /// </summary>
        public float getSetH
        {
            get { return h; }
            set { h = value; }
        }

        /// <summary>
        /// Set or Return the value of the float f
        /// </summary>
        public float getSetF
        {
            get { return g + h; }
            set { f = value; }
        }

        /// <summary>
        /// Set or Return the value of the bool walkable
        /// </summary>
        public bool getSetWalkable
        {
            get { return walkable; }
            set { walkable = value; }
        }

        public bool getSetIsTransitional
        {
            get { return transitionalNode; }
            set { transitionalNode = value; }
        }

        public Node getSetTransitionalToNode
        {
            get { return transitionalToQuadNode; }
            set { transitionalToQuadNode = value; }
        }

        public IQuadNode getSetQuadNodeLocation
        {
            get { return quadnode; }
            set { quadnode = value; }
        }

    }

}
