using Microsoft.Xna.Framework;

namespace AlmostGoodEngine.Core.Tiling
{
	public struct Tile
	{
		private static readonly Tile nullTile = new(-1, -1, 0, 0);
		public static Tile Null { get { return nullTile; } }

		/// <summary>
		/// The X coordinate of the tile
		/// </summary>
		public int X { get; set; }

		/// <summary>
		/// The Y coordinate of the tile
		/// </summary>
		public int Y { get; set; }

		/// <summary>
		/// The width size of the tile
		/// </summary>
		public int Width { get; set; }

		/// <summary>
		/// The height size of the tile
		/// </summary>
		public int Height { get; set; }

		/// <summary>
		/// The rectangle representing the source rectangle inside the tileset
		/// </summary>
		public Rectangle Source { get => new(X, Y, Width, Height); }

		public Tile(int x, int y, int width, int height)
		{
			X = x;
			Y = y;
			Width = width;
			Height = height;
		}

		public bool Equals(Tile other)
		{
			return other.X == X && other.Y == Y && other.Width == Width && other.Height == Height;
		}

		public override bool Equals(object obj)
		{
			if (obj is Tile tile)
			{
				return Equals(tile);
			}

			return false;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (X.GetHashCode() * 397) ^ Y.GetHashCode() ^ Width.GetHashCode() ^ Height.GetHashCode();
			}
		}

		public static bool operator ==(Tile tile, Tile other)
		{
			return tile.Equals(other);
		}

		public static bool operator !=(Tile left, Tile right)
		{
			return !(left == right);
		}

		public bool IsNull()
		{
			return X < 0 || Y < 0 || Width <= 0 || Height <= 0;
		}
	}
}
