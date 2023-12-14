using AlmostGoodEngine.Animations.Coroutine;
using AlmostGoodEngine.Core.Utils;
using AlmostGoodEngine.Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Runtime;

namespace AlmostGoodEngine.Core
{
    public class Engine : Game
    {
        /// <summary>
        /// Graphics device manager
        /// </summary>
        public readonly GraphicsDeviceManager Graphics;

        /// <summary>
        /// Sprite batch
        /// </summary>
        public SpriteBatch SpriteBatch { get; private set; }

        /// <summary>
        /// The game's settings
        /// </summary>
        public Settings Settings { get; set; }

        /// <summary>
        /// Used to manage the window resize calculations
        /// </summary>
        private bool _resizing = true;

        public Engine()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsFixedTimeStep = false;

            Settings = new Settings();

            Activated += Engine_Activated;
            Deactivated += Engine_Deactivated;
            Exiting += Engine_Exiting;
            Disposed += Engine_Disposed;

            GCSettings.LatencyMode = GCLatencyMode.SustainedLowLatency;
        }

        /// <summary>
        /// Load game's settings
        /// </summary>
        public void LoadSettings()
        {
            // Window position
            Window.Position = Settings.Position;

            if (Settings.CenteredAtLaunch)
            {
                Window.Position = new Point(
                    GraphicsDevice.Adapter.CurrentDisplayMode.Width / 2 - Settings.Width / 2, 
                    GraphicsDevice.Adapter.CurrentDisplayMode.Height / 2 - Settings.Height / 2);
            }

            // Window size
            Graphics.PreferredBackBufferWidth = Settings.Width;
            Graphics.PreferredBackBufferHeight = Settings.Height;
            Graphics.ApplyChanges();

            // Window other settings
            Window.AllowUserResizing = Settings.Resizable;
            Window.IsBorderless = Settings.Borderless;
            Window.Title = Settings.Name;

            // Window events
            Window.FileDrop += Window_FileDrop;
            Window.ClientSizeChanged += Window_ClientSizeChanged;
        }

        protected override void Initialize()
        {
            // Game manager initialization
            GameManager.Initialize(this);
            Draw2D.Initialize(GraphicsDevice);
            InputManager.Initialize();

            LoadSettings();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            GameManager.LoadContent();
            SpriteBatch = new(GraphicsDevice);
            GameManager.Start();
        }

        protected override void Update(GameTime gameTime)
        {
            // Game logic
            Time.Update(gameTime);

            // Inputs
            InputManager.Update();
            // Coroutine engine
            Coroutines.Update(gameTime);

            // Game manager update functions
            GameManager.BeforeUpdate(gameTime);
            GameManager.Update(gameTime);
            if (Time.IsFixedFrame)
            {
                GameManager.FixedUpdate(gameTime);
            }
            GameManager.AfterUpdate(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Settings.ClearColor);

            // Draw content
            Time.Draw(gameTime);
            GameManager.Draw(gameTime, SpriteBatch);
            GameManager.DrawUI(gameTime, SpriteBatch);

            base.Draw(gameTime);
        }

        private void Engine_Activated(object sender, EventArgs e)
        {
            Logger.Log("Engine activated.");
        }

        private void Engine_Deactivated(object sender, EventArgs e)
        {
            Logger.Log("Engine deactivated.");
        }

        private void Engine_Exiting(object sender, EventArgs e)
        {
            Logger.Log("Engine exiting...");
        }

        private void Engine_Disposed(object sender, EventArgs e)
        {
            Logger.Log("Engine disposed.");
        }

        /// <summary>
        /// Can be used for a hot-test or to load a almost good engine project (settings, or something else)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_FileDrop(object sender, FileDropEventArgs e)
        {
            Random random = new();
            Settings.ClearColor = new Color(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
        }

        private void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            // Capture the current position of the window
            Point windowPosition = Window.Position;

            if (Window.ClientBounds.Width > 0 && Window.ClientBounds.Height > 0 && _resizing)
            {
                Graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
                Graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
                Graphics.ApplyChanges();

                Window.Position = windowPosition;

                GameManager.Resize(GraphicsDevice.Viewport);
            }

            _resizing = true;

            Logger.Log("Window size changed.");
        }
    }
}