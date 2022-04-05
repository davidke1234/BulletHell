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
        private static ProjectileFactory projectileFactory = new ProjectileFactory();

        //public BasicEnemy(Texture2D texture) : base(texture)
        //{
        //    _texture = texture;
        //    Name = texture.Name;

        //    // The default origin in the centre of the sprite
        //    Origin = new Vector2(_texture.Width / 2, _texture.Height / 2);

        //    Colour = Color.White;

        //    TextureData = new Color[_texture.Width * _texture.Height];
        //    _texture.GetData(TextureData);
        //}

        public BasicEnemy(Texture2D texture, Type basicEnemyType ) : base(texture)
        {
            _texture = texture;
            Name = texture.Name;

             if (basicEnemyType == Type.BasicEnemies)
            {
                Health = 1;
                Position.X = 70;
                Position.Y = 10;
                Bullet = (Bullet)projectileFactory.Create("bullet");
            }
           
            else if (basicEnemyType == Type.MidBoss)
            {
                Health = 10;
                Position.X = 70;
                Position.Y = 60;
                Bomb = (Bomb)projectileFactory.Create("bomb");
            }
            else if (basicEnemyType == Type.FinalBoss)
            {
                Health = 15;
                Position.X = 70;
                Position.Y = 60;
                Bomb = (Bomb)projectileFactory.Create("bomb2");
            }

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
