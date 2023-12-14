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
        public static SceneManager SceneManager { get; private set; }

        public static FontSystem FontSystem { get; private set; }

        public static SpriteBatch SpriteBatch { get => Game.SpriteBatch; }

        public static void Initialize(Engine game)
        {
            Game = game;
            SceneManager = new SceneManager();
            FontSystem = new FontSystem();
            FontSystem.AddFont(File.ReadAllBytes(@"Fonts/Signika.ttf"));
        }

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
        public static void LoadContent()
        {
            SceneManager.LoadContent(Game.Content);
        }

        public static void Start()
        {
            SceneManager?.Start();
        }

        public static void Resize(Viewport viewport)
        {
            SceneManager?.CurrentScene?.Renderer.Resize(viewport);
        }

        /// <summary>
        /// Life-cycle before the update
        /// </summary>
        /// <param name="gameTime"></param>
        public static void BeforeUpdate(GameTime gameTime)
        {
            SceneManager?.CurrentScene?.BeforeUpdate(gameTime);
        }

        /// <summary>
        /// Life-cycle update
        /// </summary>
        /// <param name="gameTime"></param>
        public static void Update(GameTime gameTime)
        {
            SceneManager?.CurrentScene?.Update(gameTime);
        }

        public static void FixedUpdate(GameTime gameTime)
        {
            SceneManager?.CurrentScene?.FixedUpdate(gameTime);
        }

        /// <summary>
        /// Life-cycle after the update
        /// </summary>
        /// <param name="gameTime"></param>
        public static void AfterUpdate(GameTime gameTime)
        {
            SceneManager?.CurrentScene?.AfterUpdate(gameTime);
        }

        /// <summary>
        /// Life-cycle draw
        /// </summary>
        /// <param name="gameTime"></param>
        public static void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            SceneManager?.CurrentScene?.Draw(gameTime, spriteBatch);
        }

        /// <summary>
        /// Life-cycle draw UI
        /// </summary>
        /// <param name="gameTime"></param>
        public static void DrawUI(GameTime gameTime, SpriteBatch spriteBatch)
        {
            SceneManager?.CurrentScene?.DrawUI(gameTime, spriteBatch);
        }
    }
}
