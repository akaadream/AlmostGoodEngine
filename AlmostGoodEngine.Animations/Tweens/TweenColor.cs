using Microsoft.Xna.Framework;

namespace AlmostGoodEngine.Animations.Tweens
{
	internal class TweenColor : Tween<Color>
	{
		public TweenColor(Color start, Color end, object target, float duration, float delay) :
			base(start, end, target, duration, delay)
		{

		}
	}
}
