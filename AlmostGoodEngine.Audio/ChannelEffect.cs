using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System.IO;

namespace AlmostGoodEngine.Audio
{
	public abstract class ChannelEffect
	{
		public float EffectValue { get; set; }

		public SoundEffect Effect { get; set; }

		public ChannelEffect()
		{
		}

		public virtual void LoadContent(ContentManager contentManager)
		{
			
			
		}

		protected virtual void ApplyEffect()
		{
			// To implement
		}

		public virtual void Play(Sound sound)
		{
			// To implement
			var soundInstance = new DynamicSoundEffectInstance(44100, AudioChannels.Stereo)
			{
				IsLooped = true,
			};
			soundInstance.Play();
			var otherInstance = sound.Effect.CreateInstance();
		}
	}
}
