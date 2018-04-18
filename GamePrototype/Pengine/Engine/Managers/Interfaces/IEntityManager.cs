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
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Objects.Entities.Interfaces;

namespace Engine.Managers.Interfaces
{
    public interface IEntityManager
    {
        /// <summary>
        /// Using generic class 'T', creates an entity of type IEntity
        /// </summary>
        /// <typeparam name="T">IEntity or sub-class, the class type representing the IEntity value</typeparam>
        /// <returns>IEntity, the value from the dictionary</returns>
        IEntity Create<T>() where T : IEntity, new();

        /// <summary>
        /// Returns the entity dictionary
        /// </summary>
        IDictionary<string, IEntity> getDictionary { get; }

        /// <summary>
        /// Adds a type IEntity to the dictionary
        /// </summary>
        /// <param name="entity">IEntity, the value of the entry</param>
         void AddToDict(IEntity entity);

        /// <summary>
        /// Adds a type IEntity to the dictionary
        /// </summary>
        /// <param name="id">string, the key of the entry</param>
        /// <param name="entity">IEntity, the value of the entry</param>
        void AddToDict(string id, IEntity entity);

        /// <summary>
        /// Gets an entity value from the dictionary
        /// </summary>
        /// <param name="type">string, the key to the dictionary</param>
        /// <returns>IEntity, the value from the dictionary</returns>
        IEntity getEntity(string type);

        /// <summary>
        /// Removes an Entity with the key 'type' from the dictionary, if the dictionary contains that entity
        /// </summary>
        /// <param name="type">string, the key for the dictionary</param>
        void Remove(string type);

        /// <summary>
        /// Removes IEntity keyvaluepairs from the dictionary
        /// </summary>
        /// <param name="toRemove">contains values to be removed</param>
        /// <param name="staticsOnly">if only statics entities are being removed</param>
        void UnloadEntities(IList<string> toRemove, bool staticsOnly);
    }
}
