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

        //public IEnumerable<SpriteNew> Getall(int waveId, Enemy.Type type, GameTime gameTime, float seconds, ref bool spawned, int enemyCount)
        // {
        // List<SpriteNew> enemies = new List<SpriteNew>();

        //TODO: won't work since need to adjust other props that Update changes

        //enemies.AddRange(_enemyManager.GetWave(1, Enemy.Type.A, gameTime, 0, ref spawnedEnemies1, 1));
        //enemies.AddRange(_enemyManager.GetWave(1, Enemy.Type.A, gameTime, 1, ref spawnedEnemies2, 1));
        //enemies.AddRange(_enemyManager.GetWave(1, Enemy.Type.A, gameTime, 2, ref spawnedEnemies3, 1));
        //enemies.AddRange(_enemyManager.GetWave(1, Enemy.Type.A, gameTime, 3, ref spawnedEnemies4, 1));
        //enemies.AddRange(_enemyManager.GetWave(1, Enemy.Type.A, gameTime, 4, ref spawnedEnemies5, 1));
        //enemies.AddRange(_enemyManager.GetWave(1, Enemy.Type.A, gameTime, 5, ref spawnedEnemies6, 1));

        // return enemies;
        // }
        public IEnumerable<SpriteNew> GetEnemy(int waveId, Enemy.Type type, GameTime gameTime, float seconds, ref bool spawned)
        {
            List<SpriteNew> enemies = new List<SpriteNew>();
            float xFactor;
            float yFactor;
            Texture2D texture;
            //bool spawned = false;

            //Set starting x,y
            if (type == Enemy.Type.B)
            {
                xFactor = -20;
                yFactor = 150;
            }
            else
            {
                xFactor = 50;
                yFactor = -80;
            }

            //if (waveId == 1 && SpawnedWave1)
            //    spawned = true;
            //else if (waveId == 2 && SpawnedWave2)
            //    spawned = true;
            //else if (waveId == 3 && SpawnedWave3)
            //    spawned = true;
            //else if (waveId == 4 && SpawnedWave4)
            //    spawned = true;


            if (gameTime.TotalGameTime.TotalSeconds > seconds && !spawned)
            {
                //    for (int i = 0; i < enemyCount; i++)
                //    {
                if (type == Enemy.Type.B)
                {
                    texture = _enemyButterfly;
                }
                else
                {
                    xFactor += 30;
                    yFactor += 70;
                    texture = _textures[Game1.Random.Next(0, _textures.Count)];  //Standard enemy A
                }
                //
                enemies.Add(GetEnemy(texture, xFactor, yFactor));

                spawned = true;
                // }

                //if (waveId == 1)
                //    SpawnedWave1 = true;
                //else if (waveId == 2)
                //    SpawnedWave2 = true;
                //else if (waveId == 3)
                //    SpawnedWave3 = true;
                //else if (waveId == 4)
                //    SpawnedWave4 = true;
            }

            return enemies;
        }
    }
}

//        public Enemy GetEnemy(Enemy.Type type, GameTime gameTime, float seconds, float xFactor, float yFactor)
//        {
//            //List<SpriteNew> enemies = new List<SpriteNew>();
//            Enemy enemy;
//            Texture2D texture;
   

//            //if (gameTime.TotalGameTime.TotalSeconds > seconds && !spawned)
//            //{
//              //  if (!spawned)
//                //{
//                    //for (int i = 0; i < enemyCount; i++)
//                    //{
//                        //if (type == Enemy.Type.B)
//                        //{
//                        //    yFactor = 100;
//                        //    texture = _enemyButterfly;
//                        //}
//                        //else
//                        //{
//                        //    xFactor = 50;
//                        //    yFactor = 0;
//                        //    texture = _textures[Game1.Random.Next(0, _textures.Count)];  //Standard enemy A                         
//                        //}

//                        //xFactor += 50;
//                        enemy = GetEnemy(texture, xFactor, yFactor);
                        
//                   // }

//                   // spawned = true;
//               // }
//           // }

//            return enemy;
//        }
//    }
//}
