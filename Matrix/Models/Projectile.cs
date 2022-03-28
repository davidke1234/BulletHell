using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using XnaMatrix = Microsoft.Xna.Framework.Matrix;

namespace Matrix.Models
{
    public class Projectile : Sprite
    {        
        public Bullet Bullet;
        public float TimerStart = 1.25f;
        public float Speed = 2f;

        public int Health;

        public Projectile(Texture2D texture2D)
        : base(texture2D)
        { }


        public Projectile(Texture2D texture, Texture2D slowmoTexture)
        : base(texture, slowmoTexture) { }

        //Note: overridden in each class
        public override void Update(GameTime gameTime, List<Sprite> sprites)
        { }      
    }
}
