using Microsoft.Xna.Framework;

namespace AlmostGoodEngine.Physics
{
	public class CircleCollider2D : Collider
	{
		public float Radius { get; set; }

		public CircleCollider2D()
		{

		}

		public override bool Collide(Collider other, bool response = false)
		{
			return base.Collide(other);
		}

		public override bool IsInside(Vector2 position)
		{
			return Vector2.Distance(Origin, position) < Radius;
		}

		public override Rectangle GetRectangle()
		{
			return new((int)(Origin.X - Radius), (int)(Origin.Y - Radius), (int)(Origin.X + Radius), (int)(Origin.Y + Radius));
		}
	}
}
