using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Matrix
{
    class Bombs : Sprite, ICollidable
    {
        private static Bombs _instance;

        public Bombs(Texture2D texture)
: base(texture)
        { }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {

        }  

        public void OnCollide(Sprite sprite)
        {
            throw new NotImplementedException();
        }
    }
}
