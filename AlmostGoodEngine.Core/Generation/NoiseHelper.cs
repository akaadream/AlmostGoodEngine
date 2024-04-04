using AlmostGoodEngine.Generation;
using System;
using static AlmostGoodEngine.Generation.FastNoiseLite;

namespace AlmostGoodEngine.Core.Generation
{
	public static class NoiseHelper
	{
		#region Predefined noises

		public static FastNoiseLite Default { get => _default; }
		private static FastNoiseLite _default = CreateNoise(Guid.NewGuid().GetHashCode());

		public static FastNoiseLite Cellular { get => _cellular; }
		private static FastNoiseLite _cellular = CreateCellular();

		public static FastNoiseLite Zones { get => _zones; }
		private static FastNoiseLite _zones = CreateCellular(0.015f, CellularDistanceFunction.Hybrid, CellularReturnType.CellValue);

		private static FastNoiseLite Value { get => _value; }
		private static FastNoiseLite _value = CreateValue();

		#endregion

		public static FastNoiseLite CreateNoise(
			int seed = 1337,
			NoiseType noiseType = NoiseType.OpenSimplex2,
			float noiseFrequency = 0.01f,
			FractalType fractalType = FractalType.FBm,
			int fractalOctaves = 8,
			int fractalLacunarity = 2,
			float fractalGain = 0.5f)
		{
			var noise = new FastNoiseLite();
			noise.SetSeed(seed);
			noise.SetNoiseType(noiseType);
			noise.SetFrequency(noiseFrequency);
			noise.SetFractalType(fractalType);
			noise.SetFractalOctaves(fractalOctaves);
			noise.SetFractalLacunarity(fractalLacunarity);
			noise.SetFractalGain(fractalGain);
			noise.SetDomainWarpType(DomainWarpType.BasicGrid);
			noise.SetDomainWarpAmp(50f);
			return noise;
		}

		private static FastNoiseLite CreateCellular(
			float frequency = 0.015f,
			CellularDistanceFunction cellularDistance = CellularDistanceFunction.EuclideanSq,
			CellularReturnType cellularReturn = CellularReturnType.Distance2Add,
			float cellularJitter = 1f)
		{
			var noise = CreateNoise(Seed(), NoiseType.Cellular, 0.015f, FractalType.None);
			noise.SetCellularDistanceFunction(cellularDistance);
			noise.SetCellularReturnType(cellularReturn);
			noise.SetCellularJitter(cellularJitter);
			return noise;
		}

		private static FastNoiseLite CreateValue()
		{
			var noise = CreateNoise(Seed(), NoiseType.Value, 0.015f, FractalType.None);
			noise.SetDomainWarpType(DomainWarpType.BasicGrid);
			noise.SetDomainWarpAmp(50f);
			return noise;
		}

		public static int Seed(string text = "")
		{
			if (string.IsNullOrWhiteSpace(text))
			{
				return Guid.NewGuid().GetHashCode();
			}

			return Guid.Parse(text).GetHashCode();
		}
	}
}
