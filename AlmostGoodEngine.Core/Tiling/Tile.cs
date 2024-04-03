using Microsoft.Xna.Framework;

namespace AlmostGoodEngine.Core.Tiling
{
	public struct Tile
	{
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
	}
}
