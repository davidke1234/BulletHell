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
    public class MidBoss : Sprite
    {
        private static MidBoss _instance;

        private int counter = 0;

        List<Bombs> bombs = new List<Bombs>();

        /// <summary>
        /// Provides an instance of the midboss class
        /// </summary>
        public static MidBoss Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MidBoss();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Creates an instance of <see cref="MidBoss" class./>
        /// </summary>
        private MidBoss()
        {
            image = Arts.Boss2;
        }

        public void UpdateBombs()
        {
            if (bombs.Count() < 2)
            {
                bombs.Add(Bombs.Instance);
                bombs.Add(Bombs.Instance);
                bombs.Add(Bombs.Instance);
                bombs.Add(Bombs.Instance);
            }

            foreach (Bombs bomb in bombs.ToList())
            {
                bomb.Position += bomb.Velocity;
                if (bomb.Position.X < 0)
                {
                    bomb.IsOutdated = true;
                }

                for (int i = 0; i < bombs.Count; i++)
                {
                    if (!bombs[i].IsOutdated)
                    {
                        bombs.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        public void ShootBombs()
        {
            Bombs newBomb = Bombs.Instance;
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
        public override void Update(GameTime gameTime)
        {
            if (counter % 100000 == 0)
            {
                Position.X = rand.Next(0, 600);
                Position.Y = rand.Next(0, 100);
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

        /// <summary>
        /// <inheritdoc/>
        /// </summary>        
        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Bombs singleBomb in bombs)
            {
                singleBomb.Draw(spriteBatch);
            }
            spriteBatch.Draw(image, Position, Microsoft.Xna.Framework.Color.White);
        }

        // Boss Movement Patterns
        IEnumerable<int> FollowPlayer(float acceleration)
        {
            while (true)
            {
               
            }
        }
    }
}
