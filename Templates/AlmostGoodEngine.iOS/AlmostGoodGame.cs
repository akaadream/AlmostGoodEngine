using AlmostGoodEngine.Core;

namespace AlmostGoodEngine.iOS
{
	public class AlmostGoodGame : Engine
	{
		public AlmostGoodGame()
		{
			Content.RootDirectory = "Content";
			IsMouseVisible = true;

			Settings.Name = "Almost Good Engine - Sample game";
			Settings.Description = "";
			Settings.OriginCentered = true;
		}

		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			base.Initialize();
		}

		protected override void LoadContent()
		{
			// TODO: use this.Content to load your game content here
		}
	}
}