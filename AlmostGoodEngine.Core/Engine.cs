using AlmostGoodEngine.Animations.Coroutine;
using AlmostGoodEngine.Audio;
using AlmostGoodEngine.Core.Utils;
using AlmostGoodEngine.Core.Utils.Consoles;
using AlmostGoodEngine.GUI;
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
        private bool _resizing = false;

        /// <summary>
        /// The previous width of the game
        /// </summary>
        private int _width = 0;

        /// <summary>
        /// The previous height of the game
        /// </summary>
        private int _height = 0;

        /// <summary>
        /// True if the game window is borderless
        /// </summary>
        private bool _isBorderless = false;

        /// <summary>
        /// True if the game is in fullscreen mode
        /// </summary>
        private bool _isFullscreen = false;

        /// <summary>
        /// If the engine may not do the rendering itself
        /// </summary>
        public bool CustomRendering { get; set; } = false;

        /// <summary>
        /// The game's viewport
        /// </summary>
        public static Viewport GameViewport { get; private set; }

        /// <summary>
        /// The window's viewport
        /// </summary>
        public static Viewport WindowViewport { get; private set; }

        /// <summary>
        /// The screen's scale matrix
        /// </summary>
        public static Matrix ScreenScaleMatrix { get; private set; } = Matrix.Identity;

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
            Graphics.GraphicsProfile = GraphicsProfile.HiDef;
            Graphics.PreferMultiSampling = true;
            Graphics.ApplyChanges();

            // Window other settings
            Window.AllowUserResizing = Settings.Resizable;
            Window.IsBorderless = Settings.Borderless;
            Window.Title = Settings.Name;

            // Window events
            // When a file is droped inside the window
            Window.FileDrop += Window_FileDrop;
            // When the user resized the client
            Window.ClientSizeChanged += Window_ClientSizeChanged;
        }

        protected override void Initialize()
        {
            // Game manager initialization
            GameManager.Initialize(this);
            // Drawing helper (used to draw shapes)
            Draw2D.Initialize(GraphicsDevice);
            // Inputs
            Input.Initialize();
            // Audio
            Mixer.Initialize(Content);
            // In-engine console
            AlmostGoodConsole.Initialize();

            // Load the application's settings
            LoadSettings();

            // Compute the default viewport
            UpdateScreenScaleMatrix();

			base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

			SpriteBatch = new(GraphicsDevice);

			// Start the app cycle by loading the game content
			GameManager.LoadContent();
            // Initialize the GUI library
			GUIManager.Initialize(Content, GraphicsDevice, Graphics);

            GameManager.SceneManager.EngineStarted = true;
            GameManager.SceneManager.LoadAtStart = Settings.StartingScene;

            // Scene starting
			GameManager.Start();

            BetterMath.Test();
            //Helper.Test();

#if DEBUG
            Logger.Log(GameViewport.ToString());
#endif
        }

        protected override void Update(GameTime gameTime)
        {
            // Game logic
            Time.Update(gameTime);

            // Inputs
            Input.Update();
            // Coroutine engine
            Coroutines.Update(gameTime);

            // Game manager update functions
            GameManager.BeforeUpdate(gameTime);

            // Console update
            AlmostGoodConsole.Update(gameTime);

            // If the console is not open, we can update  the game
            if (!AlmostGoodConsole.Opened)
            {
				// The fixed update loop
				while (Time.accumulator >= Time.FixedDeltaTimeTarget)
				{
					// Fixed update
					GameManager.FixedUpdate(gameTime);

					// Update the accumulator
					Time.accumulator -= Time.FixedDeltaTimeTarget;
				}

				GameManager.Update(gameTime);
            }

            // Update the GUI layer
            GUIManager.Update(gameTime);

            // After everything gets updated
            GameManager.AfterUpdate(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // Clear the screen
            GraphicsDevice.Clear(Settings.ClearColor);

            // Update the graphics device viewport
            GraphicsDevice.Viewport = GameViewport;

            // Capture the draw calls rate
            Time.Draw(gameTime);

            if (!CustomRendering)
            {
				GameManager.Draw(gameTime, SpriteBatch);
				GameManager.DrawUI(gameTime, SpriteBatch);
			}
            else
            {

            }

#if DEBUG
            GameManager.DrawDebug(gameTime, SpriteBatch);
#endif

            // Draw the GUI
            GUIManager.Draw(gameTime, SpriteBatch);

            AlmostGoodConsole.Draw(SpriteBatch);

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

            if (Window.ClientBounds.Width > 0 && Window.ClientBounds.Height > 0)
            {
                if (_resizing)
                {
					Graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
					Graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
					Graphics.ApplyChanges();

					Window.Position = windowPosition;
				}
                else
                {
                    _resizing = true;

                    // Update the screen's scale matrix
                    UpdateScreenScaleMatrix();
					GameManager.Resize(GameViewport);

					Logger.Log("Viewport resized. New size: " + WindowViewport.ToString());

					_resizing = false;
                }
            }
        }

        internal void ToggleFullscreen()
        {
            bool oldFullscreen = _isFullscreen;

            if (_isBorderless)
            {
                _isBorderless = false;
            }
            else
            {
                _isFullscreen = !_isFullscreen;
            }

            ApplyFullscreenChange(oldFullscreen);
        }

        internal void ToggleBorderless()
        {
            bool oldFullscreen = _isFullscreen;

            _isBorderless = !_isBorderless;
            _isFullscreen = _isBorderless;

            ApplyFullscreenChange(oldFullscreen);
        }

        private void ApplyHardwareMode()
        {
            Graphics.HardwareModeSwitch = !_isBorderless;
            Graphics.ApplyChanges();
        }

        private void ApplyFullscreenChange(bool oldFullscreen)
        {
            if (_isFullscreen)
            {
                if (oldFullscreen)
                {
                    ApplyHardwareMode();
                }
                else
                {
                    SetFullscreen();
                }
            }
            else
            {
                UnsetFullscreen();
            }
        }

        /// <summary>
        /// Enable the fullscreen mode
        /// </summary>
        private void SetFullscreen()
        {
            _width = Graphics.PreferredBackBufferWidth;
            _height = Graphics.PreferredBackBufferHeight;

            Graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            Graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            Graphics.HardwareModeSwitch = !_isBorderless;

            Graphics.IsFullScreen = true;
            Graphics.ApplyChanges();
        }

        /// <summary>
        /// Disable the fullscreen mode
        /// </summary>
        private void UnsetFullscreen()
        {
            Graphics.PreferredBackBufferWidth = _width;
            Graphics.PreferredBackBufferHeight = _height;
            Graphics.IsFullScreen = false;
            Graphics.ApplyChanges();
        }

        /// <summary>
        /// Toggle the window resizing
        /// </summary>
        internal void ToggleResizable()
        {
            Window.AllowUserResizing = !Window.AllowUserResizing;
        }

        /// <summary>
        /// Update the screen's scale matrix
        /// </summary>
        internal void UpdateScreenScaleMatrix()
        {
			float screenWidth = GraphicsDevice.PresentationParameters.BackBufferWidth;
			float screenHeight = GraphicsDevice.PresentationParameters.BackBufferHeight;

			int virtualWidth = (int)screenWidth;
			int virtualHeight = (int)screenHeight;

			if (screenWidth / Settings.Width > screenHeight / Settings.Height)
			{
				float aspect = screenHeight / Settings.Height;
				virtualWidth = (int)(aspect * Settings.Width);
			}
			else
			{
				float aspect = screenWidth / Settings.Width;
				virtualHeight = (int)(aspect * Settings.Height);
			}

            ScreenScaleMatrix = Matrix.CreateScale(virtualWidth / (float)Settings.Width);

			GameViewport = new()
			{
				X = (int)(screenWidth / 2 - virtualWidth / 2),
				Y = (int)(screenHeight / 2 - virtualHeight / 2),
				Width = virtualWidth,
				Height = virtualHeight,
				MinDepth = 0,
				MaxDepth = 1
			};

            WindowViewport = new()
            {
                X = 0,
                Y = 0,
                Width = (int)screenWidth,
                Height = (int)screenHeight,
                MinDepth = 0,
                MaxDepth = 1
            };
		}
    }
}