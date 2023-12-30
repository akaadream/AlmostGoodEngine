using AlmostGoodEngine.Core.Entities;
using AlmostGoodEngine.Core.Scenes;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace AlmostGoodEngine.Core
{
    public static class GameManager
    {
        /// <summary>
        /// The game instance
        /// </summary>
        public static Engine Game { get; private set; }

        /// <summary>
        /// The scene manager
        /// </summary>
        public static SceneManager SceneManager { get; private set; }

        /// <summary>
        /// The font system used to draw text
        /// </summary>
        public static FontSystem FontSystem { get; private set; }

        /// <summary>
        /// The spritebatch used to draw the game's content
        /// </summary>
        public static SpriteBatch SpriteBatch { get => Game.SpriteBatch; }

        /// <summary>
        /// Game initialization
        /// </summary>
        /// <param name="game"></param>
        internal static void Initialize(Engine game)
        {
            Game = game;
            SceneManager = new SceneManager();
            FontSystem = new FontSystem();
            FontSystem.AddFont(File.ReadAllBytes(@"Fonts/Signika.ttf"));
        }

        /// <summary>
        /// Return the current active scene
        /// </summary>
        /// <returns></returns>
        public static Scene CurrentScene()
        {
            if (SceneManager == null)
            {
                return null;
            }

            if (SceneManager.CurrentScene == null)
            {
                return null;
            }

            return SceneManager.CurrentScene;
        }

        /// <summary>
        /// Return the main camera used by the current scene
        /// </summary>
        /// <returns></returns>
        public static Camera2D MainCamera()
        {
            Scene current = CurrentScene();

            if (current == null)
            {
                return null;
            }

            if (current.Renderer.Camera == null)
            {
                return null;
            }

            return current.Renderer.Camera;
        }

        /// <summary>
        /// Load the content of scenes
        /// </summary>
        internal static void LoadContent()
        {
            SceneManager.LoadContent(Game.Content);
        }

        /// <summary>
        /// Called when the game sart
        /// </summary>
        internal static void Start()
        {
            SceneManager?.Start();
        }

        /// <summary>
        /// Called when the window is resized
        /// </summary>
        /// <param name="viewport"></param>
        internal static void Resize(Viewport viewport)
        {
            SceneManager?.CurrentScene?.Renderer.Resize(viewport);
        }

        /// <summary>
        /// Life-cycle before the update
        /// </summary>
        /// <param name="gameTime"></param>
        internal static void BeforeUpdate(GameTime gameTime)
        {
            SceneManager?.CurrentScene?.BeforeUpdate(gameTime);
        }

        /// <summary>
        /// Life-cycle update
        /// </summary>
        /// <param name="gameTime"></param>
        internal static void Update(GameTime gameTime)
        {
            SceneManager?.CurrentScene?.Update(gameTime);
        }

        internal static void FixedUpdate(GameTime gameTime)
        {
            SceneManager?.CurrentScene?.FixedUpdate(gameTime);
        }

        /// <summary>
        /// Life-cycle after the update
        /// </summary>
        /// <param name="gameTime"></param>
        internal static void AfterUpdate(GameTime gameTime)
        {
            SceneManager?.CurrentScene?.AfterUpdate(gameTime);
        }

        /// <summary>
        /// Life-cycle draw
        /// </summary>
        /// <param name="gameTime"></param>
        internal static void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            SceneManager?.CurrentScene?.Draw(gameTime, spriteBatch);
        }

        /// <summary>
        /// Life-cycle draw UI
        /// </summary>
        /// <param name="gameTime"></param>
        internal static void DrawUI(GameTime gameTime, SpriteBatch spriteBatch)
        {
            SceneManager?.CurrentScene?.DrawUI(gameTime, spriteBatch);
        }

        /// <summary>
        /// Toggle the fullscreen mode
        /// </summary>
        public static void ToggleFullscreen()
        {
            Game.ToggleFullscreen();
        }

        /// <summary>
        /// Toggle the borderless mode
        /// </summary>
        public static void ToggleBorderless()
        {
            Game.ToggleBorderless();
        }

        /// <summary>
        /// Toggle the window resizing
        /// </summary>
        public static void ToggleResizable()
        {
            Game.ToggleResizable();
        }
    }
}
