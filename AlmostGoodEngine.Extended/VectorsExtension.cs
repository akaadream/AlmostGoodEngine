using Microsoft.Xna.Framework;

namespace AlmostGoodEngine.Extended
{
    public static class VectorsExtension
    {
        public static Vector2 ToVector2(this Vector3 vector)
        {
            return new Vector2(vector.X, vector.Y);
        }

        public static Vector2 Rounded(this Vector2 vector)
        {
            vector.Round();
            return vector;
        }

        public static Vector3 Rounded(this Vector3 vector)
        {
            vector.Round();
            return vector;
        }
    }
}
