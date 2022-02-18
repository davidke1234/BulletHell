using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Matrix
{
    public class Bullet : SpriteNew, ICollidable
    {
        private float _timer;
        //public Explosion Explosion;

        public Bullet(Texture2D texture) : base(texture)
        { }

        public void OnCollide(SpriteNew sprite)
        {
            if (sprite is Bullet)
                return;

            // Enemies can't shoot eachother
            if (sprite is Enemy && this.Parent is Enemy)
                return;

            // Players can't shoot eachother
            if (sprite is Player && this.Parent is Player)
                return;

            // Can't hit a player if they're dead
            if (sprite is Player && ((Player)sprite).IsDead)
                return;

            if (sprite is Enemy && this.Parent is Player)
            {
                IsRemoved = true;
                //AddExplosion();
            }

            if (sprite is Player && this.Parent is Enemy)
            {
                IsRemoved = true;
                //AddExplosion();
            }
        }

        public override void Update(GameTime gameTime, List<SpriteNew> sprites)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer >= LifeSpan)
                this.IsRemoved = true;

            Position += Direction * LinearVelocity;
        }

        //private void AddExplosion()
        //{
        //    if (Explosion == null)
        //        return;

        //    var explosion = Explosion.Clone() as Explosion;
        //    explosion.Position = this.Position;

        //    Children.Add(explosion);
        //}
    }
}
