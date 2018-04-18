﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.AI.Pathfinding.HPA.Infrastructure
{
    /** Interface to search environment. */
    public interface IMap<TNode>
    {
        int NrNodes { get; }

        /** Generate successor nodes for the search.
            @param lastNodeId
            Can be used to prune nodes,
            (is set to NO_NODE in Search::checkPathExists).
        */
        IEnumerable<Connection<TNode>> GetConnections(Id<TNode> nodeId);

        int GetHeuristic(Id<TNode> startNodeId, Id<TNode> targetNodeId);
    }
}
