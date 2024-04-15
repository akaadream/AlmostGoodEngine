using System;
using System.Collections.Generic;

namespace AlmostGoodEngine.Core.Utils
{
	public static class BetterRandom
	{
		/// <summary>
		/// Random instance
		/// </summary>
		public static Random Random
		{
			get
			{
				if (Random == null)
				{
					Initialize();
				}

				return _random;
			}
			set
			{
				_random = value;
			}
		}
		private static Random _random;

		/// <summary>
		/// Default initialization of the random instance
		/// </summary>
		public static void Initialize()
		{
			Initialize(Guid.NewGuid().GetHashCode());
		}

		/// <summary>
		/// Initialize the random instance using a seed
		/// </summary>
		/// <param name="seed"></param>
		public static void Initialize(int seed)
		{
			_random = new(seed);
		}

		/// <summary>
		/// Initialize the random instance using a seed
		/// </summary>
		/// <param name="seed"></param>
		public static void Initialize(string seed)
		{
			Initialize(Guid.Parse(seed).GetHashCode());
		}

		/// <summary>
		/// Get a integer random value inside the intervale [0; max[
		/// </summary>
		/// <param name="max"></param>
		/// <returns></returns>
		public static int Int(int max) => Random.Next(max);

		/// <summary>
		/// Get a integer random value inside the intervale [0; max]
		/// </summary>
		/// <param name="min"></param>
		/// <param name="max"></param>
		/// <returns></returns>
		public static int Int(int min, int max) => Random.Next(min, max + 1);

		/// <summary>
		/// Get a double random value inside the intervale [0.0; 1.0[ 
		/// </summary>
		/// <returns></returns>
		public static double Double() => Random.NextDouble();

		/// <summary>
		/// Get a double random value inside the intervale [min; max]
		/// </summary>
		/// <param name="min"></param>
		/// <param name="max"></param>
		/// <returns></returns>
		public static double Double(double min, double max) => Random.NextDouble() * (max - min) + min;

		/// <summary>
		/// Get a float random value inside the intervale [0f; 1f[
		/// </summary>
		/// <returns></returns>
		public static float Float() => (float)Random.NextDouble();

		/// <summary>
		/// Get a float random value inside the intervale [min; max]
		/// </summary>
		/// <param name="min"></param>
		/// <param name="max"></param>
		/// <returns></returns>
		public static float Float(float min, float max) => (float)Random.NextDouble() * (max - min) + min;

		/// <summary>
		/// Get a bool random value
		/// </summary>
		/// <returns></returns>
		public static bool Bool() => Int(0, 1) == 1;

		/// <summary>
		/// Get a random enum value inside from the given type
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="type"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		public static T Enumeration<T>(Type type) where T : struct, IConvertible
		{
			if (!type.IsEnum)
			{
				throw new Exception("This given parameter is not an enumeration");
			}

			var values = Enum.GetValues(type);
			int random = Int(values.Length);
			return (T)values.GetValue(random);
		}

		/// <summary>
		/// Get a random value from the given array
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="array"></param>
		/// <returns></returns>
		public static T Array<T>(T[] array) => array[Int(array.Length)];

		/// <summary>
		/// Get a random value from the given list
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <returns></returns>
		public static T List<T>(List<T> list) => list[Int(list.Count)];
	}
}
