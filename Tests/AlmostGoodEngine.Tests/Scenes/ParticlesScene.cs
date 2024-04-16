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
			var boxEmitter = new BoxEmitter(Vector3.Zero, new Vector3(300, 200, 1));
			var particleSystem = new ParticleSystem(boxEmitter, GameManager.SpriteBatch)
			{
				Lifetime = 4f,
				InitialVelocity = new Vector3(0, 1, 0),
				LinearAcceleration = new Vector3(0, 1, 0),
				Amount = 50
			};

			Entity entity = new();
			entity.AddComponent(new ParticleSystemComponent(particleSystem));

			AddEntity(entity);
		}
	}
}
