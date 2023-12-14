using AlmostGoodEngine.Core.ECS;
using Microsoft.Xna.Framework;
using System;

namespace AlmostGoodEngine.Core.Components.Time
{
    public class Timer : Component
    {
        /// <summary>
        /// The duration of the timer
        /// </summary>
        public float Duration { get; set; }

        // The timer
        private float _timer = 0f;

        /// <summary>
        /// The time remaining before the end of the timer
        /// </summary>
        public float Remaining { get => Duration - _timer; }

        /// <summary>
        /// Get the T parameter between 0 and 1
        /// </summary>
        public float T { get => Math.Clamp(_timer / Duration, 0f, 1f); }

        /// <summary>
        /// True if the timer has started
        /// </summary>
        public bool Started { get; private set; }

        /// <summary>
        /// If the timer may looping
        /// </summary>
        public bool Looping { get; set; }

        /// <summary>
        /// Callback called when the timer start
        /// </summary>
        public Action OnStarted { get; set; }

        /// <summary>
        /// Callback called when the timer loop
        /// </summary>
        public Action OnLoop { get; set; }

        /// <summary>
        /// Callback called when the timer has been completed
        /// </summary>
        public Action OnComplete { get; set; }

        public Timer(float duration = 1f, Action onComplete = null)
        {
            Duration = duration;
            OnComplete = onComplete;
        }

        /// <summary>
        /// Launch the timer
        /// </summary>
        public void Launch(float startAt = 0f)
        {
            _timer = startAt;
            Started = true;
            OnStarted?.Invoke();
        }

        public void Stop()
        {
            Started = false;
            _timer = 0f;
        }

        public override void Update(GameTime gameTime)
        {
            // The timer not started, we can skip the timer update
            if (!Started)
            {
                return;
            }

            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            // The timer may end
            if (_timer >= Duration)
            {
                _timer = Duration;
                OnComplete?.Invoke();

                // Looping
                if (Looping)
                {
                    OnLoop?.Invoke();
                    _timer = 0f;
                }
                else
                {
                    Started = false;
                }
            }
        }

        public override string ToString()
        {
            return Remaining.ToString("0");
        }
    }
}
