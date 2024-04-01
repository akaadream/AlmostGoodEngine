using System;

namespace AlmostGoodEngine.Animations
{
	public class AnimationEventArgs : EventArgs
	{
		/// <summary>
		/// The animation related to the event
		/// </summary>
		public Animation Animation { get; internal set; }

		/// <summary>
		/// The keyframe related to the event
		/// </summary>
		public AnimationKeyframe Keyframe { get; internal set; }

		/// <summary>
		/// The related keyframe inside the animation's keyframes list
		/// </summary>
		public AnimationKeyframe RelatedKeyframe { get; internal set; }

		/// <summary>
		/// The index of the current keyframe
		/// </summary>
		public int KeyframeIndex { get; internal set; }
	}
}
