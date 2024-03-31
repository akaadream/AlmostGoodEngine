using AlmostGoodEngine.Core;
using AlmostGoodEngine.GUI;
using AlmostGoodEngine.Tests.Scenes;
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

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
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
            if (!AssetsManager.DataFiles.ContainsKey("styles/main"))
            {
                return;
            }
            GUIManager.LoadPlainCss(AssetsManager.DataFiles["styles/main"]);
        }

		protected override void LoadContent()
        {
            base.LoadContent();

            GameManager.SceneManager.Add("test", new TestScene());
            GameManager.SceneManager.Add("inputs", new InputScene());
            //GameManager.SceneManager.Add("gui", new GUIScene());

            GameManager.SceneManager.Set("test");

			GUIManager.LoadFont("Content/Fonts/Signika-Bold.ttf");
			GUIManager.LoadStyle("styles/main");
        }
    }
}