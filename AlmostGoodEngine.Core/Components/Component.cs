using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AlmostGoodEngine.Core.Components
{
    public class Component
    {
        public bool Pausable { get; set; }
        public bool Enabled { get; set; }

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

        public virtual void DrawDebug(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }

        public virtual void LoadContent(ContentManager content)
        {

        }
    }
}
