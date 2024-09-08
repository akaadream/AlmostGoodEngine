using AlmostGoodEngine.Audio;
using AlmostGoodEngine.Core.Scenes;
using AlmostGoodEngine.Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace AlmostGoodEngine.Tests.Scenes
{
    public class AudioScene : Scene
    {
        Sound sound;

        public AudioScene()
        {
            Mixer.Channels["master"].Volume = 0.2f;
        }

        public override void LoadContent(ContentManager content)
        {
            sound = new(content.Load<SoundEffect>("Sounds/test_sound"));

            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            if (Input.Keyboard.IsPressed(Microsoft.Xna.Framework.Input.Keys.P))
            {
                sound.PlayAt(new Vector3(Renderer.Cameras[0].Width / 2, Renderer.Cameras[0].Height / 2, 1f), new Vector3(WorldMousePosition(), 1f), 0.5f);
            }

            base.Update(gameTime);
        }
    }
}
