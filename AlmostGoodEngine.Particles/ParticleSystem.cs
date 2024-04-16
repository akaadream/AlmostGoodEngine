using AlmostGoodEngine.Particles.Emitters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace AlmostGoodEngine.Particles
{
	public class ParticleSystem(Emitter emitter, SpriteBatch spriteBatch)
	{
		/// <summary>
		/// The particle emitter
		/// </summary>
		public Emitter Emitter { get; set; } = emitter;

		/// <summary>
		/// The list of currently living entities
		/// </summary>
		public List<Particle> Particles { get; set; } = [];

		/// <summary>
		/// Based on the initial direction
		/// </summary>
		public float Spread { get; set; }

		/// <summary>
		/// The gravity
		/// </summary>
		public Vector3 Gravity { get; set; }

		/// <summary>
		/// The velocity of the particle when it spawn
		/// </summary>
		public Vector3 InitialVelocity { get; set; }

		/// <summary>
		/// The spin velocity of the particle
		/// </summary>
		public Vector3 SpinVelocity { get; set; }

		/// <summary>
		/// The orbit velocity
		/// </summary>
		public Vector3 OrbitVelocity { get; set; }

		/// <summary>
		/// The acceleration of the particle
		/// </summary>
		public Vector3 LinearAcceleration { get; set; }

		/// <summary>
		/// The amount of particles living at the same time
		/// </summary>
		public int Amount { get; set; }

		#region New particles parameters
		/// <summary>
		/// Lifetime of new particles
		/// </summary>
		public float Lifetime { get; set; }

		/// <summary>
		/// The texture of new particles
		/// </summary>
		public Texture2D Texture { get; set; }

		#endregion

		/// <summary>
		/// The sprite which will be used to render particles
		/// </summary>
		private readonly SpriteBatch _spriteBatch = spriteBatch;

		public void Update(float delta)
		{
			for (int i = Particles.Count - 1; i >= 0; i--)
			{
				var particle = Particles[i];
				particle.Life += delta;
				if (particle.Life > particle.Lifetime)
				{
					Particles.RemoveAt(i);
					continue;
				}

				particle.Update(delta, GetForce());
			}

			if (Particles.Count >= Amount)
			{
				return;
			}

			// If there is not enough particles inside the world, spawn new particles
			for (int i = 0; i < Amount - Particles.Count; i++)
			{
				SpawnParticle();
			}
		}

		private void SpawnParticle()
		{
			Particle particle = new(_spriteBatch, Texture)
			{
				Lifetime = Lifetime,
				Velocity = InitialVelocity,
				Position = Emitter.Next()
			};
			Particles.Add(particle);
		}

		private Vector3 GetForce()
		{
			return Gravity * LinearAcceleration;
		}

		/// <summary>
		/// Draw all the particles
		/// </summary>
		/// <param name="transformMatrix"></param>
		public void Draw(Matrix transformMatrix)
		{
			foreach (var particle in Particles)
			{
				particle.Draw();
			}
		}
	}
}
