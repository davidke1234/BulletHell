using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Matrix
{
    //player sprites, normal enemy sprites, mid boss sprite, final boss sprite
    public abstract class Sprite
    {
        public Texture2D image;
        protected Color color = Color.White;
        public Vector2 Position;
        public Vector2 Velocity;
        public float Orientation;
        public bool IsOutdated;
        public float timer { get; set; }
        public static Random rand = new Random();

        //public float _speed;

        /// <summary>
        /// Returns the image size
        /// </summary>
        public Vector2 Size
        {
            get
            {
                return image == null ? Vector2.Zero : new Vector2(image.Width, image.Height);
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, Position, null, color, Orientation, Size / 2f, 1f, 0, 0);
        }

    }
}
