using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game1
{
    /// <summary>
    /// Class for adding arts.
    /// </summary>
    static class Arts
    {
        /// <summary>
        /// Bomb art.
        /// </summary>
        public static Texture2D Bomb { get; private set; }

        /// <summary>
        /// First Boss art
        /// </summary>
        public static Texture2D Boss { get; private set; }

        /// <summary>
        /// Second Boss art
        /// </summary>
        public static Texture2D Boss2 { get; private set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public static void Load(ContentManager content)
        {
            Bomb = content.Load<Texture2D>("bomb");
            Boss = content.Load<Texture2D>("boss");
            Boss2 = content.Load<Texture2D>("boss2");
        }
    }
}
