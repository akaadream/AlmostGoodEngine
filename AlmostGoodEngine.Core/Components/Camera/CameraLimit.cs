using AlmostGoodEngine.Core.ECS;
using AlmostGoodEngine.Core.Entities;
using Microsoft.Xna.Framework;

namespace AlmostGoodEngine.Core.Components.Camera
{
    public class CameraLimit : Component
    {
        public Vector3 Min { get; set; }
        public Vector3 Max { get; set; }

        public CameraLimit(Vector3 min, Vector3 max)
        {
            Min = min;
            Max = max;
        }

        public override void AfterUpdate(GameTime gameTime)
        {
            if (Owner == null || Owner is not Camera2D)
            {
                return;
            }

            Vector3 limitedPosition = Vector3.Clamp(Owner.Position, Min, Max);
            Owner.Position = limitedPosition;
        }
    }
}
