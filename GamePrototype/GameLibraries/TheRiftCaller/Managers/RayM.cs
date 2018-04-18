using Engine.Managers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRiftCaller.Managers.Interfaces;

namespace TheRiftCaller.Managers
{
    public class RayM : IRayManager
    {
        private static IRayManager instance;

        private Vector2 temp;

        private RayM()
        {

        }

        public static IRayManager getInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RayM();
                }
                return instance;
            }
        }

        public bool CastRay(Vector2 origin, Vector2 target)
        {
            bool returnMe = false;

            origin.X += 20;
            origin.Y += 20;

            target.X += 20;
            target.Y += 20;

            temp = origin;



            while (returnMe == false)
            {
                // May be jumping over the walls, try incrementing in smaller values
                temp += (target - origin) / 10;

                Rectangle tempR = new Rectangle((int)temp.X, (int)temp.Y, 1, 1);

                if (CollisionM.getInstance.CheckRayCollision(tempR))
                {
                    returnMe = true;

                    // Draw line
                    //DrawLine(sb, origin, temp);
                }
            }

            //CollisionM.getInstance.CheckRayCollision(tempR);

            // Draw line
            //DrawLine(sb, origin, temp);


            return returnMe;
        }

        public bool CastRayToPlayer(Vector2 origin)
        {
            bool returnMe = false;

            origin.X += 20;
            origin.Y += 20;

            Vector2 target = EntityM.getInstance.getEntity("Player1").getSetLocation;
            target.X += 20;
            target.Y += 20;

            temp = origin;


            while (returnMe == false)
            {
                // May be jumping over the walls, try incrementing in smaller values
                temp += (target - origin) / 100;

                Rectangle tempR = new Rectangle((int)temp.X, (int)temp.Y, 1, 1);

                if (CollisionM.getInstance.CheckRayCollision(tempR))
                {
                    returnMe = true;

                    // Draw line
                    //DrawLine(sb, origin, temp);
                }

            }

            //CollisionM.getInstance.CheckRayCollision(tempR);

            // Draw line
            //DrawLine(sb, origin, temp);

            return returnMe;


        }

        //public void DrawLine(SpriteBatch sb, Vector2 origin, Vector2 target)
        //{
        //    float angle = (float)Math.Atan2(origin.Y - target.Y, origin.X - target.X);
        //    float distance = Vector2.Distance(origin, target);

        //    sb.Draw(pixel, new Rectangle((int)target.X, (int)target.Y, (int)distance, 1), null, Color.Blue, angle, Vector2.Zero, SpriteEffects.None, 0);


        //}
    }
}
