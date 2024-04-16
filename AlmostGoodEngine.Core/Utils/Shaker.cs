using Microsoft.Xna.Framework;
using System;

namespace AlmostGoodEngine.Core.Utils
{
	/// <summary>
	/// Screen shaking class
	/// </summary>
	public static class Shaker
	{
		public static Vector2 Offset { get; private set; }

		public static float Intensity { get; private set; } = 1.0f;
		public static float Speed { get; private set; } = 1.0f;

		public static float Duration { get; private set; }
		public static bool Shaking { get; private set; }

		private static float _randomAngle;

		public static void Shake(float intensity, float duration)
		{
			Intensity = intensity;
			Duration = duration;
			Shaking = true;
			_randomAngle = BetterRandom.Float(0, 360);
		}

		public static void Update()
		{
			Offset = Vector2.Zero;
			
			if (Shaking)
			{
				_randomAngle += (180f - BetterRandom.Float() % 60);
				Offset = new(MathF.Sin(_randomAngle) * Intensity, MathF.Cos(_randomAngle) * Intensity);
			}
		}
	}
}
