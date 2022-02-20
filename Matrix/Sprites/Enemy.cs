using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Matrix
{
    public class Enemy : Sprite, ICollidable
    {
        public Bullet Bullet;
        private float _shootingTimer;
        public float TimerStart = 1.25f;
        public float Speed = 2f;
        
        public enum Type { 
            A, //small grunts
            B, //larger
            C, //midboss
            D  //finalboss
        }
        public int Health;

        public Enemy(Texture2D texture)
      : base(texture)
        { }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            _shootingTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_shootingTimer >= TimerStart)
            {
                DropBullet(sprites);
                _shootingTimer = 0;
            }

            Direction.X = 10f;
            Direction.Y = 25f;

            //If off screen, remove enemy
            if (Position.Y < -10)
                IsRemoved = true;

            //B Enemies
            if (_texture.Name == "GrumpBird" || _texture.Name == "Boss" || _texture.Name == "Boss2")
            {
                Position.X += 1f;
            }
            else
            {
                //A enemies.  Check for pivot point to go up and out
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

        private void DropBullet(List<Sprite> sprites)
        {
            var bullet = Bullet.Clone() as Bullet;
            bullet.Direction = Direction;
            bullet.Position = Position;
            bullet.LinearVelocity = 0.05f;
            bullet.LifeSpan = 6f;
            bullet.Parent = this;

            sprites.Add(bullet);
        }

        public void OnCollide(Sprite sprite)
        {
            //If the player hits an enemy, remove enemy, but score
            if (sprite is Player && !((Player)sprite).IsDead)
            {
                GetScoreValue(sprite, 1);

                // We want to remove the enemy
                IsRemoved = true;
            }

            // Hit an enemy.  Deduct 1 health point     
            if (sprite is Bullet && ((Bullet)sprite).Parent is Player)
            {
                Health--;

                if (Health <= 0)
                {
                    int scoreValue;

                    switch(Name)
                    {
                        case "Boss":
                            scoreValue = 15;
                            break;
                        case "Boss2":
                            scoreValue = 15;
                            break;
                        case "GrumpBird":
                            scoreValue = 5;
                            break;
                        default:
                            scoreValue = 1;
                            break;                            
                    }

                    IsRemoved = true;
                    GetScoreValue(sprite.Parent, scoreValue);
                }
            }
        }

        private static void GetScoreValue(Sprite sprite, int scoreValue)
        {
            ((Player)sprite).Score.Value += scoreValue;
        }
    }
}
