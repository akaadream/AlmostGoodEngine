using AlmostGoodEngine.Core.Utils;
using AlmostGoodEngine.Generation;
using System;

namespace AlmostGoodEngine.Core.Generation
{
	public class GenerationLayer
	{
		public FastNoiseLite Noise { get; set; }

		public int Width { get; set; }
		public int Height { get; set; }
		public int CellSize { get; set; }

		public float[] Data;

		public float Min { get; protected set; }
		public float Max { get; protected set; }

		public bool FallOff { get; set; }
		public float FallOffStart { get; set; }
		public float FallOffEnd { get; set; }

		public GenerationLayer(int width, int height, FastNoiseLite noise = null)
		{
			if (noise != null)
			{
				Noise = noise;
			}
			else
			{
				Noise = new FastNoiseLite();
			}

			Width = width;
			Height = height;

			Data = new float[Width * Height];
		}

		public virtual void Process()
		{
			// Default
			Max = -1f;
			Min = 1f;

			// Fill the data
			for (int y = 0; y < Height; y++)
			{
				for (int x = 0; x < Width; x++)
				{
					var noise = Noise.GetNoise(x, y);
					if (noise > Max)
					{
						Max = noise;
					}
					if (noise < Min)
					{
						Min = noise;
					}

					if (FallOff)
					{
						Data[y * Width + x] = GetFalloffNoise(noise, x, y);
					}
					else
					{
						Data[y * Width + x] = noise;
					}
				}
			}

			Logger.Log("Layer (min: " + Min + ", max: " + Max + ")");
		}

		public float GetFalloffNoise(float noise, float x, float y)
		{
			return ((noise + 1) * FalloffMapValue(x, y)) - 1.0f;
		}

		private float FalloffMapValue(float x, float y)
		{
			float i = x / Width * 2 - 1;
			float j = y / Height * 2 - 1;

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
	}
}
