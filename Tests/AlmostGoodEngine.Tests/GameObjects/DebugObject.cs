using AlmostGoodEngine.Core;
using AlmostGoodEngine.Core.ECS;
using AlmostGoodEngine.Inputs;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AlmostGoodEngine.Tests.GameObjects
{
    internal class DebugObject : Component
    {
        SpriteFontBase font;

        bool showThings = true;

        public override void LoadContent(ContentManager content)
        {
            font = GameManager.FontSystem.GetFont(24);
        }

        public override void Update(GameTime gameTime)
        {
            if (InputManager.Keyboard.IsPressed(Keys.F2))
            {
                showThings = !showThings;
            }
        }

        public override void DrawUI(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!showThings)
            {
                return;
            }

            spriteBatch.DrawString(font, "FPS:" + Time.FPS, new Vector2(15, 15), Color.White);
            spriteBatch.DrawString(font, "UPS:" + Time.UPS, new Vector2(15, 45), Color.White);

            Vector2 mousePosition = GameManager.MainCamera().ScreenToWorld(new(InputManager.Mouse.X, InputManager.Mouse.Y));

            spriteBatch.DrawString(font, "Mouse position (X:" + InputManager.Mouse.X + ", Y: " + InputManager.Mouse.Y + ")", new Vector2(15, 75), Color.White);
            spriteBatch.DrawString(font, "World mouse position (X:" + (int)mousePosition.X + ", Y: " + (int)mousePosition.Y + ")", new Vector2(15, 105), Color.White);

            Vector2 titleSize = font.MeasureString(GameManager.Game.Settings.Name);
            Vector2 descriptionSize = font.MeasureString(GameManager.Game.Settings.Description);

            spriteBatch.DrawString(font, GameManager.Game.Settings.Name, new Vector2(GameManager.Game.Graphics.PreferredBackBufferWidth - titleSize.X - 15, 15), Color.White);
            spriteBatch.DrawString(font, GameManager.Game.Settings.Description, new Vector2(GameManager.Game.Graphics.PreferredBackBufferWidth - descriptionSize.X - 15, 45), Color.White);
        }
    }
}
