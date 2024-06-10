using Microsoft.Xna.Framework;

namespace AlmostGoodEngine.Animations.Tweens
{
	public class TweenColor : Tween<Color>
	{
		internal TweenColor(Color from, Color to, float duration, float delay) : base(from, to, duration, delay)
		{

		}

		protected override void Compute()
		{
			Current = Color.Lerp(From, To, EasedT);
		}
	}
}
