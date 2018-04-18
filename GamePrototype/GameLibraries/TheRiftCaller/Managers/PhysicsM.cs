
using Engine.Managers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRiftCaller.Physics
{
    public class PhysicsM : IPhysicsManager
    {
        private static IPhysicsManager instance;
        public static float gravity = 9.81f;
        public static float damping = 0.9f;
        private float inputForce = 0;

        private PhysicsM()
        {

        }

        public static IPhysicsManager getInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PhysicsM();
                }
                return instance;
            }
        }

        public float getSetInputForce
        {
            get { return inputForce; }
            set { inputForce = value; }
        }

        public void ApplyPhysics()
        {
            
        }

        public void Update()
        {

        }
    }
}
