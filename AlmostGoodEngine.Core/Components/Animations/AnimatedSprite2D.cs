using AlmostGoodEngine.Animations;
using AlmostGoodEngine.Core.ECS;
using AlmostGoodEngine.Core.Utils;
using AlmostGoodEngine.Extended;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlmostGoodEngine.Core.Components.Animations
{
    public class AnimatedSprite2D : Component
    {
        /// <summary>
        /// The spritesheet used by the component
        /// </summary>
        public SpriteSheet SpriteSheet { get; set; }

        public AnimatedSprite2D(int frameWidth, int frameHeight, int frames, Texture2D texture, bool infinite = true)
        {
            SpriteSheet = new(frameWidth, frameHeight, frames, texture)
            {
                Infinite = infinite
            };

            if (Owner != null)
            {
                SpriteSheet.Scale = Owner.Scale.X;
            }
        }

		public override Rectangle GetBounds()
		{
            return new(
                (int)Owner.Position.X - (int)(SpriteSheet.FrameWidth / 2 * Owner.Scale.X), 
                (int)Owner.Position.Y - (int)(SpriteSheet.FrameHeight / 2 * Owner.Scale.Y), 
                (int)(SpriteSheet.FrameWidth * Owner.Scale.X), 
                (int)(SpriteSheet.FrameHeight * Owner.Scale.Y));
		}

		public override void Start()
        {
            base.Start();
            SpriteSheet.Start();
        }

        public override void End()
        {
            base.End();
            SpriteSheet.Reset();
        }

        public override void AfterUpdate(GameTime gameTime)
        {
            SpriteSheet.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Owner == null)
            {
                return;
            }

            Color color = Color.White;
            if (IsMouseHovering)
            {
                color = Color.Red;
            }
            else if (IsMouseDown)
            {
                color = Color.BlueViolet;
            }

			SpriteSheet.Draw(spriteBatch, Owner.Position.ToVector2().Rounded(), color);
			//Debug.Rectangle(spriteBatch, GetBounds(), Color.Green, 2);
		}
    }
}
