using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;

namespace AlmostGoodEngine.Audio
{
    public class Sound
    {
        /// <summary>
        /// The sound effect ressource
        /// </summary>
        public SoundEffect Effect { get; private set; }

        /// <summary>
        /// The maximum number of occurences of this sounds at the same time
        /// </summary>
        public int MaxInstance { get; set; } = 1;

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
        List<SoundEffectInstance> instances;


        public Sound(SoundEffect soundEffect)
        {
            Effect = soundEffect;
            instances = [];
        }

        public void Dispose()
        {
            Effect?.Dispose();
            Effect = null;
        }

        public void Play(float volume = 1f, bool loop = false)
        {
            var instance = Effect.CreateInstance();
            instance.Volume = volume;
            instance.IsLooped = loop;
            instance.Play();
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

            return instances[instances.Count - 1];
        }
    }
}
