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

            return new Enemy(texture)
            {
                //Colour = Color.Red,
                Bullet = new Bullet(_bulletTexture),
                //Health = 5,
                //Layer = 0.2f,
                Position = new Vector2(x, y),    //Game1.ScreenWidth + texture.Width, Game1.Random.Next(0, Game1.ScreenHeight)),
                Speed = 2 + (float)Game1.Random.NextDouble(),
                ShootingTimer = 1.5f + (float)Game1.Random.NextDouble(),
            };
        }


        public IEnumerable<SpriteNew> GetEnemies()
        {
            return new List<SpriteNew>()
            {
                GetEnemy(100, 50),
                GetEnemy(150, 50),
                GetEnemy(200, 50),
                GetEnemy(250, 50)
            };
        }
    }
}
