using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.AI.Pathfinding;
using Microsoft.Xna.Framework;

namespace Engine.AI.Managers.Interfaces
{
    public interface INodeManager
    {
        void CreateNode();

        Node GetNode(int x, int y);

        Node GetNode(Point point);

        /// <summary>
        /// Creates nodes at each point in the map, only needs to be called once per map
        /// </summary>
        /// <param name="map">bool[,] holds values which will be set to each node's walkable variable</param>
        void InitialiseNodes(bool[,] map);

        void SetTransitionNode(Node n);

        List<Node> GetAdjacentNodes(Node fromNode);

        int getWidth { get; }

        int getHeight { get; }

        Node[,] getNodes { get; }
    }
}
