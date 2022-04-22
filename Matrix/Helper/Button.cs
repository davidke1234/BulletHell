using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Matrix
{
    public class Button
  {
    #region Fields

    private SpriteFont _font;
    private Texture2D _texture;
    public Texture2D Texture;

    #endregion

    #region Properties

    public EventHandler Click;

    public float Layer { get; set; }

    public Color PenColour { get; set; }

    public Vector2 Position { get; set; }

    public string Text;

        #endregion

        #region Methods

        public Button(Texture2D texture, SpriteFont font)
        {
            Texture = texture;
            _texture = texture;

            _font = font;

            PenColour = Color.Black;
        }

        #endregion
    }
}
