using Microsoft.Xna.Framework;

namespace AlmostGoodEngine.Extended
{
    public static class GameTimeExtension
    {
        /// <summary>
        /// Get the default delta time of a game time
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        public static float DeltaTime(this GameTime gameTime)
        {
            return (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
