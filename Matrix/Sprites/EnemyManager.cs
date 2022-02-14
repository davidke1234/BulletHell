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

            _enemyButterfly = content.Load<Texture2D>("butterfly");

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
            //Health = 5,
            //Layer = 0.2f,
            e.Position = new Vector2(x, y);
            // Position = new Vector2(Game1.ScreenWidth + texture.Width, Game1.Random.Next(0, Game1.ScreenHeight)),
            e.Speed = 2 + (float)Game1.Random.NextDouble();
            e.ShootingTimer = 1.5f + (float)Game1.Random.NextDouble();

            return e;
        }

        public IEnumerable<SpriteNew> GetEnemies(Enemy.Type type, GameTime gameTime, int seconds, ref bool spawned, int enemyCount)
        {
            List<SpriteNew> enemies = new List<SpriteNew>();
            Texture2D texture;
            int xFactor = 50;
            int yFactor;


            if (gameTime.TotalGameTime.TotalSeconds > seconds && !spawned)
            {
                if (!spawned)
                {
                    for (int i = 0; i < enemyCount; i++)
                    {
                        if (type == Enemy.Type.B)
                        {
                            yFactor = 100;
                            texture = _enemyButterfly;
                        }
                        else
                        {
                            yFactor = 50 + i;
                            texture = _textures[Game1.Random.Next(0, _textures.Count)];  //Standard enemy A                         
                        }

                        enemies.Add(GetEnemy(texture, xFactor, yFactor));
                        xFactor += 50;
                    }

                    spawned = true;
                }
            }

            return enemies;
        }
    }
}
