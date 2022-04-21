using Microsoft.Xna.Framework.Input;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Matrix.Models;

namespace Matrix.Controllers
{
    public static class MenuManager
    {
        private static MouseState _currentMouse;
        private static MouseState _previousMouse;
        public static List<HighScore> HighScores = new List<HighScore>();

        public static void DisplayHighScores(SpriteBatch spriteBatch)
        {
            string returnVal = "";
            if (HighScores.Count == 0)
            {
                HighScores = DataAccessLayer.GetHighScores(ref returnVal);
            }

            float xValue = 10f;
            float yValue = 10f;
            spriteBatch.DrawString(Arts.Font, "High Scores:", new Vector2(xValue, yValue), Color.Yellow);

            for (int i = 0; i < HighScores.Count; i++)
            {
                yValue += 17f;
                spriteBatch.DrawString(Arts.Font, "Player: " + HighScores[i].Name + " " + HighScores[i].Score, new Vector2(xValue, yValue), Color.White);
            }
        }

        public static void SetupConfigMenu(ref Button arrowKeysButton, ref Button WASDKeysButton, ref Button escKeyCheatButton, ref Button mainMenuButton,  Game game1)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            Rectangle mouseRectangle = new Rectangle (_currentMouse.X, _currentMouse.Y, 1, 1);

            if (mouseRectangle.Intersects(Rectangle))
            {
                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {
                    if (mouseRectangle.Top >= 233 && mouseRectangle.Top <= 255)
                        arrowKeysButton.Click?.Invoke(game1, new EventArgs());
                    else if (mouseRectangle.Top >= 273 && mouseRectangle.Top <= 297)
                        WASDKeysButton.Click?.Invoke(game1, new EventArgs());
                    else if (mouseRectangle.Top >= 313 && mouseRectangle.Top <= 337)
                        escKeyCheatButton.Click?.Invoke(game1, new EventArgs());
                    else if (mouseRectangle.Top >= 353 && mouseRectangle.Top <= 377)
                        mainMenuButton.Click?.Invoke(game1, new EventArgs());
                }
            }
        }

        public static void SetupMainMenu(ref Button _startButton, ref Button _configButton, ref Button _quitButton, Game1 game1)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            if (mouseRectangle.Intersects(Rectangle))
            {
                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {
                    if (mouseRectangle.Top >= 233 && mouseRectangle.Top <= 255)
                        _startButton.Click?.Invoke(game1, new EventArgs());
                    else if (mouseRectangle.Top >= 273 && mouseRectangle.Top <= 297)
                        _configButton.Click?.Invoke(game1, new EventArgs());
                    else if (mouseRectangle.Top >= 313 && mouseRectangle.Top <= 337)
                        _quitButton.Click?.Invoke(game1, new EventArgs());
                }
            }
        }

        public static Button MakeButton(Texture2D buttonTexture, SpriteFont buttonFont, string buttonText, Action<object, EventArgs> eventHandler)
        {
            Button bStart = new Button(buttonTexture, buttonFont);
            bStart.Text = buttonText;
            bStart.Click = new EventHandler(eventHandler);
            bStart.Layer = 0.1f;
            bStart.Texture = buttonTexture;
            return bStart;
        }

        private static Rectangle Rectangle
        {
            get
            {
                return new Rectangle(0, 0, 800, 480);
            }
        }

        public static float GetButtonXPosition(string text)
        {
            return Rectangle.X + (Rectangle.Width / 2) - (Arts.Font.MeasureString(text).X / 2);
        }

        public static float GetButtonYPosition(string text)
        {
            return Rectangle.Y + (Rectangle.Height / 2) - (Arts.Font.MeasureString(text).Y / 2);
        }
      
    }
}
