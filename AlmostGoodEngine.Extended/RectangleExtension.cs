using Microsoft.Xna.Framework;
using System;

namespace AlmostGoodEngine.Extended
{
    public static class RectangleExtension
    {
        /// <summary>
        /// Get the half size of a rectangle
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        public static Vector2 Half(this Rectangle rectangle) => new(rectangle.Width / 2f, rectangle.Height / 2f);

        public static Vector2 Center(this Rectangle rectangle) => new (rectangle.X + rectangle.Width / 2f, rectangle.Y + rectangle.Height / 2f);

        /// <summary>
        /// Get the intersection depth of two rectangles.
        /// If rectangles are not intersecting themselves, Vector2.Zero is returned
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static Vector2 GetIntersectionDepth(this Rectangle rectangle, Rectangle other)
        {
            // Rectangles half size
            Vector2 halfRectangle = Half(rectangle);
            Vector2 halfOther = Half(other);

            // Rectangles center coordinates
            Vector2 centerRectangle = Center(rectangle);
            Vector2 centerOther = Center(other);

            // Distance between rectangles centers
            Vector2 distance = new(centerRectangle.X - centerOther.X, centerRectangle.Y - centerOther.Y);

            // Threshold of the half size we have to check
            Vector2 minHalf = new(halfRectangle.X + halfOther.X, halfRectangle.Y + halfOther.Y);

            // The response depth
            Vector2 depth = Vector2.Zero;

            // Depth on X coordinate
            if (Math.Abs(distance.X) < minHalf.X)
            {
                depth.X = distance.X < 0 ? minHalf.X - distance.X : -minHalf.X - distance.X;
            }

            // Depth on Y coordinate
            if (Math.Abs(distance.Y) < minHalf.Y)
            {
                depth.Y = distance.Y < 0 ? minHalf.Y - distance.Y : -minHalf.Y - distance.Y;
            }

            return depth;
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
