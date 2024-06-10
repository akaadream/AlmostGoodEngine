using System;

namespace AlmostGoodEngine.Animations.Tweens
{
    public abstract class Tween<T>(T from, T to, float duration, float delay) : Tween(duration, delay) where T : struct
    {
		/// <summary>
		/// The starting value of the tween animation
		/// </summary>
		public T From { get; set; } = from;

		/// <summary>
		/// The ending value of the tween animation
		/// </summary>
		public T To { get; set; } = to;

        /// <summary>
        /// The current value of the tween
        /// </summary>
        public T Current { get; set; } = from;

        /// <summary>
        /// Get a hashcode for a tween
        /// </summary>
        /// <returns></returns>
		public override int GetHashCode()
		{
			return 11 + 7 ^ From.GetHashCode() + 3 ^ To.GetHashCode();
		}
	}

	public abstract class Tween
    {
        /// <summary>
        /// The duration of the tween animation
        /// </summary>
        public float Duration { get; set; }

        /// <summary>
        /// The delay before the tween animation actually start
        /// </summary>
        public float Delay { get; set; }

        /// <summary>
        /// Time (in seconds) elapsed since the tween animation started
        /// </summary>
        public float ElapsedTime { get; private set; }

        /// <summary>
        /// If the value is true, the tween animation will loop
        /// </summary>
        public bool Looping { get; set; }

        /// <summary>
        /// If the value is true, the tween animation will be played ping-pong (pong is the reversed animation)
        /// </summary>
        public bool PingPong { get; set; }

        /// <summary>
        /// The variable is true if the tween animation is currently running
        /// </summary>
        public bool IsRunning { get; set; }

        /// <summary>
        /// The parametric curve the tween is using to compute the current value
        /// </summary>
        public EaseType EaseType { get; set; }

        /// <summary>
        /// The entry parameters used to compute the animation
        /// </summary>
        public float T { get => ElapsedTime / Duration; }

        /// <summary>
        /// Get the eased version of T
        /// </summary>
        public float EasedT { get => Easer.Ease(EaseType, T); }

        /// <summary>
        /// Callback invoked when the animation is starting
        /// </summary>
        public Action OnStart { get; set; }

        /// <summary>
        /// Callback invoked when the animation is updated
        /// </summary>
        public Action OnUpdate { get; set; }

        /// <summary>
        /// Callback invoked when the animation is finished
        /// </summary>
        public Action OnComplete { get; set; }

        /// <summary>
        /// Callback invoked when the animation is doing a loop
        /// </summary>
        public Action OnLoop { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="target"></param>
        /// <param name="duration"></param>
        /// <param name="delay"></param>
        internal Tween(float duration, float delay)
        {
            Duration = duration;
            Delay = delay;
            EaseType = EaseType.Linear;
        }

        /// <summary>
        /// Enable / Disable the looping effect of the animation
        /// </summary>
        /// <param name="looping"></param>
        /// <returns></returns>
        public Tween SetLooping(bool looping = true)
        {
            Looping = looping;
            return this;
        }

        /// <summary>
        /// Enable / Disable the ping pong effect of the animation
        /// </summary>
        /// <param name="pingPong"></param>
        /// <returns></returns>
        public Tween SetPingPong(bool pingPong = true)
        {
            PingPong = pingPong;
            return this;
        }

        /// <summary>
        /// Define a new easing method for this tween animation
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Tween SetEase(EaseType type)
        {
            EaseType = type;
            return this;
        }

        /// <summary>
        /// Define the action which will eb called when animation is starting
        /// </summary>
        /// <param name="onStart"></param>
        /// <returns></returns>
        public Tween WhenStarted(Action onStart)
        {
            OnStart = onStart;
            return this;
        }

        /// <summary>
        /// Define the action which will be called when the animation made an update
        /// </summary>
        /// <param name="onUpdate"></param>
        /// <returns></returns>
        public Tween WhenUpdated(Action onUpdate)
        {
            OnUpdate = onUpdate;
            return this;
        }

        /// <summary>
        /// Define the action which will be called when the animation ended
        /// </summary>
        /// <param name="onComplete"></param>
        /// <returns></returns>
        public Tween WhenCompleted(Action onComplete)
        {
            OnComplete = onComplete;
            return this;
        }

        /// <summary>
        /// Define the action which will be called when the animation looped
        /// </summary>
        /// <param name="onLoop"></param>
        /// <returns></returns>
        public Tween WhenLooped(Action onLoop)
        {
            OnLoop = onLoop;
            return this;
        }

        /// <summary>
        /// Start the tween animation
        /// </summary>
        public void Run(bool looping = false, bool pingPong = false)
        {
            Looping = looping;
            PingPong = pingPong;

            ElapsedTime = 0f;
            IsRunning = true;
        }

        public void Stop()
        {

        }

        /// <summary>
        /// Update the current value if the animation is running
        /// </summary>
        public void Update()
        {
            if (!IsRunning)
            {
                return;
            }

            // Animation may reached the end
            if (ElapsedTime >= Duration)
            {
                if (Looping)
                {
                    Loop();
                    return;
                }
                if (PingPong)
                {
                    Pong();
                    return;
                }
				Finish();
			}
            else
            {
                Compute();
                OnUpdate?.Invoke();
            }
        }

        /// <summary>
        /// Reset the loop at the beginning of the animation
        /// </summary>
        protected virtual void Loop()
        {
			ElapsedTime -= Duration;
			OnUpdate?.Invoke();
		}

        /// <summary>
        /// Do the pong of the ping pong effect
        /// </summary>
        protected virtual void Pong()
        {
            // Reverse end and start
			// (End, Start) = (Start, End);
			ElapsedTime -= Duration;
			OnUpdate?.Invoke();
		}

        /// <summary>
        /// Finish the animation
        /// </summary>
        protected virtual void Finish()
        {
			ElapsedTime = Duration;
			IsRunning = false;
			OnComplete?.Invoke();
		}

        /// <summary>
        /// Compute the current value of the tween
        /// </summary>
        protected virtual void Compute()
        {
		}

        /// <summary>
        /// Return true if the given object is equal to this tween aniamtion
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
		public override bool Equals(object obj)
		{
			if (obj == null)
            {
                return false;
            }

            if (obj is not Tween)
            {
                return false;
            }

            return Equals(obj as Tween);
		}

        /// <summary>
        /// Return true if the given tween is equal to this tween animation
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public virtual bool Equals(Tween other)
        {
            return other.GetHashCode() == GetHashCode();
        }

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
