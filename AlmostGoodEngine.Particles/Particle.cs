using Microsoft.Xna.Framework;

namespace AlmostGoodEngine.Particles
{
	public class Particle
	{
		/// <summary>
		/// Life of the particle
		/// </summary>
		public float Life;

		/// <summary>
		/// The current position of the particle
		/// </summary>
		public Vector2 Position;

		/// <summary>
		/// The current velocity of the particle
		/// </summary>
		public Vector2 Velocity;

		/// <summary>
		/// The spin velocity of the particle
		/// </summary>
		public Vector2 Spin;

		/// <summary>
		/// The current rotation
		/// </summary>
		public float Rotation;

		/// <summary>
		/// The current opacity of the particle
		/// </summary>
		public float Opacity;
	}
}
