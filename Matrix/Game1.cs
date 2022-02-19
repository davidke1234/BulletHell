﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Matrix
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private Texture2D _background;
        GraphicsDeviceManager graphics;
        SpriteBatch _spriteBatch;
        private List<SpriteNew> _sprites;
        private Player _player;
        private EnemyManager _enemyManager;
        private bool spE1, spE2, spE3, spE4, spE5, spE6, spE7, spE8, spE9, spE10;
        private bool spE11, spE12, spE13, spE14, spE15, spE16, spE17, spE18, spE19, spE20;
        private bool spE21, spE22, spE23, spE24, spE25, spE26, spE27, spE28, spE29;
        private SpriteFont _font;
        public static Random Random;
        public static int ScreenWidth = 1280;
        public static int ScreenHeight = 720;
        private double _gameOvertimer = 0;

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
            _font = Content.Load<SpriteFont>("Font");
            _enemyManager = new EnemyManager(Content);
          

            _background = Content.Load<Texture2D>("Stars");

            var player = Content.Load<Texture2D>("player_ship");
            var slowmoPlayer = Content.Load<Texture2D>("slowmoShip");

            //var song1 = Content.Load<Song>("sample1");
            //MediaPlayer.Play(song1);
            //Sounds.Load(Content);
            Arts.Load(Content);

            _player = new Player(player, slowmoPlayer)
            {
                Position = new Vector2(375, 335),
                Bullet = new Bullet(Content.Load<Texture2D>("Bullet")),
                Health = 10,
                Score = new Score()
            };

            _player.Score.PlayerName = "Player1";
            _sprites = new List<SpriteNew>();
            _sprites.Add(_player);
            
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
            PostUpdate();

            SpriteManager.Update(gameTime);

            base.Update(gameTime);
        }

        private void PostUpdate()
        {
            var collidedSprites = _sprites.Where(c => c is ICollidable);

            foreach (var sprite1 in collidedSprites)
            {
                foreach (var sprite2 in collidedSprites)
                {
                    if (sprite1 == sprite2)  //same sprite so continue
                        continue;

                    if (!sprite1.CollisionArea.Intersects(sprite2.CollisionArea))
                        continue;

                    //If the sprite is Player and is shooting this sprite as a bullet, continue
                    if (sprite1 is Player && sprite2.Parent is Player || sprite2 is Player && sprite1.Parent is Player)
                        continue;

                    if (sprite1.Intersects(sprite2))
                        ((ICollidable)sprite1).OnCollide(sprite2);
                }
            }

            //Clean up no longer needed sprites
            for (int i = 0; i < _sprites.Count; i++)
            {
                if (_sprites[i].IsRemoved)
                {
                    _sprites.RemoveAt(i);
                    i--;
                }
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            _spriteBatch.Draw(_background, new Rectangle(0, 0, 800, 480), Color.White);

            //Currently used for player, bullets and enemies
            foreach (var sprite in _sprites)
                sprite.Draw(gameTime, _spriteBatch);

            //Currently used for mid boss and bombs
            SpriteManager.Draw(_spriteBatch);

            _spriteBatch.DrawString(_font, "Player: " + _player.Score.PlayerName, new Vector2(10f, 10f), Color.White);
            _spriteBatch.DrawString(_font, "Health: " + _player.Health, new Vector2(10f, 30f), Color.White);
            _spriteBatch.DrawString(_font, "Score: " + _player.Score.Value, new Vector2(10f, 50f), Color.White);

            CheckGameOver(gameTime);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void CheckGameOver(GameTime gameTime)
        {
            if (_player.Health == 0)
            {
                if (_gameOvertimer == 0)
                    _gameOvertimer = gameTime.TotalGameTime.TotalSeconds;

                if (_gameOvertimer + 5 < gameTime.TotalGameTime.TotalSeconds)
                    Exit();

                _spriteBatch.DrawString(_font, "Game over", new Vector2(350f, 250f), Color.White);
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
            _sprites.AddRange(_enemyManager.GetEnemy(1, Enemy.Type.B, gameTime, 18, ref spE18));
            _sprites.AddRange(_enemyManager.GetEnemy(1, Enemy.Type.A, gameTime, 19, ref spE19));
            _sprites.AddRange(_enemyManager.GetEnemy(1, Enemy.Type.A, gameTime, 19, ref spE20));
           //sprites.AddRange(_enemyManager.GetEnemy(1, Enemy.Type.A, gameTime, 20, ref spE21));
            //prites.AddRange(_enemyManager.GetEnemy(1, Enemy.Type.A, gameTime, 21, ref spE22));
            //_sprites.AddRange(_enemyManager.GetEnemy(1, Enemy.Type.A, gameTime, 22, ref spE23));
        }

        private void GetEnemyWave3(GameTime gameTime)
        {
            _sprites.AddRange(_enemyManager.GetEnemy(1, Enemy.Type.A, gameTime, 22, ref spE24));
            _sprites.AddRange(_enemyManager.GetEnemy(1, Enemy.Type.A, gameTime, 23, ref spE25));
            _sprites.AddRange(_enemyManager.GetEnemy(1, Enemy.Type.B, gameTime, 24, ref spE26));
            _sprites.AddRange(_enemyManager.GetEnemy(1, Enemy.Type.A, gameTime, 25, ref spE27));
            _sprites.AddRange(_enemyManager.GetEnemy(1, Enemy.Type.A, gameTime, 26, ref spE28));
           // _sprites.AddRange(_enemyManager.GetEnemy(1, Enemy.Type.B, gameTime, 27, ref spE29));
        }

    }

    #endregion

}
