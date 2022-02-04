using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

/*
* Copyright/Attribution Notice: 
You can use these tilesets in your program freely. No attribution is required. As a courtesy,
include a link to the OGA page: https://opengameart.org/content/fireball-projectile
*/


namespace Game1
{
    public class Player
    {
        private Texture2D _texture;
        public Vector2 _position;
        public float _speed = 2f;

        public Player(Texture2D characterTexture, Texture2D projectileTexture)
        {
            _texture = characterTexture;
        }

        public void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                //move sprite up
                _position.Y -= _speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                //move sprite down
                _position.Y += _speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                //move sprite left
                _position.X -= _speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                //move sprite right
                _position.X += _speed;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
            {
                spriteBatch.Draw(_texture, _position, Color.White);
            }
    }
}