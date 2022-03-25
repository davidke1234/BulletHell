using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using Matrix.Controllers;

namespace Matrix
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private Texture2D _background;
        private Texture2D _menuBackground;
        private Button _startButton;
        private Button _quitButton;
        private Button _configButton;
        private Button _arrowKeysButton;
        private Button _WASDKeysButton;
        private Button _MainMenuButton;

        GraphicsDeviceManager graphics;
        SpriteBatch _spriteBatch;
        private List<Sprite> _sprites;
        private Player _player;
        private FinalBoss _finalBoss;
        private SpriteFont _font;
        public static int ScreenWidth = 1280;
        public static int ScreenHeight = 720;
        private double _gameOverTimer = 0;
        private bool _gameOver = false;
        public static SoundEffectInstance soundInstance;
        private bool _gameStarted;
        private MouseState _currentMouse;
        private MouseState _previousMouse;
        public EventHandler Click;
        private int secondsToDisplayWinLossMessage = 4;
        private bool _configButtonClicked;
        private ContentManager _content;
        private string _keysType = "arrows";

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
            //_random = new Random();
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
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Arts.Font;
            LoadMenuContent(Content);

        }

        private void LoadGameContent(ContentManager content)
        {
            _background = Arts.Stars;

            var song1 = Arts.Song1;
            MediaPlayer.Play(song1);
            Sounds.Load(content);

            _player = PlayerManager.GetPlayer(Arts.Player, Arts.SlowmoPlayer, 20, _keysType);
            _finalBoss = FinalBoss.GetInstance;
            _sprites = new List<Sprite>();
            _sprites.Add(_player);
            soundInstance = Sounds.soundEffects[0].CreateInstance();
            soundInstance.IsLooped = true;
        }      

        private void Button_1Player_Clicked(object sender, EventArgs args)
        {
            _gameStarted = true;
            LoadGameContent(Content);
        }

        private void Button_Config_Clicked(object sender, EventArgs args)
        {
            _configButtonClicked = true;
            LoadConfigMenu(_content);
        }
        private void Button_ArrowKeys_Clicked(object sender, EventArgs args)
        {
            _keysType = "arrows";
        }
        private void Button_WasdKeys_Clicked(object sender, EventArgs args)
        {
            _keysType = "wasd";
        }

        private void Button_MainMenu_Clicked(object sender, EventArgs args)
        {
            _configButtonClicked = false;
            SetupMainMenu();
        }

        private void Button_Quit_Clicked(object sender, EventArgs args)
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
                CheckGameOver(gameTime, true);

            if (!_gameStarted)
            {
                if (_configButtonClicked)
                {
                    SetupConfigMenu();
                }
                else
                    SetupMainMenu();
            }
           
            else if (_gameStarted)
            {
                // Phase 1
                _sprites.AddRange(EnemyManager.GetEnemyPhase1(gameTime));

                // Phase 2
                if (gameTime.TotalGameTime.TotalSeconds >= 40)
                {
                    _sprites.AddRange(EnemyManager.GetEnemyPhase2(gameTime));
                }

                // Phase 3
                if (gameTime.TotalGameTime.TotalSeconds >= 80)
                {
                    _sprites.AddRange(EnemyManager.GetEnemyPhase3(gameTime));
                }

                //Phase 4
                if (gameTime.TotalGameTime.TotalSeconds >= 120)
                {
                    _sprites.AddRange(EnemyManager.GetEnemyPhase4(gameTime));
                }

                if (gameTime.TotalGameTime.TotalSeconds >= 170)  
                {
                    CheckGameOver(gameTime, true);
                }

                //game time is how much time has elapsed
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    Program.ShouldRestart = true;
                    Exit();
                }

                //For spriteNew sprites
                foreach (var sprite in _sprites.ToArray())
                {
                    sprite.Update(gameTime, _sprites);
                }

                PostUpdate();

                base.Update(gameTime);
            }
        }

        private void PostUpdate()
        {
            SpriteManager.HandleCollisions(_sprites);
            SpriteManager.CleanUpRemovedSprites(_sprites);
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
                _spriteBatch.Draw(_background, new Rectangle(0, 0, 800, 480), Color.White);

                foreach (var sprite in _sprites)
                    sprite.Draw(gameTime, _spriteBatch);

                PlayerManager.DrawPlayerStatus(_spriteBatch, _player);
                _spriteBatch.End();

                base.Draw(gameTime);
            }

            CheckGameOver(gameTime, false);
        }



        private void CheckGameOver(GameTime gameTime, bool timesUp)
        {
            string winLoss = "";

            if (_player != null && (timesUp || _player.Health <= 0))
            {
                _gameOver = true;
                if (_gameOverTimer == 0)
                {
                    _gameOverTimer = gameTime.TotalGameTime.TotalSeconds;
                    int score = _player.Score.Value;
                    winLoss = _player.Health > 0 && score > 0 ? " You won!!" : " you lost";
                    _spriteBatch.Begin();
                    _spriteBatch.DrawString(_font, "Game over - " + winLoss, new Vector2(350f, 250f), Color.White);
                    _spriteBatch.End();
                }
                else
                {
                    if (_gameOverTimer + secondsToDisplayWinLossMessage < gameTime.TotalGameTime.TotalSeconds)
                    {
                        //Must exit the game and return to start menu
                        Program.ShouldRestart = true;
                        Exit();
                    }
                }
            }
        }

        private void LoadMenuContent(ContentManager content)
        {
            var buttonTexture = Arts.Button;
            var buttonFont = Arts.Font;
            _menuBackground = Arts.MainMenuBackGround;

            Button bStart = new Button(buttonTexture, buttonFont);
            bStart.Text = "Start Game";
            bStart.Click = new EventHandler(Button_1Player_Clicked);
            bStart.Layer = 0.1f;
            bStart.Texture = buttonTexture;
            _startButton = bStart;

            Button bConfig = new Button(buttonTexture, buttonFont);
            bConfig.Text = "Configuration";
            bConfig.Click = new EventHandler(Button_Config_Clicked);
            bConfig.Layer = 0.1f;
            bConfig.Texture = buttonTexture;
            _configButton = bConfig;

            Button bQuit = new Button(buttonTexture, buttonFont);
            bQuit.Text = "Quit Game";
            bQuit.Click = new EventHandler(Button_Quit_Clicked);
            bQuit.Layer = 0.1f;
            bQuit.Texture = buttonTexture;
            _quitButton = bQuit;
        }

        public void LoadConfigMenu(ContentManager content)
        {
            var buttonTexture = Arts.Button;
            var buttonFont = Arts.Font;
            _menuBackground = Arts.MainMenuBackGround;

            Button arrowKeys = new Button(buttonTexture, buttonFont);
            arrowKeys.Text = "Arrow keys";
            arrowKeys.Click = new EventHandler(Button_ArrowKeys_Clicked);
            arrowKeys.Layer = 0.1f;
            arrowKeys.Texture = buttonTexture;
            _arrowKeysButton = arrowKeys;

            Button wasdKeys = new Button(buttonTexture, buttonFont);
            wasdKeys.Text = "WASD keys";
            wasdKeys.Click = new EventHandler(Button_WasdKeys_Clicked);
            wasdKeys.Layer = 0.1f;
            wasdKeys.Texture = buttonTexture;
            _WASDKeysButton = wasdKeys;

            Button mainMenu = new Button(buttonTexture, buttonFont);
            mainMenu.Text = "Main Menu";
            mainMenu.Click = new EventHandler(Button_MainMenu_Clicked);
            mainMenu.Layer = 0.1f;
            mainMenu.Texture = buttonTexture;
            _MainMenuButton = mainMenu;
        }

        private void SetupMainMenu()
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            if (mouseRectangle.Intersects(Rectangle))
            {
                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {
                    if (mouseRectangle.Top >= 233 && mouseRectangle.Top <= 255)
                        _startButton.Click?.Invoke(this, new EventArgs());
                    else if(mouseRectangle.Top >= 273 && mouseRectangle.Top <= 297)
                        _configButton.Click?.Invoke(this, new EventArgs());
                    else if (mouseRectangle.Top >= 313 && mouseRectangle.Top <= 337)
                        _quitButton.Click?.Invoke(this, new EventArgs());
                }
            }
        }
        private void SetupConfigMenu()
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            if (mouseRectangle.Intersects(Rectangle))
            {
                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {
                    if (mouseRectangle.Top >= 233 && mouseRectangle.Top <= 255)
                        _arrowKeysButton.Click?.Invoke(this, new EventArgs());
                    else if (mouseRectangle.Top >= 273 && mouseRectangle.Top <= 297)
                        _WASDKeysButton.Click?.Invoke(this, new EventArgs());
                    else if (mouseRectangle.Top >= 313 && mouseRectangle.Top <= 337)
                        _MainMenuButton.Click?.Invoke(this, new EventArgs());
                }
            }
        }

        private void DrawMainMenu()
        {
            _spriteBatch.Draw(_menuBackground, new Rectangle(0, 0, 800, 480), Color.White);

            if (!string.IsNullOrWhiteSpace(_startButton.Text))
            {
                var x = Rectangle.X + (Rectangle.Width / 2) - (_font.MeasureString(_startButton.Text).X / 2);
                var y = Rectangle.Y + (Rectangle.Height / 2) - (_font.MeasureString(_startButton.Text).Y / 2);

                _startButton.Position = new Vector2(x, y);
                _spriteBatch.Draw(_startButton.Texture, _startButton.Position, null, Color.White, 0f, new Vector2(25, 0), 1f, SpriteEffects.None, 0.01f);
                _spriteBatch.DrawString(_font, _startButton.Text, new Vector2(x, y), Color.Black, 0f, new Vector2(5, -8), 1f, SpriteEffects.None, 0.01f);
            }


            if (!string.IsNullOrWhiteSpace(_configButton.Text))
            {
                var x = Rectangle.X + (Rectangle.Width / 2) - (_font.MeasureString(_configButton.Text).X / 2);
                var y = Rectangle.Y + (Rectangle.Height / 2) - (_font.MeasureString(_configButton.Text).Y / 2);

                _configButton.Position = new Vector2(x + 40, y);
                _spriteBatch.Draw(_configButton.Texture, _configButton.Position, null, Color.White, 0f, new Vector2(58, -40), 1f, SpriteEffects.None, 0.01f);
                _spriteBatch.DrawString(_font, _configButton.Text, new Vector2(x, y), Color.Black, 0f, new Vector2(5, -48), 1f, SpriteEffects.None, 0.01f);
            }


            if (!string.IsNullOrWhiteSpace(_quitButton.Text))
            {
                var x = Rectangle.X + (Rectangle.Width / 2) - (_font.MeasureString(_quitButton.Text).X / 2);
                var y = Rectangle.Y + (Rectangle.Height / 2) - (_font.MeasureString(_quitButton.Text).Y / 2);

                _quitButton.Position = new Vector2(x + 80, y);
                _spriteBatch.Draw(_quitButton.Texture, _quitButton.Position, null, Color.White, 0f, new Vector2(107, -80), 1f, SpriteEffects.None, 0.01f);
                _spriteBatch.DrawString(_font, _quitButton.Text, new Vector2(x, y), Color.Black, 0f, new Vector2(5, -88), 1f, SpriteEffects.None, 0.01f);
            }
        }

        private void DrawConfigMenu()
        {
            _spriteBatch.Draw(_menuBackground, new Rectangle(0, 0, 800, 480), Color.White);

            //Create a text box for keys selected
            _spriteBatch.DrawString(Arts.Font, "Keys Selected: " + _keysType, new Vector2(320f, 200f), Color.White);

            if (!string.IsNullOrWhiteSpace(_arrowKeysButton.Text))
            {
                var x = Rectangle.X + (Rectangle.Width / 2) - (_font.MeasureString(_arrowKeysButton.Text).X / 2);
                var y = Rectangle.Y + (Rectangle.Height / 2) - (_font.MeasureString(_arrowKeysButton.Text).Y / 2);

                _arrowKeysButton.Position = new Vector2(x, y);
                _spriteBatch.Draw(_arrowKeysButton.Texture, _arrowKeysButton.Position, null, Color.White, 0f, new Vector2(26, 0), 1f, SpriteEffects.None, 0.01f);
                _spriteBatch.DrawString(_font, _arrowKeysButton.Text, new Vector2(x, y), Color.Black, 0f, new Vector2(5, -8), 1f, SpriteEffects.None, 0.01f);
            }

            if (!string.IsNullOrWhiteSpace(_WASDKeysButton.Text))
            {
                var x = Rectangle.X + (Rectangle.Width / 2) - (_font.MeasureString(_WASDKeysButton.Text).X / 2);
                var y = Rectangle.Y + (Rectangle.Height / 2) - (_font.MeasureString(_WASDKeysButton.Text).Y / 2);

                _WASDKeysButton.Position = new Vector2(x + 40, y);
                _spriteBatch.Draw(_WASDKeysButton.Texture, _WASDKeysButton.Position, null, Color.White, 0f, new Vector2(62, -40), 1f, SpriteEffects.None, 0.01f);
                _spriteBatch.DrawString(_font, _WASDKeysButton.Text, new Vector2(x, y), Color.Black, 0f, new Vector2(5, -48), 1f, SpriteEffects.None, 0.01f);
            }

            if (!string.IsNullOrWhiteSpace(_MainMenuButton.Text))
            {
                var x = Rectangle.X + (Rectangle.Width / 2) - (_font.MeasureString(_MainMenuButton.Text).X / 2);
                var y = Rectangle.Y + (Rectangle.Height / 2) - (_font.MeasureString(_MainMenuButton.Text).Y / 2);

                _MainMenuButton.Position = new Vector2(x + 80, y);
                _spriteBatch.Draw(_quitButton.Texture, _MainMenuButton.Position, null, Color.White, 0f, new Vector2(107, -80), 1f, SpriteEffects.None, 0.01f);
                _spriteBatch.DrawString(_font, _MainMenuButton.Text, new Vector2(x, y), Color.Black, 0f, new Vector2(5, -88), 1f, SpriteEffects.None, 0.01f);
            }
        }

        private Rectangle Rectangle
        {
            get
            {
                return new Rectangle(0, 0, 800, 480);
            }
        }

    }
}
