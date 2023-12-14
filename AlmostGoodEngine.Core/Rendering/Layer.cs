using AlmostGoodEngine.Core.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace AlmostGoodEngine.Core.Rendering
{
    public class Layer
    {
        /// <summary>
        /// The name of the layer
        /// </summary>
        public string Name { get; set; }

        public List<Entity> Entities { get; private set; }

        public SpriteBatch SpriteBatch { get; private set; }

        public Layer(GraphicsDevice graphicsDevice, string name = "")
        {
            Name = name;
            Entities = new();

            SpriteBatch = new SpriteBatch(graphicsDevice);
        }

        public void Draw(GameTime gameTime)
        {
            foreach (var entity in Entities)
            {
                entity.Draw(gameTime, SpriteBatch);
            }
        }

        public void DrawUI(GameTime gameTime)
        {
            foreach (var entity in Entities)
            {
                entity.DrawUI(gameTime, SpriteBatch);
            }
        }

        /// <summary>
        /// Move an entity to the front of the entities list.
        /// Use the force boolean to add the entity even if the remove failed.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="force"></param>
        public void MoveToFront(Entity entity, bool force = false)
        {
            bool success = Entities.Remove(entity);

            if (success || force)
            {
                // Add the entity to the end of the list
                Entities.Add(entity);
            }
        }

        /// <summary>
        /// Move an entity to the back of the entities list.
        /// Use the force boolean to add the entity even if the remove failed
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="force"></param>
        public void MoveToBack(Entity entity, bool force = false)
        {
            bool success = Entities.Remove(entity);

            if (success || force)
            {
                // Add the entity to the start of the list
                Entities.Insert(0, entity);
            }
        }
    }
}
