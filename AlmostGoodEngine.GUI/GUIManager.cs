using Apos.Shapes;
using ExCSS;
using FontStashSharp;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;
using GumRuntime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RenderingLibrary;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ToolsUtilities;

namespace AlmostGoodEngine.GUI
{
	public static class GUIManager
	{
		/// <summary>
		/// The current width of the layout
		/// </summary>
		public static int Width { get => _graphics.PreferredBackBufferWidth; }

		/// <summary>
		/// The current heigh of the layout
		/// </summary>
		public static int Height { get => _graphics.PreferredBackBufferHeight; }

		/// <summary>
		/// GUI Layouts
		/// </summary>
		public static List<GUILayout> Layouts { get; private set; }

		/// <summary>
		/// The shape batch used to create graphics
		/// </summary>
		private static ShapeBatch _shapeBatch { get; set; }

		/// <summary>
		/// The graphics device used by the application
		/// </summary>
		private static GraphicsDevice _graphicsDevice { get; set; }

		/// <summary>
		/// The instance of the graphics device manager
		/// </summary>
		private static GraphicsDeviceManager _graphics { get; set; }

		/// <summary>
		/// Current mouse state
		/// </summary>
		public static MouseState MouseState { get; private set; }

		/// <summary>
		/// Previous mouse state
		/// </summary>
		public static MouseState PreviousState { get; private set; }

		/// <summary>
		/// The current X coordinate of the mouse
		/// </summary>
		public static int MouseX { get => MouseState.X; }

		/// <summary>
		/// The current Y coordinate of the mouse
		/// </summary>
		public static int MouseY { get => MouseState.Y; }

		/// <summary>
		/// The content manager used by the project
		/// </summary>
		private static ContentManager _contentManager;

		/// <summary>
		/// The font system used by the manager
		/// </summary>
		public static FontSystem FontSystem { get; private set; }

		/// <summary>
		/// The list of all loaded stylesheets
		/// </summary>
		internal static List<Stylesheet> Stylesheets { get; set; }

		/// <summary>
		/// The current gum project loaded
		/// </summary>
		public static GumProjectSave GumProject { get; private set; }

		/// <summary>
		/// The current screen loaded from the gum project save
		/// </summary>
		public static GraphicalUiElement CurrentScreen { get; private set; }

		/// <summary>
		/// Initialize the GUI library
		/// </summary>
		/// <param name="contentManager"></param>
		/// <param name="graphicsDevice"></param>
		public static void Initialize(ContentManager contentManager, GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics)
		{
			Layouts = [];
			Stylesheets = [];

			if (contentManager != null && graphicsDevice != null)
			{
				_shapeBatch = new(graphicsDevice, contentManager);
				_graphicsDevice = graphicsDevice;
				_graphics = graphics;
				_contentManager = contentManager;
			}

			FontSystem = new();

			SystemManagers.Default = new();
			SystemManagers.Default.Initialize(graphicsDevice, fullInstantiation: true);
			FileManager.RelativeDirectory = contentManager.RootDirectory;
		}

		/// <summary>
		/// Load the gum project
		/// </summary>
		/// <param name="fileName"></param>
		public static void LoadGum(string fileName)
		{
			GumProject = GumProjectSave.Load(fileName);
			ObjectFinder.Self.GumProjectSave = GumProject;
            GumProject.Initialize();

            CurrentScreen = GumProject.Screens.First().ToGraphicalUiElement(SystemManagers.Default, addToManagers: true);
		}

		/// <summary>
		/// Try to load a screen from the current gum project
		/// </summary>
		/// <param name="screenName"></param>
		/// <param name="screen"></param>
		/// <returns></returns>
		public static bool TryLoadScreen(string screenName, out GraphicalUiElement screen)
		{
			if (GumProject == null)
			{
				screen = null;
				return false;
			}

			foreach (ScreenSave element in GumProject.Screens)
			{
				if (element.Name == screenName)
				{
					screen = element.ToGraphicalUiElement(SystemManagers.Default, addToManagers: true);
					return true;
				}
			}

			screen = null;
			return false;
		}

		public static GraphicalUiElement GetScreenElement(string elementName)
		{
			if (CurrentScreen == null)
			{
				return null;
			}

			return CurrentScreen.GetGraphicalUiElementByName(elementName);
		}

		public static void LoadFont(string fontName)
		{
			if (FontSystem == null)
			{
				return;
			}

			if (!File.Exists(fontName))
			{
				return;
			}

			FontSystem.AddFont(File.ReadAllBytes(@"" + fontName));
		}

		public static SpriteFontBase GetFont(int size)
		{
			return FontSystem.GetFont(size);
		}

		public static void LoadStyle(string filename)
		{
			if (_contentManager == null)
			{
				return;
			}

			var stylesheet = _contentManager.Load<Stylesheet>(filename);
			Stylesheets.Add(stylesheet);
			ApplyStylesheets();
		}

