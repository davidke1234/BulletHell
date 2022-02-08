using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Matrix
{
    public class Player : SpriteNew
    {
        public Bullet Bullet;
        private float _speed = 2f;

        public Player(Texture2D texture)
      : base(texture)
        { }

        public override void Update(GameTime gameTime, List<SpriteNew> sprites)
        {
            _previousKey = _currentKey;
            _currentKey = Keyboard.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                //move sprite up
                Position.Y -= _speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                //move sprite down
                Position.Y += _speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                //move sprite left
                Position.X -= _speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                //move sprite right
                Position.X += _speed;
            }

            //Used for bullets
            Direction.X = 0;
            Direction.Y = -.9f;

            if (_currentKey.IsKeyDown(Keys.Space) &&
          _previousKey.IsKeyUp(Keys.Space))
            {
                AddBullet(sprites);
            }
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
