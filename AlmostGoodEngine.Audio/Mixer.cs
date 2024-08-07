﻿using System.Collections.Generic;

namespace AlmostGoodEngine.Audio
{
	public static class Mixer
	{
		/// <summary>
		/// The dictionary of all the available channels
		/// </summary>
		public static Dictionary<string, Channel> Channels { get; set; }


		/// <summary>
		/// Initialize the audio engine
		/// </summary>
		public static void Initialize()
		{
			Channels = [];

			// Create default channels
			var master = Register("master");
			if (master != null)
			{
				Register("musics", master);
				Register("sounds", master);
			}
		}

		/// <summary>
		/// Register the given channel
		/// </summary>
		/// <param name="name"></param>
		public static Channel Register(string name, Channel parent = null)
		{
			if (Channels.ContainsKey(name))
			{
				return null;
			}

			var chanel = new Channel(parent);
			Channels.Add(name, chanel);
			return chanel;
		}

		/// <summary>
		/// Unregister the given channel
		/// </summary>
		/// <param name="name"></param>
		public static void Unregister(string name)
		{
			if (!Channels.ContainsKey(name))
			{
				return;
			}

			Channels.Remove(name);
		}
	}
}
