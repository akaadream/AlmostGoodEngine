using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;

namespace AlmostGoodEngine.Audio
{
    public class Sound(SoundEffect soundEffect) : ISound
	{
		/// <summary>
		/// The sound effect ressource
		/// </summary>
		public SoundEffect Effect { get; private set; } = soundEffect;

        /// <summary>
        /// The channel on which this sound should play
        /// </summary>
        public string Channel { get; set; } = "master";

		/// <summary>
		/// The maximum number of occurences of this sounds at the same time
		/// </summary>
		public int MaxInstance { get; set; } = 1;

        /// <summary>
        /// If true, a new call of the play function will replace the first instance
        /// </summary>
        public bool ReplaceMax { get; set; } = true;

        /// <summary>
        /// The duration of the sound
        /// </summary>
        public TimeSpan Duration { get => Effect.Duration; }

        /// <summary>
        /// The name of the the sound
        /// </summary>
        public string Name { get => Effect.Name; }

        /// <summary>
        /// If the sound get disposed
        /// </summary>
        public bool IsDisposed { get => Effect.IsDisposed; }

		/// <summary>
		/// The list of all the instances currently played
		/// </summary>
		private readonly List<SoundEffectInstance> instances = [];

        /// <summary>
        /// Fired when an instance of the the sound is finished
        /// This event is not called if the instance has been canceled
        /// </summary>
		public event EventHandler<EventArgs> OnFinished;

		public void Dispose()
        {
            Effect?.Dispose();
            Effect = null;
        }

        /// <summary>
        /// Play the sound
        /// </summary>
        /// <param name="volume"></param>
        /// <param name="loop"></param>
        public void Play(float volume = 1f, bool loop = false)
        {
            if (instances.Count > 0 && instances.Count >= MaxInstance)
            {
                if (ReplaceMax)
                {
					instances[0].Stop();
					instances.RemoveAt(0);
				}
                else
                {
                    return;
                }
            }

            var instance = Effect.CreateInstance();
            if (Mixer.Channels.TryGetValue(Channel, out var channel))
            {
				instance.Volume = volume * channel.Volume;
			}
            else
            {
				instance.Volume = volume;
			}
            
            instance.IsLooped = loop;
            instance.Play();
            instances.Add(instance);
        }

        /// <summary>
        /// Update instances of this sound
        /// </summary>
        public void Update()
        {
            for (int i = instances.Count - 1; i >= 0; i--)
            {
                if (instances[i] == null)
                {
                    instances.RemoveAt(i);
					continue;
                }

                if (instances[i].State == SoundState.Stopped ||
                    instances[i].IsDisposed)
                {
                    OnFinished?.Invoke(this, new EventArgs());
                    instances.RemoveAt(i);
                    continue;
                }
            }
        }

        /// <summary>
        /// Pause the last instance currently playing
        /// </summary>
        public void Pause()
        {
            var instance = LastInstance();
			instance?.Pause();
		}

        /// <summary>
        /// Resume the last instance
        /// </summary>
        public void Resume()
        {
			var instance = LastInstance();
			instance?.Resume();
		}

        /// <summary>
        /// Stop the last instance currently playing
        /// </summary>
        public void Stop()
        {
			var instance = LastInstance();
            instances.Remove(instance);
		}

        /// <summary>
        /// Get the last created instance of this sound
        /// </summary>
        /// <returns></returns>
        private SoundEffectInstance LastInstance()
        {
            if (instances.Count == 0)
            {
                return null;
            }

            return instances[^1];
        }
    }
}
