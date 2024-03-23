using Apos.Shapes;
using ExCSS;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using MColor = Microsoft.Xna.Framework.Color;
using EColor = ExCSS.Color;
using AlmostGoodEngine.GUI.Utils;

namespace AlmostGoodEngine.GUI
{
	public class GUIElement
	{
		public string Id { get; set; }
		public List<string> Classes { get; set; } = [];

		public GUIStyle Style { get; set; }
		public GUIStyle HoverStyle { get; set; }
		public GUIStyle FocusStyle { get; set; }

		public GUIElement Parent { get; set; }
		public List<GUIElement> Children { get; set; } = [];

		#region State

		public bool IsHovered { get; protected set; }
		public bool IsPressed { get; protected set; }
		public bool IsDown { get; protected set; }

		#endregion

		#region Actions

		public Action OnHover { get; set; }
		public Action OnDown { get; set; }
		public Action OnPressed { get; set; }

		#endregion

		public GUITransition Transition { get; private set; }

		public GUIStyle CurrentStyle
		{
			get
			{
				if (IsDown)
				{
					return FocusStyle;
				}

				if (IsHovered)
				{
					return HoverStyle;
				}

				return Style;
			}
		}

		/// <summary>
		/// Get the X coordinate of this element
		/// </summary>
		public int X
		{
			get
			{
				int x = 0;
				if (Parent != null)
				{
					x = Parent.X;
				}
				x += CurrentStyle.Left;
				x -= CurrentStyle.Right;
				x += CurrentStyle.MarginLeft;
				x -= CurrentStyle.MarginRight;
				return x;
			}
		}

		/// <summary>
		/// Get the Y coordinate of this element
		/// </summary>
		public int Y
		{
			get
			{
				int y = 0;
				if (Parent != null)
				{
					y = Parent.Y;
				}
				y += CurrentStyle.Top;
				y -= CurrentStyle.Bottom;
				y += CurrentStyle.MarginLeft;
				y -= CurrentStyle.MarginRight;
				return y;
			}
		}

		public int Width
		{
			get
			{
				if (doingTransition)
				{
					switch (Transition)
					{
						case GUITransition.Hover:
							return (int)MathHelper.Lerp(Style.GetWidth(this), HoverStyle.GetWidth(this), transitionTimer / Style.TransitionDuration);
						case GUITransition.WasHover:
							return (int)MathHelper.Lerp(HoverStyle.GetWidth(this), Style.GetWidth(this), transitionTimer / Style.TransitionDuration);
					}
				}

				return CurrentStyle.GetWidth(this);
			}
		}

		public int Height
		{
			get
			{
				if (doingTransition)
				{
					switch (Transition)
					{
						case GUITransition.Hover:
							return (int)MathHelper.Lerp(Style.GetHeight(this), HoverStyle.GetHeight(this), transitionTimer / Style.TransitionDuration);
						case GUITransition.WasHover:
							return (int)MathHelper.Lerp(HoverStyle.GetHeight(this), Style.GetHeight(this), transitionTimer / Style.TransitionDuration);
					}
				}
				return CurrentStyle.GetHeight(this);
			}
		}

		private bool doingTransition = false;
		private bool transitionFinished = true;
		private float transitionTimer = 0f;

		private bool wasHovered = false;
		private bool wasDown = false;

		public MColor BackgroundColor
		{
			get
			{
				if (doingTransition)
				{
					switch (Transition)
					{
						case GUITransition.Hover:
							return MColor.Lerp(Style.BackgroundColor, HoverStyle.BackgroundColor, transitionTimer / Style.TransitionDuration);
						case GUITransition.WasHover:
							return MColor.Lerp(HoverStyle.BackgroundColor, Style.BackgroundColor, transitionTimer / Style.TransitionDuration);
						case GUITransition.Focus:
							return MColor.Lerp(HoverStyle.BackgroundColor, FocusStyle.BackgroundColor, transitionTimer / Style.TransitionDuration);
						case GUITransition.WasFocus:
							return MColor.Lerp(FocusStyle.BackgroundColor, Style.BackgroundColor, transitionTimer / Style.TransitionDuration);
					}
				}

				return CurrentStyle.BackgroundColor;
			}
		}

