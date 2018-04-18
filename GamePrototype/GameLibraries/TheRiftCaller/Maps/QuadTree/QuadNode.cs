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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.AI.Managers;
using Engine.AI.Pathfinding;
using Engine.Maps.QuadTree;
using Engine.Maps.QuadTree.Interfaces;
using Engine.Objects.Entities.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheRiftCaller.Maps.Quadtrees
{
    public class QuadNode : IQuadNode
    {
        private Rectangle mBounds;
        private IDictionary<string, IEntity> mContents;
        private IDictionary<int, IQuadNode> mChildNodes;

        private IDictionary<Point, Node> nodePopulation = new Dictionary<Point, Node>();
        private IDictionary<Node, string> borderNodes = new Dictionary<Node, string>();
        private IDictionary<Node, string> transitionNodes = new Dictionary<Node, string>();

        private IQuadNode mParentQuadNode;

        private Texture2D pixel;
        private bool run = true;

        private int nodeID;

        public QuadNode()
        {

        }

        public QuadNode(Rectangle bounds)
        {
            mContents = new Dictionary<string, IEntity>();
            mChildNodes = new Dictionary<int, IQuadNode>(4);

            mBounds = bounds;
        }

        /// <summary>
        /// Splits the current node into 4 child nodes
        /// </summary>
        public void Split()
        {
            int idEnd = 0;

            for (int y = 0; y < 2; y++)
            {
                for (int x = 0; x < 2; x++)
                {
                    Rectangle newBounds = new Rectangle(mBounds.Left + (x * (mBounds.Width / 2)), mBounds.Top + (y * (mBounds.Height / 2)), mBounds.Width / 2, mBounds.Height / 2);
                    IQuadNode newNode = new QuadNode(newBounds);

                    idEnd++;
                    newNode.getSetID = Convert.ToInt32(nodeID + "" + idEnd);

                    mChildNodes.Add(newNode.getSetID, newNode);
                }
            }
        }

        /// <summary>
        /// Checks if movable objects are still in the QuadNode
        /// </summary>
        /// <returns>true for yes, false for no</returns>
        public bool CheckContentsBounds()
        {
            bool returnMe = true;

            foreach (IEntity e in mContents.Values)
            {
                if (e is ICollidable)
                {
                    ICollidable c = (ICollidable)e;

                    if (!mBounds.Contains(c.getSetHitbox))
                    {
                        returnMe = false;
                    }
                }
            }

            return returnMe;
        }

        /// <summary>
        /// Get the IDictionary containing all of the contents
        /// </summary>
        public IDictionary<string, IEntity> getContents
        {
            get { return mContents; }
        }

        /// <summary>
        /// Get the value of the int mContents.Count
        /// </summary>
        public int getContentsCount
        {
            get { return mContents.Count; }
        }

        /// <summary>
        /// Get the IDictionary<int, IQuadNode> mChildNodes
        /// </summary>
        public IDictionary<int, IQuadNode> getChildNodes
        {
            get { return mChildNodes; }
        }

        /// <summary>
        /// Get or Set the Rectangle mBounds
        /// </summary>
        public Rectangle getBounds
        {
            get { return mBounds; }
        }

        /// <summary>
        /// Get or Set the value of the IQuadNode mParentQuadNode
        /// </summary>
        public IQuadNode getSetParent
        {
            get { return mParentQuadNode; }
            set { mParentQuadNode = value; }
        }

        /// <summary>
        /// Get or Set the value of the int nodeID
        /// </summary>
        public int getSetID
        {
            get { return nodeID; }
            set { nodeID = value; }
        }

        public void Draw(SpriteBatch sb, Rectangle rectangleToDraw, int thicknessOfBorder, Color borderColor)
        {
            if (run)
            {
                pixel = new Texture2D(sb.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
                pixel.SetData(new[] { Color.White });
                run = false;
            }


            // Draw top line
            sb.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, rectangleToDraw.Width, thicknessOfBorder), borderColor);

            // Draw left line
            sb.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, thicknessOfBorder, rectangleToDraw.Height), borderColor);

            // Draw right line
            sb.Draw(pixel, new Rectangle((rectangleToDraw.X + rectangleToDraw.Width - thicknessOfBorder),
                                            rectangleToDraw.Y,
                                            thicknessOfBorder,
                                            rectangleToDraw.Height), borderColor);
            // Draw bottom line
            sb.Draw(pixel, new Rectangle(rectangleToDraw.X,
                                            rectangleToDraw.Y + rectangleToDraw.Height - thicknessOfBorder,
                                            rectangleToDraw.Width,
                                            thicknessOfBorder), borderColor);
        }

        public void AddToNodePopulation(Node n)
        {
            nodePopulation.Add(n.getSetPointLocation, n);
        }

        public void SortBorderNodes()
        {
            Point lowest = new Point((mBounds.X + 32) / 64, (mBounds.Y + 32) / 64);
            Point highest = new Point(((mBounds.X + 32) / 64) + (mBounds.Width / 64), ((mBounds.Y + 32) / 64) + (mBounds.Height / 64));

            foreach (Node n in nodePopulation.Values)
            {
                if (n.getSetPointLocation.X == lowest.X || n.getSetPointLocation.X == lowest.X - 1)
                {
                    borderNodes.Add(n, "Left");
                }
                else if (n.getSetPointLocation.X == highest.X || n.getSetPointLocation.X == highest.X - 1)
                {
                    borderNodes.Add(n, "Right");
                }

                else if (n.getSetPointLocation.Y == lowest.Y || n.getSetPointLocation.Y == lowest.Y - 1)
                {
                    borderNodes.Add(n, "Top");

                }
                else if (n.getSetPointLocation.Y == highest.Y || n.getSetPointLocation.Y == highest.Y - 1)
                {
                    borderNodes.Add(n, "Bottom");
                }

                //if (n.getSetPointLocation.X == 0 || n.getSetPointLocation.Y == 0 || n.getSetPointLocation.X == 63 || n.getSetPointLocation.Y == 63)
                //{
                //    borderNodes.Remove(n);
                //}
            }

            SortTransitionNodes(lowest, highest);
        }

        private void SortTransitionNodes(Point low, Point high)
        {
            IList<Node> potentialTransitionNodes = new List<Node>();

            foreach (Node n in borderNodes.Keys)
            {
                foreach (Node m in NodeM.getInstance.GetAdjacentNodes(n))
                {
                    if (!nodePopulation.ContainsKey(m.getSetPointLocation) && n.getSetWalkable && m.getSetWalkable)
                    {
                        potentialTransitionNodes.Add(n);
                        n.getSetIsTransitional = true;
                        n.getSetTransitionalToNode = m;
                        m.getSetIsTransitional = true;
                        n.getSetQuadNodeLocation = this;
                        break;
                    }
                }
            }

            int count = 0;
            IList<Node> nodesToRemove = new List<Node>();
            IList<Node> sequence = new List<Node>();

            for (int i = 0; i < potentialTransitionNodes.Count; i++)
            {
                Node n = potentialTransitionNodes[i];

                sequence.Add(n);
                if (sequence.Count > 5)
                {
                    /*
                     * If-elses which remove the potentialTransitionNodes located in the exact corner of each QuadNode
                     */
                    // TopLeft
                    if ((n.getSetPointLocation.X == low.X || n.getSetPointLocation.X == low.X - 1) && (n.getSetPointLocation.Y == low.Y || n.getSetPointLocation.Y == low.Y - 1))
                    {
                        n.getSetIsTransitional = false;
                        nodesToRemove.Add(n);
                    }
                    // TopRight
                    else if ((n.getSetPointLocation.X == high.X || n.getSetPointLocation.X == high.X - 1) && (n.getSetPointLocation.Y == low.Y || n.getSetPointLocation.Y == low.Y - 1))
                    {
                        n.getSetIsTransitional = false;
                        nodesToRemove.Add(n);
                    }
                    // BottomLeft
                    else if ((n.getSetPointLocation.X == low.X || n.getSetPointLocation.X == low.X - 1) && (n.getSetPointLocation.Y == high.Y || n.getSetPointLocation.Y == high.Y - 1))
                    {
                        n.getSetIsTransitional = false;
                        nodesToRemove.Add(n);
                    }
                    // BottomRight
                    else if ((n.getSetPointLocation.X == high.X || n.getSetPointLocation.X == high.X - 1) && (n.getSetPointLocation.Y == high.Y || n.getSetPointLocation.Y == high.Y - 1))
                    {
                        n.getSetIsTransitional = false;
                        nodesToRemove.Add(n);
                    }
                }
            }

            foreach (Node n in nodesToRemove)
            {
                potentialTransitionNodes.Remove(n);
                n.getSetIsTransitional = false;
                n.getSetTransitionalToNode = null;
            }

            foreach (Node n in potentialTransitionNodes)
            {
                transitionNodes.Add(n, n.getSetID);
            }
        }

        private void SetTransitionNode(Node n)
        {
            transitionNodes.Add(n, borderNodes[n]);
        }

        public IDictionary<Node, string> getTransitionNodes
        {
            get { return transitionNodes; }
        }

        public IDictionary<Point, Node> getNodePopulation
        {
            get { return nodePopulation; }
        }
    }
}
