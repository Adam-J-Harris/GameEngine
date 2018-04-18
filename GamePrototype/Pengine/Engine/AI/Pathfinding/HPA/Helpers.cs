using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.AI.Pathfinding.HPA.Infrastructure;

namespace Engine.AI.Pathfinding.HPA
{
    public static class Helpers
    {
        public static int GetMaxEdges(TileType tileType)
        {
            switch (tileType)
            {
                case TileType.Hex:
                    return 6;
                case TileType.Octile:
                case TileType.OctileUnicost:
                    return 8;
                case TileType.Tile:
                    return 4;
            }

            return 0;
        }

        public static bool AreAligned(Position p1, Position p2)
        {
            return p1.X == p2.X || p1.Y == p2.Y;
        }
    }
}
