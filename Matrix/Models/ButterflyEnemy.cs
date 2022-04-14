using Matrix.Models.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Matrix.Models
{
    public class ButterflyEnemy : Enemy
    {
        private static ProjectileFactory projectileFactory = new ProjectileFactory();

        public ButterflyEnemy(Texture2D texture) : base(texture)
        {
            _texture = texture;
            Name = texture.Name;

            Health = 5;
            Position.X = 70;
            Position.Y = 40;
            Bullet = (Bullet)projectileFactory.Create(typeof(BasicEnemy).Name, Arts.BulletOrange);

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
