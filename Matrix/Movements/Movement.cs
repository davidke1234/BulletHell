using Matrix.Models;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Matrix.Movements
{
    public class StraightVerticalMovement : IMovement
    {
        public override Vector2 Movement(GameTime gameTime, Sprite sprite)
        {
            return sprite.Position += sprite.Direction * sprite.LinearVelocity;
        }
    }

    public class StraightHorizonatalMovement : IMovement
    {
        public override Vector2 Movement(GameTime gameTime, Sprite sprite)
        {
            sprite.Position.X += 1f;
            return sprite.Position;
        }
    }

    public class PivotMovement : IMovement
    {
        public override Vector2 Movement (GameTime gameTime, Sprite sprite)
        {
            //A enemies.  Check for pivot point to go up and out
            if (sprite.Position.X > 670)
            {
                //Move them up and off screen
                sprite.Position.Y -= 1f;
            }
            else if (sprite.Position.X > 150)
            {
                //stop Y and go horizantal
                sprite.Position.X += 1f;
            }
            else
            {
                sprite.Position.X += 1f;
                sprite.Position.Y += 1f;
            }
            return sprite.Position;
        }
    }

    public class RandomMovement : IMovement
    {
        private float _changePositionTimer = 5;
        public Random _random = new Random();
        private float _timerStart = 1.25f;

        public override Vector2 Movement(GameTime gameTime, Sprite sprite)
        {
            float elasped = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _changePositionTimer -= elasped;

            if (_changePositionTimer < 0)
            {
                sprite.Position.X = _random.Next(50, 600);
                sprite.Position.Y = _random.Next(50, 100);

                _changePositionTimer = _timerStart;
            }

            return sprite.Position;
        }
    }

    public class LShapedMovement : IMovement
    {
        public override Vector2 Movement(GameTime gameTime, Sprite sprite)
        {
            throw new NotImplementedException();
        }
    }

}
