using AlmostGoodEngine.Core.Interfaces;
using AlmostGoodEngine.Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AlmostGoodEngine.Core.Scenes
{
    public class Scene : IGameObject
    {
        /// <summary>
        /// The name of the scene
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The renderer used to draw the scene
        /// </summary>
        public Renderer Renderer { get; private set; }

        /// <summary>
        /// If the scene's content has already been loaded
        /// </summary>
        public bool ContentLoaded { get; private set; }

        private RenderTarget2D _renderTarget = null;

        /// <summary>
        /// Constructor
        /// </summary>
        public Scene()
        {
            Renderer = new(this);
            _renderTarget = GameManager.Engine.CreateRenderTarget();
        }

        /// <summary>
        /// Load scene's content
        /// </summary>
        public virtual void LoadContent(ContentManager content)
        {
            ContentLoaded = true;
        }

        /// <summary>
        /// When the scene's start
        /// </summary>
        public virtual void Start()
        {
            
            Renderer.Start();
        }

        /// <summary>
        /// When the scene's end
        /// </summary>
        public virtual void End()
        {
            Renderer.End();
        }
        /// <summary>
        /// Resize the scene
        /// </summary>
        public virtual void Resize(Viewport viewport)
        {
            Renderer.Resize(viewport);
        }

        /// <summary>
        /// Before the scene's update
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void BeforeUpdate(GameTime gameTime)
        {
            Renderer.BeforeUpdate(gameTime);
        }

        /// <summary>
        /// Update the scene's content
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            Renderer.Update(gameTime);
        }

        /// <summary>
        /// Fixed update the scene's content
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void FixedUpdate(GameTime gameTime)
        {
            Renderer.FixedUpdate(gameTime);
        }

        /// <summary>
        /// After the scene's update
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void AfterUpdate(GameTime gameTime)
        {
            Renderer.AfterUpdate(gameTime);
        }

        /// <summary>
        /// Draw the scene's content
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Renderer.Draw(gameTime, spriteBatch);
        }

        /// <summary>
        /// Draw the scene's UI layer
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void DrawUI(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Renderer.DrawUI(gameTime, spriteBatch);
        }

        /// <summary>
        /// Draw the scene's debug UI
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public virtual void DrawDebug(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Renderer.DrawDebug(gameTime, spriteBatch);
        }

        public Vector2 MousePosition()
        {
            return new(Input.Mouse.X, Input.Mouse.Y);
        }

        public Vector2 WorldMousePosition()
        {
            return GameManager.MainCamera().ScreenToWorld(MousePosition());
		}

        public RenderTarget2D GetFrame(GameTime gameTime)
        {
            GameManager.Engine.GraphicsDevice.SetRenderTarget(_renderTarget);
            GameManager.Engine.GraphicsDevice.Clear(Color.Black);

            Draw(gameTime, GameManager.SpriteBatch);
            DrawUI(gameTime, GameManager.SpriteBatch);

            GameManager.Engine.GraphicsDevice.SetRenderTarget(null);
            return _renderTarget;
        }
    }
}
