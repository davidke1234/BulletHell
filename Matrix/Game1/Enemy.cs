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

        public Enemy(Texture2D texture)
        {
            _texture = texture;
        }

        public override void Update()
        {

            if (_position.Y < 100)
                _position.Y += 2;
   
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }
    }
}