using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.AI.Managers.Interfaces;
using Engine.AI.Pathfinding;
using Engine.Maps;
using Microsoft.Xna.Framework;

namespace Engine.AI.Managers
{
    public class NodeM : INodeManager
    {
        private static INodeManager instance = null;

        private Node node;
        private Node[,] nodes;
        private bool[,] walkMap;
        private int width;
        private int height;

        private SearchParameters sp;

        private int nodeNumber = 0;

        private NodeM()
        {

        }

        public static INodeManager getInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NodeM();
                }
                return instance;
            }
        }

        public void CreateNode()
        {
            
        }

        public Node GetNode(int x, int y)
        {
            return nodes[x, y];
        }

        public Node GetNode(Point point)
        {
            return nodes[point.X, point.Y];
        }

        /// <summary>
        /// Creates nodes at each point in the map, only needs to be called once per map
        /// </summary>
        /// <param name="pMap">bool[,] holds values which will be set to each node's walkable variable</param>
        public void InitialiseNodes(bool[,] pMap)
        {
            /*
             * Set the width and height of the future Node[,] array using map.GetLength(x) : 0 = width, 1 = height
             * */
            height = pMap.GetLength(1);
            width = pMap.GetLength(0);

            walkMap = pMap;

            /*
             * Initialise the node multi-dimensional array
             * */
            nodes = new Node[width, height];

            /*
             * At each point in the array create a node
             * */
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    nodes[x, y] = new Node(x, y, pMap[x, y]);

                    nodeNumber++;
                    nodes[x, y].getSetID = "Node" + nodeNumber;
                    nodes[x, y].getSetIsTransitional = false;
                }
            }
        }

        public void SetSearchParameters(Node start, Node end)
        {
            sp.getSetStart = start.getSetPointLocation;
            sp.getSetEnd = end.getSetPointLocation;
        }

        public void SetTransitionNode(Node n)
        {
            GetNode(n.getSetPointLocation).getSetIsTransitional = true;
            GetNode(n.getSetPointLocation).getSetState = NodeState.untested;
            GetNode(n.getSetPointLocation).getSetWalkable = true;
        }

        public List<Node> GetAdjacentNodes(Node fromNode)
        {
            List<Node> temp = new List<Node>();

            if (fromNode.getSetPointLocation.X != 0)
            {
                temp.Add(nodes[fromNode.getSetPointLocation.X - 1, fromNode.getSetPointLocation.Y]); // MIDDLE LEFT
            }

            if (fromNode.getSetPointLocation.Y != 0)
            {
                temp.Add(nodes[fromNode.getSetPointLocation.X, fromNode.getSetPointLocation.Y - 1]); // TOP CENTRE
            }

            if (fromNode.getSetPointLocation.Y != 63)
            {
                temp.Add(nodes[fromNode.getSetPointLocation.X, fromNode.getSetPointLocation.Y + 1]); // BOTTOM CENTRE
            }

            if (fromNode.getSetPointLocation.X != 63)
            {
                temp.Add(nodes[fromNode.getSetPointLocation.X + 1, fromNode.getSetPointLocation.Y]); // MIDDLE RIGHT
            }
            
            return temp;
        }

        public int getWidth
        {
            get { return width; }
        }

        public int getHeight
        {
            get { return height; }
        }

        public Node[,] getNodes
        {
            get { return nodes; }
        }
    }
}
