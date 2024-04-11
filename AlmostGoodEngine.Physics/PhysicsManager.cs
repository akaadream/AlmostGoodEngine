using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace AlmostGoodEngine.Physics
{
	public class PhysicsManager
	{
		internal static List<Collider> Colliders { get; private set; }

		public static void Initialize()
		{
			Colliders = [];
		}

		/// <summary>
		/// Check if the given position collide with one of the given colliders
		/// </summary>
		/// <param name="colliders"></param>
		/// <param name="nextPosition"></param>
		/// <returns></returns>
		public static bool CollideAt(List<Collider> colliders, Vector2 nextPosition)
		{
			foreach (var collider in colliders)
			{
				if (collider.Collide(nextPosition))
				{
					return true;
				}
			}

			return false;
		}

		public static bool OverlapCheck(Collider a, Collider b)
		{
			return a.Collide(b);
		}

	}
}
