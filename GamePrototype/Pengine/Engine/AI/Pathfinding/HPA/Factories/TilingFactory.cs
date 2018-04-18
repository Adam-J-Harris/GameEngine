﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.AI.Pathfinding.HPA.Factories
{
    /// <summary>
    /// Constructs ConcreteMap objects
    /// </summary>
    public static class TilingFactory
    {
        public static ConcreteMap CreateTiling(int width, int height, IPassability passability, TileType tilingType = TileType.Octile)
        {
            var tiling = new ConcreteMap(tilingType, width, height, passability);
            return tiling;
        }
    }
}
