using Matrix.Controllers;
using Matrix.Models.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Matrix.Models
{
    public class Enemy : Sprite, ICollidable
    {
        public Sprite Bullet;
        public Sprite Bomb;
        private float _shootingTimer;
        public float TimerStart = 1.25f;
        public float Speed = 2f;
        private static ProjectileFactory _projectileFactory = new ProjectileFactory();
        private double _lastShotSecond = 0;

        
        public int Health { get; set; }

        public Enemy(Texture2D texture)
      : base(texture)
        { }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            _shootingTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            string name = _texture.Name.ToLower();

            if (_shootingTimer >= LifeSpan)
                IsRemoved = true;

            if (_shootingTimer >= TimerStart)
            {        
                if (ShouldShoot(gameTime, 2))
                {
                    DropBullet(sprites, new Vector2(-1, -1));
                }
                _shootingTimer = 0;
            }

            Direction.X = 1f;
            Direction.Y = 25f;

            //If off screen, remove enemy
            if (Position.Y < -10 || Position.X > 800)
                IsRemoved = true;

            //B,C,D Enemies
            if (name == "grumpbird" || name == "finalboss" || name == "midboss")
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

            EnemyManager.CheckForEscapeCheat(sprites);
        }

        private bool ShouldShoot(GameTime gameTime, int addSeconds)
        {
            //This limits bullets 
            bool shouldShoot = false;
            double second = Math.Round(gameTime.TotalGameTime.TotalSeconds, 0);

            if (second > _lastShotSecond + addSeconds)
            {
                _lastShotSecond = second;
                shouldShoot = true;
            }

            return shouldShoot;
        } 

        public void DropBullet(List<Sprite> sprites, Vector2 extraDirection)
        {
            var bullet = Bullet.Clone() as Bullet;
            bullet.Direction = Direction + extraDirection;
            bullet.Position = Position;
            bullet.LinearVelocity = 0.07f;
            bullet.LifeSpan = 5f;
            bullet.Parent = this;
            
            sprites.Add(bullet);
        }

        public void OnCollide(Sprite sprite)
        {
            // Hit an enemy.  Deduct 1 health point from enemy     
            if (sprite is Bullet && ((Bullet)sprite).Parent is Player)
            {
                Health--;
                 int scoreValue;

                if (Health <= 0)
                {
                    scoreValue = EnemyManager.GetEnemyScoreValue(this.Name);
                    IsRemoved = true;
                }
                else
                    //get 1 point for every hit regardless of who it is.
                    scoreValue = 1;

                PlayerManager.SetPlayerScoreValue(sprite.Parent, scoreValue);
            }
        }

       

       
    }
}
