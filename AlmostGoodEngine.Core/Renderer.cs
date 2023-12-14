using AlmostGoodEngine.Core.Entities;
using AlmostGoodEngine.Core.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace AlmostGoodEngine.Core
{
    public class Renderer
    {
        /// <summary>
        /// The camera used by the renderer to display game content
        /// </summary>
        public Camera2D Camera { get; private set; }

        /// <summary>
        /// The scene where this renderer is attached to
        /// </summary>
        public Scene Scene { get; private set; }

        public Renderer(Scene scene)
        {
            Scene = scene;

            Camera = new(GameManager.Game.GraphicsDevice.Viewport)
            {
                SamplerState = SamplerState.PointClamp
            };
        }

        /// <summary>
        /// Called when the viewport is resized
        /// </summary>
        /// <param name="viewport"></param>
        public void Resize(Viewport viewport)
        {
            Camera.Viewport = viewport;
            Camera.ComputeMatrixes();
        }

        public void Update(GameTime gameTime)
        {
            Camera.BeforeUpdate(gameTime);
            Camera.Update(gameTime);
            Camera.AfterUpdate(gameTime);
        }

        /// <summary>
        /// Display the game
        /// </summary>
        /// <param name="gameTime"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, Camera.SamplerState, null, null, null, Camera.GetTransform());

            // Camera
            Camera.Draw(gameTime, spriteBatch);
            
            // Draw scene objects
            foreach (var gameObject in Scene.GameObjects)
            {
                gameObject.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();
        }

        public void DrawUI(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            // Camera UI
            Camera.DrawUI(gameTime, spriteBatch);

            // Draw scene objects
            foreach (var gameObject in Scene.GameObjects)
            {
                gameObject.DrawUI(gameTime, spriteBatch);
            }

            spriteBatch.End();
        }
    }
}
