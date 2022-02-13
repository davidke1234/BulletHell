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


namespace Matrix
{
    public class Player
    {
        private Texture2D _characterTexture;
        private Texture2D _projectileTexture;
        public Vector2 _position;
        public float _speed = 2f;

        public Player(Texture2D characterTexture, Texture2D projectileTexture)
        {
            _characterTexture = characterTexture;
            _projectileTexture = projectileTexture;
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
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                //move sprite up
                _position.Y -= _speed * 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down) && Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                //move sprite down
                _position.Y += _speed * 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left) && Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                //move sprite left
                _position.X -= _speed * 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right) && Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                //move sprite right
                _position.X += _speed * 2;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
            {
                spriteBatch.Draw(_characterTexture, _position, Color.White);
            }
    }
}