using Matrix.Models;
using Matrix.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using NLog;
using System;
using System.Collections.Generic;

namespace Matrix
{
    static class EnemyManager
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        static List<Texture2D> _textures;
        static Texture2D _bulletRed;
        static Texture2D _bulletBlue;
        static Texture2D _bulletBlack;
        static Texture2D _bulletGreen;
        static Texture2D _bulletOrange;
        static Texture2D _BulletBomb;
        static Texture2D _BulletBomb2;
        static Texture2D _enemyButterfly;
        static Texture2D _enemyMidBoss;
        static Texture2D _enemyFinalBoss;
        static Random _random = new Random();
        static List<Spawner> enemiesPhase1 = new List<Spawner>();
        static List<Spawner> enemiesPhase2 = new List<Spawner>();
        static List<Spawner> enemiesPhase3 = new List<Spawner>();
        static List<Spawner> enemiesPhase4 = new List<Spawner>();
        static EnemyFactory enemyFactory = new EnemyFactory();

        public static Bullet Bullet { get; set; }

        static EnemyManager()
        {
            _textures = new List<Texture2D>()
            {
                Arts.EnemyBlack,
                Arts.EnemyBlood,
                Arts.EnemyBlue,
                Arts.EnemyGreen,
            };

            _enemyButterfly = Arts.EnemyButterfly;
            _enemyMidBoss = Arts.Boss2;
            _enemyFinalBoss = Arts.Boss;
            _bulletRed = Arts.BulletRed;
            _bulletBlue = Arts.BulletBlue;
            _bulletBlack = Arts.BulletBlack;
            _bulletGreen = Arts.BulletGreen;
            _bulletOrange = Arts.BulletOrange;
            _BulletBomb = Arts.Bomb;
            _BulletBomb2 = Arts.Bomb2;
        }

        public static Enemy GetEnemy(Texture2D texture, float x, float y)
        {
            var e = new Enemy(texture);
            string name = texture.Name.ToLower();

            {
                if (name.Contains("blood"))
                    Bullet = new Bullet(_bulletRed);
                else if (name.Contains("blue"))
                    Bullet = new Bullet(_bulletBlue);
                else if (name.Contains("black"))
                    Bullet = new Bullet(_bulletBlack);
                else if (name.Contains("green"))
                    Bullet = new Bullet(_bulletGreen);
                else if (name.Contains("grumpbird"))
                    Bullet = new Bullet(_bulletOrange);
                else if (name.Contains("boss2"))
                    Bullet = new Bullet(_BulletBomb);
                else if (name.Contains("boss"))
                    Bullet = new Bullet(_BulletBomb2);
                else
                    Bullet = new Bullet(_bulletBlack);
            }

            e.Bullet = Bullet;
            e.Position = new Vector2(x, y);
            e.Speed = 2 + (float)_random.NextDouble();
            e.TimerStart = 1.5f + (float)_random.NextDouble();
            e.LifeSpan = 5;

            if (name == "boss")
            {//finalboss
                e.LifeSpan = 5;
                e.Health = 15;
            }
            else if (name == "boss2")
            { //midboss
                e.LifeSpan = 5;
                e.Health = 10;
            }
            else if (name == "grumpbird")
                e.Health = 5;
            else
                e.Health = 1;

            _logger.Info("Build enemy: " + e.Name);

            return e;
        }



        public static Enemy GetEnemy(Enemy.Type type)
        {
            float xFactor;
            float yFactor;

            //if (type == Enemy.Type.Boss)
            //{
            //    return MidBoss.GetInstance;
            //}

            //Set initial starting x,y
            if (type == Enemy.Type.ButterFlyEnemies)
            {
                xFactor = -40;
                yFactor = 125;
            }
            else if (type == Enemy.Type.Boss || type == Enemy.Type.FinalBoss)
            {
                xFactor = -40;
                yFactor = 80;
            }
            else
            {
                //Type A
                xFactor = 40;
                yFactor = -80;
            }

            Texture2D texture;

            if (type == Enemy.Type.FinalBoss)
            {
                texture = _enemyFinalBoss;
            }
            else if (type == Enemy.Type.Boss)
            {
                texture = _enemyMidBoss;
            }
            else if (type == Enemy.Type.ButterFlyEnemies)
            {
                texture = _enemyButterfly;
            }
            else
            {
                //Type A
                xFactor += 30;
                yFactor += 90;
                texture = _textures[_random.Next(0, _textures.Count)];
            }

            return GetEnemy(texture, xFactor, yFactor);
        }

