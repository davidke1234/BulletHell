using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Matrix.Controllers;
using NLog;
using Matrix.Models;
using Microsoft.Xna.Framework.Media;

namespace Matrix
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private Button _startButton;
        private Button _quitButton;
        private Button _configButton;
        private Button _arrowKeysButton;
        private Button _WASDKeysButton;
        private Button _MainMenuButton;
        private Button _EscKeyCheatButton;
        GraphicsDeviceManager graphics;
        SpriteBatch _spriteBatch;
        private Player _player;
        private FinalBoss _finalBoss;
        private SpriteFont _font;
        public static int ScreenWidth = 1280;
        public static int ScreenHeight = 720;
        public EventHandler Click;
        private int secondsToDisplayWinLossMessage = 4;
        private ContentManager _content;
        private string _keysType = "arrows";
        private SoundEffectInstance _soundEffectInstance;
        private bool song1Started = false;
        private bool song2Started = false;
        private bool song3Started = false;
        private bool song4Started = false;

        // used to record when the actual game started.  The game loop continues while on the menu
        // and the timing of the enemy spawner will be delayed if we don't use this.
        private double _gameStartedSeconds = 0;
        private double _currentTotalGameSeconds = 0;
        
        private double _gameOverTimer = 0;
        private bool _gameOver = false;
        private bool _configButtonClicked;
        private bool _gameStarted;

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
            _logger.Info("Starting Game");
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            _content = Content;
            Arts.Load(Content);
            Sounds.Load(Content);
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Arts.Font;
            LoadMenuContent(Content);
        }

        private void LoadGameContent(ContentManager content)
        {
            _player = PlayerManager.GetPlayer(Arts.Player, Arts.SlowmoPlayer, 20, _keysType);
            _finalBoss = FinalBoss.GetInstance;
            SpriteManager.Sprites.Add(_player);
            _soundEffectInstance = Sounds.soundEffects[0].CreateInstance();
            _soundEffectInstance.IsLooped = true;
        }

        public void Button_1Player_Clicked(object sender, EventArgs args)
        {
            _gameStarted = true;
            LoadGameContent(Content);
        }

        public void Button_Config_Clicked(object sender, EventArgs args)
        {
            _configButtonClicked = true;
            LoadConfigMenu(_content);
        }
        public void Button_ArrowKeys_Clicked(object sender, EventArgs args)
        {
            _keysType = "arrows";
        }
        public void Button_WasdKeys_Clicked(object sender, EventArgs args)
        {
            _keysType = "wasd";
        }

        public void Button_EscKeyCheat_Clicked(object sender, EventArgs args)
        {
            GameManager.EnabledEscKeyCheat ^= true;
        }
        

        public void Button_MainMenu_Clicked(object sender, EventArgs args)
        {
            _configButtonClicked = false;
            MenuManager.SetupMainMenu(ref _startButton, ref _configButton, ref _quitButton, this);
        }

        public void Button_Quit_Clicked(object sender, EventArgs args)
        {
            Exit();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            if (_finalBoss != null)
            {
                _finalBoss.IsRemoved = true;
                _finalBoss.bomb.IsRemoved = true;
            }        
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (_gameOver)
                CheckGameOver(_currentTotalGameSeconds, SpriteManager.Sprites);

            if (!_gameStarted)
            {
                if (_configButtonClicked)
                {
                    MenuManager.SetupConfigMenu(ref _arrowKeysButton, ref _WASDKeysButton, ref _EscKeyCheatButton, ref _MainMenuButton, this);
                }
                else
                {
                    //if (!SetupMenuLoaded)
                    {
                        MenuManager.SetupMainMenu(ref _startButton, ref _configButton, ref _quitButton, this);
                        SetupMenuLoaded = true;
                    }
                }
            }

            else if (_gameStarted)
            {
                if (_gameStartedSeconds == 0)
                {
                    _gameStartedSeconds = gameTime.TotalGameTime.TotalSeconds;
                }

                _currentTotalGameSeconds = gameTime.TotalGameTime.TotalSeconds - _gameStartedSeconds + 1;

                if (_player.Respawn)
                {
                    PlayerManager.Respawn(SpriteManager.Sprites);
                    if (PlayerManager.ResetPlayer(gameTime))
                    {
                        _player.Respawn = false;
                    }
                }

                // Phase 1
                if (_currentTotalGameSeconds < 40)
                {
                    EnemyManager.GetEnemyPhase1(SpriteManager.Sprites, _currentTotalGameSeconds);
                    if (!song1Started)
                    {
                        _soundEffectInstance.Stop();
                        MediaPlayer.Play(Arts.Song1);
                        song1Started = true;
                    }
                }

                // Phase 2
                if (GameManager.GoToNextPhase(_currentTotalGameSeconds, 2))
                {
                    EnemyManager.GetEnemyPhase2(SpriteManager.Sprites, _currentTotalGameSeconds);
                    if (!song2Started)
                    {
                        MediaPlayer.Stop();
                        MediaPlayer.Play(Arts.Song2);
                        song2Started = true;
                    }
                }

                // Phase 3
                if (GameManager.GoToNextPhase(_currentTotalGameSeconds, 3))
                {
                    EnemyManager.GetEnemyPhase3(SpriteManager.Sprites, _currentTotalGameSeconds);
                    if (!song3Started)
                    {
                        MediaPlayer.Stop();
                        MediaPlayer.Play(Arts.Song3);
                        song3Started = true;
                    }
                }

                //Phase 4
                if (GameManager.GoToNextPhase(_currentTotalGameSeconds, 4))
                {
                    EnemyManager.GetEnemyPhase4(SpriteManager.Sprites, _currentTotalGameSeconds);
                    if (!song4Started)
                    {
                        MediaPlayer.Stop();
                        MediaPlayer.Play(Arts.Song4);
                       // if (MediaPlayer.State == MediaState.Playing)
                        song4Started = true;
                    }
                }

                if (_currentTotalGameSeconds >= 130)  
                {
                    CheckGameOver(_currentTotalGameSeconds, SpriteManager.Sprites);
                }

                //game time is how much time has elapsed
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) // || Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    RestartGame();
                }

                foreach (var sprite in SpriteManager.Sprites.ToArray())
                {
                    sprite.Update(gameTime, SpriteManager.Sprites);
                }

                PostUpdate();

                base.Update(gameTime);
            }
        }

        private void PostUpdate()
        {
            SpriteManager.HandleCollisions(SpriteManager.Sprites);
            SpriteManager.CleanUpRemovedSprites(SpriteManager.Sprites);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (!_gameStarted)
            {
                if (_configButtonClicked)
                {
                    GraphicsDevice.Clear(Color.Black);
                    _spriteBatch.Begin();
                    DrawConfigMenu();
                    _spriteBatch.End();
                    base.Draw(gameTime);
                }
                else
                {
                    GraphicsDevice.Clear(Color.Black);
                    _spriteBatch.Begin();
                    DrawMainMenu();
                    _spriteBatch.End();
                    base.Draw(gameTime);
                }
            }
            
            else if (!_gameOver)
            {
                GraphicsDevice.Clear(Color.Black);
                _spriteBatch.Begin();

                if (GameManager.GamePhase == 1)
                    _spriteBatch.Draw(Arts.StarsBackground, new Rectangle(0, 0, 800, 480), Color.White);
                else if (GameManager.GamePhase == 2)
                    _spriteBatch.Draw(Arts.BlueBackground, new Rectangle(0, 0, 800, 480), Color.White);
                else if (GameManager.GamePhase == 3)
                    _spriteBatch.Draw(Arts.RedBackground, new Rectangle(0, 0, 800, 480), Color.White);
                else if (GameManager.GamePhase == 4)
                    _spriteBatch.Draw(Arts.BattleFieldBackground, new Rectangle(0, 0, 800, 480), Color.White);

                foreach (var sprite in SpriteManager.Sprites)
                    sprite.Draw(gameTime, _spriteBatch);

                PlayerManager.DrawPlayerStatus(_spriteBatch, _player);
                _spriteBatch.End();

                base.Draw(gameTime);
            }

            if (SpriteManager.Sprites != null)
                CheckGameOver(_gameStartedSeconds, SpriteManager.Sprites);
        }

        private void CheckGameOver(double gameStartedSeconds, List<Sprite> sprites)
        {
            string winLoss = "";
            bool killedLastEnemy = FinalBossKilled(sprites, gameStartedSeconds);
            if (_player != null && (_player.Health <= 0 || killedLastEnemy))
            {
                _gameOver = true;
                if (_gameOverTimer == 0)
                {
                    _gameOverTimer = gameStartedSeconds;
                    winLoss = _player.Health > 0 && _player.Score.Value > 0 ? " You won!!" : " you lost";
                    _spriteBatch.Begin();
                    _spriteBatch.DrawString(_font, "Game over - " + winLoss, new Vector2(350f, 250f), Color.White);
                    _spriteBatch.End();

                    PlayerManager.InsertScore(_player.Score.Value);
                }
                else
                {
                    if (_gameOverTimer + secondsToDisplayWinLossMessage < gameStartedSeconds)
                    {
                        RestartGame();
                    }
                }
            }
        }

        private void RestartGame()
        {
            //Clean up
            MediaPlayer.Stop();
            SpriteManager.Sprites.Clear();
            EnemyManager.Enemies.Clear();
            MenuManager.HighScores.Clear();
            _gameStartedSeconds = 0;
            _currentTotalGameSeconds = 0;
            _gameStarted = false;
            _configButtonClicked = false;
            _gameOver = false;
            _gameOverTimer = 0;

            //Must exit the game and return to start menu
            Program.ShouldRestart = true;
            Exit();
        }

        private bool FinalBossKilled(List<Sprite> sprites, double gameStartedSeconds)
        {
            bool finalBossKilled = sprites.Find(f => f.Name == "finalboss") == null && gameStartedSeconds >= 130;
            return finalBossKilled;
        }

        private void LoadMenuContent(ContentManager content)
        {
            _soundEffectInstance = Sounds.soundEffects[1].CreateInstance();
            _soundEffectInstance.Volume = .5f;
            _soundEffectInstance.Play();

            var buttonTexture = Arts.Button;
            var buttonFont = Arts.Font;
 
            _startButton = MenuManager.MakeButton(buttonTexture, buttonFont, "Start Game", this.Button_1Player_Clicked);
            _configButton = MenuManager.MakeButton(buttonTexture, buttonFont, "Configuration", this.Button_Config_Clicked);
            _quitButton = MenuManager.MakeButton(buttonTexture, buttonFont, "Quit Game", this.Button_Quit_Clicked);
        }

        public void LoadConfigMenu(ContentManager content)
        {
            var buttonTexture = Arts.Button;
            var buttonFont = Arts.Font;

            _arrowKeysButton = MenuManager.MakeButton(buttonTexture, buttonFont, "Arrow keys", this.Button_ArrowKeys_Clicked);
            _WASDKeysButton = MenuManager.MakeButton(buttonTexture, buttonFont, "WASD keys", this.Button_WasdKeys_Clicked);
            _EscKeyCheatButton = MenuManager.MakeButton(buttonTexture, buttonFont, "Enable Esc cheat", this.Button_EscKeyCheat_Clicked);
            _WASDKeysButton = MenuManager.MakeButton(buttonTexture, buttonFont, "WASD keys", this.Button_WasdKeys_Clicked);
            _MainMenuButton = MenuManager.MakeButton(buttonTexture, buttonFont, "Main Menu", this.Button_MainMenu_Clicked);
        }
           
        private void DrawMainMenu()
        {
            _spriteBatch.Draw(Arts.MainMenuBackground, new Rectangle(0, 0, 800, 480), Color.White);

            if (!string.IsNullOrWhiteSpace(_startButton.Text))
            {
                var x = Rectangle.X + (Rectangle.Width / 2) - (_font.MeasureString(_startButton.Text).X / 2);
                var y = Rectangle.Y + (Rectangle.Height / 2) - (_font.MeasureString(_startButton.Text).Y / 2);

                _startButton.Position = new Vector2(x, y);
                _spriteBatch.Draw(_startButton.Texture, _startButton.Position, null, Color.White, 0f, new Vector2(25, 0), 1f, SpriteEffects.None, 0.01f);
                _spriteBatch.DrawString(_font, _startButton.Text, new Vector2(x, y), Color.Black, 0f, new Vector2(-10, -8), 1f, SpriteEffects.None, 0.01f);
            }


            if (!string.IsNullOrWhiteSpace(_configButton.Text))
            {
                var x = Rectangle.X + (Rectangle.Width / 2) - (_font.MeasureString(_configButton.Text).X / 2);
                var y = Rectangle.Y + (Rectangle.Height / 2) - (_font.MeasureString(_configButton.Text).Y / 2);

                _configButton.Position = new Vector2(x + 40, y);
                _spriteBatch.Draw(_configButton.Texture, _configButton.Position, null, Color.White, 0f, new Vector2(58, -40), 1f, SpriteEffects.None, 0.01f);
                _spriteBatch.DrawString(_font, _configButton.Text, new Vector2(x, y), Color.Black, 0f, new Vector2(-10, -48), 1f, SpriteEffects.None, 0.01f);
            }


            if (!string.IsNullOrWhiteSpace(_quitButton.Text))
            {
                var x = Rectangle.X + (Rectangle.Width / 2) - (_font.MeasureString(_quitButton.Text).X / 2);
                var y = Rectangle.Y + (Rectangle.Height / 2) - (_font.MeasureString(_quitButton.Text).Y / 2);

                _quitButton.Position = new Vector2(x + 80, y);
                _spriteBatch.Draw(_quitButton.Texture, _quitButton.Position, null, Color.White, 0f, new Vector2(107, -80), 1f, SpriteEffects.None, 0.01f);
                _spriteBatch.DrawString(_font, _quitButton.Text, new Vector2(x, y), Color.Black, 0f, new Vector2(-10, -88), 1f, SpriteEffects.None, 0.01f);
            }

            MenuManager.DisplayHighScores(_spriteBatch);
        }

        private void DrawConfigMenu()
        {
            _spriteBatch.Draw(Arts.MainMenuBackground, new Rectangle(0, 0, 800, 480), Color.White);

            //Create a text box for keys selected
            _spriteBatch.DrawString(Arts.Font, "Keys Selected: " + _keysType, new Vector2(320f, 150f), Color.White);
            _spriteBatch.DrawString(Arts.Font, "Esc cheat enabled: " + GameManager.EnabledEscKeyCheat.ToString(), new Vector2(320f, 180f), Color.White);

            if (!string.IsNullOrWhiteSpace(_arrowKeysButton.Text))
            {
                var x = Rectangle.X + (Rectangle.Width / 2) - (_font.MeasureString(_arrowKeysButton.Text).X / 2);
                var y = Rectangle.Y + (Rectangle.Height / 2) - (_font.MeasureString(_arrowKeysButton.Text).Y / 2);

                _arrowKeysButton.Position = new Vector2(x, y);
                _spriteBatch.Draw(_arrowKeysButton.Texture, _arrowKeysButton.Position, null, Color.White, 0f, new Vector2(26, 0), 1f, SpriteEffects.None, 0.01f);
                _spriteBatch.DrawString(_font, _arrowKeysButton.Text, new Vector2(x, y), Color.Black, 0f, new Vector2(-10, -8), 1f, SpriteEffects.None, 0.01f);
            }

            if (!string.IsNullOrWhiteSpace(_WASDKeysButton.Text))
            {
                var x = Rectangle.X + (Rectangle.Width / 2) - (_font.MeasureString(_WASDKeysButton.Text).X / 2);
                var y = Rectangle.Y + (Rectangle.Height / 2) - (_font.MeasureString(_WASDKeysButton.Text).Y / 2);

                _WASDKeysButton.Position = new Vector2(x + 40, y);
                _spriteBatch.Draw(_WASDKeysButton.Texture, _WASDKeysButton.Position, null, Color.White, 0f, new Vector2(62, -40), 1f, SpriteEffects.None, 0.01f);
                _spriteBatch.DrawString(_font, _WASDKeysButton.Text, new Vector2(x, y), Color.Black, 0f, new Vector2(-10, -48), 1f, SpriteEffects.None, 0.01f);
            }

            if (!string.IsNullOrWhiteSpace(_EscKeyCheatButton.Text))
            {
                var x = Rectangle.X + (Rectangle.Width / 2) - (_font.MeasureString(_EscKeyCheatButton.Text).X / 2);
                var y = Rectangle.Y + (Rectangle.Height / 2) - (_font.MeasureString(_EscKeyCheatButton.Text).Y / 2);

                _EscKeyCheatButton.Position = new Vector2(x + 59, y);
                _spriteBatch.Draw(_EscKeyCheatButton.Texture, _EscKeyCheatButton.Position, null, Color.White, 0f, new Vector2(62, -80), 1f, SpriteEffects.None, 0.01f);
                _spriteBatch.DrawString(_font, _EscKeyCheatButton.Text, new Vector2(x, y), Color.Black, 0f, new Vector2(-10, -88), 1f, SpriteEffects.None, 0.01f);
            }

            if (!string.IsNullOrWhiteSpace(_MainMenuButton.Text))
            {
                var x = Rectangle.X + (Rectangle.Width / 2) - (_font.MeasureString(_MainMenuButton.Text).X / 2);
                var y = Rectangle.Y + (Rectangle.Height / 2) - (_font.MeasureString(_MainMenuButton.Text).Y / 2);

                _MainMenuButton.Position = new Vector2(x + 80, y);
                _spriteBatch.Draw(_quitButton.Texture, _MainMenuButton.Position, null, Color.White, 0f, new Vector2(107, -120), 1f, SpriteEffects.None, 0.01f);
                _spriteBatch.DrawString(_font, _MainMenuButton.Text, new Vector2(x, y), Color.Black, 0f, new Vector2(-10, -128), 1f, SpriteEffects.None, 0.01f);
            }
        }

        private Rectangle Rectangle
        {
            get
            {
                return new Rectangle(0, 0, 800, 480);
            }
        }

        public bool SetupMenuLoaded { get; private set; }
    }
}
