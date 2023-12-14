using Microsoft.Xna.Framework;

namespace AlmostGoodEngine.Core.Interfaces
{
    public interface IManager
    {
        /// <summary>
        /// Update the content of the manager
        /// </summary>
        /// <param name="gameTime"></param>
        void Update(GameTime gameTime);

        /// <summary>
        /// Draw the content of the manager
        /// </summary>
        /// <param name="gameTime"></param>
        void Draw(GameTime gameTime);
    }
}
