using Matrix.Movements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Matrix.Models
{
    public class Bullet : Projectile, ICollidable
    {
        private float _timer;
        //public Explosion Explosion;

        public Bullet(Texture2D texture) : base(texture)
        { }

        public void OnCollide(Sprite sprite)
        {
            //if (sprite is Bullet)
            //    return;
            //// Enemies can't shoot eachother
            //if (sprite is Enemy && this.Parent is Enemy)
            //    return;

            //// Players can't shoot eachother
            //if (sprite is Player && this.Parent is Player)
            //    return;

            //// Can't hit a player if they're dead
            //if (sprite is Player && ((Player)sprite).Die)
            //    return;
            if (sprite is Enemy && this.Parent is Player)
                 IsRemoved = true;

            //if (sprite is Player && this.Parent is Enemy)
            //    IsRemoved = true;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer >= LifeSpan)
                this.IsRemoved = true;
            
            verticalMovement.Movement(gameTime, this);
        }

      
    }
}
