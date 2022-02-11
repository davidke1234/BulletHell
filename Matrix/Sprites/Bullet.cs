using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Matrix
{
    public class Bullet : SpriteNew
    {
        private float _timer;

        public Bullet(Texture2D texture) : base(texture)
        { }

        public override void Update(GameTime gameTime, List<SpriteNew> sprites)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer >= LifeSpan)
                this.IsRemoved = true;

            Position += Direction * LinearVelocity;
        }
    }
}
