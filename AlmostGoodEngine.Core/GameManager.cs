﻿using AlmostGoodEngine.Core.Entities;
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
        /// The game engine instance
        /// </summary>
        public static Engine Engine { get; private set; }

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
        public static SpriteBatch SpriteBatch { get => Engine.SpriteBatch; }

        /// <summary>
        /// If the game should be paused
        /// </summary>
        public static bool Paused { get; set; }

        /// <summary>
        /// The render target used to create the game texture
        /// </summary>
        public static RenderTarget2D RenderTarget { get; set; }

        /// <summary>
        /// The texture each frames
        /// </summary>
        public static Texture2D FrameTexture { get; set; } = null;

        /// <summary>
        /// Colors array of the frame texture
        /// </summary>
        private static Color[] _frameTextureColors;


        /// <summary>
        /// Game initialization
        /// </summary>
        /// <param name="game"></param>
        internal static void Initialize(Engine engine)
        {
            Engine = engine;
            SceneManager = new SceneManager();
            FontSystem = new FontSystem();
            FontSystem.AddFont(File.ReadAllBytes(@"Fonts/Signika.ttf"));

            UpdateRenderTarget(800, 600);
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

            foreach (var camera in current.Renderer.Cameras)
            {
                if (camera != null)
                {
                    return camera;
                }
            }

            return null;
        }

        public static RenderTarget2D CreateRenderTarget(int width, int height)
        {
            if (width <= 0 || height <= 0)
            {
                return null;
            }

            return new(
                Engine.GraphicsDevice,
                width, height,
                false,
                Engine.GraphicsDevice.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24,
                0,
                RenderTargetUsage.PreserveContents);
        }

        public static void UpdateRenderTarget(int width, int height)
        {
            RenderTarget = CreateRenderTarget(width, height);
            FrameTexture = new(Engine.GraphicsDevice, width, height);
            _frameTextureColors = new Color[width * height];
        }

        public static Texture2D ScreenTexture(GameTime gameTime)
        {
            // Get the main camera
            var camera = MainCamera();
            if (camera == null)
            {
                return null;
            }

            // Create a render target
            if (RenderTarget == null)
            {
                return null;
            }

            // Graphics device settings
            Engine.GraphicsDevice.Clear(Color.Transparent);
            Engine.GraphicsDevice.SetRenderTarget(RenderTarget);

            // Draw the scene
            Draw(gameTime, SpriteBatch);
            DrawUI(gameTime, SpriteBatch);

            // Create the final texture and release the render target
			RenderTarget.GetData(_frameTextureColors);
			FrameTexture.SetData(_frameTextureColors);

			// Clear the screen
			Engine.GraphicsDevice.Clear(Color.Transparent);
			Engine.GraphicsDevice.SetRenderTargets(null);

            return FrameTexture;
        }

        /// <summary>
        /// Load the content of scenes
        /// </summary>
        internal static void LoadContent()
        {
            SceneManager.LoadContent(Engine.Content);
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
            SceneManager?.Resize(viewport);
        }

        /// <summary>
        /// Life-cycle before the update
        /// </summary>
        /// <param name="gameTime"></param>
        internal static void BeforeUpdate(GameTime gameTime)
        {
            SceneManager?.BeforeUpdate(gameTime);
        }

        /// <summary>
        /// Life-cycle update
        /// </summary>
        /// <param name="gameTime"></param>
        internal static void Update(GameTime gameTime)
        {
            SceneManager?.Update(gameTime);
        }

        /// <summary>
        /// Life-cycle physics update
        /// </summary>
        /// <param name="gameTime"></param>
        internal static void FixedUpdate(GameTime gameTime)
        {
            SceneManager?.FixedUpdate(gameTime);
        }

        /// <summary>
        /// Life-cycle animations update
        /// </summary>
        /// <param name="gameTime"></param>
        internal static void AnimationsUpdate(GameTime gameTime)
        {
            SceneManager?.AnimationsUpdate(gameTime);
        }

        /// <summary>
        /// Life-cycle after the update
        /// </summary>
        /// <param name="gameTime"></param>
        internal static void AfterUpdate(GameTime gameTime)
        {
            SceneManager?.AfterUpdate(gameTime);
        }

        /// <summary>
        /// Life-cycle draw
        /// </summary>
        /// <param name="gameTime"></param>
        internal static void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            SceneManager?.Draw(gameTime, spriteBatch);
        }

        /// <summary>
        /// Life-cycle draw UI
        /// </summary>
        /// <param name="gameTime"></param>
        internal static void DrawUI(GameTime gameTime, SpriteBatch spriteBatch)
        {
            SceneManager?.DrawUI(gameTime, spriteBatch);
        }

        /// <summary>
        /// Life-cycle draw debug
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        internal static void DrawDebug(GameTime gameTime, SpriteBatch spriteBatch)
        {
            SceneManager?.DrawDebug(gameTime, spriteBatch);
        }

        /// <summary>
        /// Toggle the fullscreen mode
        /// </summary>
        public static void ToggleFullscreen()
        {
            Engine.ToggleFullscreen();
        }

        /// <summary>
        /// Toggle the borderless mode
        /// </summary>
        public static void ToggleBorderless()
        {
            Engine.ToggleBorderless();
        }

        /// <summary>
        /// Toggle the window resizing
        /// </summary>
        public static void ToggleResizable()
        {
            Engine.ToggleResizable();
        }
    }
}
