using AlmostGoodEngine.Core.Scenes;
using AlmostGoodEngine.Core.Utils;
using AlmostGoodEngine.GUI;
using Gum.Wireframe;
using Microsoft.Xna.Framework.Content;

namespace AlmostGoodEngine.Tests.Scenes
{
	internal class GUIScene : Scene
	{
		GUILayout main;

		public GUIScene()
		{
			GUILayout menu = new();
			menu.Classes.Add("menu");

			main = new();
			main.Classes.Add("main");
			var mainStyle = main.Style;
            mainStyle.Height = mainStyle.Height = mainStyle.Height = Renderer.Cameras[0].Height - 42;

			GUIElement test = new();
			test.Classes.Add("test");

			main.Children.Add(test);

			GUIManager.Layouts.Add(menu);
			GUIManager.Layouts.Add(main);

            GUIManager.LoadFont("Content/Fonts/Signika-Bold.ttf");
            GUIManager.LoadStyle("Content/styles/main.css");
            GUIManager.LoadGum("TestUI");
			GUIManager.TryLoadScreen("Title", out GraphicalUiElement screen);

			if (screen == null)
			{
				Logger.Log("Screen not found");
			}
			else
			{
				Logger.Log("Screen loaded");
			}
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
        }
    }
}
