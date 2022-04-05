﻿using Matrix.Models.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Matrix.Models
{
    public class Enemy : Sprite, ICollidable
    {
        public Bullet Bullet;
        public Bomb Bomb;
        private float _shootingTimer;
        public float TimerStart = 1.25f;
        public float Speed = 2f;
        private static ProjectileFactory _projectileFactory = new ProjectileFactory();
        private double _lastShotSecond = 0;

        public enum Type { 
            BasicEnemies, //small grunts
            ButterFlyEnemies, //larger
            MidBoss, //midboss
            FinalBoss  //finalboss
        }
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
                //if (name == "finalboss") //finalboss
                //{
                //    if (ShouldShoot(gameTime, 2))
                //    {
                //        DropBomb(sprites, new Vector2(15, 15), "bomb2", Enemy.Type.FinalBoss);
                //        DropBomb(sprites, new Vector2(-20, -20), "bomb2", Enemy.Type.FinalBoss);
                //        DropBomb(sprites, new Vector2(0, 0), "bomb2", Enemy.Type.FinalBoss);
                //    }
                //}
                //else if (name == "midboss")  //midboss
                //{
                //    if (ShouldShoot(gameTime, 2))
                //    {
                //        DropBomb(sprites, new Vector2(5, 5), "bomb", Enemy.Type.Boss);
                //        DropBomb(sprites, new Vector2(0, 0), "bomb", Enemy.Type.Boss);
                //    }
                //}
                //else
                {
                    if (ShouldShoot(gameTime, 2))
                    {
                        DropBullet(sprites, new Vector2(-1, -1));
                    }
                }

                _shootingTimer = 0;
            }

            Direction.X = 10f;
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
            // Hit an enemy.  Deduct 1 health point     
            if (sprite is Bullet && ((Bullet)sprite).Parent is Player)
            {
                Health--;

                if (Health <= 0)
                {
                    int scoreValue;

                    switch(Name)
                    {
                        case "finalboss":
                            scoreValue = 15;
                            break;
                        case "midboss":
                            scoreValue = 10;
                            break;
                        case "grumpbird":
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
