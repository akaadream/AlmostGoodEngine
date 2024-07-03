using AlmostGoodEngine.Core.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlmostGoodEngine.Core.Scenes.Transitions
{
    public class FadeTransition() : Transition
    {
        protected override void Process()
        {
            if (PreviousSceneFrame == null || NextSceneFrame == null)
            {
                Logger.Log("Scene frame null");
                return;
            }

            GameManager.SpriteBatch.Draw(PreviousSceneFrame, Vector2.Zero, Color.White * T);
            GameManager.SpriteBatch.Draw(NextSceneFrame, Vector2.Zero, Color.White * (1f - T));
        }
    }
}
