using Microsoft.Xna.Framework;

namespace AlmostGoodEngine.Physics
{
	public class CircleCollider2D : Collider
	{
		public float Radius { get; set; }

		public override float Width { get => Radius * 2; set => Radius = value / 2; }
		public override float Height { get => Radius * 2; set => Radius = value / 2; }
		public override float Top { get => Position.Y; set => Position.Y = value + Radius; }
		public override float Left { get => Position.X; set => Position.X = value + Radius; }
		public override float Right { get => Position.X + Radius * 2; set => Position.X = value - Radius; }
		public override float Bottom { get => Position.Y + Radius * 2; set => Position.Y = value - Radius; }

		public CircleCollider2D(float radius, float x = 0, float y = 0)
		{
			Radius = radius;
			Position.X = x;
			Position.Y = y;
		}

		public override bool Collide(Rectangle rectangle)
		{
			throw new System.NotImplementedException();
		}

		public override bool Collide(Vector2 position)
		{
			throw new System.NotImplementedException();
		}

		public override bool Collide(Point position)
		{
			throw new System.NotImplementedException();
		}

		public override bool Collide(BoxCollider2D box)
		{
			throw new System.NotImplementedException();
		}

		public override bool Collide(Grid grid)
		{
			throw new System.NotImplementedException();
		}

		public override bool Collide(CircleCollider2D circle)
		{
			throw new System.NotImplementedException();
		}

		public override bool Collide(Colliders colliders)
		{
			throw new System.NotImplementedException();
		}
	}
}
