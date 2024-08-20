using Apos.Shapes;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlmostGoodEngine.GUI.Elements
{
    public class GUITextElement : GUIElement
    {
        private string _text = "";
        public string Text
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Style.Content))
                {
                    return Style.Content;
                }
                return _text;
            }

            set
            {
                _text = value;
            }
        }

        public Vector2 TextPosition
        {
            get
            {
                int x = 0;
                int y = 0;

                Vector2 textSize = Vector2.Zero;
                if (Style.Font != null)
                {
                    textSize = Style.Font.MeasureString(Text);
                }

                switch (Style.HAlign)
                {
                    case GUIHAlign.Left:
                        x = X + Style.PaddingLeft + Style.Border;
                        break;
                    case GUIHAlign.Right:
                        x = (int)(X + Width - textSize.X - Style.PaddingRight - Style.Border);
                        break;
                    case GUIHAlign.Center:
                        x = (int)(X + Width / 2 - textSize.X / 2);
                        break;
                }

                switch (Style.VAlign)
                {
                    case GUIVAlign.Top:
                        y = Y + Style.PaddingTop + Style.Border;
                        break;
                    case GUIVAlign.Bottom:
                        y = (int)(Y + Height - textSize.Y - Style.PaddingBottom - Style.Border);
                        break;
                    case GUIVAlign.Middle:
                        y = (int)(Y + Height / 2 - textSize.Y / 2);
                        break;
                }

                return new Vector2(x, y);
            }
        }

        public GUITextElement(): base()
        {

        }

        public override void Draw(ShapeBatch shapeBatch, SpriteBatch spriteBatch, float delta)
        {
            base.Draw(shapeBatch, spriteBatch, delta);

            // Draw the text
            if (!string.IsNullOrWhiteSpace(Text))
            {
                if (Style.Font != null)
                {
                    spriteBatch.DrawString(Style.Font, Text, TextPosition, Style.TextColor);
                }
            }
        }
    }
}
