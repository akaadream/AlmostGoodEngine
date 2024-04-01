using System;

namespace AlmostGoodEngine.Animations.Tweens
{
    public static class Easer
    {
        /// <summary>
        /// Compute the interpolation using the right easing function
        /// </summary>
        /// <param name="type"></param>
        /// <param name="t"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static float Ease(EaseType type, float t)
        {
            switch (type)
            {
                case EaseType.Linear:
                    return Linear(t);
                case EaseType.EaseInQuad:
                    return EaseInQuad(t);
                case EaseType.EaseOutQuad:
                    return EaseOutQuad(t);
                case EaseType.EaseInOutQuad:
                    return EaseInOutQuad(t);
                case EaseType.EaseInCubic:
                    return EaseInCubic(t);
                case EaseType.EaseOutCubic:
                    return EaseOutCubic(t);
                case EaseType.EaseInOutCubic:
                    return EaseInOutCubic(t);
                case EaseType.EaseInElastic:
                    return EaseInElastic(t);
                case EaseType.EaseOutElastic:
                    return EaseOutElastic(t);
                case EaseType.EaseInOutElastic:
                    return EaseInOutElastic(t);
                case EaseType.EaseInSine:
                    return EaseInSine(t);
                case EaseType.EaseOutSine:
                    return EaseOutSine(t);
                case EaseType.EaseInOutSine:
                    return EaseInOutSine(t);
                case EaseType.EaseInBounce:
                    return EaseInBounce(t);
                case EaseType.EaseOutBounce:
                    return EaseOutBounce(t);
                case EaseType.EaseInOutBounce:
                    return EaseInOutBounce(t);
                case EaseType.EaseInQuart:
                    return EaseInQuart(t);
                case EaseType.EaseOutQuart:
                    return EaseOutQuart(t);
                case EaseType.EaseInOutQuart:
                    return EaseInOutQuart(t);
                case EaseType.EaseInQuint:
                    return EaseInQuint(t);
                case EaseType.EaseOutQuint:
                    return EaseOutQuint(t);
                case EaseType.EaseInOutQuint:
                    return EaseInOutQuint(t);
                case EaseType.EaseInExpo:
                    return EaseInExpo(t);
                case EaseType.EaseOutExpo:
                    return EaseOutExpo(t);
                case EaseType.EaseInOutExpo:
                    return EaseInOutExpo(t);
                case EaseType.EaseInCirc:
                    return EaseInCirc(t);
                case EaseType.EaseOutCirc:
                    return EaseOutCirc(t);
                case EaseType.EaseInOutCirc:
                    return EaseInOutCirc(t);
                case EaseType.EaseInBack:
                    return EaseInBack(t);
                case EaseType.EaseOutBack:
                    return EaseOutBack(t);
                case EaseType.EaseInOutBack:
                    return EaseInOutBack(t);
            }

            return t;
        }

        /// <summary>
        /// Compute the interpolation using the right easing function
        /// </summary>
        /// <param name="type"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static float Ease(EaseType type, float min, float max)
        {
            return Ease(type, min / max);
        }

        /// <summary>
        /// Compute linear function
        /// </summary>
        /// <param name="t"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static float Linear(float t)
        {
            return t;
        }

		#region Quad
		/// <summary>
		/// Compute ease in quadratic function
		/// </summary>
		/// <param name="t"></param>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <returns></returns>
		public static float EaseInQuad(float t)
        {
            return t * t;
        }

        /// <summary>
        /// Compute ease out quadratic function
        /// </summary>
        /// <param name="t"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static float EaseOutQuad(float t)
        {
            return 1f - (1f - t) * (1f - t);
        }

        /// <summary>
        /// Compute ease in out quadratic function
        /// </summary>
        /// <param name="t"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static float EaseInOutQuad(float t)
        {
            return t < 0.5f ? 2f * t * t : 1f - MathF.Pow(-2f * t + 2f, 2) / 2;
        }
        #endregion

        #region Cubic
        /// <summary>
        /// Compute ease in cubic easing function
        /// </summary>
        /// <param name="t"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static float EaseInCubic(float t)
        {
            return t * t * t;
        }

        /// <summary>
        /// Compute ease out cubic easing function
        /// </summary>
        /// <param name="t"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static float EaseOutCubic(float t)
        {
            return 1f;
        }

        /// <summary>
        /// Compute ease in out cubic easing function
        /// </summary>
        /// <param name="t"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static float EaseInOutCubic(float t)
        {
            return t < 0.5f ? 4f * t * t * t : 1f - MathF.Pow(-2f * t + 2f, 3) / 2f;
        }
		#endregion

		#region Elastic
		public static float EaseInElastic(float t)
        {
            const float c4 = (2f * MathF.PI) / 3f;
            return t == 0f ? 0f : t == 1f ? 1 : -MathF.Pow(2f, 10f * t - 10) * MathF.Sin((t * 10f - 10.75f) * c4);
        }

