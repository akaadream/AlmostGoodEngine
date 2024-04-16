using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlmostGoodEngine.Particles
{
	public class Particle
	{
		/// <summary>
		/// Life of the particle
		/// </summary>
		public int Life { get; set; }

		/// <summary>
		/// The duration of the particle
		/// </summary>
		public int Duration { get; set; }

		/// <summary>
		/// The current position of the particle
		/// </summary>
		public Vector3 Position { get; set; }

		/// <summary>
		/// The texture used as the particle. White pixel if we asign nothing to this instance
		/// </summary>
		public Texture2D Texture { get; set; }

		/// <summary>
		/// The tint of the particle
		/// </summary>
		public Color Tint { get; set; } = Color.White;

		/// <summary>
		/// Default constructor of the particle. Using a white pixel as texture by default
		/// </summary>
		public Particle()
		{

		}

		public void Update(Vector3 finalForce)
		{
			Position += finalForce;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (Texture == null)
			{
				return;
			}

			spriteBatch.Draw(Texture, new Vector2((int)Position.X, (int)Position.Y), Tint);
		}
	}
}
