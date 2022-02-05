using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace Game1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player player;
        private Enemy enemyA1;
        private Enemy enemyA2;
        private Enemy enemyA3;
        private Enemy enemyA4;
        private Enemy enemyA5;
        private Enemy enemyB1;
        private Enemy enemyB2;
        private Enemy bullet;
        private Song song1;

        // helpful properties
        public static GameTime GameTime { get; private set; }
        public static Game1 Instance { get; private set; }
        public static Viewport Viewport { get { return Instance.GraphicsDevice.Viewport; } }
        public static Vector2 ScreenSize { get { return new Vector2(Viewport.Width, Viewport.Height); } }


        public Game1()
        {
            Instance = this;
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            base.Initialize();            
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            player = new Player(Content.Load<Texture2D>("Alien-Battleship"), Content.Load<Texture2D>("fireball"));
            player._position = new Vector2(100, 100);

            bullet = new Enemy(Content.Load<Texture2D>("Bullet2"));
            enemyA1 = new Enemy(Content.Load<Texture2D>("dngn_blood_fountain"));
            enemyA2 = new Enemy(Content.Load<Texture2D>("dngn_blue_fountain"));
            enemyA3 = new Enemy(Content.Load<Texture2D>("dngn_dry_fountain"));
            enemyA4 = new Enemy(Content.Load<Texture2D>("dngn_blue_fountain"));
            enemyA5 = new Enemy(Content.Load<Texture2D>("dngn_blood_fountain"));

            //Butterfly enemies
            enemyB1 = new Enemy(Content.Load<Texture2D>("elephant_statue"));
            enemyB2 = new Enemy(Content.Load<Texture2D>("elephant_statue"));

            enemyA1._position = new Vector2(100, -50);
            enemyA2._position = new Vector2(140, -50);
            enemyA3._position = new Vector2(180, -50);
            enemyA4._position = new Vector2(220, -50);
            enemyA5._position = new Vector2(260, -50);
            enemyB1._position = new Vector2(100, -50);
            enemyB2._position = new Vector2(600, -50);
            enemyB1._isActive = false;
            enemyB2._isActive = false;
            enemyB2._inReverse = true;

            song1 = Content.Load<Song>("sample1");

            Sounds.Load(Content);

            Arts.Load(Content);

            
            //MediaPlayer.Play(song1);

            if(Game1.GameTime != null)
            {
                if (Game1.GameTime.ElapsedGameTime > TimeSpan.FromSeconds(30))
                {
                    SpriteManager.Add(MidBoss.Instance);
                }
            }
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            //game time is how much time has elapsed
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            player.Update();

            //Starts with EnemyA

            if (gameTime.TotalGameTime.TotalSeconds >= 5 && gameTime.TotalGameTime.TotalSeconds < 8)
            {
                //Exit enemyA, bring in enemyB
                enemyA1._inReverse = true;
                enemyA2._inReverse = true;
                enemyA3._inReverse = true;
                enemyA4._inReverse = true;
                enemyA5._inReverse = true;

                //Move in butterfly enemy
                enemyB1._isActive = true;
            }
            else if (gameTime.TotalGameTime.TotalSeconds >= 10) 
            {
                enemyB2._isActive = true;
            }

            enemyA1.Move(gameTime, Enemy.Type.A);
            enemyA2.Move(gameTime, Enemy.Type.A);
            enemyA3.Move(gameTime, Enemy.Type.A);
            enemyA4.Move(gameTime, Enemy.Type.A);
            enemyA5.Move(gameTime, Enemy.Type.A);
            enemyB1.Move(gameTime, Enemy.Type.B);
            enemyB2.Move(gameTime, Enemy.Type.B);

            SpriteManager.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            player.Draw(_spriteBatch);
            enemyA1.Draw(_spriteBatch);
            enemyA2.Draw(_spriteBatch);
            enemyA3.Draw(_spriteBatch);
            enemyA4.Draw(_spriteBatch);
            enemyA5.Draw(_spriteBatch);

            enemyB1.Draw(_spriteBatch);
            enemyB2.Draw(_spriteBatch);

            SpriteManager.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
