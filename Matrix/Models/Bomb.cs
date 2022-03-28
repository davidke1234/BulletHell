using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Matrix.Models
{
    public class Bomb : Sprite, ICollidable
    {
        private float _timer;

        public Bomb(Texture2D texture): base(texture)
        { 
        }

        public void OnCollide(Sprite sprite)
        {
            if (sprite is Bomb)
                return;

            // Enemies can't shoot eachother
            if (sprite is Bomb && this.Parent is MidBoss)
                return;

            if (sprite is Bomb && this.Parent is FinalBoss)
                return;

            // Players can't shoot eachother
            if (sprite is Player && this.Parent is Player)
                return;

            // Can't hit a player if they're dead
            if (sprite is Player && ((Player)sprite).Die)
                return;

            if (sprite is Player && (this.Parent is MidBoss || this.Parent is FinalBoss))
            {
                IsRemoved = true;
                //AddExplosion();
            }
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer >= LifeSpan)
                this.IsRemoved = true;

            Position += Direction * LinearVelocity;
        }
    }
}
