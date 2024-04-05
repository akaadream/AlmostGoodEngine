namespace AlmostGoodEngine.Core.Generation
{
	public class Chunk
	{
		public string Identifier { get; set; }

		public int Width { get; set; }
		public int Height { get; set; }

		public int[] Data { get; set; }

		/// <summary>
		/// The generator used
		/// </summary>
		public Generator Generator { get; set; }

		/// <summary>
		/// The biome 
		/// </summary>
		public Biome Biome { get; set; }

		public Chunk(Generator generator)
		{
			Identifier = string.Empty;
			Width = 16;
			Height = 16;

			Generator = generator;

			Data = new int[Width * Height];
		}

		public void Draw()
		{

		}
	}
}
