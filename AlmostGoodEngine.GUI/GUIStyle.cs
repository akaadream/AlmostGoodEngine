using FontStashSharp;
using Microsoft.Xna.Framework;
using System;

namespace AlmostGoodEngine.GUI
{
	public struct GUIStyle()
	{
		/// <summary>
		/// The way to display the element
		/// </summary>
		public GUIDisplay Display { get; set; } = GUIDisplay.Block;

		/// <summary>
		/// The positionning of the element
		/// </summary>
		public GUIPosition Position { get; set; } = GUIPosition.Default;

		#region Positionning

		/// <summary>
		/// The left position of the element
		/// </summary>
		public int Left { get; set; }
		public bool LeftPercent { get; set; } = false;
		public int Right { get; set; }
		public bool RightPercent { get; set; } = false;
		public int Top { get; set; }
		public bool TopPercent { get; set; } = false;
		public int Bottom { get; set; }
		public bool BottomPercent { get; set; } = false;

		#endregion

		#region Sizes

		public int Width { get; set; } = 0;
		public bool WidthPercent { get; set; } = false;

		public int Height { get; set; } = 0;
		public bool HeightPercent { get; set; } = false;

		public bool AutoWidth { get; set; } = false;
		public bool AutoHeight { get; set; } = false;
		public bool FullWidth { get; set; } = false;
		public bool FullHeight { get; set; } = false;

		public int Border { get; set; } = 0;
		public int BorderLeft { get; set; } = 0;
		public int BorderRight { get; set; } = 0;
		public int BorderTop { get; set; } = 0;
		public int BorderBottom { get; set; } = 0;

		#endregion

		#region Spacing

		public int MarginTop { get; set; } = 0;
		public bool MarginTopPercent { get; set; } = false;
		public int MarginBottom { get; set; } = 0;
		public bool MarginBottomPercent { get; set; } = false;
		public int MarginLeft { get; set; } = 0;
		public bool MarginLeftPercent { get; set; } = false;
		public int MarginRight { get; set; } = 0;

		public bool MarginRightPercent { get; set; } = false;

		public int PaddingTop { get; set; } = 0;
		public bool PaddingTopPercent { get; set; } = false;
		public int PaddingBottom { get; set; } = 0;
		public bool PaddingBottomPercent { get; set; } = false;
		public int PaddingLeft { get; set; } = 0;
		public bool PaddingLeftPercent { get; set; } = false;
		public int PaddingRight { get; set; } = 0;
		public bool PaddingRightPercent { get; set; } = false;

		#endregion

		#region Colors

		/// <summary>
		/// The background color of the element
		/// </summary>
		public Color BackgroundColor { get; set; } = Color.Transparent;

		/// <summary>
		/// The text color of the element
		/// </summary>
		public Color TextColor { get; set; } = Color.White;

		/// <summary>
		/// The border color of the element
		/// </summary>
		public Color BorderColor { get; set; } = Color.Transparent;

		#endregion

		#region Transition

		/// <summary>
		/// The duration of the transition
		/// </summary>
		public float TransitionDuration { get; set; } = 0f;

		#endregion

		#region Text

		/// <summary>
		/// The text content of the element
		/// </summary>
		public string Content = string.Empty;

		/// <summary>
		/// The font-size used by the element
		/// </summary>
		public int FontSize { get; set; } = 12;

		/// <summary>
		/// The font family the element will use
		/// </summary>
		public string FontFamily { get; set; } = "inherit";
		public SpriteFontBase Font { get; set; } = null;

		/// <summary>
		/// The horizontal alignment of the text
		/// </summary>
		public GUIHAlign HAlign { get; set; } = GUIHAlign.Center;

		/// <summary>
		/// The vertical alignment of the text
		/// </summary>
		public GUIVAlign VAlign { get; set; } = GUIVAlign.Middle;

		#endregion

		#region Others

		/// <summary>
		/// The border radius of the element
		/// </summary>
		public int BorderRadius { get; set; } = 0;

		/// <summary>
		/// The opacity of the element
		/// </summary>
		public float Opacity { get; set; } = 1f;

		/// <summary>
		/// True if the element can recieve mouse events
		/// </summary>
		public bool Events { get; set; } = true;

		#endregion

		#region Total

		/// <summary>
		/// The total width of the element
		/// </summary>
		public int TotalWidth { get => Width + PaddingLeft + PaddingRight; }

		/// <summary>
		/// The total height of the element
		/// </summary>
		public int TotalHeight { get => Height + PaddingTop + PaddingBottom; }

		/// <summary>
		/// True if the element can be considered as a circle
		/// </summary>
		public bool IsCircle { get => TotalWidth == TotalHeight; }

		#endregion

		/// <summary>
		/// Get the computed width of the element
		/// </summary>
		/// <param name="element">The GUI element using this stylesheet</param>
		/// <returns>The width of the element (in pixels)</returns>
		public int GetWidth(GUIElement element)
		{
			if (FullWidth)
			{
				return GUIManager.Width;
			}

            if (AutoWidth)
            {
                if (element.Parent != null)
				{
					return Math.Max(element.Parent.Width, Width);
				}
            }

			if (WidthPercent)
			{
				if (element.Parent != null)
				{
					return Width * element.Parent.Width / 100;
				}

				return Width * GUIManager.Width / 100;
			}

			return Width;
		}

		/// <summary>
		/// Get the computed height of the element
		/// </summary>
		/// <param name="element">The GUI element using this stylesheet</param>
		/// <returns>The height of the element (in pixels)</returns>
		public int GetHeight(GUIElement element)
		{
			if (FullHeight)
			{
				return GUIManager.Height;
			}

			if (AutoHeight)
			{
				if (element.Parent != null)
				{
					return Math.Max(element.Parent.Height, Height);
				}
			}

			if (HeightPercent)
			{
				if (element.Parent != null)
				{
					return Height * element.Parent.Height / 100;
				}

				return Height * GUIManager.Height / 100;
			}

			return Height;
		}

	}
}
