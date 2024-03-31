using Microsoft.Xna.Framework;
using System;

namespace AlmostGoodEngine.Core
{
    public static class Time
    {
        /// <summary>
        /// Time elapsed since the previous frame
        /// </summary>
        public static float DeltaTime { get; private set; }

        public static float FixedDeltaTimeTarget { get => (int)(1000 / (float)60); }

        /// <summary>
        /// The delta time of the previous frame
        /// </summary>
        public static float PreviousDeltaTime { get; private set; }

        /// <summary>
        /// Delta time scaling value
        /// </summary>
        public static float Scale
        {
            get => Scale;
            set
            {
                if (value <= 0f)
                {
                    throw new ArgumentException("The delta time scale must be greater than 0");
                }
            }
        }

        /// <summary>
        /// Delta time * a scale
        /// </summary>
        public static float ScaledDeltaTime { get => DeltaTime * Scale; }

        /// <summary>
        /// Frames per seconds
        /// </summary>
        public static int FPS { get; private set; } = 60;

        // Variables to compute the FPS counter
        private static int _fpsCount;
        private static TimeSpan _fpsElapsedTimeSpan;

        /// <summary>
        /// Updates per seconds
        /// </summary>
        public static int UPS { get; private set; } = 60;

        // Variables to compute the UPS counter
        private static int _upsCount;
        private static TimeSpan _upsElapsedTimeSpan;

		#region Fixed update parameters

		internal static float accumulatedTime;
        internal static float previousT = 0f;
        internal static float accumulator = 0f;
        internal static float maxFrameTime = 250f;
        public static float FixedDeltaTime { get; set; } = 0f;
        public static int FixedFPS { get; private set; } = 60;
        internal static float alpha = 0f;

		#endregion

		/// <summary>
		/// Compute the UPS counter
		/// </summary>
		/// <param name="gameTime"></param>
		public static void Update(GameTime gameTime)
        {
            // Check the fixed time step
            if (previousT == 0f)
            {
                previousT = (float)gameTime.TotalGameTime.TotalMilliseconds;
            }

            float now = (float)gameTime.TotalGameTime.TotalMilliseconds;
			FixedDeltaTime = now - previousT;
            if (FixedDeltaTime > maxFrameTime)
            {
				FixedDeltaTime = maxFrameTime;
            }
            previousT = now;
            accumulator += FixedDeltaTime;

            alpha = (accumulator / FixedDeltaTimeTarget);

            // Store the delta time of the previous frame
            PreviousDeltaTime = DeltaTime;

            // Get the delta time
            DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Update the UPS counter
            _upsCount++;
            _upsElapsedTimeSpan += gameTime.ElapsedGameTime;
            if (_upsElapsedTimeSpan >= TimeSpan.FromSeconds(1))
            {
                UPS = _upsCount;
                _upsCount = 0;
                _upsElapsedTimeSpan -= TimeSpan.FromSeconds(1);
            }
        }

        /// <summary>
        /// Compute the FPS counter
        /// </summary>
        /// <param name="gameTime"></param>
        public static void Draw(GameTime gameTime)
        {
            // Update the FPS counter
            _fpsCount++;
            _fpsElapsedTimeSpan += gameTime.ElapsedGameTime;
            if (_fpsElapsedTimeSpan >= TimeSpan.FromSeconds(1))
            {
                FPS = _fpsCount;
                _fpsCount = 0;
                _fpsElapsedTimeSpan -= TimeSpan.FromSeconds(1);
            }
        }
    }
}
