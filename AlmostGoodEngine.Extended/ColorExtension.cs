using Microsoft.Xna.Framework;
using System;

namespace AlmostGoodEngine.Extended
{
    public static class ColorExtension
    {
        /// <summary>
        /// Create a color from HSV values
        /// </summary>
        /// <param name="hue"></param>
        /// <param name="saturation"></param>
        /// <param name="brightness"></param>
        /// <param name="alpha"></param>
        /// <returns></returns>
        public static Color FromHSV(float hue, float saturation, float brightness, int alpha = 255)
        {
            int hi = (int)(hue / 60f) % 6;
            float f = hue / 60f - (float)Math.Floor(hue / 60f);

            byte v = (byte)(brightness * 255);
            byte p = (byte)(brightness * (1 - saturation) * 255);
            byte q = (byte)(brightness * (1 - f * saturation) * 255);
            byte t = (byte)(brightness * (1 - (1 - f) * saturation) * 255);

            return hi switch
            {
                0 => new(v, t, p, alpha),
                1 => new(q, v, p, alpha),
                2 => new(p, v, t, alpha),
                3 => new(p, q, v, alpha),
                4 => new(t, p, v, alpha),
                _ => new(v, p, q, alpha),
            };
        }

        /// <summary>
        /// Create a Color from an hexadecimal code
        /// </summary>
        /// <param name="hex"></param>
        /// <param name="alpha"></param>
        /// <returns></returns>
        public static Color FromHex(string hex, int alpha = 255)
        {
            hex = hex.TrimStart('#');
            int hexValue = int.Parse(hex, System.Globalization.NumberStyles.HexNumber);

            byte red = (byte)((hexValue >> 16) & 255);
            byte green = (byte)((hexValue >> 8) & 255);
            byte blue = (byte)(hexValue & 255);

            return new(red, green, blue, alpha);
        }
    }
}
