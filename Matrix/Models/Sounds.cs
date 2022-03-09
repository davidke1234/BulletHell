using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Matrix
{
    /// <summary>
    /// The sound class
    /// </summary>
    static class Sounds
    {
        public static Song Music { get; private set; }

        public static List<SoundEffect> soundEffects = new List<SoundEffect>();

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public static void Load(ContentManager content)
        {
            soundEffects.Add(content.Load<SoundEffect>("bombSound"));
        }
    }
}
