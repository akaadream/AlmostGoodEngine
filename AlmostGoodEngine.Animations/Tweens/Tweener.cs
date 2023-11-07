using System;

namespace AlmostGoodEngine.Animations.Tweens
{
    public class Tweener
    {
        /// <summary>
        /// The starting value of the tween animation
        /// </summary>
        public float Start { get; set; }

        /// <summary>
        /// The ending value of the tween animation
        /// </summary>
        public float End { get; set; }

        /// <summary>
        /// The duration of the tween animation
        /// </summary>
        public float Duration { get; set; }

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
        public float T { get => ElapsedTime / Duration; }

        /// <summary>
        /// Callback invoked when the animation is updated
        /// </summary>
        public Action OnUpdate { get; set; }

        /// <summary>
        /// Callback invoked when the animation is finished
        /// </summary>
        public Action OnComplete { get; set; }


        private Tweener(float start, float end, float duration)
        {
            Start = start;
            End = end;
            Duration = duration;
        }

        /// <summary>
        /// Return a freshly created tween
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="duration"></param>
        /// <param name="looping"></param>
        /// <param name="pingPong"></param>
        /// <returns></returns>
        public static Tweener Create(float start, float end, float duration)
        {
            return new Tweener(start, end, duration);
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

            if (ElapsedTime >= Duration)
            {
                if (Looping)
                {
                    ElapsedTime -= Duration;
                    Current = End;
                    OnUpdate?.Invoke();
                    return;
                }
                else if (PingPong)
                {
                    (End, Start) = (Start, End);
                    Current = End;
                    ElapsedTime -= Duration;
                    OnUpdate?.Invoke();
                    return;
                }
                else
                {
                    ElapsedTime = Duration;
                    Current = End;
                    IsRunning = false;
                    OnComplete?.Invoke();
                }
            }
            else
            {
                Current = Easer.Ease(EaseType, T, Start, End);
                OnUpdate?.Invoke();
            }
        }
    }
}
