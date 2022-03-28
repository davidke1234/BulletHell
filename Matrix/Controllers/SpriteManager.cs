using Matrix.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Matrix
{
    /// <summary>
    /// Class for managing the sprites
    /// </summary>
    static class SpriteManager
    {
        static List<Sprite> sprites = new List<Sprite>();

        static bool isUpdating;

        static List<Sprite> addedSprites = new List<Sprite>();

        //TODO: TEMP to look at movements
        static List<Enemy> removedEnemies = new List<Enemy>();

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
                singleSprite.Update(gameTime, sprites);
            }

            isUpdating = false;

            foreach (var singleSprite in addedSprites)
            {
                AddSprite(singleSprite);
            }

            addedSprites.Clear();

            sprites = sprites.Where(x => !x.IsRemoved).ToList();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public static void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var sprite in sprites)
                sprite.Draw(gameTime, spriteBatch);
        }

        public static void HandleCollisions(List<Sprite> sprites)
        {
            var collidedSprites = sprites.Where(c => c is ICollidable);

            foreach (var sprite1 in collidedSprites)
            {
                foreach (var sprite2 in collidedSprites)
                {
                    if (sprite1 == sprite2)  //same sprite so continue
                        continue;

                    if (!sprite1.CollisionArea.Intersects(sprite2.CollisionArea))
                        continue;

                    //If the sprite is Player and is shooting this sprite as a bullet, continue
                    if (sprite1 is Player && sprite2.Parent is Player || sprite2 is Player && sprite1.Parent is Player)
                        continue;

                    if (sprite1.Intersects(sprite2))
                        ((ICollidable)sprite1).OnCollide(sprite2);
                }
            }
        }

        public static void CleanUpRemovedSprites(List<Sprite> sprites)
        {
            //Clean up no longer needed sprites
            for (int i = 0; i < sprites.Count; i++)
            {
                if (sprites[i].IsRemoved)
                {
                    // TODO: temp to look at all positions
                    if (sprites[i] is Enemy)
                    {
                        removedEnemies.Add((Enemy)sprites[i]);
                    }
                    sprites.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
