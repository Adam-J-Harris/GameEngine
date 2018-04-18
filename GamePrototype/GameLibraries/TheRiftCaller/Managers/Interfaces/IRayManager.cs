using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRiftCaller.Managers.Interfaces
{
    public  interface IRayManager
    {
        bool CastRay(Vector2 origin, Vector2 target);
        bool CastRayToPlayer(Vector2 origin);
    }
}
