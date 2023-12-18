using Microsoft.Xna.Framework.Audio;

namespace AlmostGoodEngine.Audio
{
    public class Sound
    {
        /// <summary>
        /// The sound effect ressource
        /// </summary>
        internal SoundEffect Effect;

        public SoundEffectInstance SoundInstance;

        public Sound(SoundEffect soundEffect)
        {
            Effect = soundEffect;
            SoundInstance = Effect.CreateInstance();
        }

        public void Dispose()
        {
            SoundInstance?.Dispose();
            SoundInstance = null;

            Effect?.Dispose();
            Effect = null;
        }

        public void Play()
        {
            SoundInstance?.Play();
        }

        public void Pause()
        {
            SoundInstance?.Pause();
        }

        public void Resume()
        {
            SoundInstance?.Resume();
        }

        public void Stop()
        {
            SoundInstance?.Stop();
        }
    }
}
