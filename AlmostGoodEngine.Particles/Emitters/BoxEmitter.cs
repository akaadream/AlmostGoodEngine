using Microsoft.Xna.Framework;
using System;

namespace AlmostGoodEngine.Particles.Emitters
{
	public class BoxEmitter(Vector3 position, Vector3 size) : Emitter
	{
		/// <summary>
		/// The position of the box emitter
		/// </summary>
		public Vector3 Position { get; set; } = position;

		/// <summary>
		/// The size of the box emitter
		/// </summary>
		public Vector3 Size { get; set; } = size;

		/// <summary>
		/// The minimum X position of the box
		/// </summary>
		public float MinX { get => Position.X; }

		/// <summary>
		/// The maximum X position of the box
		/// </summary>
		public float MaxX { get => Position.X + Size.X; }

		/// <summary>
		/// The minimum Y position of the box
		/// </summary>
		public float MinY { get => Position.Y; }

		/// <summary>
		/// The maximum Y position of the box
		/// </summary>
		public float MaxY { get => Position.Y + Size.Y; }

		/// <summary>
		/// The minimum Z position of the box
		/// </summary>
		public float MinZ { get => Position.Z; }

		/// <summary>
		/// The maximum Z position of the box
		/// </summary>
		public float MaxZ { get => Position.Z + Size.Z; }

		// The random instance to generate random positions inside the box
		private readonly Random _random = new();

		/// <summary>
		/// Get the next random position inside the box
		/// </summary>
		/// <returns></returns>
		public override Vector3 Next()
		{
			float x = GetRandom(MinX, MaxX);
			float y = GetRandom(MinY, MaxY);
			float z = GetRandom(MinZ, MaxZ);

			return new(x, y, z);
		}

		/// <summary>
		/// Get a random float between the min/max
		/// </summary>
		/// <param name="min"></param>
		/// <param name="max"></param>
		/// <returns></returns>
		private float GetRandom(float min, float max) => (float)_random.NextDouble() * (max - min) + min;
	}
}
