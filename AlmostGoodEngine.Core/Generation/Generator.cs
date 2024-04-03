using AlmostGoodEngine.Core.Tiling;
using AlmostGoodEngine.Core.Utils;
using AlmostGoodEngine.Generation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using static AlmostGoodEngine.Generation.FastNoiseLite;

namespace AlmostGoodEngine.Core.Generation
{
	public class Generator
	{
		private int _seed = 1337;

		private FastNoiseLite _noise { get; set; }

		public Tileset Tileset { get; private set; }
		public List<TileHeight> Tiles { get; private set; }
		public int[] WorldData { get; private set; }

		public NoiseType NoiseType
		{
			get => _noiseType;
			set
			{
				_noiseType = value;
				_noise?.SetNoiseType(value);
			}
		}
		private NoiseType _noiseType = NoiseType.OpenSimplex2S;

		public float Frequency
		{
			get => _frequency;
			set
			{
				_frequency = value;
				_noise?.SetFrequency(value);
			}
		}
		private float _frequency = 0.01f;

		#region Fractal
		public FractalType FractalType
		{
			get => _fractalType;
			set
			{
				_fractalType = value;
				_noise?.SetFractalType(value);
			}
		}
		private FractalType _fractalType = FractalType.FBm;

		public int Octaves
		{
			get => _octaves;
			set
			{
				_octaves = value;
				_noise?.SetFractalOctaves(value);
			}
		}
		private int _octaves = 8;

		public int Lacunarity
		{
			get => _lacunarity;
			set
			{
				_lacunarity = value;
				_noise?.SetFractalLacunarity(value);
			}
		}
		private int _lacunarity = 2;

		public float Gain
		{
			get => _gain;
			set
			{
				_gain = value;
				_noise?.SetFractalGain(value);
			}
		}
		private float _gain = 0.5f;
		#endregion

		public int WorldWidth { get; set; }
		public int WorldHeight { get; set; }

		public bool Infinite { get; set; }
		public bool FallOff { get; set; }
		public float FallOffStart { get; set; }
		public float FallOffEnd { get; set; }


		public Generator(Tileset tileset, int seed = 1337, int worldWidth = 256, int worldHeight = 256)
		{
			_seed = seed;

			Logger.Log("Seed " + seed.ToString());

			Tileset = tileset;

			_noise = new(seed);
			_noise.SetFractalType(_fractalType);
			_noise.SetNoiseType(_noiseType);
			_noise.SetFrequency(_frequency);
			_noise.SetFractalOctaves(_octaves);
			_noise.SetFractalLacunarity(_lacunarity);
			_noise.SetFractalGain(_gain);

			_noise.SetDomainWarpType(DomainWarpType.BasicGrid);
			_noise.SetDomainWarpAmp(50f);
			_noise.SetCellularDistanceFunction(CellularDistanceFunction.EuclideanSq);
			_noise.SetCellularReturnType(CellularReturnType.Distance);

			WorldWidth = worldWidth;
			WorldHeight = worldHeight;

			WorldData = new int[WorldWidth * WorldHeight];

			FallOff = false;
			FallOffStart = 0.5f;
			FallOffEnd = 1.0f;

			Infinite = false;

			Tiles = [];
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
		/// Apply a new seed
		/// </summary>
		/// <param name="seed"></param>
		public void SetSeed(int seed)
		{
			_noise.SetSeed(seed);
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
		/// Generate the map
		/// </summary>
		public void Generate()
		{
			float min = 1f;
			float max = -1f;
			for (int y = 0; y < WorldHeight; y++)
			{
				for (int x = 0; x < WorldWidth; x++)
				{
					int index = y * WorldWidth + x;

					if (FractalType == FractalType.DomainWarpProgressive ||
						FractalType == FractalType.DomainWarpIndependent)
					{
						float fx = 0;
						float fy = 0;
						_noise.DomainWarp(ref fx, ref fy);
						
						float noise = _noise.GetNoise(fx, fy);
						if (FallOff)
						{
							noise = GetFalloffNoise(noise, x, y);
						}
						if (noise < min)
                        {
							min = noise;                            
                        }
						if (noise > max)
						{
							max = noise;
						}
						WorldData[index] = GetTile(noise);
					}
					else
					{
						float noise = _noise.GetNoise(x, y);
						if (FallOff)
						{
							noise = GetFalloffNoise(noise, x, y);
						}
						if (noise < min)
						{
							min = noise;
						}
						if (noise > max)
						{
							max = noise;
						}
						WorldData[index] = GetTile(noise);
					}
				}
			}

			Logger.Log("Min: " + min + ", Max: " + max);
		}

		public float GetFalloffNoise(float noise, float x, float y)
		{
			return ((noise + 1) * FalloffMapValue(x, y)) - 1.0f;
		}

		private float FalloffMapValue(float x, float y)
		{
			float i = x / WorldWidth * 2 - 1;
			float j = y / WorldHeight * 2 - 1;

			float value = MathF.Max(MathF.Abs(i), MathF.Abs(j));
			if (value < FallOffStart)
			{
				return 1.0f;
			}
			if (value > FallOffEnd)
			{
				return 0.0f;
			}

			return BetterMath.Smoothstep(1.0f, 0.0f, BetterMath.InverseLerp(FallOffStart, FallOffEnd, value));
		}

		public int GetTile(float height)
		{
			foreach (var tile in Tiles)
			{
				if (height >= tile.Min && height < tile.Max)
				{
					return Tileset.IndexOf(tile.Tile);
				}
			}

			return 0;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			for (int y = 0; y < WorldHeight; y++)
			{
				for (int x = 0; x < WorldWidth; x++)
				{
					Tileset.DrawTile(spriteBatch, new(x * Tileset.TileSize, y * Tileset.TileSize), WorldData[y * WorldWidth + x]);
				}
			}
		}
	}
}