		public static void LoadPlainCss(string css)
		{
			var parser = new StylesheetParser();
			var stylesheet = parser.Parse(css);
			Stylesheets.Add(stylesheet);
			ApplyStylesheets();
		}

		public static void ApplyStylesheets()
		{
			foreach (var stylesheet in Stylesheets)
			{
				foreach (var rule in stylesheet.StyleRules)
				{
					string[] selectors = rule.SelectorText.Split(':');

					if (selectors.Length == 0)
					{
						continue;
					}

					if (selectors.Length > 1)
					{
						
					}

					// Class
					if (rule.SelectorText.StartsWith("."))
					{
						foreach (var element in FindElements(selectors[0].Substring(1)))
						{
							if (selectors.Length > 1)
							{
								if (selectors[1] == "hover")
								{
									element.ApplyHoverStyle(rule.Style, true);
								}
								else if (selectors[1] == "focus")
								{
									element.ApplyFocusStyle(rule.Style, true);
								}
							}
							else
							{
								if (rule.SelectorText == ".test")
								{

								}
								element.ApplyStyle(rule.Style);
							}
						}
					}
					// Id
					else if (rule.SelectorText.StartsWith("#"))
					{
						var element = FindById(selectors[0].Substring(1));
						if (element != null)
						{
							if (rule.SelectorText.EndsWith(":hover"))
							{
								element.ApplyHoverStyle(rule.Style, true);
							}
							else if (rule.SelectorText.EndsWith(":focus"))
							{
								element.ApplyFocusStyle(rule.Style, true);
							}
							else
							{
								element.ApplyStyle(rule.Style);
							}
						}
					}
				}
			}
		}

		public static GUIElement FindById(string id)
		{
			foreach (var layout in Layouts)
			{
				if (layout.Id == id)
				{
					return layout;
				}

				if (layout.Children.Count > 0)
				{
					var element = layout.GetElementById(id);
					if (element != null)
					{
						return element;
					}
				}
			}

			return null;
		}

		public static List<GUIElement> FindElements(string className)
		{
			var elements = new List<GUIElement>();

			foreach (var layout in Layouts)
			{
				if (layout.Classes.Contains(className))
				{
					elements.Add(layout);
				}

				elements.AddRange(layout.GetElementsByClass(className));
			}

			return elements;
		}

		/// <summary>
		/// Update the GUI content
		/// </summary>
		/// <param name="gameTime"></param>
		public static void Update(GameTime gameTime)
		{
			SystemManagers.Default.Activity(gameTime.TotalGameTime.TotalSeconds);
			MouseState = Mouse.GetState();

			//Console.WriteLine("{Width: " + Width + ", Height: " + Height + "}");

			foreach (var layout in Layouts)
			{
				layout.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
			}
		}

		/// <summary>
		/// Define the scissor rectangle
		/// </summary>
		/// <param name="rectangle"></param>
		public static void SetScissor(Rectangle rectangle)
		{
			// Do not create a new instance of the rasterizer state if it currently got one with the scissor test enabled
			if (_graphicsDevice.RasterizerState == null || !_graphicsDevice.RasterizerState.ScissorTestEnable)
			{
				_graphicsDevice.RasterizerState = new RasterizerState
				{
					ScissorTestEnable = true,
				};
			}
			_graphicsDevice.ScissorRectangle = rectangle;
		}

		/// <summary>
		/// Clear the scissor rectangle currently used by the graphics device
		/// </summary>
		public static void ClearScissor()
		{
			_graphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
			_graphicsDevice.ScissorRectangle = Rectangle.Empty;
		}

		/// <summary>
		/// Draw the GUI content
		/// </summary>
		/// <param name="gameTime"></param>
		public static void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			SystemManagers.Default.Draw();
			if (_shapeBatch == null)
			{
				return;
			}

			foreach (var layout in Layouts)
			{
				layout.Draw(_shapeBatch, spriteBatch, (float)gameTime.ElapsedGameTime.TotalSeconds);
			}
		}

		internal static bool IsMouseLeftDown()
		{
			return MouseState.LeftButton == ButtonState.Pressed && PreviousState.LeftButton == ButtonState.Pressed;
		}

		internal static bool IsMouseLeftPressed()
		{
			return MouseState.LeftButton == ButtonState.Released && PreviousState.LeftButton == ButtonState.Pressed;
		}

		internal static bool IsMouseRightDown()
		{
			return MouseState.RightButton == ButtonState.Pressed && PreviousState.RightButton == ButtonState.Pressed;
		}

		internal static bool IsMouseRightPressed()
		{
			return MouseState.RightButton == ButtonState.Released && PreviousState.RightButton == ButtonState.Pressed;
		}
	}
}
