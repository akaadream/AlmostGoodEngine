using Microsoft.Xna.Framework.Audio;
using System;

namespace AlmostGoodEngine.Audio
{
    public class Music : ISound
    {
        /// <summary>
        /// The base sound effect used to create instance
        /// </summary>
        public SoundEffect Sound { get; set; }

        /// <summary>
        /// The current sound effect instance
        /// </summary>
        public SoundEffectInstance SoundInstance { get; set; }

        /// <summary>
        /// The channel on which this music should play
        /// </summary>
        public string Channel { get; set; }

        /// <summary>
        /// Return true if the current instance is a loop
        /// </summary>
        public bool IsLooping { get => SoundInstance != null && SoundInstance.IsLooped; }

        /// <summary>
        /// Event called when the music is stopped
        /// </summary>
        public event EventHandler<EventArgs> OnStopped;

        /// <summary>
        /// Event called when the volume of the music changed
        /// </summary>
        public event EventHandler<EventArgs> OnVolumeChanged;
        
        /// <summary>
        /// The volume of the music
        /// </summary>
        public float Volume
        {
            get => _volume;
            set
            {
                _volume = float.Clamp(value, 0f, 1f);
                if (!_doingTransition)
                {
                    _nextVolume = _volume;
                }

                UpdateVolume();
            }
        }
        private float _volume = 1f;

        /// <summary>
        /// The target volume of the instance
        /// </summary>
        public float NextVolume
        {
            get => _nextVolume;
            set
            {
                _nextVolume = float.Clamp(value, 0f, 1f);
            }
        }
        private float _nextVolume = 1f;
        private float _previousVolume = 1f;

        /// <summary>
        /// The transition time to go from a specific volume to another one (in seconds)
        /// </summary>
        public float Transition { get; set; } = 0.3f;
        private bool _doingTransition = false;
        private float _transitionTimer = 0f;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="soundEffect"></param>
        public Music(SoundEffect soundEffect)
        {
            Sound = soundEffect;
            SoundInstance = Sound.CreateInstance();
            Channel = "master";
        }

        /// <summary>
        /// Start the music
        /// </summary>
        /// <param name="looping"></param>
        public void Play(bool looping = true)
        {
            if (SoundInstance == null)
            {
                return;
            }
            SoundInstance.IsLooped = looping;
            UpdateVolume();
            SoundInstance.Play();
        }

        /// <summary>
        /// Pause the music
        /// </summary>
        public void Pause()
        {
            SoundInstance?.Pause();
        }

        /// <summary>
        /// Resume the music
        /// </summary>
        public void Resume()
        {
            SoundInstance?.Resume();
        }

        /// <summary>
        /// Stop the music
        /// </summary>
        public void Stop()
        {
            SoundInstance?.Stop();
        }

        /// <summary>
        /// Update the music volume if needed and call events
        /// </summary>
        /// <param name="delta"></param>
        public void Update(float delta)
        {
            if (Volume != NextVolume && !_doingTransition)
            {
                _doingTransition = true;
                _transitionTimer = 0f;
                _previousVolume = Volume;
            }

            if (_doingTransition)
            {
                if (_transitionTimer >= Transition)
                {
                    _doingTransition = false;
                    Volume = NextVolume;
                    OnVolumeChanged?.Invoke(this, new EventArgs());
                }
                else
                {
					_transitionTimer += delta;
                    Volume = float.Lerp(_previousVolume, _nextVolume, _transitionTimer / Transition);
				}
            }

            if (SoundInstance != null)
            {
                if (SoundInstance.State == SoundState.Stopped)
                {
                    OnStopped?.Invoke(this, new EventArgs());
                }
            }
        }

        private void UpdateVolume()
        {
            if (SoundInstance == null)
            {
                return;
            }

			if (Mixer.Channels.TryGetValue(Channel, out Channel channel))
			{
				SoundInstance.Volume = Volume * channel.Volume;
			}
			else
			{
				SoundInstance.Volume = Volume;
			}
		}
    }
}
