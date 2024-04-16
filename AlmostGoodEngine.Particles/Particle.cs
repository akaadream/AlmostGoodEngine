using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlmostGoodEngine.Particles
{
	public class Particle
	{
		/// <summary>
		/// Life of the particle
		/// </summary>
		public float Life { get; set; }

		/// <summary>
		/// The lifetime of the particle
		/// </summary>
		public float Lifetime { get; set; }

		/// <summary>
		/// The current position of the particle
		/// </summary>
		public Vector3 Position { get; set; }

		/// <summary>
		/// The current velocity of the particle
		/// </summary>
		public Vector3 Velocity { get; set; }

		/// <summary>
		/// The texture used as the particle. White pixel if we asign nothing to this instance
		/// </summary>
		public Texture2D Texture { get; set; }

		/// <summary>
		/// The tint of the particle
		/// </summary>
		public Color Tint { get; set; } = Color.White;

		/// <summary>
		/// The sprite batch used to draw this particle
		/// </summary>
		private readonly SpriteBatch _spriteBatch;

		/// <summary>
		/// Default constructor of the particle. Using a white pixel as texture by default
		/// </summary>
		public Particle(SpriteBatch spriteBatch, Texture2D texture = null)
		{
			_spriteBatch = spriteBatch;

			if (texture == null)
			{
				Texture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
				Texture.SetData(new[] { Color.White });
			}
			else
			{
				Texture = texture;
			}
		}

		public void Update(float delta, Vector3 finalForce)
		{
			Position += Velocity * finalForce * delta;
		}

		public void Draw()
		{
			if (Texture == null || _spriteBatch == null)
			{
				return;
			}

			_spriteBatch.Draw(Texture, new Vector2((int)Position.X, (int)Position.Y), Tint);
		}
	}
}
