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
		public static float Exp2(float x)
		{
			return MathF.Pow(2, x);
		}

		/// <summary>
		/// A smooth lerp
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <param name="dt"></param>
		/// <param name="h"></param>
		/// <returns></returns>
		public static float LerpSmooth(float a, float b, float dt, float h)
		{
			return b + (a - b) * Exp2(-dt / h);
		}

		/// <summary>
		/// Give the exp of n minus 1
		/// </summary>
		/// <param name="n"></param>
		/// <returns></returns>
		public static float Expm1(float n)
		{
			return MathF.Exp(n) - 1;
		}

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

		// Buggy
		public static float Remainder(float n)
		{
			var x = MathF.Floor(n);
			return n - x;
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
