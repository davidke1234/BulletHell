using Matrix.Models.Enums;
using Matrix.Models.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Matrix.Models
{
    public class BasicEnemy : Enemy
    {
        SpriteFactoryProvider projectileFactory = SpriteFactoryProvider.GetFactory(typeof(Projectile).Name);
        public BasicEnemy(Texture2D texture) : base(texture)
        {
            _texture = texture;
            Name = texture.Name;
            Health = 1;
            Position.X = 70;
            Position.Y = 10;
            Bullet = (Bullet)projectileFactory.Create(typeof(Bullet).Name, Arts.Bullet);
                       

            LifeSpan = 5;
            Speed = 2.65f;

            // The default origin in the centre of the sprite
            Origin = new Vector2(_texture.Width / 2, _texture.Height / 2);

            Colour = Color.White;

            TextureData = new Color[_texture.Width * _texture.Height];
            _texture.GetData(TextureData);
        }
    }
}
