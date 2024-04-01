using AlmostGoodEngine.Animations.Tweens;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;


namespace AlmostGoodEngine.Animations
{
	public class Animation()
	{
		/// <summary>
		/// All the key frames considering they are ordered
		/// </summary>
		internal List<AnimationKeyframe> Keyframes = [];

		/// <summary>
		/// The keyframe with initial values of the animation
		/// </summary>
		public AnimationKeyframe StartingKeyframe { get;  set; }

		/// <summary>
		/// Get the total duration of the animation
		/// </summary>
		public float TotalDuration
		{
			get
			{
				float duration = 0f;
				foreach (AnimationKeyframe key in Keyframes)
				{
					duration += key.Duration;
				}
				return duration;
			}
		}

		public event EventHandler<AnimationEventArgs> OnAnimationUpdate;

		public bool Running { get; set; }
		public bool Looping { get; set; } = false;

		public int CurrentAnimationIndex { get; private set; } = 0;
		public AnimationKeyframe CurrentKeyframe { get => Keyframes[CurrentAnimationIndex]; }
		public AnimationKeyframe PreviousKeyframe
		{
			get
			{
				if (CurrentAnimationIndex > 0)
				{
					return Keyframes[CurrentAnimationIndex - 1];
				}

				return StartingKeyframe;
			}
		}

		#region Timer parameters

		private float _timer = 0f;
		private float _t = 0f;

		#endregion

		/// <summary>
		/// Reorder the keyframes list, done automatically when you add/remove a keyframe
		/// </summary>
		public void Reorder()
		{
			Keyframes.Sort((k1, k2) => k1.Duration.CompareTo(k2.Duration));
		}

		/// <summary>
		/// Add a keyframe inside the list
		/// </summary>
		/// <param name="keyframe"></param>
		public void AddKeyframe(AnimationKeyframe keyframe)
		{
			if (Keyframes.Contains(keyframe))
			{
				return;
			}

			Keyframes.Add(keyframe);
			Reorder();
		}

		/// <summary>
		/// Remove a keyframe using the given instance
		/// </summary>
		/// <param name="keyframe"></param>
		public void RemoveKeyframe(AnimationKeyframe keyframe)
		{
			Keyframes.Remove(keyframe);
		}

		/// <summary>
		/// Remove a keyframe using the given index
		/// </summary>
		/// <param name="index"></param>
		public void RemoveKeyframe(int index)
		{
			if (index >= Keyframes.Count)
			{
				return;
			}

			Keyframes.RemoveAt(index);
			Reorder();
		}

		/// <summary>
		/// Get the keyframe at the given index
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public AnimationKeyframe GetKeyframe(int index)
		{
			if (index >= Keyframes.Count)
			{
				return null;
			}

			return Keyframes[index];
		}

		/// <summary>
		/// Start the animation
		/// </summary>
		public void Start()
		{
			if (Keyframes.Count == 0)
			{
				return;
			}

			CurrentAnimationIndex = 0;
			Running = true;
		}

		/// <summary>
		/// Stop the animation
		/// </summary>
		public void Stop()
		{
			Running = true;
		}

		/// <summary>
		/// Update the animation
		/// </summary>
		/// <param name="delta"></param>
		public void Update(float delta)
		{
			if (!Running)
			{
				return;
			}

			if (_timer >= TotalDuration)
			{
				Stop();
			}
			else
			{
				_timer += delta;

				if (_timer >= CurrentKeyframe.Duration)
				{
					CurrentAnimationIndex++;
				}

				_t = Easer.Ease(CurrentKeyframe.EaseType, (_timer - PreviousKeyframe.Duration) / CurrentKeyframe.Duration);

				// Apply the update
				OnAnimationUpdate?.Invoke(this, new AnimationEventArgs()
				{
					Animation = this,
					Keyframe = CreateCurrentKeyframe(),
					RelatedKeyframe = CurrentKeyframe,
					KeyframeIndex = CurrentAnimationIndex
				});
			}
		}

		private AnimationKeyframe CreateCurrentKeyframe()
		{
			return new()
			{
				Duration = CurrentKeyframe.Duration,
				EaseType = CurrentKeyframe.EaseType,
				Position = Vector3.Lerp(PreviousKeyframe.Position, CurrentKeyframe.Position, _t),
				Scale = Vector3.Lerp(PreviousKeyframe.Scale, CurrentKeyframe.Scale, _t),
			};
		}
	}
}
