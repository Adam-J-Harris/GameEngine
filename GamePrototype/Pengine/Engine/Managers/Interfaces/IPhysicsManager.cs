using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Managers.Interfaces
{
    public interface IPhysicsManager
    {
        float getSetInputForce { get; set; }
        void ApplyPhysics();
        void Update();
    }
}
