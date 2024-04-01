using AlmostGoodEngine.Physics.Extends;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace AlmostGoodEngine.Physics
{
	public class PhysicsManager
	{
		internal static List<Collider> Colliders { get; private set; }

		public static void Initialize()
		{
			Colliders = new();
		}

		/// <summary>
		/// Register the given collider
		/// </summary>
		/// <param name="collider"></param>
		/// <returns></returns>
		public static bool Register(Collider collider)
		{
			if (collider == null)
			{
				return false;
			}

			if (Colliders.Contains(collider))
			{
				return false;
			}

			Colliders.Add(collider);
			return true;
		}

		/// <summary>
		/// Unregister the given collider
		/// </summary>
		/// <param name="collider"></param>
		/// <returns></returns>
		public static bool Unregister(Collider collider)
		{
			if (collider == null)
			{
				return false;
			}

			return Colliders.Remove(collider);
		}

		/// <summary>
		/// Update the physics engine
		/// </summary>
		public static List<(Collider, Collider)> DetectCollisions()
		{
			// Sort colliders on the X axis
			// The sort should be computed only when a collider is added, removed or updated
			// So next, we can consider that the Colliders list is already sorted.

			var activeIntervals = new List<Interval>();
			var collisions = new List<(Collider, Collider)>();

			// Check colliders along the x axis
			foreach (Collider collider in Colliders)
			{
				// If the collider should not be active
				if (!collider.Active)
				{
					continue;
				}

				// Get the interval from the current collider on the Y axis
				var interval = collider.GetRectangle().ToInterval(Vector2.UnitY);

				// Delete new non active intervals
				for (int i = activeIntervals.Count - 1; i >= 0; i--)
				{
					if (activeIntervals[i].Max < interval.Min)
					{
						activeIntervals.RemoveAt(i);
					}
				}

				// Add the current interval to the actives list
				activeIntervals.Add(interval);

				// Verify collisions with actives intervals
				for (int i = 0; i < activeIntervals.Count; i++)
				{
					if (activeIntervals[i].Max > interval.Min)
					{
						collisions.Add((collider, Colliders[i]));
					}
				}
			}

			// Return the colliders pairs representing the detected collisions
			return collisions;
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
				if (!collider.Active)
				{
					continue;
				}

				if (collider.IsInside(nextPosition))
				{
					return true;
				}
			}

			return false;
		}

		public static bool OverlapCheck(Collider a, Collider b)
		{
			return a.Collide(b, false);
		}

	}
}
