using AlmostGoodEngine.Core;
using AlmostGoodEngine.Editor;
using AlmostGoodEngine.GUI;
using AlmostGoodEngine.Tests.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Aseprite;
using MonoGameReload.Assets;
using MonoGameReload.Files;
using System.IO;

namespace AlmostGoodEngine.Tests
{
    public class Game1 : Engine
    {
        private FileWatcher _watcher;

        public Game1(): base()
        {
            Settings.Name = "Almost Good Engine - Test";
            Settings.Description = "The project to test everything";
            Settings.OriginCentered = true;
            Settings.StartingScene = "test";

            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            CustomRendering = true;
        }

		protected override void Initialize()
		{
			base.Initialize();

            _watcher = new(Content);

            AssetReloader.Initialize(
                _watcher.ProjectRootPath,
                Microsoft.Xna.Framework.Content.Pipeline.TargetPlatform.DesktopGL,
                GraphicsDevice);

            _watcher.LoadFiles();

            var file = _watcher.FilesTree.Find("styles/main");
            if (file != null)
            {
                file.Updated += StyleUpdated;
            }
		}

        private void StyleUpdated(object sender, FileSystemEventArgs args)
        {
            if (!AssetsManager.DataFiles.TryGetValue("styles/main", out string value))
            {
                return;
            }
            GUIManager.LoadPlainCss(value);
        }

		protected override void LoadContent()
        {
			base.LoadContent();

			GameManager.SceneManager.Add("test", new TestScene());
			GameManager.SceneManager.Add("inputs", new InputScene());
			GameManager.SceneManager.Add("procedural", new ProceduralScene());
			GameManager.SceneManager.Add("particles", new ParticlesScene());
			//GameManager.SceneManager.Add("gui", new GUIScene());

			GUIManager.LoadFont("Content/Fonts/Signika-Bold.ttf");
			GUIManager.LoadStyle("styles/main");

			MainEditor.Initialize(this);
			MainEditor.LoadContent();
        }

		protected override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);

            MainEditor.Draw(gameTime);
        }
	}
}