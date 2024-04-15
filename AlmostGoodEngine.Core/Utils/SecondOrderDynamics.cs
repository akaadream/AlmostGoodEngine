using Microsoft.Xna.Framework;
using System;

namespace AlmostGoodEngine.Core.Utils
{
	public class SecondOrderDynamics
	{
		public Vector2 XP { get; private set; }
		public Vector2 Y { get; private set; }
		public Vector2 YD { get; private set; }

		public float K1 { get; private set; }
		public float K2 { get; private set; }
		public float K3 { get; private set; }
		
		public SecondOrderDynamics(float f, float z, float r, Vector2 x0)
		{
			K1 = z / (MathF.PI * f);
			K2 = 1 / ((2 * MathF.PI * f) * (2 * MathF.PI * f));
			K3 = r * z / (2 * MathF.PI * f);

			XP = x0;
			Y = x0;
			YD = Vector2.Zero;
		}

		public Vector2 Update(float t, Vector2 x, Vector2 xd)
		{
			if (xd == Vector2.Zero)
			{
				xd = (x - XP) / t;
				XP = x;
			}

			Y += t * YD;
			YD += t * (x + K3 * xd - Y - K1 * YD) / K2;
			return Y;
		}
	}
}
