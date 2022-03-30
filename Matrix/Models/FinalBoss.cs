using Matrix;
using Matrix.Models.Factories;
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
    public class FinalBoss : Enemy //Sprite, ICollidable
    {
        private float _changePositionTimer = 5;
        private float _timerStart = 1.25f; // 5;
        private float _shootingTimer;
        float shoot = 0;
        public Bomb bomb;
        public List<Bomb> bombs = new List<Bomb>();
        public new int Health;
        private static FinalBoss FinalBossInstance = null;
        private static ProjectileFactory _projectileFactory = new ProjectileFactory();

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
        public FinalBoss(Texture2D texture) : base(texture)
        {
            //Position = new Vector2(Game1.Viewport.Width, 50);
            //bomb = new Bomb(Arts.Bomb2);
            //Name = "finalboss";
            //Health = 150;
            Health = 15;
            Position.X = 70;
            Position.Y = 60;
            bomb = (Bomb) _projectileFactory.Create("bomb2", Enemy.Type.FinalBoss);
            bombs.Add(bomb);

            LifeSpan = 5;
            Speed = 2.65f;
            Origin = new Vector2(_texture.Width / 2, _texture.Height / 2);

            Colour = Color.White;
            TextureData = new Color[_texture.Width * _texture.Height];
            _texture.GetData(TextureData);

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

        //public void ShootBombs(List<Sprite> sprites)
        //{
        //    bomb.Velocity.Y = this.Velocity.Y + 6f;
        //    bomb.Position = this.Position;
        //    bomb.Direction = this.Direction;
        //    bomb.Parent = this;
        //    if (bombs.Count() < 3)
        //    {
        //        bombs.Add(bomb);
        //    }
        //    if (!sprites.Contains(bomb))
        //    {
        //        sprites.Add(bomb);
        //    }
        //}

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            //float elasped = (float)gameTime.ElapsedGameTime.TotalSeconds;
            //Position.X -= 60.0f * elasped;
            //if (Position.X > Game1.Viewport.Width)
            //{
            //    Position.X = 0;
            //}

            //shoot += elasped;
            //if (shoot > 1)
            //{
            //    shoot = 0;
            //    ShootBombs(sprites);
            //}

            //UpdateBombs();

            //Copied from midboss
            float elasped = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _changePositionTimer -= elasped;

            _shootingTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_changePositionTimer < 0)
            {
                Position.X = _random.Next(50, 600);
                Position.Y = _random.Next(50, 100);

                _changePositionTimer = _timerStart;
            }

            if (_shootingTimer >= LifeSpan)
                this.IsRemoved = true;

            if (Position.X > Game1.Viewport.Width)
            {
                Position.X = 0;
            }

            if (_shootingTimer >= TimerStart)
            {
                DropBomb(sprites, new Vector2(0, 0), "bomb2", Enemy.Type.FinalBoss);
                _shootingTimer = 0;
            }
        }

        public new void OnCollide(Sprite sprite)
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
