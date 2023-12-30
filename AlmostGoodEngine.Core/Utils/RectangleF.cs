using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace AlmostGoodEngine.Core.Utils
{
    [DataContract]
    [DebuggerDisplay("{DebugDisplayString,nq}")]
    public struct RectangleF : IEquatable<RectangleF>
    {
        /// <summary>
        /// Emtpy rectangle instance
        /// </summary>
        private static RectangleF emptyRectangle = new();

        /// <summary>
        /// The X coordinate of the rectangle
        /// </summary>
        [DataMember]
        public float X;

        /// <summary>
        /// The Y coordinate of the rectangle
        /// </summary>
        [DataMember]
        public float Y;

        /// <summary>
        /// The width of the rectangle
        /// </summary>
        [DataMember]
        public float Width;

        /// <summary>
        /// The height of the rectangle
        /// </summary>
        [DataMember]
        public float Height;

        /// <summary>
        /// An empty rectangle
        /// </summary>
        public static RectangleF Empty { get => emptyRectangle; }

        /// <summary>
        /// Top coordinate of the rectangle
        /// </summary>
        public float Top { get => Y; }

        /// <summary>
        /// Left coordinate of the rectangle
        /// </summary>
        public float Left { get => X; }

        /// <summary>
        /// Bottom coordinate of the rectangle
        /// </summary>
        public float Bottom { get => Y + Height; }

        /// <summary>
        /// Right coordinate of the rectangle
        /// </summary>
        public float Right { get => X + Width; }

        public RectangleF(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public RectangleF():
            this(0f, 0f, 0f, 0f)
        {
            
        }

        public RectangleF(Vector2 position, Vector2 size):
            this(position.X, position.Y, size.X, size.Y)
        {

        }

        #region Operators

        /// <summary>
        /// Check if the rectangle a is equivalent with the rectangle b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(RectangleF a, RectangleF b)
        {
            return ((a.X == b.X) && (a.Y == b.Y) && (a.Width == b.Width) && (a.Height == b.Height));
        }

        /// <summary>
        /// Check if the rectangle a is different with the rectangle b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(RectangleF a, RectangleF b)
        {
            return !(a == b);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Return true if the given object is a RectangleF instance and equivalent with the current instance
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals([NotNullWhen(true)] object obj)
        {
            return (obj is RectangleF f) && this == f;
        }

        /// <summary>
        /// Return true if the given rectangle is equal to the instance rectangle
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(RectangleF other)
        {
            return this == other;
        }

        /// <summary>
        /// Get a hash code for the following rectangle
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 23 + X.GetHashCode();
                hash = hash * 23 + Y.GetHashCode();
                hash = hash * 23 + Width.GetHashCode();
                hash = hash * 23 + Height.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// String representation of the rectangle
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "{X: " + X + ", Y: " + Y + ", Width: " + Width + ", Height: " + Height + "}";
        }

        /// <summary>
        /// Deconstruct the current rectangle
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void Deconstruct(out float x, out float y, out float width, out float height)
        {
            x = X;
            y = Y;
            width = Width;
            height = Height;
        }

        /// <summary>
        /// Get the union of the instance rectangle and the given rectangle
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public RectangleF Union(RectangleF other)
        {
            return Union(this, other);
        }

        /// <summary>
        /// Get the union rectangle of two rectangles
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static RectangleF Union(RectangleF a, RectangleF b)
        {
            float left = Math.Min(a.Left, b.Left);
            float top = Math.Min(a.Top, b.Top);
            float right = Math.Max(a.Right, b.Right);
            float bottom = Math.Max(a.Bottom, b.Bottom);

            return new(left, top, right - left, bottom - top);
        }

        /// <summary>
        /// Get the intersection of the instance rectangle and the given rectangle
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public RectangleF Intersect(RectangleF other)
        {
            return Intersect(this, other);
        }

        /// <summary>
        /// Get the intersection between two rectangles
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static RectangleF Intersect(RectangleF a, RectangleF b)
        {
            float left = Math.Max(a.Left, b.Left);
            float top = Math.Max(a.Top, b.Top);
            float right = Math.Min(a.Right, b.Right);
            float bottom = Math.Min(a.Bottom, b.Bottom);

            return new(left, top, right - left, bottom - top);
        }

        /// <summary>
        /// Return true if the given rectangle is included inside the instance rectangle
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Includes(RectangleF other)
        {
            return Includes(this, other);
        }

        public static bool Includes(RectangleF a, RectangleF b)
        {
            return !(a.Left < b.Left && a.Top < b.Top && a.Right > b.Right && a.Bottom > b.Bottom);
        }

        #endregion
    }
}