		public Rectangle ScissorRectangle
		{
			get => new(X, Y, Width, Height);
		}

		public GUIElement()
		{
			Style = new();
			HoverStyle = new();
			FocusStyle = new();

			Transition = GUITransition.None;
		}

		public virtual void Update(float delta)
		{
			IsHovered = GUIManager.MouseState.X >= X && GUIManager.MouseState.Y >= Y && GUIManager.MouseState.X < X + Width && GUIManager.MouseState.Y < Y + Height;
			IsDown = IsHovered && GUIManager.IsMouseLeftDown();
			IsPressed = IsHovered && GUIManager.IsMouseLeftPressed();

			if (doingTransition)
			{
				if (transitionTimer >= Style.TransitionDuration)
				{
					transitionTimer = Style.TransitionDuration;
					transitionFinished = true;
					doingTransition = false;
				}
				else
				{
					transitionTimer += delta;
				}
			}

			if (IsHovered)
			{
				if (!wasHovered && Style.TransitionDuration > 0f)
				{
					StartTransition(GUITransition.Hover);
				}

				OnHover?.Invoke();
			}
			else if (wasHovered && Style.TransitionDuration > 0f)
			{
				StartTransition(GUITransition.WasHover);
			}

			if (IsDown)
			{
				OnDown?.Invoke();
			}

			if (IsPressed)
			{
				OnPressed?.Invoke();
			}

			// Update previous state
			wasDown = IsDown;
			wasHovered = IsHovered;
		}

		private void StartTransition(GUITransition transition)
		{
			transitionTimer = 0f;
			doingTransition = true;
			transitionFinished = false;
			Transition = transition;
		}

		public virtual void Draw(ShapeBatch shapeBatch, float delta)
		{
			// Do not render anything if any axis does not have a size
			if (Width == 0 || Height == 0)
			{
				return;
			}

			// Background is transparent and no border or transparent border
			if (CurrentStyle.BackgroundColor == MColor.Transparent && (CurrentStyle.Border == 0 || CurrentStyle.BorderColor == MColor.Transparent))
			{
				return;
			}

			// If the element draw is a circle
			if (CurrentStyle.IsCircle)
			{
				var origin = new Vector2(X - Width / 2, Y - Height / 2);
				if (CurrentStyle.Border > 0)
				{
					shapeBatch.DrawCircle(
						origin,
						Width,
						BackgroundColor,
						CurrentStyle.BorderColor,
						CurrentStyle.Border);
				}
				else
				{
					shapeBatch.FillCircle(
						origin,
						Width,
						BackgroundColor);
				}
			}
			// Else, it's a rectangle
			else
			{
				if (CurrentStyle.Border > 0)
				{
					shapeBatch.DrawRectangle(
						new Vector2(X, Y),
						new Vector2(Width, Height),
						BackgroundColor,
						CurrentStyle.BorderColor,
						CurrentStyle.Border,
						CurrentStyle.BorderRadius);
				}
				else
				{
					shapeBatch.FillRectangle(
						new Vector2(X, Y),
						new Vector2(Width, Height),
						BackgroundColor,
						CurrentStyle.BorderRadius);

					//Console.WriteLine("{X: " + X + ", Y: " + Y + ", Width: " + Width + ", Height: " + Height + "}");
				}
			}
		}

		public T GetElement<T>() where T : GUIElement
		{
			foreach (var child in Children)
			{
				if (child.GetType() == typeof(T))
				{
					return (T)child;
				}
			}

			return default;
		}

		public List<T> GetElements<T>() where T : GUIElement
		{
			List<T> elements = [];

			foreach (var child in Children)
			{
				if (child.GetType() == typeof(T))
				{
					elements.Add((T)child);
				}
			}

			return elements;
		}

		public GUIElement GetElementById(string id)
		{
			foreach (var child in Children)
			{
				if (child.Id == id)
				{
					return child;
				}
			}

			return null;
		}

		public GUIElement GetElementByClass(string className)
		{
			foreach (var child in Children)
			{
				if (child.Classes.Contains(className))
				{
					return child;
				}
			}

			return null;
		}

