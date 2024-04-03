namespace AlmostGoodEngine.Core.Generation
{
	public class Chunk
	{
		public string Identifier { get; set; }

		public int Width { get; set; }
		public int Height { get; set; }

		public Chunk()
		{
			Identifier = string.Empty;
			Width = 16;
			Height = 16;
		}
	}
}
