using Microsoft.Xna.Framework.Audio;

namespace AlmostGoodEngine.Audio
{
    public class Music
    {
        public SoundEffect Sound { get; set; }
        public SoundEffectInstance SoundInstance { get; set; }

        public bool IsLooping { get => SoundInstance != null && SoundInstance.IsLooped; }

        public Music(SoundEffect soundEffect)
        {
            Sound = soundEffect;
            SoundInstance = Sound.CreateInstance();
        }

        public void Play(bool looping = true)
        {
            if (SoundInstance == null)
            {
                return;
            }
            SoundInstance.IsLooped = looping;
            SoundInstance.Play();
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
