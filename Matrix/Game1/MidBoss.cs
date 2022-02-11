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

        private int counter = 0;

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
        public override void Update(GameTime gameTime)
        {
            if (counter%100000==0)
            {
                Position.X = rand.Next(0, 600);
                Position.Y = rand.Next(0, 100);
            }

            counter++;

            if (Position.X > Game1.Viewport.Width)
            {
                Position.X = 0;
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
