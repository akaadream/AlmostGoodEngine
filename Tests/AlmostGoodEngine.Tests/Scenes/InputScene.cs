using AlmostGoodEngine.Core;
using AlmostGoodEngine.Core.Scenes;
using AlmostGoodEngine.Inputs;
using AlmostGoodEngine.Tests.GameObjects;
using Microsoft.Xna.Framework;

namespace AlmostGoodEngine.Tests.Scenes
{
    internal class InputScene : Scene
    {
        public InputScene()
        {
            AddEntity(new InputReader());
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Input.Keyboard.IsPressed(Microsoft.Xna.Framework.Input.Keys.F2))
            {
                GameManager.SceneManager.Set("test", "fade");
            }
        }
    }
}
