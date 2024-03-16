using Microsoft.Xna.Framework;
using System.IO;

namespace AlmostGoodEngine.Audio
{
	public abstract class ChannelEffect
	{
		public ChannelEffect()
		{
		}

		protected virtual void ApplyEffect()
		{
			// To implement
		}

		public virtual void Play(Stream audioStream, float reverbTime)
		{
			// To implement
		}
	}
}
