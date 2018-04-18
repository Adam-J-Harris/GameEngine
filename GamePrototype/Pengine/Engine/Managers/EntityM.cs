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
using Engine.Managers.Interfaces;
using Engine.Objects.Entities.Interfaces;

namespace Engine.Managers
{
    public class EntityM : IEntityManager
    {
        private static IEntityManager instance;

        private IDictionary<string, IEntity> entityDict;
        private IEntity entity;

        private int entityNumber = 0;

        public static bool removedObj = false;

        //private int playerNumber = 0;
        //private int ghostNumber = 0;
        //private int patientNumber = 0;

        //private int doorNumber = 0;
        //private int floorNumber = 0;
        //private int handrailNumber = 0;
        //private int keyNumber = 0;
        //private int stairNumber = 0;
        //private int wallNumber = 0;
        //private int windowNumber = 0;

        //private int filterNumber = 0;

        private EntityM()
        {
            entityDict = new Dictionary<string, IEntity>();
        }

        /// <summary>
        /// Makes this class a singleton
        /// </summary>
        public static IEntityManager getInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EntityM();
                }
                return instance;
            }
        }

        /// <summary>
        /// Using generic class 'T', creates an entity of type IEntity
        /// </summary>
        /// <typeparam name="T">IEntity or sub-class, the class type representing the IEntity value</typeparam>
        /// <returns>IEntity, the value from the dictionary</returns>
        public IEntity Create<T>() where T : IEntity, new()
        {
            entity = new T();
            entityNumber++;

            return entity;
        }

        /// <summary>
        /// Returns the entity dictionary
        /// </summary>
        public IDictionary<string, IEntity> getDictionary
        {
            get { return entityDict; }
        }

        /// <summary>
        /// Adds a type IEntity to the dictionary
        /// </summary>
        /// <param name="entity">IEntity, the value of the entry</param>
        public void AddToDict(IEntity entity)
        {
            entityDict.Add(entity.getSetID, entity);
        }

        /// <summary>
        /// Adds a type IEntity to the dictionary
        /// </summary>
        /// <param name="id">string, the key of the entry</param>
        /// <param name="entity">IEntity, the value of the entry</param>
        public void AddToDict(string id, IEntity entity)
        {
            entityDict.Add(id, entity);
        }

        /// <summary>
        /// Gets an entity value from the dictionary
        /// </summary>
        /// <param name="type">string, the key to the dictionary</param>
        /// <returns>IEntity, the value from the dictionary</returns>
        public IEntity getEntity(string type)
        {
            IEntity ent = null;

            // Search using the key string 
            if (entityDict.ContainsKey(type))
            {
                // Get the value from the key
                ent = entityDict[type];
            }

            return ent;
        }

        /// <summary>
        /// Removes an Entity with the key 'type' from the dictionary, if the dictionary contains that entity
        /// </summary>
        /// <param name="type">string, the key for the dictionary</param>
        public void Remove(string type)
        {
            removedObj = true;

            // Search using the key string 
            if (entityDict.ContainsKey(type))
            {
                // Get the value from the key
                entity = entityDict[type];
            }

            entityDict.Remove(type);

            entity = null;

            GC.Collect();
        }

        /// <summary>
        /// Removes IEntity keyvaluepairs from the dictionary
        /// </summary>
        /// <param name="toRemove">contains values to be removed</param>
        /// <param name="staticsOnly">if only statics entities are being removed</param>
        public void UnloadEntities(IList<string> toRemove, bool staticsOnly)
        {
            //for (int i = 0; i < toRemove.Count; i++)
            //{
            //    foreach (KeyValuePair<string, IEntity> e in entityDict)
            //    {
            //        if (e.Value.getSetName == toRemove[i])
            //        {
            //            entityDict.Remove(e);
            //        }
            //    }
            //}

            //ResetNumbers(staticsOnly);

            //if (!staticsOnly)
            //{
            //    //toRemove = new List<string> { ""}
            //    //MindM.getInstance.UnloadMinds(toRemove);
            //}

            //IEntity player = getEntity("Player1");
            //IEntity fil = getEntity("Filter");

            entityDict.Clear();
            ResetNumbers(staticsOnly);

            //playerNumber++;
            //entityDict.Add(player.getSetID, player);
            //entityDict.Add("Filter", fil);

            //ReorderDictionary(player);
        }

        /// <summary>
        /// Resets all numbers assoiciated with a IEntity sub-class
        /// </summary>
        /// <param name="staticsOnly">bool which determines to reset the non static classes as well</param>
        private void ResetNumbers(bool staticsOnly)
        {
            entityNumber = 0;
        }

        private void ReorderDictionary(IEntity p)
        {
            entityDict.Remove(p.getSetID);
            entityDict.Add(p.getSetID, p);
        }

    }

}
