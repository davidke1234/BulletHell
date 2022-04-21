using Matrix.Models;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Matrix.Movements
{
    public class StraightMovement : IMovement
    {
        public override Vector2 Movement(GameTime gameTime, Sprite sprite)
        {
            return sprite.Position += sprite.Direction * sprite.LinearVelocity;
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
