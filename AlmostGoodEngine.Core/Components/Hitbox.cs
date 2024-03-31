using AlmostGoodEngine.Core.ECS;
using AlmostGoodEngine.Core.Events;
using AlmostGoodEngine.Core.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace AlmostGoodEngine.Core.Components
{
    public class Hitbox : Component
    {
        public bool DisplayDebug { get; set; }

        public Rectangle Bounds { get; set; }

        public event EventHandler<EntityEventArgs> OnEntityEnter;
        public event EventHandler<EntityEventArgs> OnEntityExit;

        private readonly List<Entity> _insideEntities;

        public bool StaticHitbox { get; set; }

        public Hitbox(Rectangle bounds, bool staticHitbox = true)
        {
            Bounds = bounds;
            _insideEntities = new();

            StaticHitbox = staticHitbox;
        }

        public override void Update(GameTime gameTime)
        {
            if (!StaticHitbox)
            {
                return;
            }

            foreach (var entity in GameManager.CurrentScene().Entities)
            {
                Hitbox hitbox = entity.GetComponent<Hitbox>();
                if (hitbox != null && hitbox == this)
                {
                    continue;
                }

                // The default bounds of an entity
                Rectangle entityBounds = GetEntityRect(entity);

                if (entity.Tags.Contains("Player"))
                {

                }

                // The list contains the entity
                if (_insideEntities.Contains(entity))
                {
                    // Check if we have to remove the entity
                    if (!Bounds.Intersects(entityBounds) && !Bounds.Contains(entityBounds))
                    {
                        _insideEntities.Remove(entity);
                        OnEntityExit?.Invoke(this, new(entity));
                        return;
                    }
                }

                if (!_insideEntities.Contains(entity))
                {
                    if (Bounds.Intersects(entityBounds) || Bounds.Contains(entityBounds))
                    {
                        _insideEntities.Add(entity);
                        OnEntityEnter?.Invoke(this, new(entity));
                        return;
                    }
                }
            }
        }

        private Rectangle GetEntityRect(Entity entity)
        {
            if (entity == null)
            {
                return Rectangle.Empty;
            }

            // The default bounds of an entity
            Rectangle entityBounds = new((int)entity.Position.X, (int)entity.Position.Y, 1, 1);

            // Try to find a hitbox attached to the entity
            Hitbox entityHitbox = entity.GetComponent<Hitbox>();
            if (entityHitbox != null)
            {
                entityBounds = new(
                    (int)(((int)entity.Position.X + entityHitbox.Bounds.X * entity.Scale.X)),
                    (int)(((int)entity.Position.Y + entityHitbox.Bounds.Y * entity.Scale.Y)),
                    (int)(entityHitbox.Bounds.Width * entity.Scale.X),
                    (int)(entityHitbox.Bounds.Height * entity.Scale.Y));
            }

            return entityBounds;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            // Display the debug
            if (!DisplayDebug)
            {
                return;
            }

            Rectangle rect = Bounds;

            if (Owner != null)
            {
                rect = GetEntityRect(Owner);
            }

            if (StaticHitbox)
            {
                Debug.FillRectangle(spriteBatch, rect, Color.Red * 0.3f);
            }
            Debug.Rectangle(spriteBatch, rect, Color.Red);
        }

        public override void DrawUI(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
        }

		public override void DrawDebug(GameTime gameTime, SpriteBatch spriteBatch)
		{
			base.DrawDebug(gameTime, spriteBatch);
		}
	}
}
