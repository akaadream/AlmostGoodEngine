using AlmostGoodEngine.Core.Tiling;

namespace AlmostGoodEngine.Core.Generation
{
	public class TileHeight()
	{
		/// <summary>
		/// Min 0
		/// </summary>
		public float Min { get; set; } = 0.0f;

		/// <summary>
		/// Max 1
		/// </summary>
		public float Max { get; set; } = 1.0f;

		/// <summary>
		/// The tile corresponding to the height
		/// </summary>
		public Tile Tile { get; set; }
	}
}
