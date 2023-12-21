using AlmostGoodEngine.Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace AlmostGoodEngine.Core.Utils.Console
{
    internal static class AlmostGoodConsole
    {
        /// <summary>
        /// The current command
        /// </summary>
        public static string Current { get; internal set; }

        /// <summary>
        /// The list of known commands
        /// </summary>
        public static Dictionary<string, Command> Commands { get; private set; }

        /// <summary>
        /// The commands history
        /// </summary>
        public static List<string> History { get; private set; }

        /// <summary>
        /// True if the console is displayed
        /// </summary>
        public static bool Opened { get; internal set; }

        /// <summary>
        /// If the keyboard used is using an azerty layout
        /// </summary>
        public static bool Azerty { get; internal set; } = true;

        #region Cursor parameters

        private static float _cursorTimer = 0f;
        private const float cursorBlinkDuration = 0.5f;
        private static bool _cursorBlink = false;
        private static int _cursorPosition = 0;

        #endregion

        #region History parameters

        private const int maxHistoryCapacity = 10_000;
        private static int _historyPosition = -1;
        private static string _tempCommand = "";

        #endregion

        public static void Initialize()
        {
            Commands = new();
            History = new();

            BuildCommands();
        }

        /// <summary>
        /// Create the default commands
        /// </summary>
        public static void BuildCommands()
        {

        }

        public static void Update(GameTime gameTime)
        {
            if (!Opened)
            {
                return;
            }

            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            UpdateCursor(delta);
            ManageToggle(delta);

            for (int i = 0; i < InputManager.Keyboard.CurrentState.GetPressedKeyCount(); i++)
            {
                var key = InputManager.Keyboard.CurrentState.GetPressedKeys()[i];
                
                if (InputManager.Keyboard.PreviousState[key] == KeyState.Up)
                {
                    HandleInput(key);
                }
            }
        }

        private static void UpdateCursor(float delta)
        {
            _cursorTimer += delta;
            while (_cursorTimer >= cursorBlinkDuration)
            {
                _cursorTimer -= cursorBlinkDuration;
                _cursorBlink = !_cursorBlink;
            }
        }

        private static void ManageToggle(float delta)
        {
            
        }

        private static void HandleInput(Keys key)
        {
            switch (key)
            {
                default:
                    if (key.ToString().Length == 1)
                    {
                        if (InputManager.Keyboard.CurrentState[Keys.LeftShift] == KeyState.Down || InputManager.Keyboard.CurrentState[Keys.RightShift] == KeyState.Down)
                        {
                            Current += key.ToString();
                        }
                        else
                        {
                            Current += key.ToString().ToLower();
                        }
                    }
                    break;
                case Keys.Space:
                    Current += ' ';
                    break;
                case Keys.Back:
                    if (InputManager.Keyboard.CurrentState[Keys.LeftControl] == KeyState.Down || InputManager.Keyboard.CurrentState[Keys.RightControl] == KeyState.Down)
                    {
                        Current = "";
                        _cursorPosition = 0;
                    }
                    else
                    {
                        if (Current.Length > 0)
                        {
                            Current = Current.Substring(0, Current.Length - 1);
                            _cursorPosition--;
                        }
                    }
                    break;
                case Keys.Delete:
                    Current = "";
                    break;
                case Keys.D0:
                    if (InputManager.Keyboard.CurrentState[Keys.LeftShift] == KeyState.Down || InputManager.Keyboard.CurrentState[Keys.RightShift] == KeyState.Down)
                    {
                        if (Azerty)
                        {
                            Current += '0';
                        }
                        else
                        {
                            Current += ')';
                        }
                    }
                    else
                    {
                        if (Azerty)
                        {
                            Current += 'à';
                        }
                        else
                        {
                            Current += '0';
                        }
                    }
                    break;
                case Keys.D1:
                    if (InputManager.Keyboard.CurrentState[Keys.LeftShift] == KeyState.Down || InputManager.Keyboard.CurrentState[Keys.RightShift] == KeyState.Down)
                    {
                        if (Azerty)
                        {
                            Current += '1';
                        }
                        else
                        {
                            Current += '!';
                        }
                    }
                    else
                    {
                        if (Azerty)
                        {
                            Current += '&';
                        }
                        else
                        {
                            Current += '1';
                        }
                    }
                    break;
                case Keys.D2:
                    if (InputManager.Keyboard.CurrentState[Keys.LeftShift] == KeyState.Down || InputManager.Keyboard.CurrentState[Keys.RightShift] == KeyState.Down)
                    {
                        if (Azerty)
                        {
                            Current += '2';
                        }
                        else
                        {
                            Current += '@';
                        }
                    }
                    else
                    {
                        if (Azerty)
                        {
                            Current += 'é';
                        }
                        else
                        {
                            Current += '2';
                        }
                    }
                    break;
                case Keys.D3:
                    if (InputManager.Keyboard.CurrentState[Keys.LeftShift] == KeyState.Down || InputManager.Keyboard.CurrentState[Keys.RightShift] == KeyState.Down)
                    {
                        if (Azerty)
                        {
                            Current += '3';
                        }
                        else
                        {
                            Current += '#';
                        }
                    }
                    else
                    {
                        if (Azerty)
                        {
                            Current += '"';
                        }
                        else
                        {
                            Current += '3';
                        }
                    }
                    break;
                case Keys.D4:
                    if (InputManager.Keyboard.CurrentState[Keys.LeftShift] == KeyState.Down || InputManager.Keyboard.CurrentState[Keys.RightShift] == KeyState.Down)
                    {
                        if (Azerty)
                        {
                            Current += '4';
                        }
                        else
                        {
                            Current += '$';
                        }
                    }
                    else
                    {
                        if (Azerty)
                        {
                            Current += '\'';
                        }
                        else
                        {
                            Current += '4';
                        }
                    }
                    break;
                case Keys.D5:
                    if (InputManager.Keyboard.CurrentState[Keys.LeftShift] == KeyState.Down || InputManager.Keyboard.CurrentState[Keys.RightShift] == KeyState.Down)
                    {
                        if (Azerty)
                        {
                            Current += '5';
                        }
                        else
                        {
                            Current += '%';
                        }
                    }
                    else
                    {
                        if (Azerty)
                        {
                            Current += '(';
                        }
                        else
                        {
                            Current += '5';
                        }
                    }
                    break;
                case Keys.D6:
                    if (InputManager.Keyboard.CurrentState[Keys.LeftShift] == KeyState.Down || InputManager.Keyboard.CurrentState[Keys.RightShift] == KeyState.Down)
                    {
                        if (Azerty)
                        {
                            Current += '6';
                        }
                        else
                        {
                            Current += '^';
                        }
                    }
                    else
                    {
                        if (Azerty)
                        {
                            Current += '-';
                        }
                        else
                        {
                            Current += '6';
                        }
                    }
                    break;
                case Keys.D7:
                    if (InputManager.Keyboard.CurrentState[Keys.LeftShift] == KeyState.Down || InputManager.Keyboard.CurrentState[Keys.RightShift] == KeyState.Down)
                    {
                        if (Azerty)
                        {
                            Current += '7';
                        }
                        else
                        {
                            Current += '&';
                        }
                    }
                    else
                    {
                        if (Azerty)
                        {
                            Current += 'è';
                        }
                        else
                        {
                            Current += '7';
                        }
                    }
                    break;
                case Keys.D8:
                    if (InputManager.Keyboard.CurrentState[Keys.LeftShift] == KeyState.Down || InputManager.Keyboard.CurrentState[Keys.RightShift] == KeyState.Down)
                    {
                        if (Azerty)
                        {
                            Current += '8';
                        }
                        else
                        {
                            Current += '*';
                        }
                    }
                    else
                    {
                        if (Azerty)
                        {
                            Current += '_';
                        }
                        else
                        {
                            Current += '8';
                        }
                    }
                    break;
                case Keys.D9:
                    if (InputManager.Keyboard.CurrentState[Keys.LeftShift] == KeyState.Down || InputManager.Keyboard.CurrentState[Keys.RightShift] == KeyState.Down)
                    {
                        if (Azerty)
                        {
                            Current += '9';
                        }
                        else
                        {
                            Current += '(';
                        }
                    }
                    else
                    {
                        if (Azerty)
                        {
                            Current += 'ç';
                        }
                        else
                        {
                            Current += '9';
                        }
                    }
                    break;
                case Keys.OemComma:
                    if (InputManager.Keyboard.CurrentState[Keys.LeftShift] == KeyState.Down || InputManager.Keyboard.CurrentState[Keys.RightShift] == KeyState.Down)
                    {
                        if (Azerty)
                        {
                            Current += '?';
                        }
                        else
                        {
                            Current += '<';
                        }
                    }
                    else
                    {
                        if (Azerty)
                        {
                            Current += ',';
                        }
                        else
                        {
                            Current += ',';
                        }
                    }
                    break;
                case Keys.OemPeriod:
                    if (InputManager.Keyboard.CurrentState[Keys.LeftShift] == KeyState.Down || InputManager.Keyboard.CurrentState[Keys.RightShift] == KeyState.Down)
                    {
                        if (Azerty)
                        {
                            Current += '.';
                        }
                        else
                        {
                            Current += '>';
                        }

                    }
                    else
                    {
                        if (Azerty)
                        {
                            Current += ';';
                        }
                        else
                        {
                            Current += '.';
                        }
                    }
                    break;
                case Keys.OemQuestion:
                    if (InputManager.Keyboard.CurrentState[Keys.LeftShift] == KeyState.Down || InputManager.Keyboard.CurrentState[Keys.RightShift] == KeyState.Down)
                    {
                        if (Azerty)
                        {
                            Current += '/';
                        }
                        else
                        {
                            Current += '?';
                        }
                    }
                    else
                    {
                        if (Azerty)
                        {
                            Current += ':';
                        }
                        else
                        {
                            Current += '/';
                        }
                    }
                    break;
                case Keys.OemSemicolon:
                    if (InputManager.Keyboard.CurrentState[Keys.LeftShift] == KeyState.Down || InputManager.Keyboard.CurrentState[Keys.RightShift] == KeyState.Down)
                    {
                        if (Azerty)
                        {

                        }
                        else
                        {
                            Current += ':';
                        }
                    }
                    else
                    {
                        if (Azerty)
                        {

                        }
                        else
                        {
                            Current += ';';
                        }
                    }
                    break;
                case Keys.OemQuotes:
                    if (InputManager.Keyboard.CurrentState[Keys.LeftShift] == KeyState.Down || InputManager.Keyboard.CurrentState[Keys.RightShift] == KeyState.Down)
                    {
                        if (Azerty)
                        {

                        }
                        else
                        {
                            Current += '"';
                        }
                    }
                    else
                    {
                        if (Azerty)
                        {

                        }
                        else
                        {
                            Current += '\'';
                        }
                    }
                    break;
                case Keys.OemBackslash:
                    if (InputManager.Keyboard.CurrentState[Keys.LeftShift] == KeyState.Down || InputManager.Keyboard.CurrentState[Keys.RightShift] == KeyState.Down)
                    {
                        if (Azerty)
                        {

                        }
                        else
                        {
                            Current += '|';
                        }
                    }
                    else
                    {
                        if (Azerty)
                        {

                        }
                        else
                        {
                            Current += '\\';
                        }
                    }
                    break;
                case Keys.OemOpenBrackets:
                    if (InputManager.Keyboard.CurrentState[Keys.LeftShift] == KeyState.Down || InputManager.Keyboard.CurrentState[Keys.RightShift] == KeyState.Down)
                    {
                        if (Azerty)
                        {

                        }
                        else
                        {
                            Current += '{';
                        }
                    }
                    else
                    {
                        if (Azerty)
                        {

                        }
                        else
                        {
                            Current += '[';
                        }
                    }
                    break;
                case Keys.OemCloseBrackets:
                    if (InputManager.Keyboard.CurrentState[Keys.LeftShift] == KeyState.Down || InputManager.Keyboard.CurrentState[Keys.RightShift] == KeyState.Down)
                    {
                        if (Azerty)
                        {

                        }
                        else
                        {
                            Current += '}';
                        }
                    }
                    else
                    {
                        if (Azerty)
                        {

                        }
                        else
                        {
                            Current += ']';
                        }
                    }
                    break;
                case Keys.OemMinus:
                    if (InputManager.Keyboard.CurrentState[Keys.LeftShift] == KeyState.Down || InputManager.Keyboard.CurrentState[Keys.RightShift] == KeyState.Down)
                    {
                        if (Azerty)
                        {

                        }
                        else
                        {
                            Current += '_';
                        }
                    }
                    else
                    {
                        if (Azerty)
                        {

                        }
                        else
                        {
                            Current += '-';
                        }
                    }
                    break;
                case Keys.OemPlus:
                    if (InputManager.Keyboard.CurrentState[Keys.LeftShift] == KeyState.Down || InputManager.Keyboard.CurrentState[Keys.RightShift] == KeyState.Down)
                    {
                        if (Azerty)
                        {

                        }
                        else
                        {
                            Current += '+';
                        }
                    }
                    else
                    {
                        if (Azerty)
                        {

                        }
                        else
                        {
                            Current += '=';
                        }
                    }
                    break;
                case Keys.Enter:
                    EnterCommand();
                    break;
            }

            // We wrote a character
            if (KeyboardManager.IsWritable(key))
            {
                if (_cursorPosition < Current.Length)
                {
                    _cursorPosition++;
                }
            }
            else
            {
                switch (key)
                {
                    case Keys.Left:
                        if (_cursorPosition > 0) _cursorPosition--;
                        break;
                    case Keys.Right:
                        if (_cursorPosition < Current.Length)
                        {
                            _cursorPosition++;
                        }
                        break;
                    case Keys.Up:
                        if (History.Count > 0)
                        {
                            if (_historyPosition < 0)
                            {
                                _historyPosition = History.Count - 1;
                                _tempCommand = Current;
                                _cursorPosition = Current.Length;
                            }
                            else if (_historyPosition > 0)
                            {
                                _historyPosition--;
                            }

                            if (_historyPosition >= 0)
                            {
                                Current = History[_historyPosition];
                                _cursorPosition = Current.Length;
                            }

                        }
                        break;
                    case Keys.Down:
                        if (History.Count > 0 && _historyPosition != -1)
                        {
                            if (_historyPosition >= 0)
                            {
                                _historyPosition++;
                            }

                            if (_historyPosition >= History.Count)
                            {
                                _historyPosition = -1;
                                Current = _tempCommand;
                                _tempCommand = "";
                                _cursorPosition = Current.Length;
                            }
                            else if (_historyPosition >= 0)
                            {
                                Current = History[_historyPosition];
                                _cursorPosition = Current.Length;
                            }
                            else
                            {
                                Current = _tempCommand;
                                _tempCommand = "";
                                _cursorPosition = Current.Length;
                            }
                        }
                        break;
                }
            }
        }

        public static void EnterCommand()
        {

        }
    }
}
