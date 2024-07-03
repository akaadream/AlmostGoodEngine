using AlmostGoodEngine.Core;
using AlmostGoodEngine.Core.Generation;
using AlmostGoodEngine.Core.Scenes;
using AlmostGoodEngine.Core.Tiling;
using AlmostGoodEngine.Inputs;
using AlmostGoodEngine.Tests.Generation;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AlmostGoodEngine.Tests.Scenes
{
	public class ProceduralScene : Scene
	{
		public MyGenerator Generator { get; set; }
		SpriteFontBase font;

		public ProceduralScene()
		{
			Tileset tileset = new("Sprites/tileset");
			tileset.AutoGenerate();
			Generator = new();

			Generator.RegisterTile(tileset.Tiles[5], 0.95f, 1f);
			Generator.RegisterTile(tileset.Tiles[3], 0.9f, 0.95f);
			Generator.RegisterTile(tileset.Tiles[4], 0.8f, 0.9f);
			Generator.RegisterTile(tileset.Tiles[0], 0.3f, 0.8f);
			Generator.RegisterTile(tileset.Tiles[1], 0f, 0.3f);
			Generator.RegisterTile(tileset.Tiles[10], -0.1f, 0f);
			Generator.RegisterTile(tileset.Tiles[6], -0.3f, -0.1f);
			Generator.RegisterTile(tileset.Tiles[7], -1f, -0.3f);

			Renderer.Cameras[0].BackgroundColor = Color.Black;
			Renderer.Cameras[0].SetZoom(0.40f);

			// Generate
			Generator.Generate();
		}

		public override void LoadContent(ContentManager content)
		{
			base.LoadContent(content);

			font = GameManager.FontSystem.GetFont(24);
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

            if (Input.Keyboard.IsPressed(Keys.F2))
            {
                GameManager.SceneManager.Set("test", "fade");
            }

            if (Input.Keyboard.IsPressed(Keys.P))
			{
				Generator.Seed = NoiseHelper.Seed();
				Generator.Initialize();
				Generator.Generate();
			}

			if (Input.Mouse.IsWheelMovedUp())
			{
				Renderer.Cameras[0].ZoomIn(0.05f);
			}

			if (Input.Mouse.IsWheelMovedDown())
			{
				Renderer.Cameras[0].ZoomOut(0.05f);
			}
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			base.Draw(gameTime, spriteBatch);

			spriteBatch.Begin(
				samplerState: SamplerState.PointClamp,
				transformMatrix: Renderer.Cameras[0].GetTransform());
			Generator.Draw(spriteBatch);
			spriteBatch.End();
		}

		public override void DrawUI(GameTime gameTime, SpriteBatch spriteBatch)
		{
			base.DrawUI(gameTime, spriteBatch);

			spriteBatch.Begin();

			spriteBatch.DrawString(font, "FPS:" + Time.FPS, new Vector2(15, 15), Color.White);

			string zoomText = "Zoom: " + (int)(GameManager.MainCamera()?.Zoom * 100) + "%";
			Vector2 zoomTextSize = font.MeasureString(zoomText);
			spriteBatch.DrawString(font, zoomText, new Vector2(15, GameManager.Engine.Graphics.PreferredBackBufferHeight - zoomTextSize.Y - 15), Color.White);

			spriteBatch.End();
		}
	}
}
