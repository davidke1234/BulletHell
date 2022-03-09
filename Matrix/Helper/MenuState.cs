using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Matrix
{
    public class MenuState : State
    {
        private List<Component> _components;

        public MenuState(Game1 game, ContentManager content)
          : base(game, content)
        {
        }

        public override void LoadContent()
        {
            var buttonTexture = _content.Load<Texture2D>("Button");
            var buttonFont = _content.Load<SpriteFont>("Font");
            var screen = _content.Load<Texture2D>("Background/MainMenu");

            _components = new List<Component>();
            Sprite sprite = new Sprite(screen);
            sprite.Layer = 0f;
            sprite.Position = new Vector2(Game1.ScreenWidth / 2, Game1.ScreenHeight / 2);

            Button bStart = new Button(buttonTexture, buttonFont);
            bStart.Text = "Start Game";
            bStart.Position = new Vector2(Game1.ScreenWidth / 2, 400);
            bStart.Click = new EventHandler(Button_1Player_Clicked);
            bStart.Layer = 0.1f;

            Button bQuit = new Button(buttonTexture, buttonFont);
            bQuit.Text = "Quit Game";
            bQuit.Position = new Vector2(Game1.ScreenWidth / 2, 520);
            bQuit.Click = new EventHandler(Button_Quit_Clicked);
            bQuit.Layer = 0.1f;
        }


    private void Button_1Player_Clicked(object sender, EventArgs args)
    {
      _game.ChangeState(new GameState(_game, _content)
      {
        PlayerCount = 1,
      });
    }

    private void Button_Quit_Clicked(object sender, EventArgs args)
    {
      _game.Exit();
    }

    public override void Update(GameTime gameTime)
    {
      foreach (var component in _components)
        component.Update(gameTime);
    }

    public override void PostUpdate(GameTime gameTime)
    {

    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
      spriteBatch.Begin(SpriteSortMode.FrontToBack);

      foreach (var component in _components)
        component.Draw(gameTime, spriteBatch);

      spriteBatch.End();
    }
  }
}
