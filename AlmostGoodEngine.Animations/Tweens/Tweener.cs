using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmostGoodEngine.Animations.Tweens
{
	public static class Tweener
	{
		internal static List<Tween> Actives { get; private set; } = [];

		internal static List<Tween> Inactives { get; private set; } = [];

		public static void Update()
		{
			for (int i = Actives.Count - 1; i >= 0; i--)
			{
				if (Actives[i] == null)
				{
					continue;
				}
				Actives[i].Update();
			}
		}
	}
}
