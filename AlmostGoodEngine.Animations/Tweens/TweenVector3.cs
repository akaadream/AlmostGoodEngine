using Microsoft.Xna.Framework;

namespace AlmostGoodEngine.Animations.Tweens
{
	public class TweenVector3 : Tween<Vector3>
	{
		internal TweenVector3(Vector3 from, Vector3 to, float duration, float delay) : base(from, to, duration, delay) { }

		protected override void Compute()
		{
			Current = Vector3.Lerp(From, To, Eased);
		}
	}
}
