using AlmostGoodEngine.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Reflection.Metadata;

namespace AlmostGoodEngine.Core.ECS
{
    public class Component : IComponent
    {
        public Entity Owner { get; internal set; }

        public Component()
        {
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
    }
}
