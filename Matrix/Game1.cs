using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        private List<Sprite> _sprites;
        private Player _player;
        private EnemyManager _enemyManager;
        private MidBoss _midBoss;
        private Bomb bomb;
        private FinalBoss _finalBoss;
        private SpriteFont _font;
        private Random _random;
        public static int ScreenWidth = 1280;
        public static int ScreenHeight = 720;
        private double _gameOverTimer = 0;
        public static SoundEffectInstance soundInstance;

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
            _random = new Random();
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
            Sounds.Load(Content);
            Arts.Load(Content);

            _player = new Player(player, slowmoPlayer)
            {
                Position = new Vector2(375, 335),
                Bullet = new Bullet(Content.Load<Texture2D>("Bullet")),
                Health = 10,
                Score = new Score()
            };
            _midBoss = new MidBoss(Arts.Boss2);
            _finalBoss = new FinalBoss(Arts.Boss);
            _player.Score.PlayerName = "Player1";
            _sprites = new List<Sprite>();
            _sprites.Add(_player);
            soundInstance = Sounds.soundEffects[0].CreateInstance();
            soundInstance.IsLooped = true;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            _finalBoss.IsRemoved = true;
            _finalBoss.bomb.IsRemoved = true;
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            _sprites.AddRange(_enemyManager.GetEnemyWave1(gameTime));
            _sprites.AddRange(_enemyManager.GetEnemyWave2(gameTime));
            _sprites.AddRange(_enemyManager.GetEnemyWave3(gameTime));

            if (gameTime.TotalGameTime.TotalSeconds >= 40)
            {
                if (!_sprites.Contains(_midBoss))
                {
                    _sprites.Add(_midBoss);
                }

                if(!_sprites.Contains(bomb))
                {
                    _sprites.Add(_midBoss.bomb);
                }
            }

            if (gameTime.TotalGameTime.TotalSeconds >= 60)
            {
                _midBoss.bomb.IsRemoved = true;
                _midBoss.IsRemoved = true;
                soundInstance.Stop();
            }

            if(gameTime.TotalGameTime.TotalSeconds > 60 && gameTime.TotalGameTime.TotalSeconds < 90)
            {
                if (!_sprites.Contains(_finalBoss))
                {
                    _sprites.Add(_finalBoss);
                }
                if (!_sprites.Contains(bomb))
                {
                    _sprites.Add(_finalBoss.bomb);
                }
            }

            //game time is how much time has elapsed
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            //For spriteNew sprites
            foreach (var sprite in _sprites.ToArray())
            {
                sprite.Update(gameTime, _sprites);
            }

            PostUpdate();

            //SpriteManager.Update(gameTime);

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
            //SpriteManager.Draw(gameTime, _spriteBatch);

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
                if (_gameOverTimer == 0)
                    _gameOverTimer = gameTime.TotalGameTime.TotalSeconds;

                if (_gameOverTimer + 5 < gameTime.TotalGameTime.TotalSeconds)
                    Exit();

                _spriteBatch.DrawString(_font, "Game over", new Vector2(350f, 250f), Color.White);
            }
        }
    }
}
