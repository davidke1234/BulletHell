using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace Matrix
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch _spriteBatch;
        private List<SpriteNew> _sprites;
        private EnemyManager _enemyManager;
        private bool spE1, spE2, spE3, spE4, spE5, spE6, spE7, spE8, spE9, spE10;
        private bool spE11, spE12, spE13, spE14, spE15, spE16, spE17, spE18, spE19, spE20;
        private bool spE21, spE22, spE23, spE24, spE25, spE26, spE27, spE28, spE29;

        public static Random Random;
        public static int ScreenWidth = 1280;
        public static int ScreenHeight = 720;

        // helpful properties
        public static GameTime GameTime { get; private set; }
        public static Game1 Instance { get; private set; }
        public static Viewport Viewport { get { return Instance.GraphicsDevice.Viewport; } }
        public static Vector2 ScreenSize { get { return new Vector2(Viewport.Width, Viewport.Height); } }



        public Game1()
        {
            Instance = this;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Random = new Random();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _enemyManager = new EnemyManager(Content);

            var player = Content.Load<Texture2D>("player_ship");

            //var song1 = Content.Load<Song>("sample1");
            //MediaPlayer.Play(song1);
            //Sounds.Load(Content);
            Arts.Load(Content);

            _sprites = new List<SpriteNew>()
            {
                new Player(player)
                    { Position = new Vector2(375, 335),
                    Bullet = new Bullet(Content.Load<Texture2D>("Bullet")) }
            };
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            GetEnemyWave1(gameTime);
            GetEnemyWave2(gameTime);
            GetEnemyWave3(gameTime);

            if (gameTime.TotalGameTime.TotalSeconds >= 40)
            {
                SpriteManager.Add(MidBoss.Instance);
            }

            if (gameTime.TotalGameTime.TotalSeconds >= 60)
            {
                MidBoss.Instance.IsOutdated = true;
            }


            //game time is how much time has elapsed
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //For spriteNew sprites
            foreach (var sprite in _sprites.ToArray())
                sprite.Update(gameTime, _sprites);
            CleanUpSprites(_sprites);

            SpriteManager.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            //TODO: Not using SpriteManager here due to bullets and enemies needing this
            foreach (var sprite in _sprites)
                sprite.Draw(_spriteBatch);
            SpriteManager.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void CleanUpSprites(List<SpriteNew> sprites)
        {
            for (int i = 0; i < sprites.Count; i++)
            {
                if (sprites[i].IsRemoved)
                {
                    sprites.RemoveAt(i);
                    i--;
                }
            }
        }

        #region Get Enemy Waves
        private void GetEnemyWave1(GameTime gameTime)
        {
            //if (!_enemyManager.SpawnedWave1)
            //{
            _sprites.AddRange(_enemyManager.GetEnemy(1, Enemy.Type.A, gameTime, 1, ref spE1));
            _sprites.AddRange(_enemyManager.GetEnemy(1, Enemy.Type.A, gameTime, 2, ref spE2));
            _sprites.AddRange(_enemyManager.GetEnemy(1, Enemy.Type.A, gameTime, 3, ref spE3));
            _sprites.AddRange(_enemyManager.GetEnemy(1, Enemy.Type.A, gameTime, 4, ref spE4));
            _sprites.AddRange(_enemyManager.GetEnemy(1, Enemy.Type.A, gameTime, 5, ref spE5));
            _sprites.AddRange(_enemyManager.GetEnemy(1, Enemy.Type.A, gameTime, 6, ref spE6));
            _sprites.AddRange(_enemyManager.GetEnemy(1, Enemy.Type.A, gameTime, 7, ref spE7));
            _sprites.AddRange(_enemyManager.GetEnemy(1, Enemy.Type.A, gameTime, 8, ref spE8));
            _sprites.AddRange(_enemyManager.GetEnemy(1, Enemy.Type.A, gameTime, 9, ref spE9));
            _sprites.AddRange(_enemyManager.GetEnemy(1, Enemy.Type.A, gameTime, 10, ref spE10));

            //if (_sprites.Count> 10)
            //    _enemyManager.SpawnedWave1 = true;

        }

        private void GetEnemyWave2(GameTime gameTime)
        {
            _sprites.AddRange(_enemyManager.GetEnemy(1, Enemy.Type.B, gameTime, 13, ref spE11));
            _sprites.AddRange(_enemyManager.GetEnemy(1, Enemy.Type.A, gameTime, 13, ref spE12));
            _sprites.AddRange(_enemyManager.GetEnemy(1, Enemy.Type.A, gameTime, 14, ref spE13));
            _sprites.AddRange(_enemyManager.GetEnemy(1, Enemy.Type.A, gameTime, 15, ref spE14));
            _sprites.AddRange(_enemyManager.GetEnemy(1, Enemy.Type.A, gameTime, 16, ref spE15));
            _sprites.AddRange(_enemyManager.GetEnemy(1, Enemy.Type.A, gameTime, 17, ref spE16));
            _sprites.AddRange(_enemyManager.GetEnemy(1, Enemy.Type.A, gameTime, 18, ref spE18));
            _sprites.AddRange(_enemyManager.GetEnemy(1, Enemy.Type.A, gameTime, 19, ref spE19));
            _sprites.AddRange(_enemyManager.GetEnemy(1, Enemy.Type.B, gameTime, 19, ref spE20));
           //sprites.AddRange(_enemyManager.GetEnemy(1, Enemy.Type.A, gameTime, 20, ref spE21));
            //prites.AddRange(_enemyManager.GetEnemy(1, Enemy.Type.A, gameTime, 21, ref spE22));
            //_sprites.AddRange(_enemyManager.GetEnemy(1, Enemy.Type.A, gameTime, 22, ref spE23));
        }

        private void GetEnemyWave3(GameTime gameTime)
        {
            _sprites.AddRange(_enemyManager.GetEnemy(1, Enemy.Type.A, gameTime, 22, ref spE24));
            _sprites.AddRange(_enemyManager.GetEnemy(1, Enemy.Type.A, gameTime, 23, ref spE25));
            _sprites.AddRange(_enemyManager.GetEnemy(1, Enemy.Type.A, gameTime, 24, ref spE26));
            _sprites.AddRange(_enemyManager.GetEnemy(1, Enemy.Type.A, gameTime, 25, ref spE27));
            _sprites.AddRange(_enemyManager.GetEnemy(1, Enemy.Type.A, gameTime, 26, ref spE28));
            _sprites.AddRange(_enemyManager.GetEnemy(1, Enemy.Type.B, gameTime, 27, ref spE29));
        }

    }

    #endregion

}
