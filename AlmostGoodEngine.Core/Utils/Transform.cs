using Microsoft.Xna.Framework;

namespace AlmostGoodEngine.Core.Utils
{
	public class Transform
	{
		public Vector3 Position { get; set; }
		public Vector3 LocalPosition { get; set; }
		public Vector3 Scale { get; set; }
		public Vector3 LocalScale { get; set; }
		public Quaternion Rotation { get; set; }
		public Quaternion LocalRotation { get; set; }

		public void Translate(Vector3 translation, bool local = false)
		{
			if (local)
			{
				LocalPosition += translation;
			}
			else
			{
				Position += translation;
			}
		}

		public void RotateAround(Vector3 axis, float rotation)
		{
		}
	}
}
