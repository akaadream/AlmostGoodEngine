using AlmostGoodEngine.Core;
using AlmostGoodEngine.Core.Components.Particles;
using AlmostGoodEngine.Core.ECS;
using AlmostGoodEngine.Core.Scenes;
using AlmostGoodEngine.Particles;
using AlmostGoodEngine.Particles.Emitters;
using Microsoft.Xna.Framework;

namespace AlmostGoodEngine.Tests.Scenes
{
	public class ParticlesScene : Scene
	{
		public ParticlesScene()
		{
			var boxEmitter = new PointEmitter(new Vector2(200, 200));
			var particleSystem = new ParticleSystem(boxEmitter, GameManager.SpriteBatch)
			{
				Lifetime = 4f,
				InitialVelocity = new Vector2(0f, 50f),
				LinearAcceleration = new Vector2(0f, 1.4f),
				SpinVelocity = new Vector2(32f, 0),
				Gravity = new Vector2(0f, 1f),
				Scale = new Vector2(8, 8),
				Amount = 40,
				Tint = Color.Red,
				TintOut = Color.Green,
				FadeIn = true,
				FadeOut = true,
			};

			Entity entity = new();
			entity.AddComponent(new ParticleSystemComponent(particleSystem));

			AddEntity(entity);
		}
	}
}