        public static float EaseOutElastic(float t)
        {
			const float c4 = (2f * MathF.PI) / 3f;
            return t == 0f ? 0f : t == 1f ? 1f : MathF.Pow(2f, -10f * t) * MathF.Sin((t * 10f - 0.75f) * c4) + 1f;
		}

        public static float EaseInOutElastic(float t)
        {
			const float c5 = (2f * MathF.PI) / 4.5f;
            return t == 0f ? 0f : t == 1f ? 1f : t < 0.5f ? -(MathF.Pow(2f, 20f * t - 10f) * MathF.Sin((20f * t - 11.125f) * c5)) / 2f : (MathF.Pow(2f, -20f * t + 10) * MathF.Sin((20f * t - 11.125f) * c5)) / 2f + 1f; 
		}
        #endregion

        #region Sine
        public static float EaseInSine(float t)
        {
            return 1f - MathF.Cos((t * MathF.PI) / 2);
        }

        public static float EaseOutSine(float t)
        {
            return MathF.Sin((t * MathF.PI) / 2);
        }

        public static float EaseInOutSine(float t)
        {
            return -(MathF.Cos(MathF.PI * t) - 1f) / 2f;
        }
		#endregion

		#region Bounce
        public static float EaseInBounce(float t)
        {
            return 1f - EaseOutBounce(1 - t);
        }

        public static float EaseOutBounce(float t)
        {
            const float n1 = 7.5625f;
            const float d1 = 2.75f;

            if (t < 1f / d1)
            {
                return n1 * t * t;
            }

            if (t < 2f / d1)
            {
                return n1 * (t -= 1.5f / d1) * t + 0.75f;
            }

            if (t < 2.5f / d1)
            {
                return n1 * (t -= 2.25f / d1) * t + 0.9375f;
            }

            return n1 * (t -= 2.625f / d1) * t + 0.984375f;
        }

        public static float EaseInOutBounce(float t)
        {
            return t < 0.5f ? (1f - EaseOutBounce(1f - 2f * t)) / 2f : (1f + EaseOutBounce(2f * t - 1f)) / 2f;
        }
		#endregion

		#region Quart
        public static float EaseInQuart(float t)
        {
            return t * t * t * t;
        }

        public static float EaseOutQuart(float t)
        {
            return 1f - MathF.Pow(1f - t, 4f);
        }

        public static float EaseInOutQuart(float t)
        {
            return t < 0.5f ? 8f * t * t * t * t : 1f - MathF.Pow(-2f * t + 2f, 4f) / 2f;
        }
		#endregion

		#region Quint
        public static float EaseInQuint(float t)
        {
            return t * t * t * t * t;
        }

		public static float EaseOutQuint(float t)
		{
            return 1f - MathF.Pow(1f - t, 5f);
		}

		public static float EaseInOutQuint(float t)
		{
            return t < 0.5f ? 16f * t * t * t * t * t : 1f - MathF.Pow(-2f * t + 2f, 5f) / 2f;
		}
		#endregion

		#region Expo
        public static float EaseInExpo(float t)
        {
            return t == 0f ? 0f : MathF.Pow(2f, 10f * t - 10f);
        }

        public static float EaseOutExpo(float t)
        {
            return t == 1f ? 1f : 1f - MathF.Pow(2f, -10f * t);
        }

        public static float EaseInOutExpo(float t)
        {
            return t == 0f ? 0f : t == 1f ? 1f : t < 0.5f ? MathF.Pow(2f, 20f * t - 10f) / 2f : (2f - MathF.Pow(2f, -20f * t + 10f)) / 2f;
        }
		#endregion

		#region Circ
        public static float EaseInCirc(float t)
        {
            return 1f - MathF.Sqrt(1f - MathF.Pow(t, 2f));
        }

        public static float EaseOutCirc(float t)
        {
            return MathF.Sqrt(1f - MathF.Pow(t - 1f, 2f));
        }

        public static float EaseInOutCirc(float t)
        {
            return t < 0.5f ? (1f - MathF.Sqrt(1f - MathF.Pow(2f * t, 2f))) / 2f : (MathF.Sqrt(1f - MathF.Pow(-2f * t + 2f, 2f)) + 1f) / 2f;
        }
		#endregion

		#region Back
        public static float EaseInBack(float t)
        {
            const float c1 = 1.70158f;
            const float c3 = c1 + 1f;

            return c3 * t * t * t - c1 * t * t;
        }

        public static float EaseOutBack(float t)
        {
			const float c1 = 1.70158f;
			const float c3 = c1 + 1f;

            return 1f + c3 * MathF.Pow(t - 1f, 3f) + c1 * MathF.Pow(t - 1f, 2f);
		}

        public static float EaseInOutBack(float t)
        {
			const float c1 = 1.70158f;
			const float c2 = c1 * 1.525f;

            return t < 0.5f ? (MathF.Pow(2f * t, 2f) * ((c2 + 1f) * 2f * t - c2)) / 2f : (MathF.Pow(2f * t - 2f, 2f) * ((c2 + 1f) * (t * 2f - 2f) + c2) + 2f) / 2f;
		}
		#endregion
	}
}
