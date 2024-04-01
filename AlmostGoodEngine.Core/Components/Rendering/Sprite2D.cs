using AlmostGoodEngine.Extended;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlmostGoodEngine.Core.Components.Rendering
{
    public class Sprite2D : Sprite
    {
        /// <summary>
        /// The texture used by the sprite
        /// </summary>
        public Texture2D Texture { get; set; }

        /// <summary>
        /// Effects of the sprite (flip)
        /// </summary>
        public SpriteEffects Effect { get; set; } = SpriteEffects.None;

        /// <summary>
        /// The source rectangle inside the texture
        /// </summary>
        public Rectangle? Source
        {
            get => _source;

            set
            {
                _source = value;
                ComputeOrigin();
            }
        }
		private Rectangle? _source;

		/// <summary>
		/// The size of the texture
		/// </summary>
		public Vector2 Size
        {
            get
            {
                if (Texture != null)
                {
                    if (Source != null)
                    {
                        return new(Source.Value.Width, Source.Value.Height);
                    }

                    return new(Texture.Width, Texture.Height);
                }

                return Vector2.Zero;
            }
        }

		public override Rectangle GetBounds()
		{
            if (Texture == null)
            {
                return Rectangle.Empty;
            }

			return new((int)Owner.Position.X, (int)Owner.Position.Y, Texture.Width, Texture.Height);
		}

		/// <summary>
		/// Sprite's rotation
		/// </summary>
		public float Rotation { get; set; }

        /// <summary>
        /// The origin position of the sprite
        /// </summary>
        public Vector2 Origin { get; set; }

        /// <summary>
        /// The opacity of the sprite
        /// </summary>
        public float Opacity { get; set; }

        public Sprite2D(Texture2D texture = null)
        {
            Texture = texture;
            Source = null;

            Opacity = 1f;

            ComputeOrigin();
        }

        private void ComputeOrigin()
        {
            if (GameManager.Engine.Settings.OriginCentered && Texture != null)
            {
                if (Source != null)
                {
                    Origin = new(Source.Value.Width / 2, Source.Value.Height / 2);
                }
                else
                {
                    Origin = new(Texture.Width / 2, Texture.Height / 2);
                }
            }
            else
            {
                Origin = Vector2.Zero;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Owner == null)
            {
                return;
            }

            spriteBatch.Draw(Texture, Owner.Position.ToVector2().Rounded(), Source, Color.White * Opacity, Rotation, Origin, Owner.Scale.X, Effect, 1f);
        }
    }
}
