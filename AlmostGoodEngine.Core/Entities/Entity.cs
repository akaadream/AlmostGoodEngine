using AlmostGoodEngine.Core.Scenes;
using Microsoft.Xna.Framework;

namespace AlmostGoodEngine.Core.Entities
{
    public class Entity
    {
        /// <summary>
        /// The current position of the entity
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// The scene where the entity is currently located
        /// </summary>
        public Scene Scene { get; set; }
    }
}
