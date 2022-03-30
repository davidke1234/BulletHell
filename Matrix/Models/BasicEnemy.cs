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
            Bullet = GetBullet(Name);

            // The default origin in the centre of the sprite
            Origin = new Vector2(_texture.Width / 2, _texture.Height / 2);

            Colour = Color.White;

            TextureData = new Color[_texture.Width * _texture.Height];
            _texture.GetData(TextureData);
        }

        private Bullet GetBullet(string name)
        {
            Bullet bullet;

            if (name.Contains("blood"))
                bullet = new Bullet(Arts.BulletRed);
            else if (name.Contains("blue"))
                bullet = new Bullet(Arts.BulletBlue);
            else if (name.Contains("black"))
                bullet = new Bullet(Arts.BulletBlack);
            else if (name.Contains("green"))
                bullet = new Bullet(Arts.BulletGreen);
            else if (name.Contains("grumpbird"))
                bullet = new Bullet(Arts.BulletOrange);
            else if (name.Contains("boss2"))
                bullet = new Bullet(Arts.Bomb);
            else if (name.Contains("boss"))
                bullet = new Bullet(Arts.Bomb2);
            else
                bullet = new Bullet(Arts.BulletBlack);
                    
            return bullet;
        }
    }
}
