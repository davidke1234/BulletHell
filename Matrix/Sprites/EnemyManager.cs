using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Matrix
{
    public class EnemyManager
    {
        private float _timer;
        private List<Texture2D> _textures;
        private Texture2D _bulletRed;
        private Texture2D _bulletBlue;
        private Texture2D _bulletBlack;
        private Texture2D _bulletGreen;
        private Texture2D _bulletOrange;
        private Texture2D _enemyButterfly;
        public bool SpawnedWave1;
        public bool SpawnedWave2;
        public bool SpawnedWave3;
        public bool SpawnedWave4;

        public bool CanAdd { get; set; }

        public Bullet Bullet { get; set; }

        public int MaxEnemies { get; set; }

        public float SpawnTimer { get; set; }

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

            MaxEnemies = 10;
            SpawnTimer = 2.5f;
        }

        public void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            CanAdd = false;

            if (_timer > SpawnTimer)
            {
                CanAdd = true;

                _timer = 0;
            }
        }


        public Enemy GetEnemy(Texture2D texture, float x, float y)
        {
            var e = new Enemy(texture);

            // if (Bullet == null)
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

            //Colour = Color.Red,
            e.Bullet = Bullet;
            //Layer = 0.2f,
            e.Position = new Vector2(x, y);
            // Position = new Vector2(Game1.ScreenWidth + texture.Width, Game1.Random.Next(0, Game1.ScreenHeight)),
            e.Speed = 2 + (float)Game1.Random.NextDouble();
            e.ShootingTimer = 1.5f + (float)Game1.Random.NextDouble();

            if (e.Name == "GrumpBird")
                e.Health = 5;
            else
                e.Health = 1;

            return e;
        }
      
        public IEnumerable<SpriteNew> GetEnemy(int waveId, Enemy.Type type, GameTime gameTime, float seconds, ref bool spawned)
        {
            List<SpriteNew> enemies = new List<SpriteNew>();
            float xFactor;
            float yFactor;
            Texture2D texture;

            //Set starting x,y
            if (type == Enemy.Type.B)
            {
                xFactor = -40;
                yFactor = 135;
            }
            else
            {
                xFactor = 70;
                yFactor = -80;
            }

            if (gameTime.TotalGameTime.TotalSeconds > seconds && !spawned)
            {
                if (type == Enemy.Type.B)
                {
                    texture = _enemyButterfly;
                }
                else
                {
                    xFactor += 30;
                    yFactor += 110;
                    texture = _textures[Game1.Random.Next(0, _textures.Count)];  //Standard enemy A
                }

                enemies.Add(GetEnemy(texture, xFactor, yFactor));
                spawned = true;
            }

            return enemies;
        }
    }
}