		public List<GUIElement> GetElementsByClass(string className)
		{
			List<GUIElement> elements = [];

			foreach (var child in Children)
			{
				if (child.Classes.Contains(className))
				{
					elements.Add(child);
				}

				if (child.Children.Count > 0)
				{
					elements.AddRange(child.GetElementsByClass(className));
				}
			}

			return elements;
		}

		/// <summary>
		/// Apply the given style on this element
		/// </summary>
		/// <param name="style"></param>
		public void ApplyStyle(StyleDeclaration style)
		{
			ApplyStyleOn(Style, style);
			ApplyStyleOn(HoverStyle, style);
			ApplyStyleOn(FocusStyle, style);
		}

		/// <summary>
		/// Apply the given hover style on this element
		/// </summary>
		/// <param name="style"></param>
		public void ApplyHoverStyle(StyleDeclaration style, bool replaceIfExists = false)
		{
			ApplyStyleOn(HoverStyle, style, replaceIfExists);
		}

		/// <summary>
		/// Apply the given focus style on this element
		/// </summary>
		/// <param name="style"></param>
		public void ApplyFocusStyle(StyleDeclaration style, bool replaceIfExists = false)
		{
			ApplyStyleOn(FocusStyle, style, replaceIfExists);
		}

		/// <summary>
		/// Hydrate the given style with the given stylesheet
		/// </summary>
		/// <param name="style"></param>
		/// <param name="properties"></param>
		private void ApplyStyleOn(GUIStyle style, StyleDeclaration properties, bool replaceIfExists = false)
		{
			if (style == null || properties == null)
			{
				return;
			}

			// Colors
			if (IsValid(properties.BackgroundColor))
			{
				style.BackgroundColor = StylesheetHelper.FromCssString(properties.BackgroundColor);
			}

			if (IsValid(properties.BorderColor))
			{
				style.BorderColor = StylesheetHelper.FromCssString(properties.BorderColor);
			}

			if (IsValid(properties.Color))
			{
				style.TextColor = StylesheetHelper.FromCssString(properties.Color);
			}

			// Width
			if (IsValid(properties.Width))
			{
				if (IsPercent(properties.Width))
				{
					if (properties.Width == "100%")
					{
						style.FullWidth = true;
					}
					else
					{
						style.WidthPercent = true;
					}
				}
				else
				{
					style.FullWidth = false;
					style.WidthPercent = false;
				}
				if (IsAuto(properties.Width))
				{
					style.AutoWidth = true;
				}
				else
				{
					style.AutoWidth = false;
				}

				style.Width = StylesheetHelper.FromCssToSize(properties.Width);
			}

			// Height
			if (IsValid(properties.Height))
			{
				if (IsPercent(properties.Height))
				{
					if (properties.Height == "100%")
					{
						style.FullHeight = true;
					}
					else
					{
						style.HeightPercent = true;
					}
				}
				else
				{
					style.FullHeight = false;
					style.HeightPercent = false;
				}
				if (IsAuto(properties.Height))
				{
					style.AutoHeight = true;
				}
				else
				{
					style.AutoHeight = false;
				}

				style.Height = StylesheetHelper.FromCssToSize(properties.Height);
			}

			// Left
			if (IsValid(properties.Left))
			{
				if (IsPercent(properties.Left))
				{
					style.LeftPercent = true;
				}

				style.Left = StylesheetHelper.FromCssToSize(properties.Left);
			}

			// Right
			if (IsValid(properties.Right))
			{
				if (IsPercent(properties.Right))
				{
					style.RightPercent = true;
				}

				style.Right = StylesheetHelper.FromCssToSize(properties.Right);
			}

			// Top
			if (IsValid(properties.Top))
			{
				if (IsPercent(properties.Top))
				{
					style.TopPercent = true;
				}

				style.Top = StylesheetHelper.FromCssToSize(properties.Top);
			}

			// Bottom
			if (IsValid(properties.Bottom))
			{
				if (IsPercent(properties.Bottom))
				{
					style.BottomPercent = true;
				}

				style.Bottom = StylesheetHelper.FromCssToSize(properties.Bottom);
			}

			// Margin left
			if (IsValid(properties.MarginLeft))
			{
				if (IsPercent(properties.MarginLeft))
				{
					style.MarginLeftPercent = true;
				}

				style.MarginLeft = StylesheetHelper.FromCssToSize(properties.MarginLeft);
			}

			// Margin right
			if (IsValid(properties.MarginRight))
			{
				if (IsPercent(properties.MarginRight))
				{
					style.MarginRightPercent = true;
				}

				style.MarginRight = StylesheetHelper.FromCssToSize(properties.MarginRight);
			}

			// Margin top
			if (IsValid(properties.MarginTop))
			{
				if (IsPercent(properties.MarginTop))
				{
					style.MarginTopPercent = true;
				}

				style.MarginTop = StylesheetHelper.FromCssToSize(properties.MarginTop);
			}

			// Margin bottom
			if (IsValid(properties.MarginBottom))
			{
				if (IsPercent(properties.MarginBottom))
				{
					style.MarginBottomPercent = true;
				}

				style.MarginBottom = StylesheetHelper.FromCssToSize(properties.MarginBottom);
			}

			// Padding left
			if (IsValid(properties.PaddingLeft))
			{
				if (IsPercent(properties.PaddingLeft))
				{
					style.PaddingLeftPercent = true;
				}

				style.PaddingLeft = StylesheetHelper.FromCssToSize(properties.PaddingLeft);
			}

			// Padding right
			if (IsValid(properties.PaddingRight))
			{
				if (IsPercent(properties.PaddingRight))
				{
					style.PaddingRightPercent = true;
				}

				style.PaddingRight = StylesheetHelper.FromCssToSize(properties.PaddingRight);
			}

			// Padding top
			if (IsValid(properties.PaddingTop))
			{
				if (IsPercent(properties.PaddingTop))
				{
					style.PaddingTopPercent = true;
				}

				style.PaddingTop = StylesheetHelper.FromCssToSize(properties.PaddingTop);
			}

			// Padding bottom
			if (IsValid(properties.PaddingBottom))
			{
				if (IsPercent(properties.PaddingBottom))
				{
					style.PaddingBottomPercent = true;
				}

				style.PaddingBottom = StylesheetHelper.FromCssToSize(properties.PaddingBottom);
			}

			// Borders
			if (IsValid(properties.BorderLeft))
			{
				int left = StylesheetHelper.FromCssToSize(properties.BorderLeft);
				int right = StylesheetHelper.FromCssToSize(properties.BorderRight);
				int bottom = StylesheetHelper.FromCssToSize(properties.BorderBottom);
				int top = StylesheetHelper.FromCssToSize(properties.BorderTop);

				int max = Math.Max(left, right);
				max = Math.Max(max, top);
				max = Math.Max(max, bottom);

				style.Border = max;
			}

			// Border radius
			if (IsValid(properties.BorderRadius))
			{
				style.BorderRadius = StylesheetHelper.FromCssToSize(properties.BorderRadius);
			}

			// Transition
			if (IsValid(properties.TransitionDuration))
			{
				style.TransitionDuration = StylesheetHelper.FromCssToSeconds(properties.TransitionDuration);
			}

			// Opacity
			if (IsValid(properties.Opacity))
			{
				style.Opacity = float.Parse(properties.Opacity);
			}
		}

		/// <summary>
		/// Return true if the given css string is a calc
		/// </summary>
		/// <param name="css"></param>
		/// <returns></returns>
		private static bool IsCalc(string css)
		{
			return css.StartsWith("calc(") && css.EndsWith(")");
		}

		/// <summary>
		/// Return true if the given css string is in percent
		/// </summary>
		/// <param name="css"></param>
		/// <returns></returns>
		private static bool IsPercent(string css)
		{
			return css.EndsWith("%") || css.EndsWith("vw") || css.EndsWith("vh");
		}

		/// <summary>
		/// Return true if the given css string is auto
		/// </summary>
		/// <param name="css"></param>
		/// <returns></returns>
		private static bool IsAuto(string css)
		{
			return css.EndsWith("auto");
		}

		/// <summary>
		/// Return true if the given css string is valid
		/// </summary>
		/// <param name="css"></param>
		/// <returns></returns>
		private static bool IsValid(string css)
		{
			return !string.IsNullOrWhiteSpace(css);
		}
	}
}
