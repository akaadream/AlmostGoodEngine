using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace AlmostGoodEngine.Core.Tiling
{
	public class Tileset
	{
		/// <summary>
		/// The list of all the tiles
		/// </summary>
		public List<Tile> Tiles { get; set; }

		/// <summary>
		/// The texture representing the tileset
		/// </summary>
		public Texture2D Texture { get; set; }

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
		private string _filename;

		public Tileset(string filename)
		{
			Texture = GameManager.Engine.Content.Load<Texture2D>(filename);
			_filename = filename;
			Tiles = [];
		}

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

		public int IndexOf(Tile tile)
		{
			return Tiles.IndexOf(tile);
		}

		public void DrawTile(SpriteBatch spriteBatch, Vector2 position, Tile tile)
		{
			if (Texture == null)
			{
				return;
			}

			spriteBatch.Draw(Texture, position, tile.Source, Color.White);
		}

		public void DrawTile(SpriteBatch spriteBatch, Vector2 position, int index)
		{
			DrawTile(spriteBatch, position, Tiles[index]);
		}
	}
}
