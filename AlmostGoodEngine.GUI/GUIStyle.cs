using Microsoft.Xna.Framework;

namespace AlmostGoodEngine.GUI
{
	public class GUIStyle()
	{
		public GUIDisplay Display { get; set; } = GUIDisplay.Block;
		public GUIPosition Position { get; set; } = GUIPosition.Default;

		#region Positionning

		public int Left { get; set; }
		public int Right { get; set; }
		public int Top { get; set; }
		public int Bottom { get; set; }

		#endregion

		#region Sizes

		public int Width { get; set; } = 0;
		public int Height { get; set; } = 0;

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
		public int MarginBottom { get; set; } = 0;
		public int MarginLeft { get; set; } = 0;
		public int MarginRight { get; set; } = 0;

		public int PaddingTop { get; set; } = 0;
		public int PaddingBottom { get; set; } = 0;
		public int PaddingLeft { get; set; } = 0;
		public int PaddingRight { get; set; } = 0;

		#endregion

		#region Colors

		// Default
		public Color BackgroundColor { get; set; } = Color.Transparent;
		public Color TextColor { get; set; } = Color.Transparent;
		public Color BorderColor { get; set; } = Color.Transparent;

		// Hover
		public Color HoverBackgroundColor { get; set; } = Color.Transparent;
		public Color HoverTextColor { get; set; } = Color.Transparent;
		public Color HoverBorderColor { get; set; } = Color.Transparent;

		// Focus
		public Color FocusBackgroundColor { get; set; } = Color.Transparent;
		public Color FocusTextColor { get; set; } = Color.Transparent;
		public Color FocusBorderColor { get; set; } = Color.Transparent;

		#endregion

		#region Transition

		public float TransitionDuration { get; set; } = 0f;

		#endregion

		#region Others

		public int BorderRadius { get; set; } = 0;

		#endregion

		#region Total

		public int TotalWidth { get => Width + PaddingLeft + PaddingRight; }
		public int TotalHeight { get => Height + PaddingTop + PaddingBottom; }

		public bool IsCircle { get => TotalWidth == TotalHeight; }

		#endregion
	}
}
