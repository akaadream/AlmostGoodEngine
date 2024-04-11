using Microsoft.Xna.Framework;

namespace AlmostGoodEngine.Physics
{
	public class CircleCollider2D : Collider
	{
		public float Radius { get; set; }
		public override float Width { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
		public override float Height { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
		public override float Top { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
		public override float Left { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
		public override float Right { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
		public override float Bottom { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

		public CircleCollider2D()
		{

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
	}
}
