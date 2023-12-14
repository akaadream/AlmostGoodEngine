using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlmostGoodEngine.Core.Interfaces
{
    public interface IGameObjectMethods
    {
        void Start();
        void End();

        void BeforeUpdate(GameTime gameTime);
        void Update(GameTime gameTime);
        void FixedUpdate(GameTime gameTime);
        void AfterUpdate(GameTime gameTime);
        void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        void DrawUI(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
