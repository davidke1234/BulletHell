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
        private bool spawnedEnemies1;
        private bool spawnedEnemies2;
        private bool spawnedEnemies3;
        private bool spawnedEnemies4;
        private bool spawnedEnemies5;
        private bool spawnedEnemies6;
        private bool spawnedEnemies7;
        private bool spawnedEnemies8;
        private bool spawnedEnemies9;
        private bool spawnedEnemies10;
        private bool spawnedEnemies11;
        private bool spawnedEnemies12;
        private bool spawnedEnemies13;
        private bool spawnedEnemies14;
        private bool spawnedEnemies15;
        private bool spawnedEnemies16;
        private bool spawnedEnemies17;
        private bool spawnedEnemies18;
        private bool spawnedEnemies19;
        private bool spawnedEnemies20;


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

            var song1 = Content.Load<Song>("sample1");
            MediaPlayer.Play(song1);
            Sounds.Load(Content);
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
            _sprites.AddRange(_enemyManager.GetEnemies(Enemy.Type.A, gameTime, 0, ref spawnedEnemies1, 6));
            _sprites.AddRange(_enemyManager.GetEnemies(Enemy.Type.B, gameTime, 5, ref spawnedEnemies2, 1));
            _sprites.AddRange(_enemyManager.GetEnemies(Enemy.Type.A, gameTime, 12, ref spawnedEnemies3, 4));
            _sprites.AddRange(_enemyManager.GetEnemies(Enemy.Type.A, gameTime, 18, ref spawnedEnemies4, 6));

            if (gameTime.TotalGameTime.TotalSeconds >= 20)
            {
                SpriteManager.Add(MidBoss.Instance);
            }

            if (gameTime.TotalGameTime.TotalSeconds >= 40)
            {
                MidBoss.Instance.IsOutdated = true;
            }

            _sprites.AddRange(_enemyManager.GetEnemies(Enemy.Type.B, gameTime, 30, ref spawnedEnemies5, 1));
            _sprites.AddRange(_enemyManager.GetEnemies(Enemy.Type.A, gameTime, 40, ref spawnedEnemies6, 5));
            _sprites.AddRange(_enemyManager.GetEnemies(Enemy.Type.A, gameTime, 50, ref spawnedEnemies7, 5));
            _sprites.AddRange(_enemyManager.GetEnemies(Enemy.Type.B, gameTime, 53, ref spawnedEnemies8, 1));
            _sprites.AddRange(_enemyManager.GetEnemies(Enemy.Type.A, gameTime, 60, ref spawnedEnemies9, 6));
            _sprites.AddRange(_enemyManager.GetEnemies(Enemy.Type.B, gameTime, 65, ref spawnedEnemies10, 1));
            _sprites.AddRange(_enemyManager.GetEnemies(Enemy.Type.A, gameTime, 70, ref spawnedEnemies11, 4));
            _sprites.AddRange(_enemyManager.GetEnemies(Enemy.Type.A, gameTime, 80, ref spawnedEnemies12, 8));
            _sprites.AddRange(_enemyManager.GetEnemies(Enemy.Type.B, gameTime, 85, ref spawnedEnemies13, 1));
            _sprites.AddRange(_enemyManager.GetEnemies(Enemy.Type.A, gameTime, 92, ref spawnedEnemies14, 6));
            _sprites.AddRange(_enemyManager.GetEnemies(Enemy.Type.A, gameTime, 100, ref spawnedEnemies15, 3));

            //game time is how much time has elapsed
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //For spriteNew sprites
            foreach (var sprite in _sprites.ToArray())
                sprite.Update(gameTime, _sprites);
            //CleanUpSprites(_sprites);

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
    }
}
