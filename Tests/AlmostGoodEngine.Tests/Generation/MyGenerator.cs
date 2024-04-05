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
		public MyGenerator(): base(new Tileset("Sprites/tileset"), NoiseHelper.Seed(), 256, 256)
		{
			Tileset.Variations.Add("tileset_2", GameManager.Engine.Content.Load<Texture2D>("Sprites/tileset_2"));

			// Plain fielk
			Biome field = new("field", new Vector2(-0.2f, 0.3f), new Vector2(-0.3f, 0.2f), new Vector2(0f), new Vector2(0f), new Vector2(0f));
			field.RegisterTile(Tileset.GetTile(7), 0f, 0.3f);
			field.RegisterTile(Tileset.GetTile(6), 0.3f, 0.4f);
			field.RegisterTile(Tileset.GetTile(1), 0.4f, 0.7f);
			field.RegisterTile(Tileset.GetTile(0), 0.7f, 0.85f);
			field.RegisterTile(Tileset.GetTile(3), 0.85f, 1.3f);

			// Dark forest
			Biome darkForest = new("dark_forest", new(-0.6f, -0.2f), new(0.2f, 0.6f), new(0f), new(0f), new(0f))
			{
				TilesetVariation = true,
				TilesetVariationName = "tileset_2"
			};
			darkForest.RegisterTile(Tileset.GetTile(6), 0f, 0.1f);
			darkForest.RegisterTile(Tileset.GetTile(1), 0.1f, 0.4f);
			darkForest.RegisterTile(Tileset.GetTile(0), 0.4f, 0.6f);
			darkForest.RegisterTile(Tileset.GetTile(3), 0.6f, 0.75f);
			darkForest.RegisterTile(Tileset.GetTile(2), 0.75f, 0.92f);
			darkForest.RegisterTile(Tileset.GetTile(4), 0.92f, 1.3f);

			Biome ocean = new("ocean", new(-0.2f, 0.4f), new(0.9f, 1f), new(0f), new(0f), new(0f));
			ocean.RegisterTile(Tileset.GetTile(7), -1f, 0.9f);
			ocean.RegisterTile(Tileset.GetTile(6), 0.9f, 1.1f);

			// Snow
			Biome snow = new("snow", new(-1f, -0.6f), new(0.4f, 0.8f), new(), new(), new())
			{
				TilesetVariation = true,
				TilesetVariationName = "tileset_2"
			};
			snow.RegisterTile(Tileset.GetTile(3), 0f, 0.1f);
			snow.RegisterTile(Tileset.GetTile(2), 0.1f, 0.5f);
			snow.RegisterTile(Tileset.GetTile(4), 0.5f, 0.8f);
			snow.RegisterTile(Tileset.GetTile(5), 0.8f, 1.3f);

			RegisterBiome(field);
			RegisterBiome(darkForest);
			RegisterBiome(ocean);
			RegisterBiome(snow);

			WorldData = new int[WorldWidth * WorldHeight];

			Initialize();
		}

		public void Initialize()
		{
			FastNoiseLite temperatureNoise = NoiseHelper.CreateCellular();
			//temperatureNoise.SetNoiseType(FastNoiseLite.NoiseType.Perlin);
			//temperatureNoise.SetFractalGain(0.9f);
			//temperatureNoise.SetFractalLacunarity(10f);
			//temperatureNoise.SetFractalOctaves(1);
			FastNoiseLite humidityNoise = NoiseHelper.CreateCellular();
			//humidityNoise.SetNoiseType(FastNoiseLite.NoiseType.Perlin);
			//humidityNoise.SetFractalGain(0.9f);
			//humidityNoise.SetFractalLacunarity(10f);
			//humidityNoise.SetFractalOctaves(1);

			RegisterLayer("default", new GenerationLayer(256, 256, NoiseHelper.Classic()));
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
					int reductedIndex = (y / 64 * 64) * WorldWidth + (x / 64 * 64);

					// Retrive the current biome
					var temperature = BetterMath.To1(temperaturesLayer.Min, temperaturesLayer.Max, temperaturesLayer.Data[reductedIndex]);
					var humidity = BetterMath.To1(humidityLayer.Min, humidityLayer.Max, humidityLayer.Data[reductedIndex]);

					var biome = FindBiome(temperature, humidity);
					//Logger.Log("biome: " + biome.Name + " (index: " + reductedIndex + ", temperature: " + temperature + ", humidity: " + humidity + ")");

					if (biome == null)
					{
						continue;
					}

					if (biome.Name != lastBiome)
					{
						lastBiome = biome.Name;
						Logger.Log("New biome: " + biome.Name + " (temperature: " + temperature + ", humidity: " + humidity + ")");
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
					int reductedIndex = (y / 64 * 64) * WorldWidth + (x / 64 * 64);

					// Retrive the current biome
					var biome = FindBiome(temperaturesLayer.Data[reductedIndex], humidityLayer.Data[reductedIndex]);

					// Draw the tileset
					Tileset.DrawTile(spriteBatch, new(x * Tileset.TileSize, y * Tileset.TileSize), WorldData[index], biome.TilesetVariationName);
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
					int index = y * WorldWidth + x;
					float to1 = BetterMath.To1(layer.Min, layer.Max, layer.Data[index]);
					Color color = Color.Lerp(color1, color2, to1);
					Debug.FillRectangle(spriteBatch, new Rectangle(x * Tileset.TileSize, y * Tileset.TileSize, Tileset.TileSize, Tileset.TileSize), color);
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
