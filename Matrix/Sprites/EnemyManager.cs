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
               content.Load<Texture2D>("dngn_dry_fountain")
            };

            _bulletTexture = content.Load<Texture2D>("Bullet");

            //var enemyButterfly = Content.Load<Texture2D>("elephant_statue");

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

        public Enemy GetEnemy(float x, float y)
        {
            var texture = _textures[Game1.Random.Next(0, _textures.Count)];

            var e = new Enemy(texture);


            //Colour = Color.Red,
            e.Bullet = new Bullet(_bulletTexture);
            //Health = 5,
            //Layer = 0.2f,
            e.Position = new Vector2(x, y);
            // Position = new Vector2(Game1.ScreenWidth + texture.Width, Game1.Random.Next(0, Game1.ScreenHeight)),
            e.Speed = 2 + (float)Game1.Random.NextDouble();
            e.ShootingTimer = 1.5f + (float)Game1.Random.NextDouble();

            return e;
        }

        public IEnumerable<SpriteNew> GetEnemies(GameTime gameTime, int seconds, ref bool spawned, int enemyCount)
        {
            List<SpriteNew> enemies = new List<SpriteNew>();

            if (gameTime.TotalGameTime.TotalSeconds > seconds && !spawned)
            {
                if (!spawned)
                {
                    int xFactor = 50;

                    for (int i = 0; i < enemyCount; i++)
                    {
                        enemies.Add(GetEnemy(xFactor, 50 + i));
                        xFactor += 50;
                    }

                    spawned = true;
                }
            }

            return enemies;
        }
    }
}
