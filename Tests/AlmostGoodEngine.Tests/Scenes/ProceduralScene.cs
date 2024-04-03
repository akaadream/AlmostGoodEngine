using AlmostGoodEngine.Core;
using AlmostGoodEngine.Core.Generation;
using AlmostGoodEngine.Core.Scenes;
using AlmostGoodEngine.Core.Tiling;
using AlmostGoodEngine.Inputs;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace AlmostGoodEngine.Tests.Scenes
{
	public class ProceduralScene : Scene
	{
		public Generator Generator { get; set; }
		SpriteFontBase font;
		private int _seed = 1337;

		public ProceduralScene()
		{
			Tileset tileset = new("Sprites/tileset");
			tileset.AutoGenerate();
			_seed = Guid.NewGuid().GetHashCode();
			Generator = new(tileset, _seed, 256, 144)
			{
				FallOff = false,
			};

			Generator.RegisterTile(tileset.Tiles[5], 0.95f, 1f);
			Generator.RegisterTile(tileset.Tiles[3], 0.9f, 0.95f);
			Generator.RegisterTile(tileset.Tiles[4], 0.8f, 0.9f);
			Generator.RegisterTile(tileset.Tiles[0], 0.3f, 0.8f);
			Generator.RegisterTile(tileset.Tiles[1], 0f, 0.3f);
			Generator.RegisterTile(tileset.Tiles[10], -0.1f, 0f);
			Generator.RegisterTile(tileset.Tiles[6], -0.3f, -0.1f);
			Generator.RegisterTile(tileset.Tiles[7], -1f, -0.3f);

			Renderer.Cameras[0].BackgroundColor = Color.Black;

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

			if (Input.Keyboard.IsPressed(Keys.P))
			{
				_seed += 1;
				Generator.SetSeed(_seed);
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

			// Noise type
			if (Input.Keyboard.IsPressed(Keys.F2))
			{
				switch (Generator.NoiseType)
				{
					case Generation.FastNoiseLite.NoiseType.OpenSimplex2:
						Generator.NoiseType = Generation.FastNoiseLite.NoiseType.OpenSimplex2S;
						break;
					case Generation.FastNoiseLite.NoiseType.OpenSimplex2S:
						Generator.NoiseType = Generation.FastNoiseLite.NoiseType.Perlin;
						break;
					case Generation.FastNoiseLite.NoiseType.Perlin:
						Generator.NoiseType = Generation.FastNoiseLite.NoiseType.Cellular;
						break;
					case Generation.FastNoiseLite.NoiseType.Cellular:
						Generator.NoiseType = Generation.FastNoiseLite.NoiseType.Value;
						break;
					case Generation.FastNoiseLite.NoiseType.Value:
						Generator.NoiseType = Generation.FastNoiseLite.NoiseType.ValueCubic;
						break;
					case Generation.FastNoiseLite.NoiseType.ValueCubic:
						Generator.NoiseType = Generation.FastNoiseLite.NoiseType.OpenSimplex2;
						break;
				}

				Generator.Generate();
			}
			if (Input.Keyboard.IsPressed(Keys.F3))
			{
				if (Generator.Frequency > 0.09f)
				{
					Generator.Frequency = 0.01f;
				}
				else
				{
					Generator.Frequency += 0.01f;
				}
				Generator.Generate();
			}
			if (Input.Keyboard.IsPressed(Keys.F4))
			{
				switch (Generator.FractalType)
				{
					case Generation.FastNoiseLite.FractalType.FBm:
						Generator.FractalType = Generation.FastNoiseLite.FractalType.Ridged;
						break;
					case Generation.FastNoiseLite.FractalType.Ridged:
						Generator.FractalType = Generation.FastNoiseLite.FractalType.PingPong;
						break;
					case Generation.FastNoiseLite.FractalType.PingPong:
						Generator.FractalType = Generation.FastNoiseLite.FractalType.DomainWarpProgressive;
						break;
					case Generation.FastNoiseLite.FractalType.DomainWarpProgressive:
						Generator.FractalType = Generation.FastNoiseLite.FractalType.DomainWarpIndependent;
						break;
					case Generation.FastNoiseLite.FractalType.DomainWarpIndependent:
						Generator.FractalType = Generation.FastNoiseLite.FractalType.None;
						break;
					case Generation.FastNoiseLite.FractalType.None:
						Generator.FractalType = Generation.FastNoiseLite.FractalType.FBm;
						break;
				}
				Generator.Generate();
			}
			if (Input.Keyboard.IsPressed(Keys.F5))
			{
				if (Generator.Octaves > 9)
				{
					Generator.Octaves = 1;
				}
				else
				{
					Generator.Octaves++;
				}

				Generator.Generate();
			}
			if (Input.Keyboard.IsPressed(Keys.F6))
			{
				if (Generator.Lacunarity > 4)
				{
					Generator.Lacunarity = 1;
				}
				else
				{
					Generator.Lacunarity++;
				}
				Generator.Generate();
			}
			if (Input.Keyboard.IsPressed(Keys.F7))
			{
				if (Generator.Gain > 0.9f)
				{
					Generator.Gain = 0.1f;
				}
				else
				{
					Generator.Gain += 0.1f;
				}

				Generator.Generate();
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
			spriteBatch.DrawString(font, "[F2] Noise Type: " + Generator.NoiseType.ToString(), new Vector2(15, 45), Color.White);
			spriteBatch.DrawString(font, "[F3] Noise frequency: " + Generator.Frequency.ToString("F"), new Vector2(15, 75), Color.White);
			spriteBatch.DrawString(font, "[F4] Fractal type: " + Generator.FractalType.ToString(), new Vector2(15, 105), Color.White);
			spriteBatch.DrawString(font, "[F5] Octaves: " + Generator.Octaves.ToString(), new Vector2(15, 135), Color.White);
			spriteBatch.DrawString(font, "[F6] Lacunarity: " + Generator.Lacunarity.ToString(), new Vector2(15, 165), Color.White);
			spriteBatch.DrawString(font, "[F7] Gain: " + Generator.Gain.ToString("F"), new Vector2(15, 195), Color.White);

			string zoomText = "Zoom: " + (int)(GameManager.MainCamera()?.Zoom * 100) + "%";
			Vector2 zoomTextSize = font.MeasureString(zoomText);
			spriteBatch.DrawString(font, zoomText, new Vector2(15, GameManager.Engine.Graphics.PreferredBackBufferHeight - zoomTextSize.Y - 15), Color.White);

			spriteBatch.End();
		}
	}
}
