using Matrix.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Matrix
{
    /// <summary>
    /// Class for managing the sprites
    /// </summary>
    static class SpriteManager
    {
        public static List<Sprite> Sprites = new List<Sprite>();

        static bool isUpdating;

        static List<Sprite> addedSprites = new List<Sprite>();

        /// <summary>
        /// The number of sprites
        /// </summary>
        public static int Count { get { return Sprites.Count; } }

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
            Sprites.Add(sprite);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public static void Update(GameTime gameTime)
        {
            isUpdating = true;

            foreach (var singleSprite in Sprites)
            {
                singleSprite.Update(gameTime, Sprites);
            }

            isUpdating = false;

            foreach (var singleSprite in addedSprites)
            {
                AddSprite(singleSprite);
            }

            addedSprites.Clear();

            Sprites = Sprites.Where(x => !x.IsRemoved).ToList();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public static void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var sprite in Sprites)
                sprite.Draw(gameTime, spriteBatch);
        }

        public static void HandleCollisions(List<Sprite> sprites)
        {
            //Compare sprites for a possible collision
            var collidedSprites = sprites.Where(c => c is ICollidable);

            foreach (var sprite1 in collidedSprites)
            {
                foreach (var sprite2 in collidedSprites)
                {
                    if (ValidCollision(sprite1, sprite2))
                    {
                        if (!sprite1.CollisionArea.Intersects(sprite2.CollisionArea))
                            continue;

                        if (sprite1.Intersects(sprite2))
                            ((ICollidable)sprite1).OnCollide(sprite2);
                    }
                }
            }
        }

        private static bool ValidCollision(Sprite sprite1, Sprite sprite2)
        {
            if ((sprite1 is Player && (sprite2 is Bullet || sprite2 is Bomb) && sprite2.Parent is Enemy) // enemy bullet hitting player
                || ((sprite1 is Bullet || sprite1 is Bomb) && sprite2.Parent is Enemy && sprite2 is Player) // enemy bullet hitting player
                || ((sprite1 is Bullet || sprite1 is Bomb) && sprite1.Parent is Player && sprite2 is Enemy) // player shooting enemy
                || (sprite1 is Enemy && (sprite2 is Bullet || sprite2 is Bomb) && sprite2.Parent is Player))// player shooting enemy
                return true;
            else
                return false;
        }
        
        public static void CleanUpRemovedSprites(List<Sprite> sprites)
        {
            //Clean up no longer needed sprites
            for (int i = 0; i < sprites.Count; i++)
            {
                if (sprites[i].IsRemoved)
                {                   
                    sprites.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
