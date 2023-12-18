using AlmostGoodEngine.Core.Scenes;
using AlmostGoodEngine.Tests.GameObjects;

namespace AlmostGoodEngine.Tests.Scenes
{
    internal class InputScene : Scene
    {
        public InputScene()
        {
            GameObjects.Add(new InputReader());
        }
    }
}
