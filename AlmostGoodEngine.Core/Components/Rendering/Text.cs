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
        public Vector2 Position { get; set; }
        public string Value { get; set; }

        public int FontSize { get; private set; }

        public Anchor Anchor { get; set; }

        public Anchor TextAnchor { get; set; }

        private SpriteFontBase _font;

        public bool ShowDebug { get; set; }

        public bool DisplayInsideWorld { get; set; }

        public Color Color { get; set; }

        public Text(string value, int fontSize)
        {
            Value = value;
            Anchor = Anchor.TopLeft;
            TextAnchor = Anchor.TopLeft;

            Position = Vector2.Zero;
            Color = Color.White;

            SetSize(fontSize);
        }

        public void SetSize(int fontSize)
        {
            if (FontSize == fontSize)
            {
                return;
            }

            FontSize = fontSize;
            _font = GameManager.FontSystem.GetFont(FontSize);
        }

        public override void Update(GameTime gameTime)
        {
#if DEBUG
            if (Input.Keyboard.IsPressed(Microsoft.Xna.Framework.Input.Keys.F3))
            {
                ShowDebug = !ShowDebug;
            }
#endif
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (DisplayInsideWorld)
            {
                DrawDebug(spriteBatch);
                DrawText(spriteBatch);
            }
        }

        public override void DrawUI(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!DisplayInsideWorld)
            {
                DrawDebug(spriteBatch);
                DrawText(spriteBatch);
            }
        }

        private void DrawText(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, Value, FinalPosition(), Color);
        }

        private void DrawDebug(SpriteBatch spriteBatch)
        {
            if (!ShowDebug)
            {
                return;
            }

            int width = GameManager.Game.GraphicsDevice.Viewport.Width;
            int height = GameManager.Game.GraphicsDevice.Viewport.Height;

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

        private Vector2 AnchorPosition()
        {
            int width = GameManager.Game.GraphicsDevice.Viewport.Width;
            int height = GameManager.Game.GraphicsDevice.Viewport.Height;

            return Anchor switch
            {
                Anchor.TopCentered => new(width / 2 + 0, 0),
                Anchor.TopRight => new(width + 0, 0),
                Anchor.MiddleLeft => new(0, height / 2 + 0),
                Anchor.MiddleCentered => new(width / 2 + 0, height / 2 + 0),
                Anchor.MiddleRight => new(width + 0, height / 2 + 0),
                Anchor.BottomLeft => new(0, height + 0),
                Anchor.BottomCentered => new(width / 2 + 0, height + 0),
                Anchor.BottomRight => new(width + 0, height + 0),
                _ => Position,
            };
        }

        private Vector2 FinalPosition()
        {
            Vector2 anchorPosition = AnchorPosition();
            if (DisplayInsideWorld)
            {
                anchorPosition = new(Position.X, Position.Y);
            }
            Vector2 textSize = _font.MeasureString(Value);

            switch (TextAnchor)
            {
                case Anchor.TopCentered:
                    return new(anchorPosition.X - textSize.X / 2, anchorPosition.Y + Position.Y);
                case Anchor.TopRight:
                    return new(anchorPosition.X - textSize.X - Position.X, anchorPosition.Y + Position.Y);
                case Anchor.MiddleLeft:
                    return new(anchorPosition.X + Position.X, anchorPosition.Y - textSize.Y / 2);
                case Anchor.MiddleCentered:
                    return new(anchorPosition.X - textSize.X / 2, anchorPosition.Y - textSize.Y / 2);
                case Anchor.MiddleRight:
                    return new(anchorPosition.X - Position.X - textSize.X, anchorPosition.Y - textSize.Y / 2);
                case Anchor.BottomLeft:
                    return new(anchorPosition.X + Position.X, anchorPosition.Y - Position.Y - textSize.Y);
                case Anchor.BottomCentered:
                    return new(anchorPosition.X - textSize.X / 2, anchorPosition.Y - Position.Y - textSize.Y);
                case Anchor.BottomRight:
                    return new(anchorPosition.X - Position.X - textSize.X, anchorPosition.Y - Position.Y - textSize.Y);
            }

            return anchorPosition;
        }
    }
}
