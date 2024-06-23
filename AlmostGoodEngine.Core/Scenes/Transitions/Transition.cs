using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlmostGoodEngine.Core.Scenes.Transitions
{
    public abstract class Transition() : ITransition
    {
        protected GraphicsDevice GraphicsDevice { get; private set; } = GameManager.Engine.GraphicsDevice;
        protected RenderTarget2D Frame { get; set; } = GameManager.Engine.CreateRenderTarget();
        protected RenderTarget2D PreviousSceneFrame { get; set; }
        protected RenderTarget2D NextSceneFrame { get; set; }

        protected float Duration { get; set; }
        protected float DurationLeft { get; set; }
        protected float T { get; set; }

        public void Start(RenderTarget2D previousSceneFrame, RenderTarget2D nextSceneFrame, float duration)
        {
            PreviousSceneFrame = previousSceneFrame;
            NextSceneFrame = nextSceneFrame;
            Duration = duration;
            DurationLeft = duration;
        }

        public virtual bool Update()
        {
            DurationLeft -= Time.DeltaTime;
            T = DurationLeft / Duration;
            return DurationLeft < 0f;
        }

        public RenderTarget2D GetFrame()
        {
            GraphicsDevice.SetRenderTarget(Frame);
            GraphicsDevice.Clear(Color.Black);
            GameManager.SpriteBatch.Begin();
            Process();
            GameManager.SpriteBatch.End();
            GraphicsDevice.SetRenderTarget(null);
            return Frame;
        }

        protected virtual void Process()
        {

        }
    }
}
