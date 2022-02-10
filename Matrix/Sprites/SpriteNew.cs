using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Matrix
{
    public class SpriteNew : ICloneable
    {
        public Texture2D image;
        protected Texture2D _texture;
        protected KeyboardState _currentKey;
        protected KeyboardState _previousKey;
        public Vector2 Position;
        public Vector2 Origin;
        public Vector2 Direction;
        public Vector2 Velocity;
        public float LinearVelocity = 4f;
        public SpriteNew Parent;
        public float LifeSpan = 0f;
        public bool IsRemoved = false;
        public bool IsOutdated;
        public float timer { get; set; }
        public static Random rand = new Random();

        public SpriteNew(Texture2D texture)
        {
            _texture = texture;

            // The default origin in the centre of the sprite
            Origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
        }

        //Note: overridden in each class
        public virtual void Update(GameTime gameTime, List<SpriteNew> sprites)
        { }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, null, Color.White, 0f, Origin, 1f, SpriteEffects.None, 0);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
