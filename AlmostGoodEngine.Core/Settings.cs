using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace AlmostGoodEngine.Core
{
    public class Settings
    {
        #region Application

        /// <summary>
        /// The name of the application
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Translated application's name
        /// </summary>
        public Dictionary<string, string> NameTranslations { get; set; }

        /// <summary>
        /// The description of the application
        /// </summary>
        public string Description { get; set; }

        #endregion

        #region Window

        /// <summary>
        /// Starting position of the window
        /// </summary>
        public Point Position { get; set; }

        /// <summary>
        /// If the window may be centered on the user's screen at launch
        /// </summary>
        public bool CenteredAtLaunch { get; set; }

        /// <summary>
        /// Width of the window
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Height of the window
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Color used to clear the screen each frames
        /// </summary>
        public Color ClearColor { get; set; }

        /// <summary>
        /// If the window can be resized
        /// </summary>
        public bool Resizable { get; set; }

        /// <summary>
        /// Display the game with a borderless window
        /// </summary>
        public bool Borderless { get; set; }

        #endregion

        #region Assets

        public bool OriginCentered { get; set; }

        #endregion

        public Settings()
        {
            Name = "Almost Good Project";
            NameTranslations = new Dictionary<string, string>();
            Description = "The description of the project";

            Position = new Point(0, 0);
            CenteredAtLaunch = true;

            Width = 1600;
            Height = 900;

            ClearColor = Color.BurlyWood;

            Resizable = true;
            Borderless = false;

            OriginCentered = true;
        }
    }
}
