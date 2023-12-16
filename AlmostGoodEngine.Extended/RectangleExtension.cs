using Microsoft.Xna.Framework;
using System;

namespace AlmostGoodEngine.Extended
{
    public static class RectangleExtension
    {

        /// <summary>
        /// Get the intersection depth of two rectangles.
        /// If rectangles are not intersecting themselves, Vector2.Zero is returned
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static Vector2 GetIntersectionDepth(this Rectangle rectangle, Rectangle other)
        {
            // There is no intersection between both rectangles, so we return an empty 2D vector
            if (!rectangle.Intersects(other))
            {
                return Vector2.Zero;
            }

            float depthX = Math.Abs(rectangle.X - other.X);
            float depthY = Math.Abs(rectangle.Y - other.Y);

            // TODO: update

            return new(depthX, depthY);
        }

        /// <summary>
        /// Get the center of the left segment of the given rectangle
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        public static Vector2 GetLeftCenter(this Rectangle rectangle) => new(rectangle.X, rectangle.Y + rectangle.Height / 2);

        /// <summary>
        /// Get the center of the top segment of the given rectangle
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        public static Vector2 GetTopCenter(this Rectangle rectangle) => new(rectangle.X + rectangle.Width / 2, rectangle.Y);

        /// <summary>
        /// Get the center of the right segment of the given rectangle
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        public static Vector2 GetRightCenter(this Rectangle rectangle) => new(rectangle.Right, rectangle.Y + rectangle.Height / 2);

        /// <summary>
        /// Get the center of the bottom segment of the given rectangle
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        public static Vector2 GetBottomCenter(this Rectangle rectangle) => new(rectangle.X + rectangle.Width / 2, rectangle.Bottom);
    }
}
