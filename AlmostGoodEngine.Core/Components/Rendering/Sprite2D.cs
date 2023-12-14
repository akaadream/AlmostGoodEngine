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

        private Rectangle? _source;

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
            if (GameManager.Game.Settings.OriginCentered && Texture != null)
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

            spriteBatch.Draw(Texture, Owner.Position.ToVector2().Rounded(), Source, Color.White * Opacity, Rotation, Origin, Owner.Scale.X, SpriteEffects.None, 1f);
        }
    }
}
