using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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

            var player = Content.Load<Texture2D>("Alien-Battleship");
            var enemy1 = Content.Load<Texture2D>("dngn_blood_fountain");
            var enemy2 = Content.Load<Texture2D>("dngn_blue_fountain");
            var enemy3 = Content.Load<Texture2D>("dngn_dry_fountain");
            var enemyButterfly = Content.Load<Texture2D>("elephant_statue");
            var song1 = Content.Load<Song>("sample1");
            //MediaPlayer.Play(song1);
            Sounds.Load(Content);
            Arts.Load(Content);

            _sprites = new List<SpriteNew>()
            {
                new Player(player) 
                    { Position = new Vector2(375, 335), 
                    Bullet = new Bullet(Content.Load<Texture2D>("Bullet"))},
                new Enemy(enemy1) 
                    {Position = new Vector2(100,50), 
                    Bullet = new Bullet(Content.Load<Texture2D>("Bullet"))},
                new Enemy(enemy2) 
                    {Position = new Vector2(150,50), 
                    Bullet = new Bullet(Content.Load<Texture2D>("Bullet"))},
                new Enemy(enemy3) 
                    {Position = new Vector2(200,50), 
                    Bullet = new Bullet(Content.Load<Texture2D>("Bullet"))},
                new Enemy(enemyButterfly) 
                    {Position = new Vector2(250,50), 
                    Bullet = new Bullet(Content.Load<Texture2D>("Bullet"))}
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
 
            if (gameTime.TotalGameTime.TotalSeconds >= 20)
            {
                SpriteManager.Add(MidBoss.Instance);
            }

            if (gameTime.TotalGameTime.TotalSeconds >= 40)
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
    }
}
