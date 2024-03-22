using Apos.Shapes;
using ExCSS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace AlmostGoodEngine.GUI
{
	public static class GUIManager
	{
		public static int Width { get; private set; }

		public static int Height { get; private set; }

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

		public static MouseState MouseState { get; private set; }
		public static MouseState PreviousState { get; private set; }

		public static int MouseX { get => MouseState.X; }
		public static int MouseY { get => MouseState.Y; }

		private static ContentManager _contentManager;

		internal static List<Stylesheet> Stylesheets { get; set; }

		/// <summary>
		/// Initialize the GUI library
		/// </summary>
		/// <param name="contentManager"></param>
		/// <param name="graphicsDevice"></param>
		public static void Initialize(ContentManager contentManager, GraphicsDevice graphicsDevice)
		{
			Layouts = [];
			Stylesheets = [];

			if (contentManager != null && graphicsDevice != null)
			{
				_shapeBatch = new(graphicsDevice, contentManager);
				_graphicsDevice = graphicsDevice;
				Width = _graphicsDevice.Viewport.Width;
				Height = _graphicsDevice.Viewport.Height;
				_contentManager = contentManager;
			}
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
					
					// Class
					if (rule.SelectorText.StartsWith("."))
					{
						foreach (var element in FindElements(rule.SelectorText.Substring(1)))
						{
							if (Contains(selectors, "hover"))
							{
								element.ApplyHoverStyle(rule.Style);
							}
							else if (Contains(selectors, "focus"))
							{
								element.ApplyFocusStyle(rule.Style);
							}
							else
							{
								element.ApplyStyle(rule.Style);
							}
						}
					}
					// Id
					else if (rule.SelectorText.StartsWith("#"))
					{
						var element = FindById(rule.SelectorText.Substring(1));
						if (element != null)
						{
							if (Contains(selectors, "hover"))
							{
								element.ApplyHoverStyle(rule.Style);
							}
							else if (Contains(selectors, "focus"))
							{
								element.ApplyFocusStyle(rule.Style);
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

		private static bool Contains(string[] arr, string text)
		{
			foreach (var item in arr)
			{
				if (item == text)
				{
					return true;
				}
			}

			return false;
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
			MouseState = Mouse.GetState();

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
		public static void Draw(GameTime gameTime)
		{
			if (_shapeBatch == null)
			{
				return;
			}

			foreach (var layout in Layouts)
			{
				layout.Draw(_shapeBatch, (float)gameTime.ElapsedGameTime.TotalSeconds);
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
