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
using Engine.Managers.Interfaces;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.Managers
{
    public class ImageM : IImageManager
    {
        private static IImageManager instance = null;

        private ContentManager mContent;

        /// <summary>
        /// If instance is null
        ///     Create a new instance
        /// return instance
        /// </summary>
        public static IImageManager getInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ImageM();
                }

                return instance;
            }
        }

        private ImageM()
        {

        }

        /// <summary>
        /// Gets and sets the value of mContent, so that files can be retrieved via the contentManager in XNA
        /// </summary>
        /// <param name="pContent">Value of content</param>
        /// <returns>ContentManager</returns>
        public ContentManager getContent(ContentManager pContent)
        {
            mContent = pContent;
            return mContent;
        }

        // Gets and Sets the assets string value
        /// <summary>
        /// Gets a value from the string path which is searched through the content manager
        /// </summary>
        /// <param name="path">file path for content</param>
        /// <returns>Texture2D image file</returns>
        public Texture2D getAsset(string path)
        {
            return mContent.Load<Texture2D>(path);
        }

    }
}
