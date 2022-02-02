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
        public bool inReverse;

        public Enemy(Texture2D texture)
        {
            _texture = texture;
        }

        public void Update()
        {
            if (inReverse)
            {
                //change direction if going too far left
                if (_position.X < 20)
                {
                    inReverse = false;
                    _position.X += 2;
                }
                else
                    _position.X -= 2;
            }
            //change direction if going too far right
            else if (_position.X > 750)
            {
                inReverse = true;
                _position.X -= 2;

            }
            else
                _position.X += 2;

        }

        public void Draw(SpriteBatch spriteBatch)
            {
                Draw(spriteBatch, _texture, _position, Color.White);
            }
    }
}