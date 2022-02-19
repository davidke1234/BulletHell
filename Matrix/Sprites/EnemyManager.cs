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
        private Texture2D _enemyButterfly;
        public Random _random = new Random();
        private bool spE1, spE2, spE3, spE4, spE5, spE6, spE7, spE8, spE9, spE10;
        private bool spE11, spE12, spE13, spE14, spE15, spE16, spE17, spE18, spE19, spE20;
        private bool spE21, spE22, spE23, spE24, spE25, spE26, spE27, spE28, spE29, spE30;

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
            
            _bulletRed = content.Load<Texture2D>("BulletRed");
            _bulletBlue = content.Load<Texture2D>("BulletBlue");
            _bulletBlack = content.Load<Texture2D>("BulletBlack");
            _bulletGreen = content.Load<Texture2D>("BulletGreen");
            _bulletOrange = content.Load<Texture2D>("BulletOrange");
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
                else
                    Bullet = new Bullet(_bulletOrange);
            }

            e.Bullet = Bullet;
            e.Position = new Vector2(x, y);
            e.Speed = 2 + (float)_random.NextDouble();
            e.TimerStart = 1.5f + (float)_random.NextDouble();

            if (e.Name == "GrumpBird")
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
            if (type == Enemy.Type.B)
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

                if (type == Enemy.Type.B)
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
    }
}
