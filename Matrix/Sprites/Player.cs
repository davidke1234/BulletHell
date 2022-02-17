using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Matrix
{
    public class Player : SpriteNew
    {
        public Bullet Bullet;

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
                if (this.Position.Y != 0)
                {
                    Position -= this.YVelocity;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                //move sprite down
                Position += this.YVelocity;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                //move sprite left
                if (Position.X > 55)
                {
                    Position -= this.XVelocity;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                //move sprite right
                if (Position.X < 740)
                {
                    Position += this.XVelocity;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                //move sprite up
                Position -= this.YVelocity / 8;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down) && Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                //move sprite down
                Position += this.YVelocity / 8;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left) && Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                //move sprite left
                Position -= this.XVelocity / 8;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right) && Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                //move sprite right
                Position += this.XVelocity / 8;
            }

            if (_currentKey.IsKeyDown(Keys.F) &&
          _previousKey.IsKeyUp(Keys.F))
            {
                //Used for bullets
                Direction.X = 0;
                Direction.Y = -.9f;

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
