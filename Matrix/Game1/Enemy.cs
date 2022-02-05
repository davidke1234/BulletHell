using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
/*
 * Copyright/Attribution Notice: 
You can use these tilesets in your program freely. No attribution is required. As a courtesy,
include a link to the OGA page: http://opengameart.org/content/dungeon-crawl-32x32-tiles,
the crawl-tiles page: http://code.google.com/p/crawl-tiles/ 
or see the License Notice instructions on http://rltiles.sourceforge.net/
 */
namespace Game1
{
    public class Enemy : Sprite
    {
        private Texture2D _texture;
        public Vector2 _position;
        public float _speed = 2f;
        public bool _inReverse;
        private GameTime _gameTime;
        private double _elapsedSeconds = 0;
        private Type _type = Type.A;
        public bool _hide;
        public bool _isActive = true;

        public enum Type
        {
            A,
            B,
            None
        }

        public Enemy(Texture2D texture)
        {
            _texture = texture;
        }

        public override void Update(GameTime gameTime)
        {
            if (_isActive)
            {
                if (_type == Type.B)
                {
                    if (_inReverse)
                        _position.X -= 2;
                    else
                        _position.X += 2;

                    if (_position.Y < 100)
                        _position.Y += 4;
                }

                else
                {  //Type A
                    if (_inReverse)
                    {
                        _position.X -= 1;
                        _position.Y -= 2;
                    }
                    else if (_position.Y < 100)
                    {
                        _position.X += 1;
                        _position.Y += 2;
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }

        internal void Move(GameTime gameTime, Type type) 
        {
            _type = type;
            if (_elapsedSeconds == 0)
                _elapsedSeconds = gameTime.TotalGameTime.TotalSeconds;
            this._gameTime = gameTime;

            this.Update(gameTime);
        }
    }
}