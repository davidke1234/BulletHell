using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Matrix
{
    public class Bomb : Sprite
    {
        private float _timer;

        public Bomb(Texture2D texture): base(texture)
        { 
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
        }
    }
}
