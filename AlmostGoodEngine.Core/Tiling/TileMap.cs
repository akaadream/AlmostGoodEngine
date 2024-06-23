using System.Collections.Generic;

namespace AlmostGoodEngine.Core.Tiling
{
	internal class TileMap()
	{
		public List<TileMapLayer> Layers { get; private set; } = [];
	}
}
