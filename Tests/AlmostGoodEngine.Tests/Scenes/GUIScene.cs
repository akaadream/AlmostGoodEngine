using AlmostGoodEngine.Core.Scenes;
using AlmostGoodEngine.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
			main.Style.Height = main.HoverStyle.Height = main.FocusStyle.Height = Renderer.Cameras[0].Height - 42;

			GUIElement test = new();
			test.Classes.Add("test");

			main.Children.Add(test);

			GUIManager.Layouts.Add(menu);
			GUIManager.Layouts.Add(main);
		}

		public override void Resize(Viewport viewport)
		{
			base.Resize(viewport);

			main.Style.Height = main.HoverStyle.Height = main.FocusStyle.Height = Renderer.Cameras[0].Height - 42;
		}
	}
}
