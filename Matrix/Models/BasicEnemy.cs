using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Matrix.Models
{
    public class BasicEnemy : Enemy
    {
        public BasicEnemy(Texture2D texture) : base(texture)
        {
            _texture = texture;
            Name = texture.Name;

            // The default origin in the centre of the sprite
            Origin = new Vector2(_texture.Width / 2, _texture.Height / 2);

            Colour = Color.White;

            TextureData = new Color[_texture.Width * _texture.Height];
            _texture.GetData(TextureData);
        }

        public BasicEnemy(Texture2D texture, Type basicEnemyType ) : base(texture)
        {
            _texture = texture;
            Name = texture.Name;
            Health = 1;
            LifeSpan = 5;
            Position.X = 70;
            Position.Y = 10;
            Speed = 2.65f;
            Bullet = new Bullet(Arts.BulletBlack);

            // The default origin in the centre of the sprite
            Origin = new Vector2(_texture.Width / 2, _texture.Height / 2);

            Colour = Color.White;

            TextureData = new Color[_texture.Width * _texture.Height];
            _texture.GetData(TextureData);

        }
    }
}
