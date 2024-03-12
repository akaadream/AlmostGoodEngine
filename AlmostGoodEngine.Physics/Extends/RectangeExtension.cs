using Microsoft.Xna.Framework;

namespace AlmostGoodEngine.Physics.Extends
{
	public static class RectangeExtension
	{
		public static Interval ToInterval(this Rectangle rectangle, Vector2 axis)
		{
			Vector2 position = new(rectangle.X, rectangle.Y);
			Vector2 size = new(rectangle.Width, rectangle.Height);
			return new(Vector2.Dot(position - size / 2, axis), Vector2.Dot(position + size / 2, axis));
		}

		public static Vector2 GetSize(this Rectangle rectangle)
		{
			return new(rectangle.Width, rectangle.Height);
		}

		public static Vector2 GetPosition(this Rectangle rectangle)
		{
			return new(rectangle.X, rectangle.Y);
		}
	}
}
