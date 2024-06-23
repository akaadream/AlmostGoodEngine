using Microsoft.Xna.Framework;

namespace AlmostGoodEngine.Animations.Tweens
{
	public class TweenVector2 : Tween<Vector2>
	{
		internal TweenVector2(Vector2 from, Vector2 to, float duration, float delay) : base(from, to, duration, delay)
		{

		}

		protected override void Compute()
		{
			Current = Vector2.Lerp(From, To, Eased);
		}
	}
}
