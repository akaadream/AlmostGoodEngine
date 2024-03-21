using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmostGoodEngine.Audio.Effects
{
	public class ReverbEffect : ChannelEffect
	{
		public override void LoadContent(ContentManager contentManager)
		{
			base.LoadContent(contentManager);
		}

		public override void Play(Sound sound)
		{
			base.Play(sound);

		}
	}
}
