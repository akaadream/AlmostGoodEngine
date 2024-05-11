using Microsoft.Xna.Framework;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace AlmostGoodEngine.Core.Utils
{
	public struct Vector2I
	{
		#region Private fields

		private static readonly Vector2I vectorZero = new(0, 0);
		private static readonly Vector2I vectorUnit = new(1, 1);
		private static readonly Vector2I vectorUnitX = new(1, 0);
		private static readonly Vector2I vectorUnitY = new(0, 1);

		#endregion

		#region Public fields

		[DataMember]
		public int X;

		[DataMember]
		public int Y;

		#endregion

		#region Properties

		public static Vector2I Zero { get => vectorZero; }
		public static Vector2I Unit { get => vectorUnit; }
		public static Vector2I UnitX { get => vectorUnitX; }
		public static Vector2I UnitY { get => vectorUnitY; }

		#endregion

		#region Others

		internal string DebugText { get => string.Concat(X.ToString(), " ", Y.ToString()); }

		#endregion

		public Vector2I(int x, int y)
		{
			X = x;
			Y = y;
		}

		public Vector2I(int value)
		{
			X = value;
			Y = value;
		}

		public static implicit operator Vector2I(System.Numerics.Vector2 vector)
		{
			return new((int)vector.X, (int)vector.Y);
		}

		public static implicit operator Vector2I(Vector2 vector)
		{
			return new((int)vector.X, (int)vector.Y);
		}

		public static Vector2I operator -(Vector2I vector)
		{
			vector.X = -vector.X;
			vector.Y = -vector.Y;
			return vector;
		}

		public static Vector2I operator +(Vector2I vector, Vector2I other)
		{
			vector.X += other.X;
			vector.Y += other.Y;
			return vector;
		}

		public static Vector2I operator -(Vector2I vector, Vector2I other)
		{
			vector.X -= other.X;
			vector.Y -= other.Y;
			return vector;
		}

		public static Vector2I operator *(Vector2I vector, Vector2I other)
		{
			vector.X *= other.X;
			vector.Y *= other.Y;
			return vector;
		}

		public static Vector2I operator *(Vector2I vector, int value)
		{
			vector.X *= value;
			vector.Y *= value;
			return vector;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2I operator /(Vector2I vector, Vector2I other)
		{
			vector.X /= other.X;
			vector.Y /= other.Y;
			return vector;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2I operator /(Vector2I vector, int divider)
		{
			vector.X /= divider;
			vector.Y /= divider;
			return vector;
		}

		public static bool operator ==(Vector2I left, Vector2I right)
		{
			return left.X == right.X && left.Y == right.Y;
		}

		public static bool operator !=(Vector2I left, Vector2I right)
		{
			return left.X != right.X || left.Y != right.Y;
		}

		public static Vector2 Barycentric(Vector2I vector1, Vector2I vector2, Vector2I vector3, float amount1, float amount2)
		{
			return new(
				MathHelper.Barycentric(vector1.X, vector2.X, vector3.X, amount1, amount2),
				MathHelper.Barycentric(vector1.Y, vector2.Y, vector3.Y, amount1, amount2)
				);
		}

		public static Vector2 CatmullRom(Vector2I vector1, Vector2I vector2, Vector2I vector3, Vector2I vector4, float amount)
		{
			return new(
				MathHelper.CatmullRom(vector1.X, vector2.X, vector3.X, vector4.X, amount),
				MathHelper.CatmullRom(vector1.Y, vector2.Y, vector3.Y, vector4.Y, amount)
				);
		}

		public static Vector2I Clamp(Vector2I current, Vector2I min, Vector2I max)
		{
			current.X = MathHelper.Clamp(current.X, min.X, max.X);
			current.Y = MathHelper.Clamp(current.X, min.Y, max.Y);
			return current;
		}

		public static Vector2I Cross(Vector2I vector, Vector2I other)
		{
			return new((vector.Y * other.X) - (vector.X * other.Y), (vector.X * other.Y) - (vector.Y * other.X));
		}

		public static float Distance(Vector2I vector, Vector2I other)
		{
			float dX = vector.X - other.X;
			float dY = vector.Y - other.Y;
			return MathF.Sqrt((dX * dX) + (dY * dY));
		}

		public static float DistanceSquared(Vector2I vector, Vector2I other)
		{
			float dX = vector.X - other.X;
			float dY = vector.Y - other.Y;
			return (dX * dX) + (dY * dY);
		}

		public float Dot(Vector2I vector, Vector2I other)
		{
			return (vector.X * other.X) + (vector.Y * other.Y);
		}

		public override bool Equals([NotNullWhen(true)] object obj)
		{
			if (obj is Vector2I vector)
			{
				return Equals(vector);
			}

			return false;
		}

		public bool Equals(Vector2I other)
		{
			return (X == other.X) && (Y == other.Y);
		}

		public override int GetHashCode()
		{
			return (X.GetHashCode() * 397) ^ Y.GetHashCode();
		}

		public static Vector2I Lerp(Vector2I start, Vector2I end, float amount)
		{
			float x = MathHelper.Lerp(start.X, end.X, amount);
			float y = MathHelper.Lerp(start.Y, end.Y, amount);
			return new((int)x, (int)y);
		}

		public static Vector2I LerpPrecise(Vector2I start, Vector2I end, float amount)
		{
			float x = MathHelper.LerpPrecise(start.X, end.X, amount);
			float y = MathHelper.LerpPrecise(start.Y, end.Y, amount);
			return new((int)x, (int)y);
		}

		public static Vector2I Max(params Vector2I[] vectors)
		{
			Vector2I max = new(int.MinValue, int.MinValue);

			foreach (var vector in vectors)
			{
				if (vector.X > max.X) max.X = vector.X;
				if (vector.Y > max.Y) max.Y = vector.Y;
			}

			return max;
		}

		public static Vector2I Min(params Vector2I[] vectors)
		{
			Vector2I min = new(int.MaxValue, int.MaxValue);

			foreach (var vector in vectors)
			{
				if (vector.X < min.X) min.X = vector.X;
				if (vector.Y < min.Y) min.Y = vector.Y;
			}

			return min;
		}

		public Vector2 Normalize()
		{
			float value = 1f / MathF.Sqrt((X * X) + (Y * Y));
			return new(X * value, Y * value);
		}

		public void Rotate(float radians)
		{
			float cos = MathF.Cos(radians);
			float sin = MathF.Sin(radians);
			float oldX = X;

			X = (int)(X * cos - Y * sin);
			Y = (int)(oldX * sin + Y * cos);
		}

		public void RotateAround(Vector2I origin, float radians)
		{
			this -= origin;
			Rotate(radians);
			this += origin;
		}

		public void Deconstruct(out int x, out int y)
		{
			x = X;
			y = Y;
		}

		public Vector2 ToVector2()
		{
			return new Vector2(X, Y);
		}

		public override string ToString()
		{
			return "{X:" + X + ", " + Y + "}";
		}

		public Point ToPoint()
		{
			return new Point(X, Y);
		}
	}
}
