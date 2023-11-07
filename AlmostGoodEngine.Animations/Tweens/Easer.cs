using Microsoft.Xna.Framework;

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
        public static float Ease(EaseType type, float t, float start, float end)
        {
            switch (type)
            {
                case EaseType.Linear:
                    return Linear(t, start, end);
                case EaseType.EaseInQuad:
                    return EaseInQuad(t, start, end);
                case EaseType.EaseOutQuad:
                    return EaseOutQuad(t, start, end);
                case EaseType.EaseInOutQuad:
                    return EaseInOutQuad(t, start, end);
            }

            return t;
        }

        /// <summary>
        /// Compute linear function
        /// </summary>
        /// <param name="t"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static float Linear(float t, float start, float end)
        {
            return MathHelper.Lerp(start, end, t);
        }

        /// <summary>
        /// Compute ease in quadratic function
        /// </summary>
        /// <param name="t"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static float EaseInQuad(float t, float start, float end)
        {
            return (end - start) * t * t;
        }

        /// <summary>
        /// Compute ease out quadratic function
        /// </summary>
        /// <param name="t"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static float EaseOutQuad(float t, float start, float end)
        {
            return (end - start) * t * (2 - t) + start;
        }

        /// <summary>
        /// Compute ease in out quadratic function
        /// </summary>
        /// <param name="t"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static float EaseInOutQuad(float t, float start, float end)
        {
            if (t < 0.5f)
            {
                return EaseInQuad(t * 2, start, (start + end) / 2);
            }

            return EaseOutQuad((t - 0.5f) * 2, (start + end) / 2, end);
        }

        /// <summary>
        /// Compute ease in cubic easing function
        /// </summary>
        /// <param name="t"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static float EaseInCubic(float t, float start, float end)
        {
            return (end - start) * t * t * t + start;
        }

        /// <summary>
        /// Compute ease out cubic easing function
        /// </summary>
        /// <param name="t"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static float EaseOutCubic(float t, float start, float end)
        {
            return (end - start) * (t * t * t + 1) + start;
        }

        /// <summary>
        /// Compute ease in out cubic easing function
        /// </summary>
        /// <param name="t"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static float EaseInOutCubic(float t, float start, float end)
        {
            if (t < 0.5f)
            {
                return EaseInCubic(t * 2, start, (start + end) / 2);
            }

            return EaseOutCubic((t - 0.5f) * 2, (start + end) / 2, end);
        }
    }
}
