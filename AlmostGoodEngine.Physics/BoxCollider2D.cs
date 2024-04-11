using Microsoft.Xna.Framework;
using System;

namespace AlmostGoodEngine.Physics
{
	public class BoxCollider2D : Collider
	{
		private float width;
		private float height;

		public BoxCollider2D(float width, float height, float x = 0, float y = 0)
		{
			this.width = width;
			this.height = height;

			Position.X = x;
			Position.Y = y;
		}

		public override float Width { get => width; set => width = value; }
		public override float Height { get => height; set => height = value; }
		public override float Left { get => Position.X; set => Position.X = value; }
		public override float Top { get => Position.Y; set => Position.Y = value; }
		public override float Bottom { get => Position.Y + Height; set => Position.Y = value - Height; }
		public override float Right { get => Position.X + Width; set => Position.X = value - Width; }


		public bool Intersects(BoxCollider2D other)
		{
			return Left < other.Right &&
				Right > other.Left &&
				Bottom > other.Top &&
				Top < other.Bottom;
		}

		public bool Intersects(float x, float y, float width, float height)
		{
			return Right > x && Bottom > y && Left < x + width && Top < y + height;
		}

		public void SetFromRectangle(Rectangle rect)
		{
			Position = new(rect.X, rect.Y);
			Width = rect.Width;
			Height = rect.Height;
		}

		public void Set(float x, float y, float w, float h)
		{
			Position.X = x;
			Position.Y = y;
			Width = w;
			Height = h;
		}

		public override bool Collide(Rectangle rectangle)
		{
			throw new NotImplementedException();
		}

		public override bool Collide(Vector2 position)
		{
			throw new NotImplementedException();
		}

		public override bool Collide(Point position)
		{
			throw new NotImplementedException();
		}
	}
}
