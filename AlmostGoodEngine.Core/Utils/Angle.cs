using System;

namespace AlmostGoodEngine.Core.Utils
{
    public struct Angle
    {
        /// <summary>
        /// The angle in degrees
        /// </summary>
        public float Degrees { get; set; }

        /// <summary>
        /// The angle in radians
        /// </summary>
        public float Radians { get => (float)(Degrees * Math.PI) / 180; }

        public static Angle Zero { get => new(0.0); }

        public Angle(double degrees)
        {
            Degrees = (float)degrees;
        }
    }
}
