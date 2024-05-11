using AlmostGoodEngine.Core.ECS;
using AlmostGoodEngine.Core.Utils;
using AlmostGoodEngine.Inputs;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlmostGoodEngine.Core.Components.Rendering
{
    public class Text : Component
    {
        /// <summary>
        /// The position of the text
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// The value of the text label
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// The size of the text
        /// </summary>
        public int FontSize { get; private set; }

        /// <summary>
        /// The global anchor
        /// </summary>
        public Anchor Anchor { get; set; }

        /// <summary>
        /// The text anchor
        /// </summary>
        public Anchor TextAnchor { get; set; }

        /// <summary>
        /// The font used to draw the text
        /// </summary>

        private SpriteFontBase _font;

        /// <summary>
        /// If true, the debug lines will be rendered
        /// </summary>
        public bool ShowDebug { get; set; }

        /// <summary>
        /// True if the text should be displayed inside the world
        /// </summary>
        public bool DisplayInsideWorld { get; set; }

        /// <summary>
        /// The position where the text is displayed
        /// If the text may be drawn inside the world, it takes the position only,
        /// If the text is drawn on the screen, it takes consideration of the anchors
        /// </summary>
		public Vector2 DisplayPosition { get => DisplayInsideWorld ? Position : FinalPosition(); }

        /// <summary>
        /// The color of the text
        /// </summary>
		public Color Color { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value"></param>
        /// <param name="fontSize"></param>
        public Text(string value, int fontSize)
        {
            Value = value;
            Anchor = Anchor.TopLeft;
            TextAnchor = Anchor.TopLeft;

            Position = Vector2.Zero;
            Color = Color.White;

            SetSize(fontSize);
        }

        /// <summary>
        /// Define a new size for the text
        /// </summary>
        /// <param name="fontSize"></param>
        public void SetSize(int fontSize)
        {
            if (FontSize == fontSize)
            {
                return;
            }

            FontSize = fontSize;
            _font = GameManager.FontSystem.GetFont(FontSize);
        }

        /// <summary>
        /// Get the bounds rectangle of the text
        /// </summary>
        /// <returns></returns>
		public override Rectangle GetBounds()
		{
            if (string.IsNullOrWhiteSpace(Value))
            {
                return Rectangle.Empty;
            }

            Vector2 textSize = _font.MeasureString(Value);
            return new((int)DisplayPosition.X, (int)DisplayPosition.Y, (int)textSize.X, (int)textSize.Y);
		}

        /// <summary>
        /// Update (used only for the debug)
        /// </summary>
        /// <param name="gameTime"></param>
		public override void Update(GameTime gameTime)
        {
#if DEBUG
            if (Input.Keyboard.IsPressed(Microsoft.Xna.Framework.Input.Keys.F3))
            {
                ShowDebug = !ShowDebug;
            }
#endif

            if (IsMouseHovering)
            {
                Logger.Log("Text hovered");
            }
        }

        /// <summary>
        /// Draw (if the text is drawn inside the world)
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (DisplayInsideWorld)
            {
                DrawDebug(spriteBatch);
                DrawText(spriteBatch);
            }
        }

        /// <summary>
        /// Draw UI (if the text is drawn on the screen)
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public override void DrawUI(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!DisplayInsideWorld)
            {
                DrawDebug(spriteBatch);
                DrawText(spriteBatch);
            }
        }

        /// <summary>
        /// Display the text on the screen
        /// </summary>
        /// <param name="spriteBatch"></param>
        private void DrawText(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, Value, DisplayPosition, Color);
        }

        /// <summary>
        /// Draw debug
        /// </summary>
        /// <param name="spriteBatch"></param>
        private void DrawDebug(SpriteBatch spriteBatch)
        {
            if (!ShowDebug)
            {
                return;
            }

            int width = GameManager.MainCamera()?.Width ?? 0;
            int height = GameManager.MainCamera()?.Height ?? 0;

            Vector2 finalPosition = FinalPosition();
            Vector2 textSize = _font.MeasureString(Value);

            Debug.Line(spriteBatch, new Vector2(0, Position.Y), new Vector2(width, Position.Y), Color.Black * 0.5f);
            Debug.Line(spriteBatch, new Vector2(0, height - Position.Y), new Vector2(width, height - Position.Y), Color.Black * 0.5f);
            Debug.Line(spriteBatch, new Vector2(Position.X, 0), new Vector2(Position.X, height), Color.Black * 0.5f);
            Debug.Line(spriteBatch, new Vector2(width - Position.X, 0), new Vector2(width - Position.X, height), Color.Black * 0.5f);

            Debug.Line(spriteBatch, new Vector2(width / 2, 0), new Vector2(width / 2, height), Color.Red);
            Debug.Line(spriteBatch, new Vector2(0, height / 2), new Vector2(width, height / 2), Color.Red);

            Debug.Line(spriteBatch, new Vector2(finalPosition.X, finalPosition.Y), new Vector2(finalPosition.X, finalPosition.Y + textSize.Y), Color.Blue);
            Debug.Line(spriteBatch, new Vector2(finalPosition.X + textSize.X / 2, finalPosition.Y), new Vector2(finalPosition.X + textSize.X / 2, finalPosition.Y + textSize.Y), Color.Blue);
            Debug.Line(spriteBatch, new Vector2(finalPosition.X + textSize.X, finalPosition.Y), new Vector2(finalPosition.X + textSize.X, finalPosition.Y + textSize.Y), Color.Blue);
            Debug.Line(spriteBatch, new Vector2(finalPosition.X, finalPosition.Y), new Vector2(finalPosition.X + textSize.X, finalPosition.Y), Color.Blue);
            Debug.Line(spriteBatch, new Vector2(finalPosition.X, finalPosition.Y + textSize.Y / 2), new Vector2(finalPosition.X + textSize.X, finalPosition.Y + textSize.Y / 2), Color.Blue);
            Debug.Line(spriteBatch, new Vector2(finalPosition.X, finalPosition.Y + textSize.Y), new Vector2(finalPosition.X + textSize.X, finalPosition.Y + textSize.Y), Color.Blue);
        }

        /// <summary>
        /// Get the position of the global anchor
        /// </summary>
        /// <returns></returns>
        private Vector2 AnchorPosition()
        {
            int width = Owner.Scene.Renderer.Cameras[0].Viewport.Width;
            int height = Owner.Scene.Renderer.Cameras[0].Viewport.Height;

			return Anchor switch
            {
                Anchor.TopCentered => new(width / 2, 0),
                Anchor.TopRight => new(width, 0),
                Anchor.MiddleLeft => new(0, height / 2),
                Anchor.MiddleCentered => new(width / 2, height / 2),
                Anchor.MiddleRight => new(width, height / 2),
                Anchor.BottomLeft => new(0, height),
                Anchor.BottomCentered => new(width / 2, height),
                Anchor.BottomRight => new(width, height),
                _ => Position,
            };
        }

        /// <summary>
        /// Get the final position based on the text anchors
        /// </summary>
        /// <returns></returns>
        private Vector2 FinalPosition()
        {
            Vector2 anchorPosition = AnchorPosition();
            if (DisplayInsideWorld)
            {
                anchorPosition = new(Position.X, Position.Y);
            }

            Vector2 textSize = _font.MeasureString(Value);

			return TextAnchor switch
			{
				Anchor.TopCentered => new(anchorPosition.X - textSize.X / 2, anchorPosition.Y + Position.Y),
				Anchor.TopRight => new(anchorPosition.X - textSize.X - Position.X, anchorPosition.Y + Position.Y),
				Anchor.MiddleLeft => new(anchorPosition.X + Position.X, anchorPosition.Y - textSize.Y / 2),
				Anchor.MiddleCentered => new(anchorPosition.X - textSize.X / 2, anchorPosition.Y - textSize.Y / 2),
				Anchor.MiddleRight => new(anchorPosition.X - Position.X - textSize.X, anchorPosition.Y - textSize.Y / 2),
				Anchor.BottomLeft => new(anchorPosition.X + Position.X, anchorPosition.Y - Position.Y - textSize.Y),
				Anchor.BottomCentered => new(anchorPosition.X - textSize.X / 2, anchorPosition.Y - Position.Y - textSize.Y),
				Anchor.BottomRight => new(anchorPosition.X - Position.X - textSize.X, anchorPosition.Y - Position.Y - textSize.Y),
				_ => anchorPosition,
			};
		}
    }
}
