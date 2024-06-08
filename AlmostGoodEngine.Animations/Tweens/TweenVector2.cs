using Microsoft.Xna.Framework;

namespace AlmostGoodEngine.Animations.Tweens
{
	internal class TweenVector2 : Tween<Vector2>
	{
		public TweenVector2(Vector2 start, Vector2 end, object target, float duration, float delay) :
			base(start, end, target, duration, delay)
		{

		}
	}
}
