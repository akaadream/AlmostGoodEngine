using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmostGoodEngine.Core.Utils
{
	public static class BetterMath
	{
		/// <summary>
		/// Return 2^x
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		public static float Exp2(float x) => MathF.Pow(2, x);

		/// <summary>
		/// A smooth lerp
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <param name="dt"></param>
		/// <param name="h"></param>
		/// <returns></returns>
		public static float LerpSmooth(float a, float b, float dt, float h) => b + (a - b) * Exp2(-dt / h);

		/// <summary>
		/// Give the exp of n minus 1
		/// </summary>
		/// <param name="n"></param>
		/// <returns></returns>
		public static float Expm1(float n) => MathF.Exp(n) - 1;

		/// <summary>
		/// Return the positive difference between the given x and y
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public static float Dim(float x, float y)
		{
			if (y >= x)
			{
				return 0f;
			}

			return x - y;
		}

		public static int Roundi(double n) => (int)Math.Round(n);

		public static int Roundi(float n) => (int)MathF.Round(n);

		// Buggy
		public static float Remainder(float n)
		{
			var x = MathF.Floor(n);
			return n - x;
		}

		public static float Smoothstep(float from, float to, float x)
		{
			x = Math.Clamp((x - from) / (to - from), 0.0f, 1.0f);
			return x * x * (3.0f - 2.0f * x);
		}

		public static float InverseLerp(float a, float b, float v)
		{
			return (v - a) / (b - a);
		}

		public static void Test()
		{
			var result = Expm1(1);
			var result2 = Exp2(5);
			var result3 = Remainder(4.6f);
			Logger.Log("Expm1: " + result, "MATH");
			Logger.Log("Exp2: " + result2, "MATH");
			Logger.Log("Remainder: " + result3, "MATH");
		}
	}
}
