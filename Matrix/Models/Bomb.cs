using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Matrix.Models
{
    public class Bomb : Projectile, ICollidable
    {
        private float _timer;

        public Bomb(Texture2D texture): base(texture)
        {
        }

        public void OnCollide(Sprite sprite)
        {
            if (sprite is Player && (this.Parent is MidBoss || this.Parent is FinalBoss))
            {
                IsRemoved = true;
                //AddExplosion();
            }
            if (sprite is Enemy && this.Parent is Player)
                IsRemoved = true;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer >= LifeSpan)
                this.IsRemoved = true;

            Direction.X = 10f;
            Direction.Y = 25f;

            Position += Direction * LinearVelocity;

        }
    }
}
