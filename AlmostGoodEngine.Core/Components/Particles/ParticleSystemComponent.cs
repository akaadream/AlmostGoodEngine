using AlmostGoodEngine.Core.ECS;
using AlmostGoodEngine.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlmostGoodEngine.Core.Components.Particles
{
	public class ParticleSystemComponent(ParticleSystem system) : Component
	{
		public ParticleSystem ParticleSystem { get; set; } = system;

		public override Rectangle GetBounds()
		{
			return ParticleSystem.Emitter.Bounds();
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			ParticleSystem.Update(Time.DeltaTime);
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			base.Draw(gameTime, spriteBatch);

			ParticleSystem.Draw();
		}
	}
}
