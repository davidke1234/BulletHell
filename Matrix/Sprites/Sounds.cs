using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Linq;

namespace Matrix
{
    /// <summary>
    /// The sound class
    /// </summary>
    static class Sounds
    {
        private static readonly Random random = new Random();
        public static Song Music { get; private set; }

        private static SoundEffect[] explosions;

        /// <summary>
        /// Explosion sound effect
        /// </summary>
        public static SoundEffect Explosion { get { return explosions[random.Next(explosions.Length)]; } }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public static void Load(ContentManager content)
        {
            // Load the game music here
            explosions = Enumerable.Range(1, 10).Select(x => content.Load<SoundEffect>("bombSound")).ToArray();
        }
    }
}
