using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.AI.Pathfinding.HPA.Infrastructure;

namespace Engine.AI.Pathfinding.HPA
{
    public interface IPassability
    {
        /// <summary>
        /// Tells whether for a given position this passability class can enter or not.
        /// </summary>
        bool CanEnter(Position pos, out int movementCost);
    }
}
