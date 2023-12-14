using AlmostGoodEngine.Core.ECS;
using AlmostGoodEngine.Core.Entities;
using Microsoft.Xna.Framework;

namespace AlmostGoodEngine.Core.Components.Camera
{
    public class FollowTarget : Component
    {
        public Entity Target { get; set; }
        public Camera2D Camera { get; set; }

        readonly float smoothSpeed = 0.05f;

        public FollowTarget(Camera2D camera, Entity target)
        {
            Camera = camera;
            Target = target;
        }

        public override void AfterUpdate(GameTime gameTime)
        {
            Vector3 desiredPosition = new (Target.Position.X - Camera.Width / 2, Target.Position.Y - Camera.Height / 2, Camera.Position.Z);
            Vector3 smoothedPosition = Vector3.Lerp(Camera.Position, desiredPosition, smoothSpeed);
            Camera.SetPosition(smoothedPosition);
        }
    }
}
