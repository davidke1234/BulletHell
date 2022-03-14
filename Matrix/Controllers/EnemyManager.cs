using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Matrix
{
    static class EnemyManager
    {
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

        #region - private bools for enemy spawning
        static bool spE1, spE2, spE3, spE4, spE5, spE6, spE7, spE8, spE9, spE10;
        static bool spE11, spE12, spE13, spE14, spE15, spE16, spE17, spE18, spE19, spE20;
        static bool spE21, spE22, spE23, spE24, spE25, spE26, spE27, spE28, spE29, spE30;
        static bool spE31, spE32, spE33, spE34, spE35, spE36, spE37, spE38, spE39, spE40;
        static bool spE41, spE42, spE43, spE44, spE45, spE46, spE47, spE48, spE49, spE50;
        static bool spE51, spE52, spE53, spE54, spE55, spE56, spE57, spE58, spE59, spE60;
        static bool spE61, spE62, spE63, spE64, spE65, spE66, spE67, spE68, spE69, spE70;
        static bool spE71, spE72, spE73, spE74, spE75, spE76, spE77, spE78, spE79, spE80;
        static bool spE81, spE82, spE83, spE84, spE85;
        #endregion

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

            {
                if (texture.Name.Contains("blood"))
                    Bullet = new Bullet(_bulletRed);
                else if (texture.Name.Contains("blue"))
                    Bullet = new Bullet(_bulletBlue);
                else if (texture.Name.Contains("black"))
                    Bullet = new Bullet(_bulletBlack);
                else if (texture.Name.Contains("green"))
                    Bullet = new Bullet(_bulletGreen);
                else if (texture.Name.Contains("GrumpBird"))
                    Bullet = new Bullet(_bulletOrange);
                else if (texture.Name.Contains("Boss2"))
                    Bullet = new Bullet(_BulletBomb);
                else if (texture.Name.Contains("Boss"))
                    Bullet = new Bullet(_BulletBomb2);
                else
                    Bullet = new Bullet(_bulletBlack);
            }

            e.Bullet = Bullet;
            e.Position = new Vector2(x, y);
            e.Speed = 2 + (float)_random.NextDouble();
            e.TimerStart = 1.5f + (float)_random.NextDouble();

            if (e.Name == "Boss") //finalboss
                e.Health = 15;
            else if (e.Name == "Boss2") //midboss
                e.Health = 10;
            else if (e.Name == "GrumpBird")
                e.Health = 5;
            else
                e.Health = 1;

            return e;
        }

        public static IEnumerable<Sprite> GetEnemy(Enemy.Type type, GameTime gameTime, float seconds, ref bool spawned)
        {
            //Note this currently returns 1 enemy

            List<Sprite> enemies = new List<Sprite>();
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

            if (gameTime.TotalGameTime.TotalSeconds > seconds && !spawned)
            {
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

                enemies.Add(GetEnemy(texture, xFactor, yFactor));
                spawned = true;
            }

            return enemies;
        }

    #region Phases of game - spawing enemies
        public static IEnumerable<Sprite> GetEnemyPhase1(GameTime gameTime)
        {
            List<Sprite> _sprites = new List<Sprite>();

            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 1, ref spE1));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 2, ref spE2));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 3, ref spE3));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 4, ref spE4));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 5, ref spE5));
            //_sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 6, ref spE6));
            //_sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 7, ref spE7));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 8, ref spE8));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 9, ref spE9));
            //_sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 12, ref spE12));
            //_sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 13, ref spE13));
            _sprites.AddRange(GetEnemy(Enemy.Type.B, gameTime, 14, ref spE14));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 15, ref spE15));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 16, ref spE16));
            _sprites.AddRange(GetEnemy(Enemy.Type.B, gameTime, 17, ref spE17));
            //_sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 18, ref spE18));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 19, ref spE19));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 20, ref spE20));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 21, ref spE21));
            //_sprites.AddRange(GetEnemy(Enemy.Type.B, gameTime, 24, ref spE22));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 25, ref spE23));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 26, ref spE24));
            _sprites.AddRange(GetEnemy(Enemy.Type.B, gameTime, 27, ref spE25));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 28, ref spE26));
            //_sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 29, ref spE27));
           // _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 30, ref spE28));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 33, ref spE29));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 34, ref spE30));
            _sprites.AddRange(GetEnemy(Enemy.Type.B, gameTime, 35, ref spE31));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 37, ref spE32));


            return _sprites;
        }

        static internal IEnumerable<Sprite> GetEnemyPhase2(GameTime gameTime)
        {
            List<Sprite> _sprites = new List<Sprite>();

            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 40, ref spE32));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 42, ref spE33));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 43, ref spE34));
            //_sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 44, ref spE35));
            _sprites.AddRange(GetEnemy(Enemy.Type.C, gameTime, 48, ref spE36));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 52, ref spE37));
            //_sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 53, ref spE38));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 54, ref spE39));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 55, ref spE38));
            //_sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 56, ref spE39));
            _sprites.AddRange(GetEnemy(Enemy.Type.C, gameTime, 60, ref spE40));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 65, ref spE41));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 66, ref spE42));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 67, ref spE43));
           // _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 68, ref spE44));
            _sprites.AddRange(GetEnemy(Enemy.Type.C, gameTime, 72, ref spE45));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 76, ref spE46));
            //_sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 77, ref spE47));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 78, ref spE48));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 79, ref spE49));

            return _sprites;
        }

        static internal IEnumerable<Sprite> GetEnemyPhase3(GameTime gameTime)
        {
            List<Sprite> _sprites = new List<Sprite>();

            _sprites.AddRange(GetEnemy(Enemy.Type.D, gameTime, 81, ref spE50));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 85, ref spE51));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 86, ref spE52));
            _sprites.AddRange(GetEnemy(Enemy.Type.D, gameTime, 90, ref spE53));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 94, ref spE54));
            //_sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 95, ref spE56));
            //_sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 96, ref spE57));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 97, ref spE58));
            _sprites.AddRange(GetEnemy(Enemy.Type.D, gameTime, 101, ref spE60));
            //_sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 105, ref spE61));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 106, ref spE62));
            //_sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 107, ref spE63));
            //_sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 108, ref spE64));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 109, ref spE65));
            //_sprites.AddRange(GetEnemy(Enemy.Type.D, gameTime, 112, ref spE66));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 115, ref spE67));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 116, ref spE68));
            //_sprites.AddRange(GetEnemy(Enemy.Type.D, gameTime, 118, ref spE72));

            return _sprites;
        }
        static internal IEnumerable<Sprite> GetEnemyPhase4(GameTime gameTime)
        {
            List<Sprite> _sprites = new List<Sprite>();

            _sprites.AddRange(GetEnemy(Enemy.Type.D, gameTime, 121, ref spE75));
            //_sprites.AddRange(GetEnemy(Enemy.Type.D, gameTime, 126, ref spE76));
            _sprites.AddRange(GetEnemy(Enemy.Type.D, gameTime, 131, ref spE77));
           // _sprites.AddRange(GetEnemy(Enemy.Type.D, gameTime, 136, ref spE78));
            _sprites.AddRange(GetEnemy(Enemy.Type.D, gameTime, 141, ref spE79));
            //_sprites.AddRange(GetEnemy(Enemy.Type.D, gameTime, 146, ref spE80));
            _sprites.AddRange(GetEnemy(Enemy.Type.D, gameTime, 151, ref spE81));
            //_sprites.AddRange(GetEnemy(Enemy.Type.D, gameTime, 156, ref spE82));
            _sprites.AddRange(GetEnemy(Enemy.Type.D, gameTime, 161, ref spE83));


            return _sprites;
        }
        #endregion
    }
}
