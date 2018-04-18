using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Objects.Entities.Interfaces;
using Microsoft.Xna.Framework;

namespace Engine.CollisionDetection
{
    public class Projection
    {
        private Vector2 C;

        private double overlap;

        private Vector2 axis;

        private ICollidable collider;

        public Projection(Vector2 a, ICollidable c)
        {
            axis = a;
            collider = c;
        }

        public bool Overlapping(Projection test)
        {
            double gap = 0;

            C = (test.collider.getSetCenter - collider.getSetCenter);

            double projC = C.Length();
            //double projA = collider.getSetTexture.Width / 2;
            //double projB = test.collider.getSetTexture.Width / 2;

            if (axis.X > axis.Y)
            {
                double projA = collider.getSetTexture.Width / 2;
                double projB = test.collider.getSetTexture.Width / 2;

                gap = Math.Abs(C.X) - projA - projB;
            }
            else
            {
                double projA = collider.getSetTexture.Height / 2;
                double projB = test.collider.getSetTexture.Height / 2;

                gap = Math.Abs(C.Y) - projA - projB;
            }

            if (gap > 0)
            {
                //Console.WriteLine("Not colliding");
                return false;
            }
            else
            {
                //Console.WriteLine("Touching");
                overlap = Math.Abs(gap);
                //overlap = gap;

                return true;
            }
        }

        public double getOverlap
        {
            get { return overlap; }
        }

        public double GetOverlap(Projection p2)
        {
            return getOverlap;
        }

        public Vector2 getAxis
        {
            get { return axis; }
        }

        public ICollidable getCollider
        {
            get { return collider; }
        }

        //public Vector2 CalculateMTV()
        //{ 
        //      shape1.getAxes();
        //      shape1.MTV = someweirdcalculation
        //}

        //public bool PerformTests()
        //{
        //    if (PerformXTest() && PerformYTest())
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //public bool PerformXTest()
        //{
        //    if (shape1.getCenter.X >= shape2.getCenter.X)
        //    {
        //        distance = shape1.getCenter.X - shape2.getCenter.X;
        //    }
        //    else
        //    {
        //        distance = shape2.getCenter.X - shape1.getCenter.X;
        //    }

        //    if(distance - (shape1.getSetTexture.Width / 2) - (shape2.getSetTexture.Width / 2) > 0)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        overlapFloat = distance;
        //        return true;
        //    }
        //}

        //public bool PerformYTest()
        //{
        //    if (shape1.getCenter.Y >= shape2.getCenter.Y)
        //    {
        //        distance = shape1.getCenter.Y - shape2.getCenter.Y;
        //    }
        //    else
        //    {
        //        distance = shape2.getCenter.Y - shape1.getCenter.Y;
        //    }

        //    if (distance - (shape1.getSetTexture.Height / 2) - (shape2.getSetTexture.Height / 2) > 0)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        if (distance < overlapFloat)
        //        {
        //            overlapFloat = distance;
        //        }
        //        return true;
        //    }
        //}
    }
}
