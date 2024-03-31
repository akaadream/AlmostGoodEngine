using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlmostGoodEngine.Core.Utils
{
    public class Debug
    {
        public static bool DisplayBounds { get; set; } = false;
        public static bool DisplayColliders { get; set; } = false;
        public static bool DisplayText { get; set; } = false;

        public static void Line(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color, float thickness = 1f)
        {
            Draw2D.SpriteBatch = spriteBatch;
            Draw2D.DrawLine(start, end, color, thickness);
        }

        public static void Rectangle(SpriteBatch spriteBatch, Rectangle rectangle, Color color, float thickness = 1f)
        {
            Draw2D.SpriteBatch = spriteBatch;
            Draw2D.DrawRectangle(rectangle, color, thickness, 1f);
        }

        public static void FillRectangle(SpriteBatch spriteBatch, Rectangle rectangle, Color color)
        {
            Draw2D.SpriteBatch = spriteBatch;
            Draw2D.FillRectangle(rectangle, color);
        }
    }
}
