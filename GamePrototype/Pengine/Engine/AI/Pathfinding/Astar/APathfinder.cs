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
using Engine.AI.Managers;
using Engine.Managers;
using Engine.Screens;
//using System.Drawing;
using Microsoft.Xna.Framework;

namespace Engine.AI.Pathfinding.Astar
{
    public class APathfinder
    {
        private IList<Node> usedNodes;
        private Node startNode;
        private Node endNode;

        public APathfinder()
        {

        }

        public APathfinder(SearchParameters searchParameters)
        {
            usedNodes = new List<Node>();

            //startNode = this.nodes[(int)searchParameters.getSetStart.Y, (int)searchParameters.getSetStart.X];
            startNode = NodeM.getInstance.GetNode((int)searchParameters.getSetStart.X, (int)searchParameters.getSetStart.Y);
            startNode.getSetState = NodeState.open;
            endNode = NodeM.getInstance.GetNode((int)searchParameters.getSetEnd.X, (int)searchParameters.getSetEnd.Y);
        }

        /// <summary>
        /// H is the estimated distance between n and the endNode.
        /// G is the distance already travelled to get to n.
        /// F is G and H added together.
        /// </summary>
        /// <param name="n">Node type to set G, H and F values</param>
        private void SetNodesGHF(Node n)
        {
            n.getSetH = n.GetTraversalCost(n.getSetPointLocation, endNode.getSetPointLocation);
            
            if (n.getSetParentNode != null)
            {
                n.getSetG = n.getSetParentNode.getSetG + n.GetTraversalCost(n.getSetPointLocation, n.getSetParentNode.getSetPointLocation);
            }
            else
            {
                n.getSetG = 0;
            }
        }

        /// <summary>
        /// Changes all node states back to original
        /// </summary>
        public void ResetNodes()
        {
            foreach (Node n in usedNodes)
            {
                n.getSetState = NodeState.untested;
                n.getSetParentNode = null;
            }

            usedNodes.Clear();
        }

        /// <summary>
        /// Adds nodes, which have a parent, to List of Points
        /// </summary>
        /// <returns>Point List which contains a path to the end point from the start point</returns>
        public List<Point> FindPath()
        {
            List<Point> path = new List<Point>();

            bool success = Search(startNode);

            if (success)
            {
                Node node = endNode;

                while (node.getSetParentNode != null)
                {
                    path.Add(node.getSetPointLocation);
                    node = node.getSetParentNode;
                }

                path.Reverse();
            }

            return path;
        }

        /// <summary>
        /// Using a node value, finds adjacent nodes and keeps searching in a loop until the end node and the searched node are the same.
        /// </summary>
        /// <param name="currentNode">Node type to search through</param>
        /// <returns>bool type which states whether or not the pathfinder has finished</returns>
        private bool Search(Node currentNode)
        {
            bool finished = false;

            //currentNode.getSetState = NodeState.closed;
            usedNodes.Add(currentNode);

            
            //Close the currentNode being searched so the algorithm doesn't look at it again and get confused
            currentNode.getSetState = NodeState.closed;

            // Find the next adjacent nodes to search through
            List<Node> nextNodes = GetAdjacentWalkableNodes(currentNode);
            // Sort the nodes in ascending order by their getSetF values
            nextNodes.Sort((node1, node2) => node1.getSetF.CompareTo(node2.getSetF));

            // Loop through each Node in nextNodes list
            foreach (Node n in nextNodes)
            {
                // If n's location is the same as the end location, pathfinder has finished
                if (n.getSetPointLocation == endNode.getSetPointLocation)
                {
                    finished = true;
                    // Don't need to look at any other nodes
                    break;
                }
                else
                {
                    // Search using n
                    if (Search(n))
                    {
                        finished = true;
                        break;
                    }
                }
            }

            return finished;
        }

