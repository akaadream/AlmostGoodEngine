namespace AlmostGoodEngine.Core.Tiling
{
    internal class TileMapLayer(int width, int height)
    {
        /// <summary>
        /// Tiles which are store in this layer
        /// </summary>
        public int[,] Tiles { get; set; } = new int[width, height];

        /// <summary>
        /// The width size of the layer
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// The height size of the layer
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Shortcut to find or replace a tile
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int this[int x, int y]
        {
            get
            {
                return Tiles[x, y];
            }

            set
            {
                Tiles[x, y] = value;
            }
        }
    }
}
