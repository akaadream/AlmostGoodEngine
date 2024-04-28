using AlmostGoodEngine.Core.Entities;
using AlmostGoodEngine.Core.Scenes;
using AlmostGoodEngine.Core.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
namespace AlmostGoodEngine.Core
{
    public class Renderer
    {
        /// <summary>
        /// The camera used by the renderer to display game content
        /// </summary>
        public List<Camera2D> Cameras { get; private set; }

        /// <summary>
        /// The scene where this renderer is attached to
        /// </summary>
        public Scene Scene { get; private set; }

		/// <summary>
		/// Game main viewport shortcut
		/// </summary>
		public static Viewport Viewport { get; set; }

        public Renderer(Scene scene)
        {
            Scene = scene;
			Viewport = Engine.GameViewport;

            Cameras = [];
			Cameras.Add(new(Viewport)
			{
                SamplerState = SamplerState.PointClamp
            });
        }

        public void Start()
        {
            foreach (var camera in Cameras)
            {
                camera.Start();
            }
        }

        public void End()
        {
			foreach (var camera in Cameras)
			{
				camera.End();
			}
		}

        /// <summary>
        /// Called when the viewport is resized
        /// </summary>
        /// <param name="viewport"></param>
        public void Resize(Viewport viewport)
        {
            foreach (var camera in Cameras)
            {
				camera.Viewport = viewport;
			}
        }

        public void BeforeUpdate(GameTime gameTime)
        {
			foreach (var camera in Cameras)
			{
				camera.BeforeUpdate(gameTime);
			}
		}

        public void Update(GameTime gameTime)
        {
            foreach (var camera in Cameras)
            {
				camera.Update(gameTime);
			}
        }

		public void AfterUpdate(GameTime gameTime)
		{
			foreach (var camera in Cameras)
			{
				camera.AfterUpdate(gameTime);
			}
		}

		public void FixedUpdate(GameTime gameTime)
		{
			foreach (var camera in Cameras)
			{
				camera.FixedUpdate(gameTime);
			}
		}

		/// <summary>
		/// Display the game
		/// </summary>
		/// <param name="gameTime"></param>
		public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var camera in Cameras)
            {
				// Camera draw
				spriteBatch.Begin();

				camera.Draw(gameTime, spriteBatch);

				spriteBatch.End();

				//spriteBatch.GraphicsDevice.Viewport = camera.Viewport;
				var rasterizerState = new RasterizerState()
				{
					ScissorTestEnable = true,
				};
				spriteBatch.GraphicsDevice.ScissorRectangle = new Rectangle(camera.Viewport.X, camera.Viewport.Y, camera.Width, camera.Height);
				spriteBatch.Begin(SpriteSortMode.Deferred, null, camera.SamplerState, null, rasterizerState, null, camera.GetTransform());

				// Draw scene objects
				foreach (var entity in Scene.Entities)
				{
					if (!entity.Enabled)
					{
						continue;
					}

					if (!camera.CanSee(entity.GetBounds()))
					{
						continue;
					}

					entity.Draw(gameTime, spriteBatch);
				}

				spriteBatch.End();
			}
        }

        public void DrawUI(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var camera in Cameras)
            {
				spriteBatch.Begin();

				// Camera UI
				camera.DrawUI(gameTime, spriteBatch);

				// Draw scene objects
				foreach (var entity in Scene.Entities)
				{
					if (!entity.Enabled)
					{
						continue;
					}
					entity.DrawUI(gameTime, spriteBatch);
				}

				spriteBatch.End();
			}
        }

		public void DrawDebug(GameTime gameTime, SpriteBatch spriteBatch)
		{
			spriteBatch.Begin();

			// Draw scene objects
			foreach (var entity in Scene.Entities)
			{
				if (!entity.Enabled)
				{
					continue;
				}
				entity.DrawDebug(gameTime, spriteBatch);
			}

			spriteBatch.End();
		}
    }
}
