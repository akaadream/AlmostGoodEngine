using Microsoft.Xna.Framework;

namespace AlmostGoodEngine.Animations.Tweens
{
	public static class Tweener
	{
		//internal static List<Tween<T>> Actives { get; private set; } = [];

		//internal static List<Tween<T>> Inactives { get; private set; } = [];

		public static TweenColor Color(Color from, Color to, float duration = 1f, float delay = 0f)
		{
			return new(from, to, duration, delay);
		}

		public static TweenVector2 Vector2(Vector2 from, Vector2 to, float duration = 1f, float delay = 0f)
		{
			return new(from, to, duration, delay);
		}

		public static TweenVector3 Vector3(Vector3 from, Vector3 to, float duration = 1f, float delay = 0f)
		{
			return new(from, to, duration, delay);
		}

		public static TweenFloat Float(float from, float to, float duration = 1f, float delay = 0f)
		{
			return new(from, to, duration, delay);
		}

		/// <summary>
		/// Update current active tweens
		/// </summary>
		public static void Update(float delta)
		{
			//for (int i = Actives.Count - 1; i >= 0; i--)
			//{
			//	if (Actives[i] == null)
			//	{
			//		continue;
			//	}
			//	Actives[i].Update(delta);
			//}
		}

		/// <summary>
		/// Reset cache
		/// </summary>
		internal static void Reset()
		{
			//Actives.Clear();
			//Inactives.Clear();
		}

		/// <summary>
		/// Add the given tween inside the chached actives tweens list
		/// </summary>
		/// <param name="tween"></param>
		/// <returns></returns>
		//internal static bool AddActive(this Tween tween)
		//{
		//	if (Actives.Contains(tween))
		//	{
		//		return false;
		//	}

		//	Actives.Add(tween);
		//	Inactives.Remove(tween);
		//	return true;
		//}

		/// <summary>
		/// Remove the given tween from the actives tweens list
		/// </summary>
		/// <param name="tween"></param>
		/// <returns></returns>
		//internal static bool RemoveActive(this Tween tween)
		//{
		//	return Actives.Remove(tween);
		//}

		/// <summary>
		/// Add the given tween inside the cached inactives tweens list
		/// </summary>
		/// <param name="tween"></param>
		/// <returns></returns>
		//internal static bool AddInactive(this Tween tween)
		//{
		//	if (Inactives.Contains(tween))
		//	{
		//		return false;
		//	}

		//	Inactives.Add(tween);
		//	Actives.Remove(tween);
		//	return true;
		//}

		/// <summary>
		/// Remove the given tween from the inactives tweens list
		/// </summary>
		/// <param name="tween"></param>
		/// <returns></returns>
		//internal static bool RemoveInactive(this Tween tween)
		//{
		//	return Inactives.Remove(tween);
		//}

		/// <summary>
		/// Cancel all the tween animations where the tween object is the same than the other tween animations
		/// </summary>
		/// <param name="tween"></param>
		//internal static void CancelOthers(Tween tween)
		//{
		//	foreach (var active in Actives)
		//	{
		//		if (active == tween)
		//		{
		//			continue;
		//		}

		//		if (active.Equals(tween))
		//		{
		//			active.Stop();
		//		}
		//	}
		//}

		/// <summary>
		/// Find a cached tween
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="T2"></typeparam>
		/// <param name="target"></param>
		/// <returns></returns>
		//private static T GetCachedTween<T, T2>(Tween target) where T : Tween where T2 : struct
		//{
		//	var inactiveIndex = InactiveExists<T, T2>(target);
		//	if (inactiveIndex >= 0)
		//	{
		//		var tween = Inactives[inactiveIndex];
		//		Inactives.RemoveAt(inactiveIndex);

		//		return tween as T;
		//	}

		//	var activeIndex = ActiveExists<T, T2>(target);
		//	if (activeIndex >= 0)
		//	{
		//		return Actives[activeIndex] as T;
		//	}

		//	return default;
		//}

		/// <summary>
		/// Find an existing active tween
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="T2"></typeparam>
		/// <param name="target"></param>
		/// <returns></returns>
		//private static int ActiveExists<T, T2>(Tween target) where T : Tween where T2 : struct
		//{
		//	return GetIndexOfSame<T, T2>(Actives, target);
		//}

		/// <summary>
		/// Find an existing inactive tween
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="T2"></typeparam>
		/// <param name="target"></param>
		/// <returns></returns>
		//private static int InactiveExists<T, T2>(Tween target) where T : Tween where T2 : struct
		//{
		//	return GetIndexOfSame<T, T2>(Inactives, target);
		//}

		/// <summary>
		/// Find the index of the tween corresponding to the given parameters
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="T2"></typeparam>
		/// <param name="tweens"></param>
		/// <param name="target"></param>
		/// <returns></returns>
		//private static int GetIndexOfSame<T, T2>(List<Tween> tweens, Tween target) where T : Tween where T2 : struct
		//{
		//	for (int i = 0; i < tweens.Count; i++)
		//	{
		//		if (tweens[i] is not T)
		//		{
		//			continue;
		//		}

		//		if (tweens[i].Equals(target))
		//		{
		//			return i;
		//		}
		//	}

		//	return -1;
		//}
	}
}
