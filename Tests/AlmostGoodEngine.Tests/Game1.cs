using AlmostGoodEngine.Core;
using AlmostGoodEngine.Tests.Scenes;

namespace AlmostGoodEngine.Tests
{
    public class Game1 : Engine
    {
        public Game1(): base()
        {
            Settings.Name = "Almost Good Engine - Test";
            Settings.Description = "The project to test everything";
            Settings.OriginCentered = true;

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            GameManager.SceneManager.Add("test", new TestScene());
            GameManager.SceneManager.A

    }
}