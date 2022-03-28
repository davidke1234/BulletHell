using Matrix;
using Matrix.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Matrix.Models
{
    /// <summary>
    /// The Midboss class
    /// </summary>
    public class FinalBoss : Sprite, ICollidable
    {
        float shoot = 0;
        public Bomb bomb;
        public List<Bomb> bombs = new List<Bomb>();
        public int Health;
        private static FinalBoss FinalBossInstance = null;

        /// <summary>
        /// Returns a singleton instance of Final Boss.
        /// </summary>
        /// <returns>Final Boss instance.</returns>
        public static FinalBoss GetInstance
        {
            get
            {
                if (FinalBossInstance == null)
                {
                    FinalBossInstance = new FinalBoss(Arts.Boss);
                }
                return FinalBossInstance;
            }
        }

        /// <summary>
        /// Initializes an instance of <see cref="FinalBoss"/> class.
        /// </summary>
        /// <param name="texture"></param>
        private FinalBoss(Texture2D texture) : base(texture)
        {
            Position = new Vector2(Game1.Viewport.Width, 50);
            bomb = new Bomb(Arts.Bomb2);
            Name = "finalboss";
            Health = 150;
        }

        public void UpdateBombs()
        {
            if (bombs.Count() < 2)
            {
                bombs.Add(bomb);
            }

            foreach (Bomb bomb in bombs.ToList())
            {
                bomb.Position += bomb.Velocity;
                if (bomb.Position.X < 0)
                {
                    bomb.IsRemoved = true;
                }

                for (int i = 0; i < bombs.Count; i++)
                {
                    if (!bombs[i].IsRemoved)
                    {
                        bombs.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        public void ShootBombs(List<Sprite> sprites)
        {
            bomb.Velocity.Y = this.Velocity.Y + 6f;
            bomb.Position = this.Position;
            bomb.Direction = this.Direction;
            bomb.Parent = this;
            if (bombs.Count() < 3)
            {
                bombs.Add(bomb);
            }
            if (!sprites.Contains(bomb))
            {
                sprites.Add(bomb);
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            float elasped = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position.X -= 60.0f * elasped;
            if (Position.X > Game1.Viewport.Width)
            {
                Position.X = 0;
            }

            shoot += elasped;
            if (shoot > 1)
            {
                shoot = 0;
                ShootBombs(sprites);
            }

            UpdateBombs();
        }

        public void OnCollide(Sprite sprite)
        {
            if (sprite is Bullet && ((Bullet)sprite).Parent is Player)
            {
                Health--;

                if (Health <= 0)
                {
                    int scoreValue = 15;

                    IsRemoved = true;
                    GetScoreValue(sprite.Parent, scoreValue);
                }
            }  
        }
        private static void GetScoreValue(Sprite sprite, int scoreValue)
        {
            ((Player)sprite).Score.Value += scoreValue;
        }
    }
}
