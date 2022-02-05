using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1
{
    /// <summary>
    /// Class for managing the sprites
    /// </summary>
    static class SpriteManager
    {
        static List<Sprite> sprites = new List<Sprite>();

        static bool isUpdating;

        static List<Sprite> addedSprites = new List<Sprite>();

        /// <summary>
        /// The number of sprites
        /// </summary>
        public static int Count { get { return sprites.Count; } }

        /// <summary>
        /// Adds a sprite to the sprite list 
        /// </summary>
        /// <param name="sprite">The sprite to add.</param>
        public static void Add(Sprite sprite)
        {
            if (!isUpdating)
            {
                AddSprite(sprite);
            }
            else
            {
                addedSprites.Add(sprite);
            }
        }

        /// <summary>
        /// Adds sprite to the list of sprites
        /// </summary>
        /// <param name="sprite"></param>
        private static void AddSprite(Sprite sprite)
        {
            sprites.Add(sprite);            
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public static void Update(GameTime gameTime)
        {
            isUpdating = true;

            foreach (var singleSprite in sprites)
            {
                singleSprite.Update(gameTime);
            }

            isUpdating = false;

            foreach (var singleSprite in addedSprites)
            {
                AddSprite(singleSprite);
            }

            addedSprites.Clear();

            sprites = sprites.Where(x => !x.IsOutdated).ToList();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (var sprite in sprites)
                sprite.Draw(spriteBatch);
        }
    }
}
