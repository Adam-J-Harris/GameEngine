using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.AI.Pathfinding.HPA.Graph;
using Engine.AI.Pathfinding.HPA.Infrastructure;

namespace Engine.AI.Pathfinding.HPA
{
    public interface IPathNode
    {
        int IdValue { get; }

    }

    public struct AbstractPathNode : IPathNode
    {
        public Id<AbstractNode> Id;
        public int Level;

        public AbstractPathNode(Id<AbstractNode> id, int lvl)
        {
            Id = id;
            Level = lvl;
        }

        public int IdValue
        {
            get { return Id.IdValue; }
        }
    }

    public struct ConcretePathNode : IPathNode
    {
        public Id<ConcreteNode> Id;

        public ConcretePathNode(Id<ConcreteNode> id)
        {
            Id = id;
        }

        public int IdValue
        {
            get { return Id.IdValue; }
        }
    }
}
