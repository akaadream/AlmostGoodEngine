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
				x += Style.Left;
				x -= Style.Right;
				x += Style.MarginLeft;
				x -= Style.MarginRight;
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
				y += Style.Top;
				y -= Style.Bottom;
				y += Style.MarginLeft;
				y -= Style.MarginRight;
				return y;
			}
		}

		public int Width
		{
			get
			{
				if (Parent != null && Style.FullWidth)
				{
					return Math.Max(Style.TotalWidth, Parent.Width);
				}

				if (Style.FullWidth)
				{
					return GUIManager.Width;
				}

				return Style.TotalWidth;
			}
		}

		public int Height
		{
			get
			{
				if (Parent != null && Style.FullHeight)
				{
					return Math.Max(Style.TotalHeight, Parent.Height);
				}

				if (Style.FullHeight)
				{
					return GUIManager.Height;
				}

				return Style.TotalHeight;
			}
		}

		public Rectangle ScissorRectangle
		{
			get => new(X, Y, Width, Height);
		}

		public GUIElement()
		{
			Style = new();	
		}

		public virtual void Update(float delta)
		{
			IsHovered = GUIManager.MouseState.X >= X && GUIManager.MouseState.Y >= Y && GUIManager.MouseState.X < X + Width && GUIManager.MouseState.Y < Y + Height;
			IsDown = IsHovered && GUIManager.IsMouseLeftDown();
			IsPressed = IsHovered && GUIManager.IsMouseLeftPressed();

			if (IsHovered)
			{
				OnHover?.Invoke();
			}

			if (IsDown)
			{
				OnDown?.Invoke();
			}

			if (IsPressed)
			{
				OnPressed?.Invoke();
			}
		}

		public virtual void Draw(ShapeBatch shapeBatch, float delta)
		{
			// Do not render anything if any axis does not have a size
			if (Width == 0 || Height == 0)
			{
				return;
			}

			// Background is transparent and no border or transparent border
			if (Style.BackgroundColor == MColor.Transparent && (Style.Border == 0 || Style.BorderColor == MColor.Transparent))
			{
				return;
			}

			// If the element draw is a circle
			if (Style.IsCircle)
			{
				var origin = new Vector2(X - Width / 2, Y - Height / 2);
				if (Style.Border > 0)
				{
					shapeBatch.DrawCircle(
						origin,
						Width,
						Style.BackgroundColor,
						Style.BorderColor,
						Style.Border);
				}
				else
				{
					shapeBatch.FillCircle(
						origin,
						Width,
						Style.BackgroundColor);
				}
			}
			// Else, it's a rectangle
			else
			{
				if (Style.Border > 0)
				{
					shapeBatch.DrawRectangle(
						new Vector2(X, Y),
						new Vector2(Width, Height),
						Style.BackgroundColor,
						Style.BorderColor,
						Style.Border,
						Style.BorderRadius);
				}
				else
				{
					shapeBatch.FillRectangle(
						new Vector2(X, Y),
						new Vector2(Width, Height),
						Style.BackgroundColor,
						Style.BorderRadius);
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
		public void ApplyHoverStyle(StyleDeclaration style)
		{
			ApplyStyleOn(HoverStyle, style);
		}

		/// <summary>
		/// Apply the given focus style on this element
		/// </summary>
		/// <param name="style"></param>
		public void ApplyFocusStyle(StyleDeclaration style)
		{
			ApplyStyleOn(FocusStyle, style);
		}

		/// <summary>
		/// Hydrate the given style with the given stylesheet
		/// </summary>
		/// <param name="style"></param>
		/// <param name="properties"></param>
		private void ApplyStyleOn(GUIStyle style, StyleDeclaration properties)
		{
			if (style == null || properties == null)
			{
				return;
			}

			// Colors
			style.BackgroundColor = StylesheetHelper.FromCssString(properties.BackgroundColor);
			style.BorderColor = StylesheetHelper.FromCssString(properties.BorderColor);
			style.TextColor = StylesheetHelper.FromCssString(properties.Color);

			// Width
			if (IsValid(properties.Width))
			{
				if (IsPercent(properties.Width))
				{
					style.WidthPercent = true;
				}
				else if (IsAuto(properties.Width))
				{
					style.AutoWidth = true;
				}

				style.Width = StylesheetHelper.FromCssToSize(properties.Width);
			}

			// Height
			if (IsValid(properties.Height))
			{
				if (IsPercent(properties.Height))
				{
					style.HeightPercent = true;
				}
				else if (IsAuto(properties.Height))
				{
					style.AutoHeight = true;
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
