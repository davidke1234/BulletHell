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
        private Texture2D _bulletTexture;
        private Texture2D enemyButterfly;

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
               content.Load<Texture2D>("dngn_dry_fountain")
            };

            _bulletTexture = content.Load<Texture2D>("Bullet");

            enemyButterfly = content.Load<Texture2D>("gif-preview");

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

            if (Bullet == null)
                Bullet = new Bullet(_bulletTexture);

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
                            texture = enemyButterfly;
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
