using AlmostGoodEngine.Particles.Emitters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
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
		internal List<Particle> Particles { get; set; } = [];

		/// <summary>
		/// Based on the initial direction
		/// </summary>
		public float Spread { get; set; }

		/// <summary>
		/// The gravity
		/// </summary>
		public Vector2 Gravity { get; set; } = new Vector2(0f, 98f);

		/// <summary>
		/// The velocity of the particle when it spawn
		/// </summary>
		public Vector2 InitialVelocity { get; set; }

		/// <summary>
		/// The spin velocity of the particle
		/// </summary>
		public Vector2 SpinVelocity { get; set; }

		/// <summary>
		/// The orbit velocity
		/// </summary>
		public Vector2 OrbitVelocity { get; set; }

		/// <summary>
		/// The acceleration of the particle
		/// </summary>
		public Vector2 LinearAcceleration { get; set; }

		/// <summary>
		/// The target amount of particles generated per cycle
		/// </summary>
		public int Amount { get; set; }

		/// <summary>
		/// Retrieve the number of living particles
		/// </summary>
		public int Count { get =>  Particles.Count; }

		#region New particles parameters

		/// <summary>
		/// Lifetime of new particles
		/// </summary>
		public float Lifetime { get; set; }

		/// <summary>
		/// The texture of new particles
		/// </summary>
		public Texture2D Texture
		{
			get => _texture;
			set
			{
				_texture = value;
				ComputeCenter();
			}
		}
		private Texture2D _texture;

		/// <summary>
		/// The tint of the new particle
		/// </summary>
		public Color Tint { get; set; } = Color.White;

		/// <summary>
		/// The tint at the end of the particle life
		/// </summary>
		public Color TintOut { get; set; } = Color.Transparent;

		/// <summary>
		/// The scaling of the new particle
		/// </summary>
		public Vector2 Scale { get; set; }

		/// <summary>
		/// The scaling of the particle at the end of its lifetime
		/// </summary>
		public Vector2 ScaleOut { get; set; } = Vector2.One;

		/// <summary>
		/// If the particle should fade in
		/// </summary>
		public bool FadeIn { get; set; }

		/// <summary>
		/// If the particle should fade out
		/// </summary>
		public bool FadeOut { get; set; }

		/// <summary>
		/// If the fade out of the particle should be late
		/// </summary>
		public bool LateFade { get; set; }

		#endregion

		private float lastTimeSpawn = 0f;
		private float particleStep { get => 1f / (Amount / Lifetime); }

		/// <summary>
		/// The sprite which will be used to render particles
		/// </summary>
		private readonly SpriteBatch _spriteBatch = spriteBatch;

		/// <summary>
		/// Random instance
		/// </summary>
		private readonly Random _random = new();

		private void WhitePixel()
		{
			Texture = new Texture2D(_spriteBatch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
			Texture.SetData(new[] { Color.White });
			ComputeCenter();
		}

		public void Update(float delta)
		{
			for (int i = Particles.Count - 1; i >= 0; i--)
			{
				var particle = Particles[i];
				particle.Life += delta;
				
				if (particle.Life > Lifetime)
				{
					Particles.RemoveAt(i);
					continue;
				}

				particle.Position += particle.Velocity * delta;
				particle.Rotation += SpinVelocity.X * delta;
				particle.Velocity += LinearAcceleration * Gravity * delta;

				var t = particle.Life / Lifetime;
				if (FadeIn && t <= 0.2f)
				{
					particle.Opacity = MathHelper.Lerp(0f, 1f, t / 0.2f);
				}
				else if (FadeOut && t >= 0.8f)
				{
					particle.Opacity = MathHelper.Lerp(1f, 0f, (t - 0.8f) / 0.1f);
				}
				else
				{
					particle.Opacity = 1f;
				}

				if (TintOut != Color.Transparent)
				{
					particle.Tint = Color.Lerp(Tint, TintOut, t);
				}
			}

			if (lastTimeSpawn >= particleStep)
			{
				lastTimeSpawn -= particleStep;
				SpawnParticle();
			}
			else
			{
				lastTimeSpawn += delta;
			}
		}

		private void SpawnParticle()
		{
			Particle particle = new()
			{
				Velocity = InitialVelocity,
				Position = Emitter.Next(),
				Spin = RandomVec2(-SpinVelocity, SpinVelocity),
				Tint = Tint,
			};
			if (FadeIn)
			{
				particle.Opacity = 0f;
			}
			else
			{
				particle.Opacity = 1f;
			}

			Particles.Add(particle);
		}

		public Vector2 TextureCenter
		{
			get
			{
				return _textureCenter;
			}
			private set
			{
				_textureCenter = value;
			}
		}
		private Vector2 _textureCenter;

		private void ComputeCenter()
		{
			if (Texture == null)
			{
				TextureCenter = Vector2.Zero;
			}

			TextureCenter = new Vector2(Texture.Width, Texture.Height) * 0.5f;
		}

		private Vector2 RandomVec2(Vector2 min, Vector2 max)
		{
			return new(RandomFloat(min.X, max.X), RandomFloat(min.Y, max.Y));
		}

		private float RandomFloat(float min, float max)
		{
			return (float)_random.NextDouble() * (max - min) + min;
		}

		/// <summary>
		/// Draw the particle system. The sprite batch should already been opened.
		/// </summary>
		public void Draw()
		{
			if (Texture == null)
			{
				WhitePixel();
			}

			foreach (var particle in Particles)
			{
				var position = new Vector2((int)particle.Position.X, (int)particle.Position.Y);
				var t = particle.Life / Lifetime;

				var finalScale = Vector2.Lerp(Scale, ScaleOut, t);

				_spriteBatch.Draw(Texture, position, null, particle.Tint, particle.Rotation, TextureCenter, finalScale.X, SpriteEffects.None, 1f);
			}
		}
	}
}