        /// <summary>
        /// Searches through the parameters adjacent point values to find and assign parent nodes using G
        /// </summary>
        /// <param name="currentNode">Node type which adjacent nodes will be found</param>
        /// <returns>List of Node values which will be used to determine the node to be searched next</returns>
        private List<Node> GetAdjacentWalkableNodes(Node currentNode)
        {
            // Set up a list which will be returned
            List<Node> walkableNodes = new List<Node>();

            // Find the adjacent points of currentNode
            IEnumerable<Point> nextLocations = GetAdjacentPoints(currentNode.getSetPointLocation);

            // Loop through each point
            foreach (Point location in nextLocations)
            {
                float x = location.X;
                float y = location.Y;

                // If the x or y point is outside of the grid/screen
                if (x < 0 || x >= NodeM.getInstance.getWidth || y < 0 || y >= NodeM.getInstance.getHeight)
                {
                    //Move onto the next adjacent location
                    continue;
                }

                // Set a local node value now that x and y are definitely on screen/grid
                Node node = NodeM.getInstance.GetNode((int)x, (int)y);

                // If the node is a wall OR the node has already been searched
                if (!node.getSetWalkable || node.getSetState == NodeState.closed)
                {
                    // Ensures that all non-walkable nodes have a closed state
                    node.getSetState = NodeState.closed;
                    continue;
                }

                // Set the values of G, H and F of the node being searched
                SetNodesGHF(node);

                // If nodes state is labelled open
                if (node.getSetState == NodeState.open)
                {
                    // Finds the distance between a node and its parent, should always be 1 (1.41 if using 8-directional travel on diagonals)
                    float traversalCost = node.GetTraversalCost(node.getSetPointLocation, node.getSetParentNode.getSetPointLocation);
                    // Essentially the node.getSetG value is being calculated here
                    float gTemp = currentNode.getSetG + traversalCost;

                    // Compare gTemp with nodes current G value
                    if (gTemp > node.getSetG)
                    {
                        // Set the parent of currentNode to node, allows a* algorithm to follow the path backwards
                        currentNode.getSetParentNode = node;
                        // Add node to returnable list
                        walkableNodes.Add(node);
                    }
                }
                else
                {
                    // If it's untested, set the parent and flag it as 'Open' for consideration
                    node.getSetParentNode = currentNode;
                    node.getSetState = NodeState.open;
                    walkableNodes.Add(node);
                    usedNodes.Add(node);
                }
            }

            return walkableNodes;
        }

        /// <summary>
        /// Returns 4 Point values from a Point location specified. IEnumerable ensures that data cannot be changed (read-only)
        /// </summary>
        /// <param name="fromLocation">Point type location</param>
        /// <returns>IEnumerable of Point which are adjacent</returns>
        private static IEnumerable<Point> GetAdjacentPoints(Point fromLocation)
        {
            return new Point[]
            {
                new Point(fromLocation.X - 1, fromLocation.Y), // MIDDLE LEFT
                new Point(fromLocation.X, fromLocation.Y - 1), // TOP CENTRE
                new Point(fromLocation.X, fromLocation.Y + 1), // BOTTOM CENTRE
                new Point(fromLocation.X + 1, fromLocation.Y), // MIDDLE RIGHT

                //new Point(fromLocation.X - 1, fromLocation.Y - 1), // TOP LEFT
                //new Point(fromLocation.X - 1, fromLocation.Y), // MIDDLE LEFT
                //new Point(fromLocation.X - 1, fromLocation.Y + 1), // BOTTOM LEFT
                //new Point(fromLocation.X, fromLocation.Y - 1), // TOP CENTRE
                //new Point(fromLocation.X, fromLocation.Y + 1), // BOTTOM CENTRE
                //new Point(fromLocation.X + 1, fromLocation.Y - 1), // TOP RIGHT
                //new Point(fromLocation.X + 1, fromLocation.Y), // MIDDLE RIGHT
                //new Point(fromLocation.X + 1, fromLocation.Y + 1) // BOTTOM RIGHT
            };
        }

        /// <summary>
        /// Returns all of the nodes used during the pathfinder algorithm
        /// </summary>
        public IList<Node> getUsedNodes
        {
            get { return usedNodes; }
        }

    }

}
