using AlmostGoodEngine.Core.Interfaces;
using AlmostGoodEngine.Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace AlmostGoodEngine.Core.ECS
{
    public class Component : IComponent
    {
        public Entity Owner { get; internal set; }

        public event EventHandler<EventArgs> OnMouseEnter;
        public event EventHandler<EventArgs> OnMouseHover;
        public event EventHandler<EventArgs> OnMouseLeave;
        public event EventHandler<EventArgs> OnMouseClicked;
        public event EventHandler<EventArgs> OnMouseDown;

        public bool IsMouseHovering { get; private set; }
        public bool IsMouseDown { get; private set; }

        private Vector2 _worldMousePosition = Vector2.Zero;
        private bool wasHovering = false;

        public Component()
        {
        }
        
        public virtual Rectangle GetBounds()
        {
            return Rectangle.Empty;
        }

        public virtual void LoadContent(ContentManager content)
        {

        }

        public virtual void Start()
        {

        }

        public virtual void End()
        {

        }

        public virtual void BeforeUpdate(GameTime gameTime)
        {
            
        }

        public virtual void Update(GameTime gameTime)
        {
            if (OnMouseDown == null &&
                OnMouseClicked == null &&
                OnMouseEnter == null &&
                OnMouseHover == null &&
                OnMouseLeave == null)
            {
                return;
            }

            if (Owner == null)
            {
                return;
            }

            if (Owner.Scene == null)
            {
                return;
            }

            _worldMousePosition = Owner.Scene.WorldMousePosition();
            var bounds = GetBounds();

            if (bounds.IsEmpty)
            {
                return;
            }

			IsMouseDown = false;
			IsMouseHovering = false;

			if (_worldMousePosition.X >= bounds.Left &&
                _worldMousePosition.X < bounds.Right &&
                _worldMousePosition.Y >= bounds.Top &&
                _worldMousePosition.Y < bounds.Bottom)
            {
                IsMouseHovering = true;
                if (!wasHovering)
                {
					OnMouseHover?.Invoke(this, new EventArgs());
					wasHovering = true;
                }

                OnMouseHover?.Invoke(this, new EventArgs());
                
                if (Input.Mouse.IsLeftButtonPressed())
                {
                    OnMouseClicked?.Invoke(this, new EventArgs());
                }
                else if (Input.Mouse.IsLeftButtonDown())
                {
                    OnMouseDown?.Invoke(this, new EventArgs());
                    IsMouseDown = true;
                }
            }
            else
            {
                if (wasHovering)
                {
                    OnMouseLeave?.Invoke(this, new EventArgs());
                }
            }
        }

        public virtual void FixedUpdate(GameTime gameTime)
        {

        }

        public virtual void AfterUpdate(GameTime gameTime)
        {
            
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
        }

        public virtual void DrawUI(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
        }

        public virtual void DrawDebug(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
        }
    }
}
