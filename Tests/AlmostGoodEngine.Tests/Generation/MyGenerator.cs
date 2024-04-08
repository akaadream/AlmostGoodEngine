using AlmostGoodEngine.Core;
using AlmostGoodEngine.Core.Generation;
using AlmostGoodEngine.Core.Tiling;
using AlmostGoodEngine.Core.Utils;
using AlmostGoodEngine.Generation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlmostGoodEngine.Tests.Generation
{
	public class MyGenerator : Generator
	{
		private const int BiomeSize = 4;

		public MyGenerator(): base(new Tileset("Sprites/tileset"), NoiseHelper.Seed(), 256, 256)
		{
			Tileset.Variations.Add("tileset_2", GameManager.Engine.Content.Load<Texture2D>("Sprites/tileset_2"));

			// Plain fielk
			Biome field = new("field", new Vector2(0.1f, 0.2f), new Vector2(-0.3f, 0.2f), new Vector2(0f), new Vector2(0f), new Vector2(0f));
			field.RegisterTile(Tileset.GetTile(7), 0f, 0.1f);
			field.RegisterTile(Tileset.GetTile(6), 0.1f, 0.2f);
			field.RegisterTile(Tileset.GetTile(10), 0.2f, 0.24f);
			field.RegisterTile(Tileset.GetTile(1), 0.24f, 0.56f);
			field.RegisterTile(Tileset.GetTile(0), 0.56f, 0.75f);
			field.RegisterTile(Tileset.GetTile(3), 0.75f, 0.87f);
			field.RegisterTile(Tileset.GetTile(2), 0.87f, 0.94f);
			field.RegisterTile(Tileset.GetTile(4), 0.94f, 1f);

			// Dark forest
			Biome darkForest = new("dark_forest", new(-0.4f, 0.1f), new(0.1f, 0.7f), new(0f), new(0f), new(0f))
			{
				TilesetVariation = true,
				TilesetVariationName = "tileset_2"
			};
			darkForest.RegisterTile(Tileset.GetTile(6), 0f, 0.2f);
			darkForest.RegisterTile(Tileset.GetTile(10), 0.2f, 0.24f);
			darkForest.RegisterTile(Tileset.GetTile(1), 0.24f, 0.4f);
			darkForest.RegisterTile(Tileset.GetTile(0), 0.4f, 0.6f);
			darkForest.RegisterTile(Tileset.GetTile(3), 0.6f, 0.75f);
			darkForest.RegisterTile(Tileset.GetTile(2), 0.75f, 0.92f);
			darkForest.RegisterTile(Tileset.GetTile(4), 0.92f, 1.0f);

			Biome ocean = new("ocean", new(-0.4f, 0.4f), new(0.5f, 1f), new(0f), new(0f), new(0f));
			ocean.RegisterTile(Tileset.GetTile(7), -1f, 0.9f);
			ocean.RegisterTile(Tileset.GetTile(6), 0.9f, 1.1f);

			// Snow
			Biome snow = new("snow", new(-1.2f, -0.2f), new(0.2f, 1.2f), new(), new(), new())
			{
				TilesetVariation = true,
				TilesetVariationName = "tileset_2"
			};
			snow.RegisterTile(Tileset.GetTile(3), 0f, 0.1f);
			snow.RegisterTile(Tileset.GetTile(2), 0.1f, 0.5f);
			snow.RegisterTile(Tileset.GetTile(4), 0.5f, 0.8f);
			snow.RegisterTile(Tileset.GetTile(5), 0.8f, 1.0f);

			Biome desert = new("desert", new(0.2f, 1.2f), new(-1.2f, -0.2f), new(), new(), new())
			{
				TilesetVariation = true,
				TilesetVariationName = "tileset_2"
			};
			desert.RegisterTile(Tileset.GetTile(7), 0f, 0.1f);
			desert.RegisterTile(Tileset.GetTile(6), 0.1f, 0.2f);
			desert.RegisterTile(Tileset.GetTile(10), 0.2f, 0.8f);
			desert.RegisterTile(Tileset.GetTile(3), 0.8f, 1f);

			RegisterBiome(field);
			RegisterBiome(darkForest);
			RegisterBiome(ocean);
			RegisterBiome(snow);
			RegisterBiome(desert);

			WorldData = new int[WorldWidth * WorldHeight];

			Initialize();
		}

		public void Initialize()
		{
			FastNoiseLite defaultNoise = NoiseHelper.Classic();
			defaultNoise.SetSeed(Seed);

			FastNoiseLite temperatureNoise = NoiseHelper.CreateCellular();
			temperatureNoise.SetSeed(Seed);

			FastNoiseLite humidityNoise = NoiseHelper.CreateCellular();
			humidityNoise.SetSeed(Seed);

			RegisterLayer("default", new GenerationLayer(256, 256, defaultNoise));
			RegisterLayer("temperatures", new GenerationLayer(256, 256, temperatureNoise));
			RegisterLayer("humidity", new GenerationLayer(256, 256, humidityNoise));
		}

		public override void Generate()
		{
			base.Generate();

			if (!Layers.TryGetValue("default", out GenerationLayer defaultLayer))
			{
				return;
			}
			if (!Layers.TryGetValue("temperatures", out GenerationLayer temperaturesLayer))
			{
				return;
			}
			if (!Layers.TryGetValue("humidity", out GenerationLayer humidityLayer))
			{
				return;
			}

			string lastBiome = "";

			for (int y = 0; y < WorldHeight; y++)
			{
				for (int x = 0; x < WorldWidth; x++)
				{
					int index = y * WorldWidth + x;
					int cellX = x / BiomeSize * BiomeSize;
					int cellY = y / BiomeSize * BiomeSize;
					int cellIndex = cellY * WorldWidth + cellX;

					var biome = FindBiome(temperaturesLayer.Data[cellIndex], humidityLayer.Data[cellIndex]);
					//Logger.Log("biome: " + biome.Name + " (index: " + reductedIndex + ", temperature: " + temperature + ", humidity: " + humidity + ")");

					if (biome == null)
					{
						continue;
					}

					if (biome.Name != lastBiome)
					{
						lastBiome = biome.Name;
						Logger.Log("New biome: " + biome.Name + " (temperature: " + temperaturesLayer.Data[cellIndex] + ", humidity: " + humidityLayer.Data[cellIndex] + ")");
					}

					if (index < defaultLayer.Data.Length)
					{
						var tile = biome.GetTile(defaultLayer.Data[index], defaultLayer.Min, defaultLayer.Max);
						if (tile.IsNull())
						{
							Logger.Log("ERROR: tile null (biome: " + biome.Name + ", index: " + index + ", value: " + defaultLayer.Data[index] + ")", "Generation", System.ConsoleColor.DarkYellow, System.ConsoleColor.DarkRed);
						}
						WorldData[index] = Tileset.IndexOf(tile);
					}
				}
			}

			Logger.Log("World Generated");
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);

			if (!Layers.TryGetValue("temperatures", out GenerationLayer temperaturesLayer))
			{
				return;
			}

			if (!Layers.TryGetValue("humidity", out GenerationLayer humidityLayer))
			{
				return;
			}

			for (int y = 0; y < WorldHeight; y++)
			{
				for (int x = 0; x < WorldWidth; x++)
				{
					int index = y * WorldWidth + x;
					int targetX = x * Tileset.TileSize;
					int targetY = y * Tileset.TileSize;
					int cellX = x / BiomeSize * BiomeSize;
					int cellY = y / BiomeSize * BiomeSize;
					int cellIndex = cellY * WorldWidth + cellX;

					if (!GameManager.MainCamera().CanSee(new Rectangle(targetX, targetY, Tileset.TileSize, Tileset.TileSize)))
					{
						continue;
					}

					// Retrive the current biome
					var biome = FindBiome(temperaturesLayer.Data[cellIndex], humidityLayer.Data[cellIndex]);

					// Draw the tileset
					Tileset.DrawTile(spriteBatch, new(targetX, targetY), WorldData[index], biome.TilesetVariationName);
				}
			}
		}

		private void DrawLayer(SpriteBatch spriteBatch, GenerationLayer layer, Color color1, Color color2)
		{
			if (layer == null)
			{
				return;
			}

			for (int y = 0; y < layer.Height; y++)
			{
				for (int x = 0; x < layer.Width; x++)
				{
					int targetX = x * Tileset.TileSize;
					int targetY = y * Tileset.TileSize;

					if (!GameManager.MainCamera().CanSee(new Rectangle(targetX, targetY, Tileset.TileSize, Tileset.TileSize)))
					{
						continue;
					}

					int index = y * WorldWidth + x;
					float to1 = BetterMath.To1(layer.Min, layer.Max, layer.Data[index]);
					Color color = Color.Lerp(color1, color2, to1);
					Debug.FillRectangle(spriteBatch, new Rectangle(targetX, targetY, Tileset.TileSize, Tileset.TileSize), color);
				}
			}
		}

		private void DrawTemperatures(SpriteBatch spriteBatch)
		{
			if (!Layers.TryGetValue("temperatures", out GenerationLayer temperaturesLayer))
			{
				return;
			}

			DrawLayer(spriteBatch, temperaturesLayer, Color.OrangeRed, Color.DarkViolet);
		}

		private void DrawHumidity(SpriteBatch spriteBatch)
		{
			if (!Layers.TryGetValue("humidity", out GenerationLayer humidityLayer))
			{
				return;
			}

			DrawLayer(spriteBatch, humidityLayer, Color.Black, Color.White);
		}
	}
}
