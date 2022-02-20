using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Matrix
{
    public class Player : Sprite, ICollidable
    {
        public Bullet Bullet;
        public int Health { get; set; }
        public bool IsDead
        {
            get
            {
                return Health <= 0;
            }
        }
        
        public Score Score { get; set; }

        public Player(Texture2D texture)
      : base(texture)
        { }

        public Player(Texture2D playerTexture, Texture2D slowmoTexture) : base(playerTexture, slowmoTexture) { }

        public void OnCollide(Sprite sprite)
        {
            if (IsDead)
                return;

           

            if (sprite is Bullet && ((Bullet)sprite).Parent is Enemy && sprite.Name=="Bomb")
                Health -= 2;

            else if (sprite is Bullet && ((Bullet)sprite).Parent is Enemy && sprite.Name == "Bomb2")
                Health -= 4;

            else if (sprite is Bullet && ((Bullet)sprite).Parent is Enemy)
                Health--;

           else if (sprite is Bomb && (((Bomb)sprite).Parent is MidBoss || ((Bomb)sprite).Parent is FinalBoss))
                Health -= 2;

            //If player collides with enemy
            if (sprite is Enemy)
                Health--;
        }
        
        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            _previousKey = _currentKey;
            _currentKey = Keyboard.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                //move sprite up
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    if (Position.Y > 50)
                    {
                        this.currentState = SpriteState.slowmoSprite;
                        Position -= YVelocitySlow;
                    }
                }
                else
                {
                    if (Position.Y > 50)
                    {
                        Position -= YVelocity;
                    }
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                //move sprite down
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    if (Position.Y < 421)
                    {
                        this.currentState = SpriteState.slowmoSprite;
                        Position += YVelocitySlow;
                    }
                }
                else
                {
                    if (Position.Y < 421)
                    {
                        this.currentState = SpriteState.normalSprite;
                        Position += YVelocity;
                    }
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                //move sprite left
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    if (Position.X > 55)
                    {
                        this.currentState = SpriteState.slowmoSprite;
                        Position -= XVelocitySlow;
                    }
                }
                else
                {
                    if (Position.X > 55)
                    {
                        this.currentState = SpriteState.normalSprite;
                        Position -= XVelocity;
                    }
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                //move sprite right
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    if (Position.X < 740)
                    {
                        this.currentState = SpriteState.slowmoSprite;
                        Position += XVelocitySlow;
                    }
                }
                else
                {
                    if (Position.X < 740)
                    {
                        this.currentState = SpriteState.normalSprite;
                        Position += XVelocity;
                    }
                }
            }

            if (_currentKey.IsKeyDown(Keys.F) &&
          _previousKey.IsKeyUp(Keys.F))
            {
                //Used for bullets
                Direction.X = 0;
                Direction.Y = -.9f;

                AddBullet(sprites);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (IsDead)
                return;

            base.Draw(gameTime, spriteBatch);
        }

        private void AddBullet(List<Sprite> sprites)
        {
            var bullet = Bullet.Clone() as Bullet;
            bullet.Direction = this.Direction;
            bullet.Position = this.Position;
            bullet.LinearVelocity = this.LinearVelocity * 2;
            bullet.LifeSpan = 2f;
            bullet.Parent = this;

            sprites.Add(bullet);
        }
    }
}
