using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Matrix
{
    public class Enemy : SpriteNew, ICollidable
    {
        public Bullet Bullet;
        private float _shootingTimer;
        private float _enemyTimer;
        public float ShootingTimer; // = 1.25f;
        public float Speed = 2f;
        private bool _collisionDetected = false;

        public enum Type { A, B }
        public int Health;

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

            Direction.X = 10f;
            Direction.Y = 25f;

            //If off screen, remove enemy
            if (Position.Y < -10)
                this.IsRemoved = true;

            if (this._texture.Name == "GrumpBird")
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
        }

        private void DropBullet(List<SpriteNew> sprites)
        {
            var bullet = Bullet.Clone() as Bullet;
            bullet.Direction = this.Direction;
            bullet.Position = this.Position;
            bullet.LinearVelocity = 0.05f;
            bullet.LifeSpan = 6f;
            bullet.Parent = this;

            sprites.Add(bullet);
        }

        public void OnCollide(SpriteNew sprite)
        {
            //If we crash into a player that is still alive
            if (sprite is Player && !((Player)sprite).IsDead)
            {
                //((Player)sprite).Score.Value++;

                // We want to remove the ship completely
                IsRemoved = true;
            }

            // If we hit a bullet that belongs to a player      
            if (sprite is Bullet && ((Bullet)sprite).Parent is Player)
            {
                Health--;

                if (Health <= 0)
                {
                    IsRemoved = true;
                    //((Player)sprite.Parent).Score.Value++;
                }
            }
        }
    }
}
