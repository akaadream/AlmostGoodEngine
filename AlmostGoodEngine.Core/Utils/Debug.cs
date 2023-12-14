using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlmostGoodEngine.Core.Utils
{
    public class Debug
    {
        public static void Line(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color)
        {
            Draw2D.SpriteBatch = spriteBatch;
            Draw2D.DrawLine(start, end, color);
        }

        public static void Rectangle(SpriteBatch spriteBatch, Rectangle rectangle, Color color)
        {
            Draw2D.SpriteBatch = spriteBatch;
            Draw2D.DrawRectangle(rectangle, color);
        }

        public static void FillRectangle(SpriteBatch spriteBatch, Rectangle rectangle, Color color)
        {
            Draw2D.SpriteBatch = spriteBatch;
            Draw2D.FillRectangle(rectangle, color);
        }
    }
}
