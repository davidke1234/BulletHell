using Matrix;
using Matrix.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX;
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
        private static MidBoss _instance;

        private int counter = 0;
        private float _shootingTimer;
        private float _timerStart = 1f;

        List<Bombs> bombs = new List<Bombs>();

        public MidBoss(Texture2D texture)
: base(texture)
        { }

         public void UpdateBombs()
        {
            if (bombs.Count() < 2)
            {
                bombs.Add(new Bombs(_texture));
                //bombs.Add(Bombs.Instance);
                //bombs.Add(Bombs.Instance);
                //bombs.Add(Bombs.Instance);
            }

            foreach (Bombs bomb in bombs.ToList())
            {
                bomb.Position += bomb.Velocity;
                if (bomb.Position.X < 0)
                {
                    bomb.IsRemoved = true;
                }              
            }
        }

        public void ShootBombs()
        {
            Bombs newBomb = new Bombs(_texture);
            newBomb.Velocity.X = Velocity.X - 3f;
            newBomb.Velocity.Y = Velocity.Y + 4f;
            newBomb.Position = Position;

            if (bombs.Count() < 3)
            {
                bombs.Add(newBomb);
            }
        }

        float shoot = 0;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            _shootingTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_shootingTimer >= _timerStart)
            {
                Position.X = _random.Next(0, 600);
                Position.Y = _random.Next(0, 100);
                _shootingTimer = 0;
            }

            counter++;

            if (Position.X > Game1.Viewport.Width)
            {
                Position.X = 0;
            }

            shoot += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (shoot > 1)
            {
                shoot = 0;
                ShootBombs();
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
            throw new NotImplementedException();
        }
    }
}
