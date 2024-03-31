using Microsoft.Xna.Framework;

namespace AlmostGoodEngine.Core.Utils
{
    public class Axis
    {
        /// <summary>
        /// The X coordinate of the axis
        /// </summary>
        public int X { get; private set; }

        /// <summary>
        /// The Y coordinate of the axis
        /// </summary>
        public int Y { get; private set; }

        /// <summary>
        /// Previous X coordinate
        /// </summary>
        public int PreviousX { get; private set; }

        /// <summary>
        /// Previous Y coordinate
        /// </summary>
        public int PreviousY { get; private set; }

        /// <summary>
        /// True if the origin of the coordinate if the top/left one, else it's the bottom/left one
        /// </summary>
        public bool TopLeftOrigin { get; private set; }

        public Axis(int x, int y)
        {
            Compute(x, y);
        }

        /// <summary>
        /// Put the coordinate from the top left one. The coordinate will be automatically converted if its necessary
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Set(int x, int y)
        {
            PreviousX = X;
            PreviousY = Y;
            if (TopLeftOrigin)
            {
                X = x;
                Y = y;
                return;
            }

            Vector2 computed = Compute(x, y);
            X = (int)computed.X;
            Y = (int)computed.Y;
        }

        /// <summary>
        /// Switch the coordinate between the top/left axis and the bottom/left axis
        /// </summary>
        public void Switch()
        {
            TopLeftOrigin = !TopLeftOrigin;
            Set(X, Y);
        }

        /// <summary>
        /// Tranform a coordinate in top/left axis to a coordinate in bottom/left axis
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Vector2 Compute(int x, int y)
        {
            return new(x, GameManager.Engine.GraphicsDevice.Viewport.Height - y);
        }
    }
}
