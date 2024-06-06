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

		/// <summary>
		/// The parent channel used to merge the parent's volume on this channel's volume
		/// </summary>
		public Channel Parent { get; internal set; } = parent;
	}
}
