using AlmostGoodEngine.Physics.Extends;
using Microsoft.Xna.Framework;
using System;

namespace AlmostGoodEngine.Physics
{
	public class BoxCollider2D : Collider
	{
		public Rectangle Rectangle { get; set; }

		public BoxCollider2D(Rectangle rect)
		{
			Rectangle = rect;
		}

		public override bool Collide(Collider other, bool response = true)
		{
			if (other is BoxCollider2D boxCollider2D)
			{
				return true;
			}

			if (other is CircleCollider2D circleCollider2D)
			{

			}

			return false;
		}

		public override bool IsInside(Vector2 position)
		{
			return position.X >= Origin.X + Rectangle.Left &&
				position.X < Origin.X + Rectangle.Right &&
				position.Y >= Origin.Y + Rectangle.Top &&
				position.Y < Origin.Y + Rectangle.Bottom;
		}

		public override Rectangle GetRectangle()
		{
			return new((int)(Origin.X + Rectangle.Left), (int)(Origin.Y + Rectangle.Top), Rectangle.Width, Rectangle.Height);
		}

		/// <summary>
		/// Get the minimum depth between this collider and the given box collider
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public float GetDepth(BoxCollider2D other)
		{
			Vector2 edge1 = Origin + Rectangle.GetPosition() + Rectangle.GetSize() / 2 - (other.Origin + other.Rectangle.GetPosition()) - other.Rectangle.GetSize() / 2;
			Vector2 edge2 = other.Origin + other.Rectangle.GetPosition() + other.Rectangle.GetSize() / 2 - (Origin + Rectangle.GetPosition()) - Rectangle.GetSize() / 2;

			Vector2 normal1 = new(-edge1.Y, edge1.X);
			normal1.Normalize();
			Vector2 normal2 = new(-edge2.Y, edge2.X);
			normal2.Normalize();

			float depth1 = Math.Min(Vector2.Dot(Rectangle.GetSize() / 2, normal1), Vector2.Dot(other.Rectangle.GetSize() / 2, -normal1)) - Vector2.Dot(edge1, normal1);
			float depth2 = Math.Min(Vector2.Dot(Rectangle.GetSize() / 2, normal2), Vector2.Dot(other.Rectangle.GetSize() / 2, -normal2)) - Vector2.Dot(edge2, normal2);
			return Math.Min(depth1, depth2);
		}

		/// <summary>
		/// Get the minimum depth between this collider and the given circle collider
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public float GetDepth(CircleCollider2D other)
		{
			return 0f;
		}
	}
}
