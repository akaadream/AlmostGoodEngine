using AlmostGoodEngine.Core;
using AlmostGoodEngine.Core.ECS;
using AlmostGoodEngine.Inputs;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

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
            if (Input.Keyboard.IsPressed(Keys.F2))
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

            string scene = "Test scene";
            string totalMemoryText = "Total memory usage: " + (GC.GetTotalMemory(false) / 1_048_576f).ToString("F") + " MB";

            spriteBatch.DrawString(font, "FPS:" + Time.FPS, new Vector2(15, 15), Color.White);
            spriteBatch.DrawString(font, "Fixed FPS:" + (int)(Time.FixedDeltaTime * 10), new Vector2(15, 45), Color.White);
            spriteBatch.DrawString(font, "UPS:" + Time.UPS, new Vector2(15, 75), Color.White);

            Vector2 mousePosition = GameManager.MainCamera().ScreenToWorld(new(Input.Mouse.X, Input.Mouse.Y));

            spriteBatch.DrawString(font, "Mouse position (X:" + Input.Mouse.X + ", Y: " + Input.Mouse.Y + ")", new Vector2(15, 105), Color.White);
            spriteBatch.DrawString(font, "World mouse position (X:" + (int)mousePosition.X + ", Y: " + (int)mousePosition.Y + ")", new Vector2(15, 135), Color.White);

            Vector2 titleSize = font.MeasureString(GameManager.Engine.Settings.Name);
            Vector2 descriptionSize = font.MeasureString(scene);
            Vector2 totalMemoryTextSize = font.MeasureString(totalMemoryText);

            spriteBatch.DrawString(font, GameManager.Engine.Settings.Name, new Vector2(GameManager.Engine.Graphics.PreferredBackBufferWidth - titleSize.X - 15, 15), Color.White);
            spriteBatch.DrawString(font, scene, new Vector2(GameManager.Engine.Graphics.PreferredBackBufferWidth - descriptionSize.X - 15, 45), Color.White);
            spriteBatch.DrawString(font, totalMemoryText, new Vector2(GameManager.Engine.Graphics.PreferredBackBufferWidth - totalMemoryTextSize.X - 15, 75), Color.White);

            string zoomText = "Zoom: " + (int)(GameManager.MainCamera()?.Zoom * 100) + "%";
            Vector2 zoomTextSize = font.MeasureString(zoomText);
			spriteBatch.DrawString(font, zoomText, new Vector2(15, GameManager.Engine.Graphics.PreferredBackBufferHeight - zoomTextSize.Y - 15), Color.White);
		}
    }
}
