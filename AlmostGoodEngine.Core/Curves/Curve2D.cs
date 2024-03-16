using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AlmostGoodEngine.Core.Curves
{
	public class Curve2D
	{
		public List<Vector2> Points { get; private set; } = new();

		public List<Vector2> GetCurvePoints(List<Vector2> points, int subdivisions)
		{
			Points = points;
			List<Vector2> result = new();

			if (Points.Count >= 2 )
			{
				for (float t = 0; t <= 1; t += 1f / subdivisions)
				{
					result.Add(GetPosition(t));
				}
			}

			return result;
		}

		public Vector2 GetPosition(float t)
		{
			if (Points.Count < 2)
			{
				return Vector2.Zero;
			}

			int segment = (int)MathHelper.Clamp(t * (Points.Count - 1), 0, Points.Count - 2);
			float segmentT = t * (Points.Count - 1) - segment;

			Vector2 a = Points[segment];
			Vector2 b = Points[segment + 1];

			Vector2 handleA = a;
			Vector2 handleB = b;

			return Bezier.GetPoint(segmentT, a, handleA, handleB, b);
		}
	}
}
