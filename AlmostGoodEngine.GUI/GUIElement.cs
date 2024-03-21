using Apos.Shapes;
using ExCSS;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace AlmostGoodEngine.GUI
{
	public class GUIElement
	{
		public string Id { get; set; }
		public List<string> Classes { get; set; } = [];

		public GUIStyle Style { get; set; }

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
			if (Style.BackgroundColor == Color.Transparent && (Style.Border == 0 || Style.BorderColor == Color.Transparent))
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

		public void ApplyStylesheet(Stylesheet stylesheet)
		{
			if (stylesheet == null)
			{
				return;
			}


		}
	}
}
