using AlmostGoodEngine.Physics.Extends;
using Microsoft.Xna.Framework;
using System;

namespace AlmostGoodEngine.Physics
{
	public class BoxCollider2D : Collider
	{
		public Rectangle Rectangle { get; set; }

		public override bool Collide(Collider other, bool response = true)
		{
			if (other is BoxCollider2D boxCollider2D)
			{
				ApplyCollisionResponse(boxCollider2D);
				return true;
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

		/// <summary>
		/// Compute the impulse of the collision
		/// </summary>
		/// <param name="other"></param>
		/// <param name="penetrationDepth"></param>
		/// <returns></returns>
		private Vector2 ComputeImpulse(BoxCollider2D other, float penetrationDepth)
		{
			// Compute objects mass
			float mass1 = Rectangle.Width * Rectangle.Height;
			float mass2 = other.Rectangle.Width * other.Rectangle.Height;

			// Speed
			Vector2 relativeVelocity = Velocity - other.Velocity;

			// Compute the normalalized vector of the collision
			Vector2 normal = Rectangle.GetPosition() - other.Rectangle.GetPosition();
			normal.Normalize();

			// Compute the impulse

			// Elasticity of the collision
			float e = 0.5f;
			float j = -(1 + e) * Vector2.Dot(relativeVelocity, normal) / ((1 / mass1) + (1 / mass2));
			return normal * j;
		}

		private void ApplyCollisionResponse(BoxCollider2D other)
		{
			float penetrationDepth = GetDepth(other);
			Vector2 normal = Rectangle.GetPosition() - other.Rectangle.GetPosition();
			normal.Normalize();

			// Move rectangles to eliminate penetration
			var offset = normal * penetrationDepth * (Rectangle.Width * Rectangle.Height) / (Rectangle.Width * Rectangle.Height + other.Rectangle.Width * other.Rectangle.Height);
			var otherOffset = normal * penetrationDepth * (other.Rectangle.Width * other.Rectangle.Height) / (Rectangle.Width * Rectangle.Height + other.Rectangle.Width * other.Rectangle.Height);
			Rectangle = new((int)(Rectangle.X - offset.X), (int)(Rectangle.Y - offset.Y), Rectangle.Width, Rectangle.Height);
			other.Rectangle = new((int)(other.Rectangle.X + otherOffset.X), (int)(other.Rectangle.Y + otherOffset.Y), other.Rectangle.Width, other.Rectangle.Height);

			// Get the result of the impulse computation
			Vector2 impulse = ComputeImpulse(other, penetrationDepth);

			// Apply the impulse
			Velocity -= impulse / (Rectangle.Width * Rectangle.Height);
			other.Velocity += impulse / (other.Rectangle.Width * other.Rectangle.Height);
		}
	}
}
