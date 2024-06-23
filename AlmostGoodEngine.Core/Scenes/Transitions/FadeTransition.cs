using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlmostGoodEngine.Core.Scenes.Transitions
{
    public class FadeTransition() : Transition
    {
        protected override void Process()
        {
            GameManager.SpriteBatch.Draw(PreviousSceneFrame, Vector2.Zero, Color.White * T);
            GameManager.SpriteBatch.Draw(NextSceneFrame, Vector2.Zero, Color.White * (1f - T));
        }
    }
}
