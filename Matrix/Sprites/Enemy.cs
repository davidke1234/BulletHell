using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Matrix
{
    public class Enemy : SpriteNew
    {
        public Bullet Bullet;
        private float _timer;
        public float ShootingTimer; // = 1.25f;
        public float Speed = 2f;
        public enum Type { A, B }

        public Enemy(Texture2D texture)
      : base(texture)
        { }

        public override void Update(GameTime gameTime, List<SpriteNew> sprites)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer >= ShootingTimer)
            {
                DropBullet(sprites);
                _timer = 0;
            }
            Direction.X = 0f;
            Direction.Y = 25f;

            //Moves the enemies
            Position += new Vector2(1f, 0);

            // if the enemy is off the left side of the screen
            if (Position.X < -_texture.Width)
                this.IsRemoved = true;
        }

        private void DropBullet(List<SpriteNew> sprites)
        {
            var bullet = Bullet.Clone() as Bullet;
            bullet.Direction = this.Direction;
            bullet.Position = this.Position;
            bullet.LifeSpan = 2f;

            sprites.Add(bullet);
        }
    }
}
