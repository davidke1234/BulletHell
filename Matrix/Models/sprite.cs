using Matrix.Movements;
using Matrix.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using XnaMatrix= Microsoft.Xna.Framework.Matrix;

namespace Matrix.Models
{
    public enum SpriteState
    {
        normalSprite,
        slowmoSprite
    }

    public class Sprite : ICloneable
    {
        public SpriteState currentState = SpriteState.normalSprite;
        public Texture2D _texture;
        protected KeyboardState _currentKey;
        protected KeyboardState _previousKey;
        public Vector2 Position;
        public Vector2 Origin;
        public Vector2 Direction;
        public Vector2 Velocity;
        public Vector2 YVelocity = new Vector2(0, 2);
        public Vector2 XVelocity = new Vector2(2, 0);
        public Vector2 YVelocitySlow = new Vector2(0, 1);
        public Vector2 XVelocitySlow = new Vector2(1, 0);
        public IMovement Movement;
        private Texture2D _slowmoSprite;
        public float LinearVelocity = 4f;
        public Sprite Parent;
        public float LifeSpan = 0f;
        public bool IsRemoved = false;
        public string Name;
        public Color Colour = Color.White;
        public Color[] TextureData;
        public Random _random = new Random();
        protected float _rotation { get; set; }
        public float SpawnSeconds = 0f;
        public IMovement straightMovement = new StraightMovement();
        public IMovement randomMovement = new RandomMovement();

        public XnaMatrix Transform
        {
            get
            {
                return XnaMatrix.CreateTranslation(new Vector3(-Origin, 0)) *
                  XnaMatrix.CreateRotationZ(_rotation) *
                  XnaMatrix.CreateTranslation(new Vector3(Position, 0));
            }
        }


        public Rectangle Rectangle
        {
            get
            {
                if (_texture != null)
                {
                    if (this.Name == "player_ship")
                    {
                        //shrink hit box, move x to right, shrink width
                        int xValue = 30;
                        int yValue = 0;
                        int widthValue = -30;
                        int heightValue = 0;
                        return new Rectangle((int)Position.X - (int)Origin.X + xValue, (int)Position.Y - (int)Origin.Y + yValue, _texture.Width + widthValue, _texture.Height + heightValue);
                    }
                    else
                    {
                        return new Rectangle((int)Position.X - (int)Origin.X, (int)Position.Y - (int)Origin.Y, _texture.Width, _texture.Height);
                    }
                }

                throw new Exception("sprite not found");
            }
        }
        public Rectangle CollisionArea
        {
            get
            {
                return new Rectangle(Rectangle.X, Rectangle.Y, MathHelper.Max(Rectangle.Width, Rectangle.Height), MathHelper.Max(Rectangle.Width, Rectangle.Height));
            }
        }

        public float Layer { get; internal set; }

        public Sprite(Texture2D texture)
        {
            _texture = texture;
            Name = texture.Name.ToLower();

            //Children = new List<Sprite>();

            // The default origin in the centre of the sprite
            Origin = new Vector2(_texture.Width / 2, _texture.Height / 2);

            Colour = Color.White;

            TextureData = new Color[_texture.Width * _texture.Height];
            _texture.GetData(TextureData);
        }

        public Sprite(Texture2D texture, Texture2D slowmoTexture)
        {
            _texture = texture;
            _slowmoSprite = slowmoTexture;
            Name = texture.Name.ToLower();

            //Children = new List<Sprite>();

            // The default origin in the centre of the sprite
            Origin = new Vector2(_texture.Width / 2, _texture.Height / 2);

            Colour = Color.White;

            TextureData = new Color[_texture.Width * _texture.Height];
            _texture.GetData(TextureData);
        }

        //Note: overridden in each class
        public virtual void Update(GameTime gameTime, List<Sprite> sprites)
        { }


        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            switch (currentState)
            {
                case SpriteState.normalSprite:
                    spriteBatch.Draw(_texture, Position, null, Colour, 0f, Origin, 1f, SpriteEffects.None, 0);
                    break;
                case SpriteState.slowmoSprite:
                    spriteBatch.Draw(_slowmoSprite, Position, null, Colour, 0f, Origin, 1f, SpriteEffects.None, 0);
                    break;
            }
        }
        public bool Intersects(Sprite sprite)
        {
            if (this.TextureData == null)
                return false;

            if (sprite.TextureData == null)
                return false;

            // Calculate a matrix which transforms from A's local space into
            // world space and then into B's local space
            var transformAToB = this.Transform * XnaMatrix.Invert(sprite.Transform);

            // When a point moves in A's local space, it moves in B's local space with a
            // fixed direction and distance proportional to the movement in A.
            // This algorithm steps through A one pixel at a time along A's X and Y axes
            // Calculate the analogous steps in B:
            var stepX = Vector2.TransformNormal(Vector2.UnitX, transformAToB);
            var stepY = Vector2.TransformNormal(Vector2.UnitY, transformAToB);

            // Calculate the top left corner of A in B's local space
            // This variable will be reused to keep track of the start of each row
            var yPosInB = Vector2.Transform(Vector2.Zero, transformAToB);

            for (int yA = 0; yA < this.Rectangle.Height; yA++)
            {
                // Start at the beginning of the row
                var posInB = yPosInB;

                for (int xA = 0; xA < this.Rectangle.Width; xA++)
                {
                    // Round to the nearest pixel
                    var xB = (int)Math.Round(posInB.X);
                    var yB = (int)Math.Round(posInB.Y);

                    if (0 <= xB && xB < sprite.Rectangle.Width &&
                        0 <= yB && yB < sprite.Rectangle.Height)
                    {
                        // Get the colors of the overlapping pixels
                        var colourA = this.TextureData[xA + yA * this.Rectangle.Width];
                        var colourB = sprite.TextureData[xB + yB * sprite.Rectangle.Width];

                        // If both pixel are not completely transparent
                        if (colourA.A != 0 && colourB.A != 0)
                        {
                            return true;
                        }
                    }

                    // Move to the next pixel in the row
                    posInB += stepX;
                }

                // Move to the next row
                yPosInB += stepY;
            }

            // No intersection found
            return false;
        }

        public void SetMovement(IMovement newMovement)
        {
            this.Movement = newMovement;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
