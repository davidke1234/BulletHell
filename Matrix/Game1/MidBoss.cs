using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game1
{
    /// <summary>
    /// The Midboss class
    /// </summary>
    public class MidBoss: Sprite
    {
        private static MidBoss _instance;

        /// <summary>
        /// Provides an instance of the midboss class
        /// </summary>
        public static MidBoss Instance
        {
            get
            {
                if(_instance == null)
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
            Position.X = 0;
            Position.Y = 0;
            timer = 30;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override void Update()
        {

            if (Game1.GameTime?.ElapsedGameTime == TimeSpan.FromSeconds(30))
            {
                const float bossTimer = 30;
                float elapsed = (float)Game1.GameTime.ElapsedGameTime.TotalSeconds;
                timer -= elapsed;
                Random random = new Random();
                if (timer < 0)
                {
                    Position += Velocity;
                    timer = bossTimer;
                }
                Sounds.Explosion.Play(0.2f, random.Next(1, 2), 0);
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>        
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, Position, Color.White );
        }
    }
}
