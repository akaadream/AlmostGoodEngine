using Microsoft.Xna.Framework;

namespace AlmostGoodEngine.Core.Curves
{
	public static class Bezier
	{
		public static Vector2 GetPoint(float t, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
		{
			float u = 1 - t;
			float tt = t * t;
			float uu = u * u;

			return uu * (u * p0 + t * p1) + 2 * u * t * (u * p1 + t * p2) + tt * (u * p2 + t * p3);
		}
	}
}
