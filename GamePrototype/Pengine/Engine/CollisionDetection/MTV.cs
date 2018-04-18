using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Engine.CollisionDetection
{
    public class MTV
    {
        private static MTV instance = null;

        private MTV()
        {

        }

        public static MTV getInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MTV();
                }
                return instance;
            }
        }

        public Vector2 MTVCalc(Vector2 axis, float overlap)
        {
            Vector2 movement = axis;

            movement = new Vector2(movement.X * overlap, movement.Y * overlap);

            movement.Normalize();

            return movement;
        }
    }
}
