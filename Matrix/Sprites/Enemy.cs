using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Matrix
{
    public class Enemy : SpriteNew
    {
        public Bullet Bullet;

        public Enemy(Texture2D texture)
      : base(texture)
        { }

        public override void Update(GameTime gameTime, List<SpriteNew> sprites)
        {
            //Used for bullets
            Direction.X = 0f;
            Direction.Y = 25f;

            AddBullet(sprites);
        }

        private void AddBullet(List<SpriteNew> sprites)
        {
            var bullet = Bullet.Clone() as Bullet;
            bullet.Direction = this.Direction;
            bullet.Position = this.Position;
            bullet.LinearVelocity = this.LinearVelocity * 2;
            bullet.LifeSpan = 2f;
            bullet.Parent = this;

            sprites.Add(bullet);
        }
    }
}
