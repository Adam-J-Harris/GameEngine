using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Objects.Entities.Interfaces;
using Microsoft.Xna.Framework;

namespace Engine.CollisionDetection
{
    public class SAT
    {
        private Projection p;
        private bool pass = false;

        private Vector2 smallest;

        private double overlap;

        public SAT()
        {

        }

        /// <summary>
        /// Finds the overlap of two ICollidable objects and sets the colliding axis according to the lowest overlap
        /// </summary>
        /// <param name="a">ICollidable object 1</param>
        /// <param name="b">ICollidable object 2</param>
        public void SATTest(ICollidable a, ICollidable b)
        {
            pass = false;
            // Set overlap to a large number so it will definitely be overwritten
            overlap = 500;
            // Create a temporary axis which will become the axis to move the objects away from
            Vector2 tempAxis = new Vector2(0, 0);            

            bool set1 = false;
            bool set2 = false;

            // Loop through all of the axes which 'a' has
            for (int i = 0; i < a.getAxes.Length; i++)
            {
                Vector2 axis = a.getAxes[i];

                // Project a and b onto the same axis
                Projection p1 = a.Project(axis);
                Projection p2 = b.Project(axis);

                // If p1 and p2 do not overlap
                if (!p1.Overlapping(p2))
                {
                    // set1 equals false
                    set1 = false;
                    break;
                }
                else
                {
                    // set1 equal true
                    set1 = true;

                    // set a double type to the value of the overlap
                    double o = p1.GetOverlap(p2);

                    // if the o value is less than the current overlap value
                    if (o < overlap)
                    {
                        // Set overlap to o
                        overlap = o;
                        // Set the new axis
                        tempAxis = axis;
                    }
                }
            }

            // Loop through all of the axes which 'b' has
            for (int i = 0; i < b.getAxes.Length; i++)
            {
                Vector2 axis = b.getAxes[i];

                // Project a and b onto the same axis
                Projection p1 = a.Project(axis);
                Projection p2 = b.Project(axis);

                // If they are not overlapping
                if (!p2.Overlapping(p1))
                {
                    // set2 equals false
                    set2 = false;
                    break;
                }
                else
                {
                    // set2 equals true
                    set2 = true;

                    // set a double type to the value of the overlap
                    double o = p2.GetOverlap(p1);

                    // if the o value is less than the current overlap value
                    if (o < overlap)
                    {
                        // set overlap to o
                        overlap = o;
                        // set the new tempaxis
                        tempAxis = axis;
                    }
                }
            }

            // set smallest (type Vector2) to the tempAxis which holds the smallest calculated axis
            smallest = tempAxis;

            // if set1 and set2 are true
            if (set1 && set2)
            {
                // ICollidables are colliding, pass equals true
                pass = true;
            }
            else
            {
                // ICollidables aren't colliding, pass equals false
                pass = false;
            }


        }

        public Vector2 CalculateMTV()
        {
            return MTV.getInstance.MTVCalc(smallest, (float)overlap);
        }

        public bool getPass
        {
            get { return pass; }
        }

        public double getOverlap
        {
            get { return overlap; }
        }

        public Vector2 getSmallest
        {
            get { return smallest; }
        }

    }
}
