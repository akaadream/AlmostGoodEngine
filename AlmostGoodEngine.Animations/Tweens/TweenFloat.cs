using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmostGoodEngine.Animations.Tweens
{
	internal class TweenFloat : Tween<float>
	{
		public TweenFloat(float start, float end, object target, float duration, float delay):
			base(start, end, target, duration, delay)
		{

		}
	}
}
