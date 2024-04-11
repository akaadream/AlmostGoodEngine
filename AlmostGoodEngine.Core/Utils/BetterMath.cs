using Microsoft.Xna.Framework;
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

		public static float To1(float min, float max, float value)
		{
			return ToIntervale(min, max, 0f, 1f, value);
		}

		public static float ToIntervale(float fromMin, float fromMax, float toMin, float toMax, float value)
		{
			return (value - fromMin) * (toMax - toMin) / (fromMax - fromMin) + toMin;
		}

		public static float ManhattanDistance(Vector2 point1, Vector2 point2)
		{
			return Math.Abs(point2.X - point1.X) + Math.Abs(point2.Y - point1.Y);
		}

		public static float SquaredEuclidianDistance(Vector2 point1, Vector2 point2)
		{
			return MathF.Pow(point2.X - point1.X, 2) + MathF.Pow(point2.Y - point1.Y, 2);
		}

		public static float EuclidianDistance(Vector2 point1, Vector2 point2)
		{
			return MathF.Sqrt(SquaredEuclidianDistance(point1, point2));
		}

		public static int DiscreteDistance(Vector2 point1, Vector2 point2)
		{
			if (point1 == point2)
			{
				return 0;
			}

			return 1;
		}

		public static int DamerauLevenshteinDistance(string str1, string str2)
		{
			if (string.IsNullOrEmpty(str1) && string.IsNullOrEmpty(str2))
			{
				return 0;
			}

			if (string.IsNullOrEmpty(str1))
			{
				return str2.Length;
			}

			if (string.IsNullOrEmpty(str2))
			{
				return str1.Length;
			}

			var distances = new int[str1.Length + 1, str2.Length + 1];

			for (int i = 0; i <= str1.Length; distances[i, 0] = i++) ;
			for (int j = 0; j <= str2.Length; distances[0, j] = j++) ;

			for (int i = 1; i <= str1.Length; i++)
			{
				for (int j = 1; j <= str2.Length; j++)
				{
					int cost = str2[j - 1] == str1[i - 1] ? 0 : 1;
					distances[i, j] = Min(distances[i - 1, j] + 1, distances[i, j - 1] + 1, distances[i - 1, j - 1] + cost);
				}
			}

			return distances[str1.Length, str2.Length];
		}

		public static T Min<T>(params T[] values) where T : IComparable<T>
		{
			if (values.Length == 0)
			{
				return default;
			}

			T min = values[0];

			for (int i = 1; i < values.Length; i++)
			{
				if (values[i].CompareTo(min) < 0)
				{
					min = values[i];
				}
			}

			return min;
		}

		public static T Max<T>(params T[] values) where T : IComparable<T>
		{
			if (values.Length == 0)
			{
				return default;
			}

			T max = values[0];

			for (int i = 1; i < values.Length; i++)
			{
				if (values[i].CompareTo(max) > 0)
				{
					max = values[i];
				}
			}

			return max;
		}

		public static void Test()
		{
			Logger.Log("Distance damerau levenshtein: " + DamerauLevenshteinDistance("Bonjour", "Bonsoir"));
			Logger.Log("Distance damerau levenshtein: " + DamerauLevenshteinDistance("Début", "Fin"));
			Logger.Log("Distance damerau levenshtein: " + DamerauLevenshteinDistance("Dunkerque", "Perpignan"));
		}
	}
}
