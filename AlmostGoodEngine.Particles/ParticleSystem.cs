using AlmostGoodEngine.Particles.Emitters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace AlmostGoodEngine.Particles
{
	public class ParticleSystem
	{
		public Emitter Emitter { get; set; }

		public List<Particle> Particles { get; set; }

		public ParticleSystem(Emitter emitter)
		{
			Particles = [];
			Emitter = emitter;
		}

		public void Update(float delta)
		{
			for (int i = Particles.Count - 1; i >= 0; i--)
			{
				var particle = Particles[i];
				particle.Life++;
				if (particle.Life > particle.Duration)
				{
					Particles.RemoveAt(i);
					continue;
				}

				// TODO: compute the final force
				particle.Update(Vector3.Zero);
			}
		}

		private void SpawnParticle()
		{
			Particle particle = new()
			{

			};
			particle.Position = Emitter.Next();
		}

		/// <summary>
		/// Draw all the particles
		/// </summary>
		/// <param name="spriteBatch"></param>
		/// <param name="transformMatrix"></param>
		public void Draw(SpriteBatch spriteBatch, Matrix transformMatrix)
		{
			spriteBatch.Begin(transformMatrix: transformMatrix);

			foreach (var particle in Particles)
			{
				particle.Draw(spriteBatch);
			}

			spriteBatch.End();
		}
	}
}
