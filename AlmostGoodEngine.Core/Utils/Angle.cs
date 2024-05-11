using System;

namespace AlmostGoodEngine.Core.Utils
{
    public struct Angle(double degrees)
	{
		/// <summary>
		/// The angle in degrees
		/// </summary>
		public float Degrees { get; set; } = (float)degrees;

		/// <summary>
		/// The angle in radians
		/// </summary>
		public float Radians { get => (float)(Degrees * Math.PI) / 180; }

        public static Angle Zero { get => new(0.0); }
	}
}
