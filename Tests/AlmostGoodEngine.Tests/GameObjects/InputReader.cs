using AlmostGoodEngine.Core.Components.Rendering;
using AlmostGoodEngine.Core.ECS;
using AlmostGoodEngine.Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AlmostGoodEngine.Tests.GameObjects
{
    internal class InputReader : Entity
    {
        readonly Text keyboardText;

        public InputReader()
        {
            keyboardText = new("Pressed keys: ", 32)
            {
                Anchor = Core.Utils.Anchor.MiddleCentered,
                TextAnchor = Core.Utils.Anchor.MiddleCentered
            };
            AddComponent(keyboardText);
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            string keys = "Pressed keys: ";
            foreach (var key in InputManager.Keyboard.CurrentState.GetPressedKeys())
            {
                keys += key.ToString() + ",";
            }
            keyboardText.Value = keys;
        }

        public override void DrawUI(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.DrawUI(gameTime, spriteBatch);
        }
    }
}
