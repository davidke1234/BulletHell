using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Matrix
{
    public class EnemyManager
    {
        private List<Texture2D> _textures;
        private Texture2D _bulletRed;
        private Texture2D _bulletBlue;
        private Texture2D _bulletBlack;
        private Texture2D _bulletGreen;
        private Texture2D _bulletOrange;
        private Texture2D _BulletBomb;
        private Texture2D _BulletBomb2;
        private Texture2D _enemyButterfly;
        private Texture2D _enemyMidBoss;
        private Texture2D _enemyFinalBoss;
        public Random _random = new Random();
        private bool spE1, spE2, spE3, spE4, spE5, spE6, spE7, spE8, spE9, spE10;
        private bool spE11, spE12, spE13, spE14, spE15, spE16, spE17, spE18, spE19, spE20;
        private bool spE21, spE22, spE23, spE24, spE25, spE26, spE27, spE28, spE29, spE30;
        private bool spE31, spE32, spE33, spE34, spE35, spE36, spE37, spE38, spE39, spE40;
        private bool spE41, spE42, spE43, spE44, spE45, spE46, spE47, spE48, spE49, spE50;

        public Bullet Bullet { get; set; }

        public EnemyManager(ContentManager content)
        {
            _textures = new List<Texture2D>()
            {
               content.Load<Texture2D>("dngn_blood_fountain"),
               content.Load<Texture2D>("dngn_blue_fountain"),
               content.Load<Texture2D>("dngn_green_fountain"),
               content.Load<Texture2D>("dngn_black_fountain")
            };

             _enemyButterfly = content.Load<Texture2D>("GrumpBird");
            _enemyMidBoss = content.Load<Texture2D>("Boss2");
            _enemyFinalBoss = content.Load<Texture2D>("Boss");

            _bulletRed = content.Load<Texture2D>("BulletRed");
            _bulletBlue = content.Load<Texture2D>("BulletBlue");
            _bulletBlack = content.Load<Texture2D>("BulletBlack");
            _bulletGreen = content.Load<Texture2D>("BulletGreen");
            _bulletOrange = content.Load<Texture2D>("BulletOrange");
            _BulletBomb = content.Load<Texture2D>("Bomb");
            _BulletBomb2 = content.Load<Texture2D>("Bomb2");
        }

        public Enemy GetEnemy(Texture2D texture, float x, float y)
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
      
        public IEnumerable<Sprite> GetEnemy(Enemy.Type type, GameTime gameTime, float seconds, ref bool spawned)
        {
            //Note this currently returns 1 enemy

            List<Sprite> enemies = new List<Sprite>();
            float xFactor;
            float yFactor;

            //Set starting x,y
            if (type == Enemy.Type.B || type==Enemy.Type.C || type == Enemy.Type.D)
            {
                xFactor = -40;
                yFactor = 135;
            }
            else
            {
                //Type A
                xFactor = 70;
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
                    yFactor += 110;
                    texture = _textures[_random.Next(0, _textures.Count)];  
                }

                enemies.Add(GetEnemy(texture, xFactor, yFactor));
                spawned = true;
            }

            return enemies;
        }

        public IEnumerable<Sprite> GetEnemyWave1(GameTime gameTime)
        {
            List<Sprite> _sprites = new List<Sprite>();

            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 1, ref spE1));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 2, ref spE2));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 3, ref spE3));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 4, ref spE4));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 5, ref spE5));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 6, ref spE6));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 7, ref spE7));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 8, ref spE8));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 9, ref spE9));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 10, ref spE10));

            return _sprites;
        }

        public IEnumerable<Sprite> GetEnemyWave2(GameTime gameTime)
        {
            List<Sprite> _sprites = new List<Sprite>();

            _sprites.AddRange(GetEnemy(Enemy.Type.B, gameTime, 13, ref spE11));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 13, ref spE12));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 14, ref spE13));
            _sprites.AddRange(GetEnemy(Enemy.Type.B, gameTime, 15, ref spE14));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 16, ref spE15));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 17, ref spE16));
            _sprites.AddRange(GetEnemy(Enemy.Type.B, gameTime, 18, ref spE18));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 19, ref spE19));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 19, ref spE20));

            return _sprites;
        }

        public IEnumerable<Sprite> GetEnemyWave3(GameTime gameTime)
        {
            List<Sprite> _sprites = new List<Sprite>();

            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 22, ref spE24));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 23, ref spE25));
            _sprites.AddRange(GetEnemy(Enemy.Type.B, gameTime, 24, ref spE26));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 25, ref spE27));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 26, ref spE28));
            _sprites.AddRange(GetEnemy(Enemy.Type.B, gameTime, 26, ref spE29));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 26, ref spE30));

            return _sprites;
        }

        internal IEnumerable<Sprite> GetEnemyWave4(GameTime gameTime)
        {
            List<Sprite> _sprites = new List<Sprite>();

            _sprites.AddRange(GetEnemy(Enemy.Type.C, gameTime, 40, ref spE31));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 45, ref spE32));
            _sprites.AddRange(GetEnemy(Enemy.Type.C, gameTime, 50, ref spE33));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 55, ref spE34));
            _sprites.AddRange(GetEnemy(Enemy.Type.C, gameTime, 60, ref spE35));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 65, ref spE36));
            _sprites.AddRange(GetEnemy(Enemy.Type.C, gameTime, 70, ref spE37));

            return _sprites;
        }

        internal IEnumerable<Sprite> GetEnemyWave5(GameTime gameTime)
        {
            List<Sprite> _sprites = new List<Sprite>();

            _sprites.AddRange(GetEnemy(Enemy.Type.D, gameTime, 80, ref spE40));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 85, ref spE41));
            _sprites.AddRange(GetEnemy(Enemy.Type.D, gameTime, 90, ref spE42));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 95, ref spE43));
            _sprites.AddRange(GetEnemy(Enemy.Type.D, gameTime, 100, ref spE44));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 110, ref spE45));
            _sprites.AddRange(GetEnemy(Enemy.Type.D, gameTime, 120, ref spE46));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 125, ref spE47));
            _sprites.AddRange(GetEnemy(Enemy.Type.D, gameTime, 130, ref spE48));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 135, ref spE49));
            _sprites.AddRange(GetEnemy(Enemy.Type.A, gameTime, 136, ref spE50));


            return _sprites;
        }
    }
}
