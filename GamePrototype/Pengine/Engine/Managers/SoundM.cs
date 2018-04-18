using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Managers.Interfaces;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Engine.Managers
{
    public class SoundM : ISoundManager
    {
        private static ISoundManager instance;
        private ContentManager mContent;

        private SoundM()
        {

        }

        public static ISoundManager getInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SoundM();
                }
                return instance;
            }
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
        /// <returns>SoundEffect sound file</returns>
        public SoundEffect getSoundEffect(string path)
        {
            return mContent.Load<SoundEffect>(path);
        }

        // Gets and Sets the assets string value
        /// <summary>
        /// Gets a value from the string path which is searched through the content manager
        /// </summary>
        /// <param name="path">file path for content</param>
        /// <returns>SoundEffect sound file</returns>
        public Song getSong(string path)
        {
            return mContent.Load<Song>(path);
        }
    }
}
