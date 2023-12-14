using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlmostGoodEngine.Animations
{
    public class SpriteSheet
    {
        /// <summary>
        /// The texture used by the spritesheet animator
        /// </summary>
        public Texture2D Texture { get; set; }

        /// <summary>
        /// The origin of the animation inside the spritesheet
        /// </summary>
        public Vector2 Origin { get; set; }

        /// <summary>
        /// Width of a frame
        /// </summary>
        public int FrameWidth { get; set; }
        
        /// <summary>
        /// Height of a frame
        /// </summary>
        public int FrameHeight { get; set; }

        /// <summary>
        /// Number of frames inside the animation
        /// </summary>

        public int Frames { get; set; }

        /// <summary>
        /// Duration of a frame (in seconds)
        /// </summary>

        public float FrameDuration { get; set; }

        /// <summary>
        /// The current frame index
        /// </summary>
        public int CurrentFrame { get; set; }

        /// <summary>
        /// The number of frames per row
        /// </summary>
        public int FramesPerRow { get => Texture.Width / FrameWidth; }

        /// <summary>
        /// If true, the animation will loop
        /// </summary>
        public bool Infinite { get; set; }

        /// <summary>
        /// If true, the animation will be paused
        /// </summary>
        public bool Paused { get; set; }

        public float Rotation { get; set; }

        public float Scale { get; set; }

        public Vector2 SpriteOrigin { get; set; }

        /// <summary>
        /// The current source rectangle inside the texture
        /// </summary>
        public Rectangle Source
        {
            get
            {
                return new(
                    (int)Origin.X + (CurrentFrame % FramesPerRow) * FrameWidth, 
                    (int)Origin.Y + (CurrentFrame / FramesPerRow) * FrameHeight,
                    FrameWidth,
                    FrameHeight);
            }
        }

        /// <summary>
        /// If the texture should be pixelated
        /// </summary>
        public bool Pixelated { get; set; }

        // Timer of the animation
        private float _animationTimer;

        public SpriteSheet(int frameWidth, int frameHeight, int frames, Texture2D texture)
        {
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;

            Origin = Vector2.Zero;
            Rotation = 0f;
            Scale = 1f;

            SpriteOrigin = new(frameWidth / 2, frameHeight / 2);

            Frames = frames;
            FrameDuration = 0.12f;

            Texture = texture;

            Paused = true;
        }

        /// <summary>
        /// Start the animation
        /// </summary>
        public void Start()
        {
            Reset();
            Paused = false;
        }

        /// <summary>
        /// Reset the animation
        /// </summary>
        public void Reset()
        {
            CurrentFrame = 0;
            _animationTimer = 0f;
        }

        /// <summary>
        /// Update the animation
        /// </summary>
        /// <param name="deltaTime"></param>
        public void Update(float deltaTime)
        {
            if (Paused)
            {
                return;
            }

            _animationTimer += deltaTime;
            if (_animationTimer >= FrameDuration)
            {
                _animationTimer -= FrameDuration;

                if (CurrentFrame + 1 >= Frames)
                {
                    if (Infinite)
                    {
                        CurrentFrame = 0;
                    }
                }
                else
                {
                    CurrentFrame++;
                }
            }
        }

        /// <summary>
        /// Get the current frame texture
        /// </summary>
        /// <returns></returns>
        public Texture2D GetCurrentFrame()
        {
            if (Texture == null)
            {
                return null;
            }

            Texture2D sub = new(Texture.GraphicsDevice, Texture.Width, Texture.Height);
            Color[] data = new Color[Source.Width * Source.Height];
            Texture.GetData(0, Source, data, 0, data.Length);
            sub.SetData(data);

            return sub;
        }

        /// <summary>
        /// Draw the animation
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="position"></param>
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(Texture, position, Source, Color.White, Rotation, SpriteOrigin, Scale, SpriteEffects.None, 1f);
        }
    }
}
