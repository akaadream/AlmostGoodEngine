using Microsoft.Xna.Framework;

namespace AlmostGoodEngine.Animations.Tweens
{
	public class TweenFloat : Tween<float>
	{
		internal TweenFloat(float from, float to, float duration, float delay): base(from, to, duration, delay)
		{

		}

		protected override void Compute()
		{
			Current = MathHelper.Lerp(From, To, EasedT);
		}
	}
}
