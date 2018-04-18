using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.AI
{
    public interface IPersonality
    {
        Personality getType { get; }

        string getSetName { get; set; }

        void ApplyStates();
    }
}
