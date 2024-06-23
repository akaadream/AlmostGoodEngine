using Microsoft.Xna.Framework.Graphics;

namespace AlmostGoodEngine.Core.Scenes.Transitions
{
    public interface ITransition
    {
        public void Start(RenderTarget2D previousSceneFrame, RenderTarget2D nextSceneFrame, float duration);
        public bool Update();
        public RenderTarget2D GetFrame();
    }
}
