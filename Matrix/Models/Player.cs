using Matrix.Controllers;
using Matrix.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Matrix.Models
{
    public class Player : Sprite, ICollidable
    {
        public string PlayerName;
        public Sprite Bullet;
        public int Health { get; set; }
        public bool Die
        {
            get
            {
                return Health <= 0;
            }
        }

        
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

            if (sprite.IsRemoved)
                return;

            if (sprite is Bullet && ((Bullet)sprite).Parent is Enemy)
                AdjustHealth();
            else if (sprite is Bomb && (((Bomb)sprite).Parent is MidBoss || ((Bomb)sprite).Parent is FinalBoss))
                AdjustHealth();
        }

        private void AdjustHealth()
        {
            PlayerManager.AdjustPlayerHealth(-1);
            Respawn = true;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            _previousKey = _currentKey;
            _currentKey = Keyboard.GetState();

            if (Keyboard.GetState().IsKeyDown(GameKeys.Up) || Keyboard.GetState().IsKeyDown(GameKeys.UpUpperCase))
            {
                //move sprite up
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift))
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
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift))
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
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift))
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
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift))
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

            if (_currentKey.IsKeyDown(Keys.Space) && _previousKey.IsKeyUp(Keys.Space))
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
