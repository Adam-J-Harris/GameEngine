using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Engine.Managers.Interfaces
{
    public interface ISoundManager
    {
        /// <summary>
        /// Gets and sets the value of mContent, so that files can be retrieved via the contentManager in XNA
        /// </summary>
        /// <param name="pContent">Value of content</param>
        /// <returns>ContentManager</returns>
        ContentManager getContent(ContentManager pContent);

        // Gets and Sets the assets string value
        /// <summary>
        /// Gets a value from the string path which is searched through the content manager
        /// </summary>
        /// <param name="path">file path for content</param>
        /// <returns>SoundEffect sound file</returns>
        SoundEffect getSoundEffect(string path);

        // Gets and Sets the assets string value
        /// <summary>
        /// Gets a value from the string path which is searched through the content manager
        /// </summary>
        /// <param name="path">file path for content</param>
        /// <returns>Song sound file</returns>
        Song getSong(string path);
    }
}
