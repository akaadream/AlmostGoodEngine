using Microsoft.Xna.Framework;

namespace AlmostGoodEngine.Core.Tiling
{
	public struct Tile(int x, int y, int width, int height)
    {
		// Null tile
		private static readonly Tile nullTile = new(-1, -1, 0, 0);
		public static Tile Null { get { return nullTile; } }

        /// <summary>
        /// The X coordinate of the tile
        /// </summary>
        public int X { get; set; } = x;

        /// <summary>
        /// The Y coordinate of the tile
        /// </summary>
        public int Y { get; set; } = y;

        /// <summary>
        /// The width size of the tile
        /// </summary>
        public int Width { get; set; } = width;

        /// <summary>
        /// The height size of the tile
        /// </summary>
        public int Height { get; set; } = height;

        /// <summary>
        /// The rectangle representing the source rectangle inside the tileset
        /// </summary>
        public readonly Rectangle Source { get => new(X, Y, Width, Height); }

        public readonly bool Equals(Tile other)
		{
			return other.X == X && other.Y == Y && other.Width == Width && other.Height == Height;
		}

		/// <summary>
		/// Check if the given object is equal with the current tile instance
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override readonly bool Equals(object obj)
		{
			if (obj is Tile tile)
			{
				return Equals(tile);
			}

			return false;
		}

		/// <summary>
		/// The hash code og the tile
		/// </summary>
		/// <returns></returns>
		public override readonly int GetHashCode()
		{
			unchecked
			{
				return (X.GetHashCode() * 397) ^ Y.GetHashCode() ^ Width.GetHashCode() ^ Height.GetHashCode();
			}
		}

		/// <summary>
		/// Return true if tile is equal to other
		/// </summary>
		/// <param name="tile"></param>
		/// <param name="other"></param>
		/// <returns></returns>
		public static bool operator ==(Tile tile, Tile other)
		{
			return tile.Equals(other);
		}

		/// <summary>
		/// Return true if left tile is equal to right tile
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static bool operator !=(Tile left, Tile right)
		{
			return !(left == right);
		}

		/// <summary>
		/// Check if a tile can be considered as null
		/// </summary>
		/// <returns></returns>
        public readonly bool IsNull() => X < 0 || Y < 0 || Width <= 0 || Height <= 0;
    }
}
