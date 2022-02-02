using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game1
{
    //player sprites, normal enemy sprites, mid boss sprite, final boss sprite
    abstract class Sprite
    {
        private Texture2D _texture;
        public Vector2 _position;
        public float _speed;

        abstract public void Update();

        abstract public void Draw(SpriteBatch spriteBatch);

    }
}
