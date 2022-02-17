using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Matrix
{
    public class Enemy : SpriteNew
    {
        public Bullet Bullet;
        private float _shootingTimer;
        private float _enemyTimer;
        public float ShootingTimer; // = 1.25f;
        public float Speed = 2f;
        public enum Type { A, B }

        public Enemy(Texture2D texture)
      : base(texture)
        { }

        public override void Update(GameTime gameTime, List<SpriteNew> sprites)
        {
            _shootingTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            _enemyTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_shootingTimer >= ShootingTimer)
            {
                DropBullet(sprites);
                _shootingTimer = 0;
            }
            Direction.X = 0f;
            Direction.Y = 25f;

             //If off screen, remove enemy
            if (Position.Y < -10)
                this.IsRemoved = true;

            if (this._texture.Name == "butterfly")
            {
                Position.X += 1f;
            }
            else
            {
                //Check for pivot point to go up and out
                if (Position.X > 670)
                {
                    //Move them up and off screen
                    Position.Y -= 1f;
                }
                else if (Position.X > 150)
                {
                    //stop Y and go horizantal
                    Position.X += 1f;
                }
                else
                {
                    Position.X += 1f;
                    Position.Y += 1f;
                }
            }

                




            // if the enemy is off the left side of the screen

          

            //if (Position.X < -_texture.Width)
            //    this.IsRemoved = true;
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
