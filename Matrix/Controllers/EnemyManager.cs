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

            //Set starting x,y
            if (type == Enemy.Type.B)
            {
                xFactor = -40;
                yFactor = 125;
            }
            else if (type == Enemy.Type.C || type == Enemy.Type.D)
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

            if (type == Enemy.Type.D)
            {
                texture = _enemyFinalBoss;
            }
            else if (type == Enemy.Type.C)
            {
                texture = _enemyMidBoss;
            }
            else if (type == Enemy.Type.B)
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
                    enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.A, SpawnSeconds = i });
                }
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.B, SpawnSeconds = 12 });
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.A, SpawnSeconds = 15 });
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.A, SpawnSeconds = 16 });
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.A, SpawnSeconds = 17 });
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.A, SpawnSeconds = 18 });
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.B, SpawnSeconds = 20 });
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.A, SpawnSeconds = 22 });
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.A, SpawnSeconds = 23 });
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.A, SpawnSeconds = 25 });
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.A, SpawnSeconds = 26 });
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.A, SpawnSeconds = 27 });
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.A, SpawnSeconds = 28 });
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.A, SpawnSeconds = 29 });
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.B, SpawnSeconds = 32 });
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.A, SpawnSeconds = 34 });
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.A, SpawnSeconds = 35 });
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.A, SpawnSeconds = 36 });
            }

            return LoadEnemiesIntoPhase(gameTime, enemiesPhase1);
        }

        public static IEnumerable<Sprite> GetEnemyPhase2(GameTime gameTime)
        {
            if (enemiesPhase2.Count == 0)
            {
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.A, SpawnSeconds = 40 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.A, SpawnSeconds = 42 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.A, SpawnSeconds = 43 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.C, SpawnSeconds = 48 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.A, SpawnSeconds = 52 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.A, SpawnSeconds = 54 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.A, SpawnSeconds = 55 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.C, SpawnSeconds = 60 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.A, SpawnSeconds = 65 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.A, SpawnSeconds = 66 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.A, SpawnSeconds = 67 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.C, SpawnSeconds = 72 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.A, SpawnSeconds = 76 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.A, SpawnSeconds = 78 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.A, SpawnSeconds = 79 });
            }

            return LoadEnemiesIntoPhase(gameTime, enemiesPhase2);
        }    

        public static IEnumerable<Sprite> GetEnemyPhase3(GameTime gameTime)
        {
            if (enemiesPhase3.Count == 0)
            {
                enemiesPhase3.Add(new Spawner() { EnemyType = Enemy.Type.D, SpawnSeconds = 81 });
                enemiesPhase3.Add(new Spawner() { EnemyType = Enemy.Type.A, SpawnSeconds = 85 });
                enemiesPhase3.Add(new Spawner() { EnemyType = Enemy.Type.A, SpawnSeconds = 86 });
                enemiesPhase3.Add(new Spawner() { EnemyType = Enemy.Type.A, SpawnSeconds = 90 });
                enemiesPhase3.Add(new Spawner() { EnemyType = Enemy.Type.A, SpawnSeconds = 94 });
                enemiesPhase3.Add(new Spawner() { EnemyType = Enemy.Type.A, SpawnSeconds = 97 });
                enemiesPhase3.Add(new Spawner() { EnemyType = Enemy.Type.A, SpawnSeconds = 101 });
                enemiesPhase3.Add(new Spawner() { EnemyType = Enemy.Type.D, SpawnSeconds = 106 });
                enemiesPhase3.Add(new Spawner() { EnemyType = Enemy.Type.A, SpawnSeconds = 109 });
                enemiesPhase3.Add(new Spawner() { EnemyType = Enemy.Type.A, SpawnSeconds = 115});
                enemiesPhase3.Add(new Spawner() { EnemyType = Enemy.Type.A, SpawnSeconds = 116});
            }

            return LoadEnemiesIntoPhase(gameTime, enemiesPhase3);
        }

        public static IEnumerable<Sprite> GetEnemyPhase4(GameTime gameTime)
        {
            if (enemiesPhase4.Count == 0)
            {
                enemiesPhase4.Add(new Spawner() { EnemyType = Enemy.Type.D, SpawnSeconds = 121 });
                enemiesPhase4.Add(new Spawner() { EnemyType = Enemy.Type.D, SpawnSeconds = 131});
                enemiesPhase4.Add(new Spawner() { EnemyType = Enemy.Type.D, SpawnSeconds = 141});
                enemiesPhase4.Add(new Spawner() { EnemyType = Enemy.Type.D, SpawnSeconds = 151});
                enemiesPhase4.Add(new Spawner() { EnemyType = Enemy.Type.D, SpawnSeconds = 161});
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

        //_sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 1, ref spE1));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 2, ref spE2));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 3, ref spE3));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 4, ref spE4));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 5, ref spE5));
        //    //_sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 6, ref spE6));
        //    //_sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 7, ref spE7));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 8, ref spE8));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 9, ref spE9));
        //    //_sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 12, ref spE12));
        //    //_sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 13, ref spE13));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.B, gameTime, 14, ref spE14));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 15, ref spE15));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 16, ref spE16));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.B, gameTime, 17, ref spE17));
        //    //_sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 18, ref spE18));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 19, ref spE19));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 20, ref spE20));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 21, ref spE21));
        //    //_sprites.AddRange(GetEnemy(Enemy.Type.B, gameTime, 24, ref spE22));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 25, ref spE23));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 26, ref spE24));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.B, gameTime, 27, ref spE25));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 28, ref spE26));
        //    //_sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 29, ref spE27));
        //   // _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 30, ref spE28));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 33, ref spE29));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 34, ref spE30));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.B, gameTime, 35, ref spE31));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 37, ref spE32));

        //static internal IEnumerable<Sprite> GetEnemyPhase2Old(GameTime gameTime)
        //{
        //    List<Sprite> _sprites = new List<Sprite>();

        //    _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 40, ref spE32));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 42, ref spE33));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 43, ref spE34));
        //    //_sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 44, ref spE35));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.C, gameTime, 48, ref spE36));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 52, ref spE37));
        //    //_sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 53, ref spE38));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 54, ref spE39));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 55, ref spE38));
        //    //_sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 56, ref spE39));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.C, gameTime, 60, ref spE40));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 65, ref spE41));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 66, ref spE42));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 67, ref spE43));
        //   // _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 68, ref spE44));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.C, gameTime, 72, ref spE45));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 76, ref spE46));
        //    //_sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 77, ref spE47));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 78, ref spE48));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 79, ref spE49));

        //    return _sprites;
        //}

        //static internal IEnumerable<Sprite> GetEnemyPhase3Old(GameTime gameTime)
        //{
        //    List<Sprite> _sprites = new List<Sprite>();

        //    _sprites.AddRange(GetEnemy(Enemy.Type.D, gameTime, 81, ref spE50));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 85, ref spE51));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 86, ref spE52));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.D, gameTime, 90, ref spE53));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 94, ref spE54));
        //    //_sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 95, ref spE56));
        //    //_sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 96, ref spE57));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 97, ref spE58));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.D, gameTime, 101, ref spE60));
        //    //_sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 105, ref spE61));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 106, ref spE62));
        //    //_sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 107, ref spE63));
        //    //_sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 108, ref spE64));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 109, ref spE65));
        //    //_sprites.AddRange(GetEnemy(Enemy.Type.D, gameTime, 112, ref spE66));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 115, ref spE67));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 116, ref spE68));
        //    //_sprites.AddRange(GetEnemy(Enemy.Type.D, gameTime, 118, ref spE72));

        //    return _sprites;
        //}
        //static internal IEnumerable<Sprite> GetEnemyPhase4Old(GameTime gameTime)
        //{
        //    List<Sprite> _sprites = new List<Sprite>();

        //    _sprites.AddRange(GetEnemy(Enemy.Type.D, gameTime, 121, ref spE75));
        //    //_sprites.AddRange(GetEnemy(Enemy.Type.D, gameTime, 126, ref spE76));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.D, gameTime, 131, ref spE77));
        //   // _sprites.AddRange(GetEnemy(Enemy.Type.D, gameTime, 136, ref spE78));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.D, gameTime, 141, ref spE79));
        //    //_sprites.AddRange(GetEnemy(Enemy.Type.D, gameTime, 146, ref spE80));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.D, gameTime, 151, ref spE81));
        //    //_sprites.AddRange(GetEnemy(Enemy.Type.D, gameTime, 156, ref spE82));
        //    _sprites.AddRange(GetEnemy(Enemy.Type.D, gameTime, 161, ref spE83));


        //    return _sprites;
        //}
        #endregion
    }
     class Spawner
    {
        public Enemy.Type EnemyType;
        public float SpawnSeconds;
        //public bool spawned;
    }
}
