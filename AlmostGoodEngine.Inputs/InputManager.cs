using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace AlmostGoodEngine.Inputs
{
    public class InputManager
    {
        /// <summary>
        /// Keyboard manager
        /// </summary>
        public static KeyboardManager Keyboard { get; private set; }

        /// <summary>
        /// Mouse manager
        /// </summary>
        public static MouseManager Mouse { get; private set; }

        /// <summary>
        /// Touch panel manager
        /// </summary>
        public static TouchPanelManager TouchPanel { get; private set; }

        /// <summary>
        /// Gamepads managers
        /// </summary>
        public static List<GamePadManager> GamePads { get; private set; }

        /// <summary>
        /// Gamepads capabilities
        /// </summary>
        public static List<GamePadCapabilities> GamePadCapabilities { get; private set; }

        /// <summary>
        /// Dictionary of inputs binds
        /// </summary>
        public static Dictionary<string, InputBinds> Binds { get; private set; }

        /// <summary>
        /// Initialize input manager components.
        /// Call this initialize function in you game's Initialize.
        /// </summary>
        public static void Initialize()
        {
            Keyboard = new KeyboardManager();
            Mouse = new MouseManager();
            TouchPanel = new TouchPanelManager();
            GamePads = new List<GamePadManager>();
            GamePadCapabilities = new List<GamePadCapabilities>();

            Binds = new Dictionary<string, InputBinds>();

            // All possible connected gamepads
            for (int i = 0; i < GamePad.MaximumGamePadCount; i++)
            {
                GamePads.Add(new GamePadManager(i));
                GamePadCapabilities.Add(GamePad.GetCapabilities(i));
            }
        }

        /// <summary>
        /// Update all the managers
        /// </summary>
        public static void Update()
        {
            Keyboard.Update();
            Mouse.Update();
            TouchPanel.Update();
            
            foreach (GamePadManager gamePad in GamePads)
            {
                gamePad.Update();
            }
        }

        #region Key binds checking

        public static void AddBind(InputBinds bind)
        {
            if (Binds.ContainsKey(bind.Name))
            {
                return;
            }

            Binds.Add(bind.Name, bind);
        }

        public static bool IsPressed(string bind)
        {
            if (!Binds.ContainsKey(bind))
            {
                return false;
            }

            return Binds[bind].Pressed();
        }

        public static bool IsReleased(string bind)
        {
            if (!Binds.ContainsKey(bind))
            {
                return false;
            }

            return Binds[bind].Released();
        }

        public static bool IsDown(string bind)
        {
            if (!Binds.ContainsKey(bind))
            {
                return false;
            }

            return Binds[bind].Down();
        }

        #endregion
    }
}
