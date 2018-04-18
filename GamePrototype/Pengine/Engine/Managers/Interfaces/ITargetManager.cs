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
using Engine.Objects.Minds.Interfaces;

namespace Engine.Managers.Interfaces
{
    public interface ITargetManager
    {
        /// <summary>
        /// Link the IEntity entity and the IMind mind
        /// </summary>
        /// <param name="entity">IEntity to be linked</param>
        /// <param name="mind">IMind to be linked</param>
        void SetEntityToMind(IEntity entity, IMind mind);

        /// <summary>
        /// Set the ITargetable entity's getSetTarget field to the IEntity target
        /// </summary>
        /// <param name="entity">ITargetable who wants a target</param>
        /// <param name="target">IEntity to be targeted</param>
        void SetTargetToEntity(ITargetable entity, IEntity target);
    }
}
