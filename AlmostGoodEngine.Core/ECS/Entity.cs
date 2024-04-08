using AlmostGoodEngine.Core.Interfaces;
using AlmostGoodEngine.Core.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace AlmostGoodEngine.Core.ECS
{
    public class Entity : IGameObject
    {
        /// <summary>
        /// Entity's tag
        /// </summary>
        public List<string> Tags { get; set; }

        /// <summary>
        /// The position of the entity inside the world
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// The scaling of the entity
        /// </summary>
        public Vector3 Scale { get; set; }

        /// <summary>
        /// Children entities of this entity
        /// </summary>
        internal List<Entity> Children { get; set; }

        /// <summary>
        /// Components attached to this entity
        /// </summary>
        internal List<Component> Components { get; set; }

        /// <summary>
        /// Parent entity of this entity
        /// </summary>
        public Entity Parent { get; internal set; }

        /// <summary>
        /// The scene where this entity is
        /// </summary>
        public Scene Scene { get; internal set; }

        /// <summary>
        /// If true, then this entity will be paused when you set the game paused
        /// </summary>
        public bool Pausable { get; set; }

        /// <summary>
        /// If true, this entity will be disabled (not drawn and not updated)
        /// </summary>
        public bool Enabled { get; set; }

        public Entity()
        {
            Children = [];
            Components = [];

            Scale = new(1f);
            Position = Vector3.Zero;

            Tags = [];
        }

        public virtual Rectangle GetBounds()
        {
            int minLeft = 0;
            int minTop = 0;
            int maxRight = 0;
            int maxBottom = 0;

            foreach (var component in Components)
            {
                var rect = component.GetBounds();
                if (rect.Left < minLeft)
                {
                    minLeft = rect.Left;
                }
                if (rect.Top < minTop)
                {
                    minTop = rect.Top;
                }
                if (rect.Right > maxRight)
                {
                    maxRight = rect.Right;
                }
                if (rect.Bottom > maxBottom)
                {
                    maxBottom = rect.Bottom;
                }
            }

            return new Rectangle(minLeft, minTop, maxRight - minLeft, maxBottom - minTop);
        }

        /// <summary>
        /// Load content of the entity
        /// </summary>
        public virtual void LoadContent(ContentManager content)
        {
            foreach (var child in Children)
            {
                child.LoadContent(content);
            }

            foreach (var component in Components)
            {
                component.LoadContent(content);
            }
        }

        /// <summary>
        /// When the game's start
        /// </summary>
        public virtual void Start()
        {
            foreach (var child in Children)
            {
                child.Start();
            }

            foreach (var component in Components)
            {
                component.Start();
            }
        }

        /// <summary>
        /// When the game's end
        /// </summary>
        public virtual void End()
        {
            foreach (var child in Children)
            {
                child.End();
            }

            foreach (var component in Components)
            {
                component.End();
            }
        }

        /// <summary>
        /// Before the update
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void BeforeUpdate(GameTime gameTime)
        {
            foreach (var component in Components)
            {
                component.BeforeUpdate(gameTime);
            }

            foreach (var child in Children)
            {
                child.BeforeUpdate(gameTime);
            }
        }

        /// <summary>
        /// Update the entity
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            foreach (var component in Components)
            {
                component.Update(gameTime);
            }

            foreach (var child in Children)
            {
                child.Update(gameTime);
            }
        }

        /// <summary>
        /// Fixed update
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void FixedUpdate(GameTime gameTime)
        {
            foreach (var component in Components)
            {
                component.FixedUpdate(gameTime);
            }

            foreach (var child in Children)
            {
                child.FixedUpdate(gameTime);
            }
        }

        /// <summary>
        /// After the update
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void AfterUpdate(GameTime gameTime)
        {
            foreach (var component in Components)
            {
                component.AfterUpdate(gameTime);
            }

            foreach (var child in Children)
            {
                child.AfterUpdate(gameTime);
            }
        }

        /// <summary>
        /// Draw entity
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var component in Components)
            {
                component.Draw(gameTime, spriteBatch);
            }

            foreach (var child in Children)
            {
                child.Draw(gameTime, spriteBatch);
            }
        }

        /// <summary>
        /// Draw entity UI
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void DrawUI(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var component in Components)
            {
                component.DrawUI(gameTime, spriteBatch);
            }

            foreach (var child in Children)
            {
                child.DrawUI(gameTime, spriteBatch);
            }
        }

		/// <summary>
		/// Draw entity debug
		/// </summary>
		/// <param name="gameTime"></param>
		public virtual void DrawDebug(GameTime gameTime, SpriteBatch spriteBatch)
		{
			foreach (var component in Components)
			{
				component.DrawDebug(gameTime, spriteBatch);
			}

			foreach (var child in Children)
			{
				child.DrawDebug(gameTime, spriteBatch);
			}
		}

        /// <summary>
        /// Add the given component instance inside the list
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
		public bool AddComponent(Component component)
        {
            if (Components.Contains(component))
            {
                return false;
            }

            component.Owner = this;
            Components.Add(component);
            return true;
        }
        
        /// <summary>
        /// Add a child entity to this entity
        /// </summary>
        /// <returns></returns>
        public bool AddChild(Entity entity)
        {
            if (Children.Contains(entity))
            {
                return false;
            }

            entity.Parent = this;
            Children.Add(entity);
            return true;
        }

        /// <summary>
        /// Find a child
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Entity GetChild(int index)
        {
            if (index < 0 || index >= Children.Count)
            {
                return null;
            }

            return Children[index];
        }

        /// <summary>
        /// Find a child using its tag
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public Entity GetChild(string tag)
        {
            foreach (Entity entity in Children)
            {
                if (entity.Tags.Contains(tag))
                {
                    return entity;
                }
            }

            return null;
        }

        /// <summary>
        /// Find the firt component of the type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetComponent<T>() where T : Component
        {
            foreach (Component component in Components)
            {
                if (component is T)
                {
                    return component as T;
                }
            }

            return null;
        }

        /// <summary>
        /// Find the first component corresponding with the given type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Component GetComponent(Type type)
        {
            foreach (Component component in Components)
            {
                if (component.GetType() == type)
                {
                    return component;
                }
            }

            return null;
        }

        /// <summary>
        /// Get a list of component corresponding with the type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetComponents<T>() where T : Component
        {
            List<T> components = [];
            
            foreach (Component component in Components)
            {
                if (component is T t)
                {
                    components.Add(t);
                }
            }

            return components;
        }

        /// <summary>
        /// Get a list of components corresponding with the given type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<Component> GetComponents(Type type)
        {
            List<Component> components = [];

            foreach (Component component in Components)
            {
                if (component.GetType() == type)
                {
                    components.Add(component);
                }
            }

            return components;
        }
    }
}
