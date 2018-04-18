/*
MIT License

Copyright (c) 2016 Duncan Baldwin & Adam Harris

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.CollisionDetection;
using Engine.Events;
using Engine.Managers.Interfaces;
using Engine.Maps;
using Engine.Objects.Entities.Interfaces;
using Microsoft.Xna.Framework;

namespace Engine.Managers
{
    public class CollisionM : ICollisionManager
    {
        private static ICollisionManager instance;

        public event EventHandler<CollisionEvent> newCollision;

        private IDictionary<string, ICollidable> smallDictionary = new Dictionary<string, ICollidable>();
        private IDictionary<string, ICollidable> largeDictionary = new Dictionary<string, ICollidable>();

        private IDictionary<string, ICollidable> movingEntities = new Dictionary<string, ICollidable>();

        private ICollidable playerRef;

        private SAT sat;

        public ICollidable BigRef;
        public ICollidable plaRef;

        private CollisionM()
        {
            //dictionary = new Dictionary<string, ICollidable>();
            sat = new SAT();
            SetUpMovingEntities();
        }

        /// <summary>
        /// Make this class a singleton
        /// </summary>
        public static ICollisionManager getInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CollisionM();
                }
                return instance;
            }
        }

        /// <summary>
        /// Calls all of the listeners collision event function
        /// </summary>
        /// <param name="collideRef">THe colliding object reference</param>
        public virtual void OnNewCollision(ICollidable collideRef, Vector2 mtv)
        {
            //Call subscriber and assign it to a var. 
            CollisionEvent eve = new CollisionEvent(collideRef, mtv);

            //Call all objects subscribed to the event.
            newCollision(this, eve);
        }

        /// <summary>
        /// Starts a class having an event function called when triggered
        /// </summary>
        /// <param name="handler">the collision event reference</param>
        public void AddListener(EventHandler<CollisionEvent> handler)
        {
            // Add event handler:
            newCollision += handler;
        }

        /// <summary>
        /// Stop a class from having the collision event function called
        /// </summary>
        /// <param name="handler">The collision event reference</param>
        public void RemoveListener(EventHandler<CollisionEvent> handler)
        {
            newCollision -= handler;
        }

        /// <summary>
        /// Set up all hitboxes of entities which are in the same map cluster as the player character
        /// </summary>
        /// <param name="d"></param>
        public void SetUpStaticHitboxes(IDictionary<string, ICollidable> d)
        {
            largeDictionary = d;

            foreach (KeyValuePair<string, ICollidable> c in largeDictionary)
            {
                c.Value.SetUpHitbox();
                c.Value.SetVertices();
                c.Value.SetAxes();
            }
        }

        /// <summary>
        /// Gets rid of static hitboxes which are no longer in the same cluster as the player character
        /// </summary>
        public void ResetStaticHitboxes()
        {
            foreach (ICollidable c in smallDictionary.Values)
            {
                c.SetUpHitbox();
                c.SetVertices();
                c.SetAxes();
            }
        }

        public IDictionary<string, ICollidable> getSetDictionary
        {
            get { return smallDictionary; }
            set { smallDictionary = value; }
        }

        public bool CastRay(Vector2 origin, Vector2 target)
        {
            bool returnMe = false;

            while (origin.Length() < target.Length())
            {
                origin += (target - origin) / 10;

                if (origin == target)
                {
                    returnMe = true;
                }
            }

            return returnMe;
        }

        public bool CheckRayCollision(Rectangle rayEnd)
        {
            bool returnMe = true;

            foreach (ICollidable c in largeDictionary.Values)
            {
                if (c.getSetHitbox.Intersects(rayEnd))
                {
                    returnMe = false;
                    //Console.WriteLine(c.getSetID + " : Collision with ray");
                    break;
                }
            }

            return returnMe;
        }

        public void DeleteCollidable(ICollidable c)
        {
            if (largeDictionary.ContainsKey(c.getSetID))
            {
                largeDictionary.Remove(c.getSetID);
            }
        }

        public void SetUpMovingEntities()
        {
            movingEntities.Clear();

            foreach (IEntity e in EntityM.getInstance.getDictionary.Values)
            {
                if (e.getSetName == "Player" || e.getSetName == "Patient" || e.getSetName == "Ghost")
                {
                    ICollidable c = (ICollidable)e;
                    movingEntities.Add(c.getSetID, c);

                    c.SetVertices();
                    c.SetAxes();
                }
            }

        }

        /// <summary>
        /// Update hitboxes of non-structure Entities, check for rectangle intersections
        /// </summary>
        public void Update()
        {
            if (plaRef == null)
            {
                //    playerRef.SetUpHitbox();
                //    playerRef.SetVertices();
                //}
                //else
                //{
                plaRef = (ICollidable)EntityM.getInstance.getEntity("Player1");

                //    playerRef.SetVertices();
                //    playerRef.SetUpHitbox();
                //    playerRef.SetAxes();
            }



            if (EntityM.getInstance.getEntity("BigNote1") != null)
            {
                plaRef = (ICollidable)EntityM.getInstance.getEntity("Player1");

                //plaRef.AddCollisionListener();

                //plaRef.SetUpHitbox();

                BigRef = (ICollidable)EntityM.getInstance.getEntity("BigNote1");

                BigRef.SetUpHitbox();

                if (plaRef.getSetHitbox.Intersects(BigRef.getSetHitbox))
                {
                     OnNewCollision(BigRef, new Vector2(0, 0));
                }

                //plaRef.RemoveCollisionListener();
            }

            //// Loop through each key value pair in the dictionary
            //foreach (KeyValuePair<string, ICollidable> c in smallDictionary)
            //{
            //    // Set the vertices of all objects
            //    // Specifically for moving objects
            //    c.Value.SetVertices();

            //    // Check if the objects intersect with the player reference
            //    if (playerRef.getSetHitbox.Intersects(c.Value.getSetHitbox))
            //    {
            //        // Perform sat test
            //        sat.SATTest(playerRef, c.Value);

            //        // If the sat test passes
            //        if (sat.getPass)
            //        {
            //            // Call collision event with kvp element as the data
            //            OnNewCollision(c.Value, sat.CalculateMTV());
            //        }
            //    }
            //}

            // Loop through each key value pair in the dictionary
            foreach (ICollidable c in movingEntities.Values)
            {
                //c.AddCollisionListener();
                c.SetUpHitbox();

                foreach (ICollidable a in smallDictionary.Values)
                { 

                    if (a.getSetName == "Key" || a.getSetName == "Note")
                    {
                        // Check if the objects intersect with the player reference
                        if (c.getSetHitbox.Intersects(a.getSetHitbox))
                        {
                            // Set the vertices of all objects
                            // Specifically for moving objects
                            //c.SetVertices();
                            //c.SetAxes();

                            // Call collision event with kvp element as the data
                            OnNewCollision(a, new Vector2(0, 0));
                        }
                    }
                    else
                    {
                        // Check if the objects intersect with the player reference
                        if (c.getSetHitbox.Intersects(a.getSetHitbox) && c.getSetName != "Ghost")
                        {
                            // Set the vertices of all objects
                            // Specifically for moving objects
                            c.SetVertices();


                            // Perform sat test
                            sat.SATTest(c, a);

                            // If the sat test passes
                            if (sat.getPass)
                            {
                                // Call collision event with kvp element as the data
                                OnNewCollision(a, sat.CalculateMTV());
                            }
                        }
                    }
                }

                //c.RemoveCollisionListener();
            }

        }

    }

}
