using System;

namespace AlmostGoodEngine.Animations.Tweens
{
    public abstract class Tween<T>(T start, T end, object target, float duration, float delay) : Tween(target, duration, delay) where T : struct
    {
		/// <summary>
		/// The starting value of the tween animation
		/// </summary>
		public T Start { get; set; } = start;

		/// <summary>
		/// The ending value of the tween animation
		/// </summary>
		public T End { get; set; } = end;
	}

	public abstract class Tween
    {
        /// <summary>
        /// The target which is using this tween
        /// </summary>
        public object Target { get; private set; }

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
        /// The current value of the tween
        /// </summary>
        public float Current { get; private set; }

        /// <summary>
        /// The entry parameters used to compute the animation
        /// </summary>
        public float Value { get => ElapsedTime / Duration; }

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
        internal Tween(object target, float duration, float delay)
        {
            Target = target;
            Duration = duration;
            Delay = delay;
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
                Current = Easer.Ease(EaseType, Value);
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
    }
}
