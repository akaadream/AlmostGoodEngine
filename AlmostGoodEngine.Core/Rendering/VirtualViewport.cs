namespace AlmostGoodEngine.Core.Rendering
{
    public struct VirtualViewport
    {
        /// <summary>
        /// X coordinate of the viewport
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Y coordinate of the viewport
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Width of the viewport
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Height of the viewport
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Left bounds of the viewport
        /// </summary>
        public int Left { get => X; }

        /// <summary>
        /// Top bounds of the viewport
        /// </summary>
        public int Top { get => Y; }

        /// <summary>
        /// Right bounds of the viewport
        /// </summary>
        public int Right { get => X + Width; }

        /// <summary>
        /// Bottom bounds of the viewport
        /// </summary>
        public int Bottom { get => Y + Height; }

        public VirtualViewport(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public VirtualViewport(int width, int height): this(0, 0, width, height)
        {

        }
    }
}
