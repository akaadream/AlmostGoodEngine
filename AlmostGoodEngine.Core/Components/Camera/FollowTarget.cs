using AlmostGoodEngine.Core.ECS;
using AlmostGoodEngine.Core.Entities;
using AlmostGoodEngine.Core.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlmostGoodEngine.Core.Components.Camera
{
    public class FollowTarget(Camera2D camera, Entity target) : Component
    {
		public Entity Target { get; set; } = target;
		public Camera2D Camera { get; set; } = camera;

		public bool Smoothed { get; set; } = false;
		public bool Limit { get; set; } = true;
		public Vector3 Min { get; set; }
		public Vector3 Max { get; set; }

		readonly float smoothSpeed = 0.05f;

		public override void FixedUpdate(GameTime gameTime)
		{
			//Vector3 desiredPosition = new(Target.Position.X - Camera.Width / 2, Target.Position.Y - Camera.Height / 2, Camera.Position.Z);
			//Vector3 smoothedPosition = Vector3.Lerp(Camera.Position, desiredPosition, smoothSpeed);
			//Camera.SetPosition(smoothedPosition);
		}

		public override void AfterUpdate(GameTime gameTime)
        {
			Vector3 desiredPosition = new(Target.Position.X - Camera.Width / 2, Target.Position.Y - Camera.Height / 2, Camera.Position.Z);

			if (Limit)
			{
				desiredPosition = Vector3.Clamp(desiredPosition, Min, Max);
			}

			if (Smoothed)
			{
				Vector3 smoothedPosition = Vector3.Lerp(Camera.Position, desiredPosition, smoothSpeed);
				Camera.SetPosition(smoothedPosition);
			}
			else
			{
				Camera.SetPosition(desiredPosition);
			}
		}
	}
}
