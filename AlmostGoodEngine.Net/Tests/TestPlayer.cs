using Microsoft.Xna.Framework;

namespace AlmostGoodEngine.Net.Tests
{
    public class TestPlayer
    {
        [Sync]
        public Vector2 Position { get; set; }

        public TestPlayer()
        {
            Position = Vector2.Zero;
        }
    }
}
