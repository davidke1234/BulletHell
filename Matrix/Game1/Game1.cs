using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Game1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player player;
        private Enemy enemy1;
        private Enemy enemy2;
        private Enemy enemy3;
        private Enemy enemy4;
        private Enemy enemy5;
        private Song song1;


        public Game1()
        {
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
            player = new Player(Content.Load<Texture2D>("Alien-Battleship"));
            player._position = new Vector2(100, 100);
            enemy1 = new Enemy(Content.Load<Texture2D>("dngn_blood_fountain"));
            enemy2 = new Enemy(Content.Load<Texture2D>("dngn_blue_fountain"));
            enemy3 = new Enemy(Content.Load<Texture2D>("dngn_dry_fountain"));
            enemy4 = new Enemy(Content.Load<Texture2D>("dngn_trap_shaft"));
            enemy5 = new Enemy(Content.Load<Texture2D>("elephant_statue"));
            enemy1._position = new Vector2(0, 0);
            enemy2._position = new Vector2(40, 0);
            enemy3._position = new Vector2(80, 0);
            enemy4._position = new Vector2(120, 0);
            enemy5._position = new Vector2(160, 0);
            song1 = Content.Load<Song>("sample1");
            MediaPlayer.Play(song1);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            //game time is how much time has elapsed
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            player.Update();
            enemy1.Update();
            enemy2.Update();
            enemy3.Update();
            enemy4.Update();
            enemy5.Update();
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            player.Draw(_spriteBatch);
            enemy1.Draw(_spriteBatch);
            enemy2.Draw(_spriteBatch);
            enemy3.Draw(_spriteBatch);
            enemy4.Draw(_spriteBatch);
            enemy5.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
