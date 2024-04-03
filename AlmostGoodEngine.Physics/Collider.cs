using Microsoft.Xna.Framework;

namespace AlmostGoodEngine.Physics
{
	public class Collider
	{
		/// <summary>
		/// The origin of the collider
		/// </summary>
		public Vector2 Origin { get; set; } = Vector2.Zero;

		/// <summary>
		/// If true, the collider is active and will check collisions whith other colliders
		/// </summary>
		public bool Active { get; set; } = true;

		/// <summary>
		/// Check if the given collider is colliding this collider
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public virtual bool Collide(Collider other, bool response = true)
		{
			return Origin == other.Origin;
		}

		/// <summary>
		/// Check if the given position is contained by this collider
		/// </summary>
		/// <param name="position"></param>
		/// <returns></returns>
		public virtual bool IsInside(Vector2 position)
		{
			return false;
		}

		/// <summary>
		/// Get the rectangle that cover the collider
		/// </summary>
		/// <returns></returns>
		public virtual Rectangle GetRectangle()
		{
			return Rectangle.Empty;
		}
	}
}
