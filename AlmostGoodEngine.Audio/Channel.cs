using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace AlmostGoodEngine.Audio
{
	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="parent"></param>
	public class Channel(Channel parent = null)
	{
		/// <summary>
		/// The volume of the channel. The value should be between 0 and 1
		/// </summary>
		public float Volume { get; set; } = 1.0f;

		/// <summary>
		/// List of the effects we want to apply to this channel
		/// </summary>
		public List<ChannelEffect> Effects { get; set; } = [];

		/// <summary>
		/// Sounds dictionary
		/// </summary>
		private Dictionary<string, Sound> sounds { get; set; } = [];

		/// <summary>
		/// The parent channel used to merge the parent's volume on this channel's volume
		/// </summary>
		public Channel Parent { get; internal set; } = parent;

		/// <summary>
		/// Play a sounds using its name
		/// </summary>
		/// <param name="name"></param>
		public void Play(string name)
		{
			if (sounds.ContainsKey(name))
			{
				PlayExisting(name);
				return;
			}

			if (Audio.ContentManager == null)
			{
				return;
			}

			sounds.Add(name, new Sound(Audio.ContentManager.Load<SoundEffect>(name)));
			PlayExisting(name);
		}

		public void PlayExisting(string name)
		{
			if (!sounds.ContainsKey(name))
			{
				return;
			}

			var sound = sounds[name];
			var volume = Volume;
			if (Parent != null)
			{
				volume *= Parent.Volume;
			}
			sound.Play(volume);
		}
	}
}
