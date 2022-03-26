using Matrix.Models;
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
        public bool Die
        {
            get
            {
                return Health <= 0;
            }
        }
        MouseState _prevMouseState;
        MouseState _currMouseState;
        
        public Score Score { get; set; }
        public GameKeys GameKeys { get; set; }
        public bool Respawn { get; set; }

        public Player(Texture2D texture)
      : base(texture)
        { }

        public Player(Texture2D playerTexture, Texture2D slowmoTexture) : base(playerTexture, slowmoTexture) { }

        public void OnCollide(Sprite sprite)
        {
            if (Die)
                return;

            if (sprite is Bullet && ((Bullet)sprite).Parent is Enemy && sprite.Name == "Bomb")
                AdjustHealth();

            else if (sprite is Bullet && ((Bullet)sprite).Parent is Enemy && sprite.Name == "Bomb2")
                AdjustHealth();

            else if (sprite is Bullet && ((Bullet)sprite).Parent is Enemy)
                AdjustHealth();

            else if (sprite is Bomb && (((Bomb)sprite).Parent is MidBoss || ((Bomb)sprite).Parent is FinalBoss))
                AdjustHealth();

            //If player collides with enemy
            if (sprite is Enemy)
                AdjustHealth();
        }

        private void AdjustHealth()
        {
            Health--;
            Respawn = true;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            _previousKey = _currentKey;
            _currentKey = Keyboard.GetState();
            _prevMouseState = _currMouseState;
            _currMouseState = Mouse.GetState();

            if (Keyboard.GetState().IsKeyDown(GameKeys.Up) || Keyboard.GetState().IsKeyDown(GameKeys.UpUpperCase))
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
            if (Keyboard.GetState().IsKeyDown(GameKeys.Down) || Keyboard.GetState().IsKeyDown(GameKeys.DownUpperCase))
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
            if (Keyboard.GetState().IsKeyDown(GameKeys.Left) || Keyboard.GetState().IsKeyDown(GameKeys.LeftUpperCase))
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
            if (Keyboard.GetState().IsKeyDown(GameKeys.Right) || Keyboard.GetState().IsKeyDown(GameKeys.RightUpperCase))
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


           

            //if (_currentKey.IsKeyDown(Keys.F) && _previousKey.IsKeyUp(Keys.F))
            if(_currMouseState.LeftButton == ButtonState.Pressed && _prevMouseState.LeftButton == ButtonState.Released)
            {
                //Used for bullets
                Direction.X = 0;
                Direction.Y = -.9f;

                AddBullet(sprites);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Die)
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