        #region Phases of game - spawing enemies
        public static IEnumerable<Sprite> GetEnemyPhase1(GameTime gameTime)
        {
            if (enemiesPhase1.Count == 0)
            {
                for (int i = 1; i < 10; i++)
                {
                    enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = i });
                }
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.ButterFlyEnemies, SpawnSeconds = 12 });
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 15 });
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 16 });
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 17 });
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 18 });
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.ButterFlyEnemies, SpawnSeconds = 20 });
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 22 });
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 23 });
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 25 });
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 26 });
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 27 });
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 28 });
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 29 });
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.ButterFlyEnemies, SpawnSeconds = 32 });
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 34 });
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 35 });
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 36 });
            }

            return LoadEnemiesIntoPhase(gameTime, enemiesPhase1);
        }

        public static IEnumerable<Sprite> GetEnemyPhase2(GameTime gameTime)
        {
            if (enemiesPhase2.Count == 0)
            {
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 40 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 42 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 43 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.Boss, SpawnSeconds = 48 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 52 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 54 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 55 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.Boss, SpawnSeconds = 60 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 65 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 66 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 67 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.Boss, SpawnSeconds = 72 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 76 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 78 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 79 });
            }

            return LoadEnemiesIntoPhase(gameTime, enemiesPhase2);
        }    

        public static IEnumerable<Sprite> GetEnemyPhase3(GameTime gameTime)
        {
            if (enemiesPhase3.Count == 0)
            {
                enemiesPhase3.Add(new Spawner() { EnemyType = Enemy.Type.FinalBoss, SpawnSeconds = 81 });
                enemiesPhase3.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 85 });
                enemiesPhase3.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 86 });
                enemiesPhase3.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 90 });
                enemiesPhase3.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 94 });
                enemiesPhase3.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 97 });
                enemiesPhase3.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 101 });
                enemiesPhase3.Add(new Spawner() { EnemyType = Enemy.Type.FinalBoss, SpawnSeconds = 106 });
                enemiesPhase3.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 109 });
                enemiesPhase3.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 115});
                enemiesPhase3.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 116});
            }

            return LoadEnemiesIntoPhase(gameTime, enemiesPhase3);
        }

        public static IEnumerable<Sprite> GetEnemyPhase4(GameTime gameTime)
        {
            if (enemiesPhase4.Count == 0)
            {
                enemiesPhase4.Add(new Spawner() { EnemyType = Enemy.Type.FinalBoss, SpawnSeconds = 121 });
                enemiesPhase4.Add(new Spawner() { EnemyType = Enemy.Type.FinalBoss, SpawnSeconds = 131});
                enemiesPhase4.Add(new Spawner() { EnemyType = Enemy.Type.FinalBoss, SpawnSeconds = 141});
                enemiesPhase4.Add(new Spawner() { EnemyType = Enemy.Type.FinalBoss, SpawnSeconds = 151});
                enemiesPhase4.Add(new Spawner() { EnemyType = Enemy.Type.FinalBoss, SpawnSeconds = 161});
            }

            return LoadEnemiesIntoPhase(gameTime, enemiesPhase4);
        }

        private static IEnumerable<Sprite> LoadEnemiesIntoPhase(GameTime gameTime, List<Spawner> spawnedEnemies)
        {
            List<Sprite> enemies = new List<Sprite>();

            //Load enemies each second
            double gameSecond = 0;
            double returnValue = Math.Round(gameTime.TotalGameTime.TotalSeconds, 2) % 1;

            if (returnValue == 0)
            {
                gameSecond = Math.Round(gameTime.TotalGameTime.TotalSeconds, 0);

                //Check if a certain enemy should be sent to view based on time
                Spawner spawner = spawnedEnemies.Find(e => e.SpawnSeconds == gameSecond);
                if (spawner != null)
                    enemies.Add(GetEnemy(spawner.EnemyType));
            }

            return enemies;
        }

        #endregion
    }

}
