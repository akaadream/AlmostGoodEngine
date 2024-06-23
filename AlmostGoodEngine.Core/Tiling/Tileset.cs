using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace AlmostGoodEngine.Core.Tiling
{
	public class Tileset(string filename)
    {
        /// <summary>
        /// The list of all the tiles
        /// </summary>
        public List<Tile> Tiles { get; set; } = [];

        /// <summary>
        /// The texture representing the tileset
        /// </summary>
        public Texture2D Texture { get; set; } = GameManager.Engine.Content.Load<Texture2D>(filename);

        /// <summary>
        /// The width of the tileset
        /// </summary>
        public int Width { get => Texture?.Width ?? 0; }

		/// <summary>
		/// The height of the tileset
		/// </summary>
		public int Height { get => Texture?.Height ?? 0; }

		/// <summary>
		/// The size of a tile
		/// </summary>
		public int TileSize { get; set; } = 16;

		// The filename of the tileset texture
		public string Filename { get; private set; } = filename;

		/// <summary>
		/// Variations of the tiles
		/// </summary>
        public Dictionary<string, Texture2D> Variations { get; set; } = [];

        /// <summary>
        /// Automatically generate tiles from the tilesiet
        /// </summary>
        /// <param name="cellOffset"></param>
        /// <param name="margin"></param>
        public void AutoGenerate(int cellOffset = 0, int margin = 0)
		{
			if (Texture == null)
			{
				return;
			}

			Tiles.Clear();

			for (int y = 0; y < Height / TileSize; y++)
			{
				for (int x = 0; x < Width / TileSize; x++)
				{
					Tiles.Add(new Tile()
					{
						X = x * TileSize + margin + x * cellOffset,
						Y = y * TileSize + margin + y * cellOffset,
						Width = TileSize,
						Height = TileSize
					});
				}
			}
		}

		/// <summary>
		/// Retrieve the index of the given tile
		/// </summary>
		/// <param name="tile"></param>
		/// <returns></returns>
		public int IndexOf(Tile tile)
		{
			for (int i = 0; i < Tiles.Count; i++)
			{
				if (Tiles[i] == tile)
				{
					return i;
				}
			}

			return -1;
		}

		/// <summary>
		/// Get the tile of the given index
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public Tile GetTile(int index)
		{
			return Tiles[index];
		}

		/// <summary>
		/// Draw the given tile at the given position
		/// </summary>
		/// <param name="spriteBatch"></param>
		/// <param name="position"></param>
		/// <param name="tile"></param>
		/// <param name="variation"></param>
		public void DrawTile(SpriteBatch spriteBatch, Vector2 position, Tile tile, string variation = "")
		{
			if (Texture == null)
			{
				return;
			}

			if (Variations.TryGetValue(variation, out Texture2D texture))
			{
				spriteBatch.Draw(texture, position, tile.Source, Color.White);
				return;
			}
			spriteBatch.Draw(Texture, position, tile.Source, Color.White);
		}

		/// <summary>
		/// Draw the tile corresponding the given index at the given position
		/// </summary>
		/// <param name="spriteBatch"></param>
		/// <param name="position"></param>
		/// <param name="index"></param>
		/// <param name="variation"></param>
		public void DrawTile(SpriteBatch spriteBatch, Vector2 position, int index, string variation = "")
		{
			if (index < 0)
			{
				return;
			}

			DrawTile(spriteBatch, position, Tiles[index], variation);
		}
	}
}
