using Microsoft.Xna.Framework;

namespace AlmostGoodEngine.Animations.Tweens
{
	internal class TweenVector3 : Tween<Vector3>
	{
		public TweenVector3(Vector3 start, Vector3 end, object target, float duration, float delay) :
			base(start, end, target, duration, delay)
		{

		}
	}
}
