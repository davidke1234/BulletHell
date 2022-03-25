using Matrix;
using Matrix.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Matrix
{
    /// <summary>
    /// The Midboss class
    /// </summary>
    public class MidBoss : Sprite, ICollidable
    {
        private float _changePositionTimer = 5;
        private float _timerStart = 5;
        float shoot = 0;
        public Bomb bomb;
        public List<Bomb> bombs = new List<Bomb>();
        public int Health;
        private MidBoss MidBossInstance;

        /// <summary>
        /// Returns a singleton instance of midboss
        /// </summary>
        /// <returns>An instance of the midboss</returns>
        public MidBoss GetInstance ()
        {
            if (MidBossInstance == null)
            {
                MidBossInstance = new MidBoss(Arts.Boss2);
            }
            return MidBossInstance;
        }

        /// <summary>
        /// Initializes an instance of <see cref="MidBoss"/> class.
        /// </summary>
        /// <param name="texture"></param>
        private MidBoss(Texture2D texture): base(texture)
        {
            Position = new Vector2(Game1.Viewport.Width / 2, 50);
            Health = 75;
            bomb = new Bomb(Arts.Bomb);
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
            if(!sprites.Contains(bomb))
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
            _changePositionTimer -= elasped;
            if(_changePositionTimer < 0)
            {
                Position.X = _random.Next(50, 600);
                Position.Y = _random.Next(50, 100);

                _changePositionTimer = _timerStart;
            }

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


        // Boss Movement Patterns
        IEnumerable<int> FollowPlayer(float acceleration)
        {
            while (true)
            {
               
            }
        }

        public void OnCollide(Sprite sprite)
        {
            if (sprite is Bullet && ((Bullet)sprite).Parent is Player)
            {
                Health--;

                if (Health <= 0)
                {
                    int scoreValue = 10;                                                   

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
