using AlmostGoodEngine.Animations;
using AlmostGoodEngine.Core.ECS;
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

            SpriteSheet.Draw(spriteBatch, Owner.Position.ToVector2().Rounded());
        }
    }
}
