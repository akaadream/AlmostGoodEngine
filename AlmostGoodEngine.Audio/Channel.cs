using System.Collections.Generic;
using MonoSound;
using MonoSound.Default;
using MonoSound.Filters;

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
		public float Volume
		{
			get
			{
				if (Parent != null)
				{
					return _volume * Parent.Volume;
				}

				return _volume;
			}

			set
			{
				_volume = value;
			}
		}
		private float _volume = 1f;

		public List<int> Effects { get; set; }

		/// <summary>
		/// The parent channel used to merge the parent's volume on this channel's volume
		/// </summary>
		public Channel Parent { get; internal set; } = parent;

		public void SetReverb(float strength = 0.5f, float feedback = 0.5f, float dampness = 0.5f, float stereoWidth = 1f)
		{
			Remove<FreeverbFilter>();
			Effects.Add(FilterLoader.RegisterReverbFilter(strength, feedback, dampness, stereoWidth));
		}

		public void SetEcho(float strength = 0.5f, float delay = 0.5f, float decay = 0.5f, float bias = 0.5f)
		{
			Remove<EchoFilter>();
			Effects.Add(FilterLoader.RegisterEchoFilter(strength, delay, decay, bias));
		}

		private void Remove<T>()
		{
            for (int i = Effects.Count - 1; i >= 0; i--)
            {
                SoLoudFilter filter = FilterLoader.GetRegisteredFilter(Effects[i]);
                if (filter is T)
                {
					Effects.RemoveAt(i);
                    return;
                }
            }
        }
	}
}
