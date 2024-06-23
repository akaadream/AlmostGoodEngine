using AlmostGoodEngine.Core.Components.Physics;
using AlmostGoodEngine.Core.ECS;
using AlmostGoodEngine.Core.Entities;
using AlmostGoodEngine.Core.Interfaces;
using AlmostGoodEngine.Inputs;
using AlmostGoodEngine.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

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
        /// Scene's entities
        /// </summary>
        internal List<Entity> Entities { get; set; }

        /// <summary>
        /// If the scene's content has already been loaded
        /// </summary>
        public bool ContentLoaded { get; private set; }

        /// <summary>
        /// Get the number of entities registered inside the scene
        /// </summary>
        public int EntitiesCount { get => Entities.Count; }

        private RenderTarget2D _renderTarget = null;

        /// <summary>
        /// Constructor
        /// </summary>
        public Scene()
        {
            Entities = [];
            Renderer = new(this);
        }

        /// <summary>
        /// Load scene's content
        /// </summary>
        public virtual void LoadContent(ContentManager content)
        {
            foreach (var entity in Entities)
            {
				entity.LoadContent(content);
            }

            ContentLoaded = true;
        }

        /// <summary>
        /// When the scene's start
        /// </summary>
        public virtual void Start()
        {
            
            Renderer.Start();
            foreach (var entity in Entities)
            {
				entity.Start();
            }
        }

        /// <summary>
        /// When the scene's end
        /// </summary>
        public virtual void End()
        {
            Renderer.End();
            foreach (var entity in Entities)
            {
				entity.End();
            }
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
            foreach (var entity in Entities)
            {
                if (GameManager.Paused && entity.Pausable)
                {
                    continue;
                }
				if (!entity.Enabled)
				{
					continue;
				}
				entity.BeforeUpdate(gameTime);
            }
        }

        /// <summary>
        /// Update the scene's content
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            Renderer.Update(gameTime);
            foreach (var entity in Entities)
            {
				if (GameManager.Paused && entity.Pausable)
				{
					continue;
				}
				if (!entity.Enabled)
				{
					continue;
				}
				entity.Update(gameTime);
            }
        }

        /// <summary>
        /// Fixed update the scene's content
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void FixedUpdate(GameTime gameTime)
        {
            Renderer.FixedUpdate(gameTime);
            foreach (var entity in Entities)
            {
				if (GameManager.Paused && entity.Pausable)
				{
					continue;
				}
				if (!entity.Enabled)
				{
					continue;
				}
				entity.FixedUpdate(gameTime);
            }
        }

        /// <summary>
        /// After the scene's update
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void AfterUpdate(GameTime gameTime)
        {
            Renderer.AfterUpdate(gameTime);
            foreach (var entity in Entities)
            {
				if (GameManager.Paused && entity.Pausable)
				{
					continue;
				}
                if (!entity.Enabled)
                {
                    continue;
                }
				entity.AfterUpdate(gameTime);
            }
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

        public void AddEntity(Entity entity)
        {
            if (Entities.Contains(entity))
            {
                return;
            }

            entity.Scene = this;
            if (string.IsNullOrEmpty(entity.Name))
            {
                entity.Name = "Entity";
            }
            Entities.Add(entity);
        }

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns></returns>
        public List<Entity> GetEntities()
        {
            return Entities;
        }

        public Vector2 MousePosition()
        {
            return new(Input.Mouse.X, Input.Mouse.Y);
        }

        public Vector2 WorldMousePosition()
        {
            return GameManager.MainCamera().ScreenToWorld(MousePosition());
		}

        public List<Collider> GetColliders()
        {
            List<Collider> colliders = [];
            foreach (var entity in Entities)
            {
                if (entity is Solid2D solid)
                {
					if (solid.Collider == null)
					{
						continue;
					}

					colliders.Add(solid.Collider);
				}
            }

            return colliders;
        }

        public List<Actor2D> GetActors()
        {
            List<Actor2D> actors = [];
            foreach (var entity in Entities)
            {
                if (entity is Actor2D actor)
                {
                    actors.Add(actor);
                }
            }

            return actors;
        }

        public List<Actor2D> GetAllRidingActors(Solid2D solid)
        {
			List<Actor2D> actors = [];
			foreach (var entity in Entities)
			{
				if (entity is Actor2D actor && actor.IsRiding(solid))
				{
					actors.Add(actor);
				}
			}

			return actors;
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
