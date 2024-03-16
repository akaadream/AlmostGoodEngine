using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace AlmostGoodEngine.Audio
{
	public static class Audio
	{
		/// <summary>
		/// The dictionary of all the available channels
		/// </summary>
		public static Dictionary<string, Channel> Channels { get; set; }

		/// <summary>
		/// The content manager used to load sounds
		/// </summary>
		public static ContentManager ContentManager { get; set; }


		/// <summary>
		/// Initialize the audio engine
		/// </summary>
		public static void Initialize(ContentManager contentManager)
		{
			Channels = [];
			ContentManager = contentManager;

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
