using AlmostGoodEngine.Core.Tiling;
using AlmostGoodEngine.Core.Utils;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace AlmostGoodEngine.Core.Generation
{
	public class Generator
	{
		public Tileset Tileset { get; private set; }
		public List<TileHeight> Tiles { get; private set; }
		public int[] WorldData { get; protected set; }

		public int WorldWidth { get; set; }
		public int WorldHeight { get; set; }

		public bool Infinite { get; set; }
		public bool FallOff { get; set; }
		public float FallOffStart { get; set; }
		public float FallOffEnd { get; set; }

		public Dictionary<string, GenerationLayer> Layers { get; private set; }
		public Dictionary<string, Biome> Biomes { get; private set; }

		public Generator(Tileset tileset, int seed = 1337, int worldWidth = 256, int worldHeight = 256)
		{
			Logger.Log("Seed " + seed.ToString());

			Tileset = tileset;
			if (Tileset != null)
			{
				Tileset.AutoGenerate();
			}

			WorldWidth = worldWidth;
			WorldHeight = worldHeight;

			WorldData = new int[WorldWidth * WorldHeight];

			FallOff = false;
			FallOffStart = 0.5f;
			FallOffEnd = 1.0f;

			Infinite = false;

			Tiles = [];
			Layers = [];
			Biomes = [];
		}

		public void RegisterTile(Tile tile, float min = 0.0f, float max = 1.0f)
		{
			if (Exists(tile))
			{
				return;
			}

			Tiles.Add(new()
			{
				Min = min,
				Max = max,
				Tile = tile,
			});
		}

		public void RegisterBiome(Biome biome)
		{
			if (Biomes.TryGetValue(biome.Name, out Biome b))
			{
				return;
			}

			Biomes.Add(biome.Name, biome);
		}

		public bool RemoveBiome(string name)
		{
			return Biomes.Remove(name);
		}

		public Biome FindBiome(float temperature, float humidity)
		{
			Biome defaultBiome = null;
			foreach (var biome in Biomes.Values)
			{
				if (defaultBiome == null)
				{
					defaultBiome = biome;
				}

				if (temperature >= biome.Temperature.X &&
					temperature < biome.Temperature.Y &&
					humidity >= biome.Humidity.X &&
					humidity < biome.Humidity.Y)
				{
					return biome;
				}
			}

			return defaultBiome;
		}

		public bool Exists(Tile tile)
		{
			foreach (var tileHeight in Tiles)
			{
				if (tileHeight.Tile.X == tile.X &&
					tileHeight.Tile.Y == tile.Y)
				{
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Apply a new tileset
		/// </summary>
		/// <param name="tileset"></param>
		public void SetTileset(Tileset tileset)
		{
			Tileset = tileset;
		}

		/// <summary>
		/// Register a new layer
		/// </summary>
		/// <param name="name"></param>
		/// <param name="layer"></param>
		public void RegisterLayer(string name, GenerationLayer layer)
		{
			if (Layers.TryGetValue(name, out GenerationLayer existing))
			{
				Layers[name] = layer;
				return;
			}

			Layers.Add(name, layer);
		}

		/// <summary>
		/// Remove an existing layer
		/// </summary>
		/// <param name="name"></param>
		public void RemoveLayer(string name)
		{
			Layers.Remove(name);
		}

		/// <summary>
		/// Process generation layers
		/// </summary>
		public void Process()
		{
			foreach (var layer in Layers.Values)
			{
				layer.Process();
			}
		}

		/// <summary>
		/// Generate the map
		/// </summary>
		public virtual void Generate()
		{
			Process();
		}

		/// <summary>
		/// Retrieve a generation layer using the given name
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public GenerationLayer GetLayer(string name)
		{
			if (Layers.TryGetValue(name, out GenerationLayer value))
			{
				return value;
			}

			return default;
		}

		/// <summary>
		/// Render the generated world
		/// </summary>
		/// <param name="spriteBatch"></param>
		public virtual void Draw(SpriteBatch spriteBatch)
		{
			
		}
	}
}
