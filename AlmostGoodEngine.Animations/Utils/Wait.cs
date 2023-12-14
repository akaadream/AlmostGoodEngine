using System;

namespace AlmostGoodEngine.Animations.Utils
{
    public static class Wait
    {
        public static TimeSpan ForMilliseconds(float milliseconds)
        {
            return TimeSpan.FromMilliseconds(milliseconds);
        }

        public static TimeSpan ForSeconds(float seconds)
        {
            return TimeSpan.FromSeconds(seconds);
        }

        public static TimeSpan ForMinutes(float minutes)
        {
            return TimeSpan.FromMinutes(minutes);
        }

        public static TimeSpan ForHours(float hours)
        {
            return TimeSpan.FromHours(hours);
        }

        public static TimeSpan ForDays(float days)
        {
            return TimeSpan.FromDays(days);
        }
    }
}
