using AlmostGoodEngine.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace AlmostGoodEngine.Core.Scenes
{
    public class Scene : IGameObject
    {
        public Renderer Renderer { get; private set; }

        /// <summary>
        /// Scene's game objects
        /// </summary>
        public List<IGameObject> GameObjects { get; private set; }

        /// <summary>
        /// If the scene's content has already been loaded
        /// </summary>
        public bool ContentLoaded { get; private set; }

        public Scene()
        {
            GameObjects = new();
            Renderer = new(this);
        }

        /// <summary>
        /// Load scene's content
        /// </summary>
        public void LoadContent(ContentManager content)
        {
            foreach (var gameObject in GameObjects)
            {
                gameObject.LoadContent(content);
            }

            ContentLoaded = true;
        }

        /// <summary>
        /// When the scene's start
        /// </summary>
        public void Start()
        {
            
            Renderer.Camera?.Start();
            foreach (var gameObject in GameObjects)
            {
                gameObject.Start();
            }
        }

        /// <summary>
        /// When the scene's end
        /// </summary>
        public void End()
        {
            Renderer.Camera.End();
            foreach (var gameObject in GameObjects)
            {
                gameObject.End();
            }
        }

        /// <summary>
        /// Before the scene's update
        /// </summary>
        /// <param name="gameTime"></param>
        public void BeforeUpdate(GameTime gameTime)
        {
            Renderer.Camera.BeforeUpdate(gameTime);
            foreach (var gameObject in GameObjects)
            {
                gameObject.BeforeUpdate(gameTime);
            }
        }

        /// <summary>
        /// Update the scene's content
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            Renderer.Camera.Update(gameTime);
            foreach (var gameObject in GameObjects)
            {
                gameObject.Update(gameTime);
            }
        }

        public void FixedUpdate(GameTime gameTime)
        {
            Renderer.Camera.FixedUpdate(gameTime);
            foreach (var gameObject in GameObjects)
            {
                gameObject.FixedUpdate(gameTime);
            }
        }

        /// <summary>
        /// After the scene's update
        /// </summary>
        /// <param name="gameTime"></param>
        public void AfterUpdate(GameTime gameTime)
        {
            Renderer.Camera.AfterUpdate(gameTime);
            foreach (var gameObject in GameObjects)
            {
                gameObject.AfterUpdate(gameTime);
            }
        }

        /// <summary>
        /// Draw the scene's content
        /// </summary>
        /// <param name="gameTime"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Renderer.Draw(gameTime, spriteBatch);
            
        }

        /// <summary>
        /// Draw the scene's UI layer
        /// </summary>
        /// <param name="gameTime"></param>
        public void DrawUI(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Renderer.DrawUI(gameTime, spriteBatch);
        }
    }
}
