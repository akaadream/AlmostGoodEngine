using AlmostGoodEngine.Animations.Tweens;
using Microsoft.Xna.Framework;

namespace AlmostGoodEngine.Animations
{
	public class AnimationKeyframe
	{
		/// <summary>
		/// The duration of the animation part
		/// </summary>
		public float Duration { get; set; }

		/// <summary>
		/// The next position
		/// </summary>
		public Vector3 Position { get; set; }

		/// <summary>
		/// The next scale
		/// </summary>
		public Vector3 Scale { get; set; }

		/// <summary>
		/// The next rotation
		/// </summary>
		public Vector3 Rotation { get; set; }

		/// <summary>
		/// The easing method used during this animation part
		/// </summary>
		public EaseType EaseType { get; set; } = EaseType.Linear;

		public AnimationKeyframe()
		{

		}
	}
}
