using AlmostGoodEngine.Inputs;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AlmostGoodEngine.Core.Utils.Consoles
{
    public static class AlmostGoodConsole
    {
        /// <summary>
        /// The current command
        /// </summary>
        public static string Current { get; internal set; }

        /// <summary>
        /// The prexif of the command displayed firstly inside the console
        /// </summary>
        public static string Prefix { get; set; }

        /// <summary>
        /// The command line displayed on the screen using the prefix
        /// </summary>
        internal static string DisplayCommand { get => Prefix + Current; }

        /// <summary>
        /// The list of known commands
        /// </summary>
        internal static Dictionary<string, Command> Commands { get; set; }

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

        #region Display parameters

        private const int consolePadding = 10;
        private const float consoleOpacity = 0.65f;
        private static SpriteFontBase _font;

        private static Vector2 _prefixSize;
        private static Vector2 _commandSize;
        private static int _textHeight;

        #endregion

        /// <summary>
        /// Initialize the console
        /// </summary>
        internal static void Initialize()
        {
            Commands = [];
            History = [];

            Current = "";
            Prefix = "> ";

            _font = GameManager.FontSystem.GetFont(18);
            _prefixSize = _font.MeasureString(Prefix);
            _commandSize = _font.MeasureString(Current);
            _textHeight = (int)_font.MeasureString("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789|çà&><!:;,?./§*$£µ^ù€²()=").Y;

            BuildCommands();
        }

        /// <summary>
        /// Create the default commands
        /// </summary>
        private static void BuildCommands()
        {
            MethodInfo[] methods = typeof(AlmostGoodConsole).GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var method in methods)
            {
                CommandAttribute attribute = (CommandAttribute)method.GetCustomAttributes(typeof(CommandAttribute), false).FirstOrDefault();
                if (attribute != null)
                {
                    Command command = new()
                    {
						Action = (args) =>
                        {
                            method.Invoke(method, args);
                        },
                        Usage = attribute.Name,
                        Description = attribute.Description,
					};
                    Commands.Add(command.Usage, command);
                }
            }
        }

        /// <summary>
        /// Update the console
        /// </summary>
        /// <param name="gameTime"></param>
        internal static void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            ManageToggle(delta);

            if (!Opened)
            {
                return;
            }

            UpdateCursor(delta);

            string previous = Current;
            for (int i = 0; i < Input.Keyboard.CurrentState.GetPressedKeyCount(); i++)
            {
                var key = Input.Keyboard.CurrentState.GetPressedKeys()[i];
                
                if (Input.Keyboard.PreviousState[key] == KeyState.Up)
                {
                    HandleInput(key);
                }
            }

            // If the command has changed, measure the new size of the command text
            if (previous != Current)
            {
                _commandSize = _font.MeasureString(Current);
            }
        }

        /// <summary>
        /// Cursor blink animation
        /// </summary>
        /// <param name="delta"></param>
        private static void UpdateCursor(float delta)
        {
            _cursorTimer += delta;
            while (_cursorTimer >= cursorBlinkDuration)
            {
                _cursorTimer -= cursorBlinkDuration;
                _cursorBlink = !_cursorBlink;
            }
        }

        /// <summary>
        /// Manager the console's toggle
        /// </summary>
        /// <param name="delta"></param>
        private static void ManageToggle(float delta)
        {
            // The key used to open the console
            if (Input.Keyboard.IsPressed(Keys.F1))
            {
                Opened = !Opened;
            }
        }

        /// <summary>
        /// Check inputs
        /// </summary>
        /// <param name="key"></param>
        private static void HandleInput(Keys key)
        {
            switch (key)
            {
                default:
                    if (key.ToString().Length == 1)
                    {
                        if (Input.Keyboard.CurrentState[Keys.LeftShift] == KeyState.Down || Input.Keyboard.CurrentState[Keys.RightShift] == KeyState.Down)
                        {
                            AddToCurrent(key.ToString());
                        }
                        else
                        {
                            AddToCurrent(key.ToString().ToLower());
                        }
                    }
                    break;
                case Keys.Space:
                    AddToCurrent(' ');
                    break;
                case Keys.Back:
                    if (Input.Keyboard.CurrentState[Keys.LeftControl] == KeyState.Down || Input.Keyboard.CurrentState[Keys.RightControl] == KeyState.Down)
                    {
                        Current = "";
                        _cursorPosition = 0;
                    }
                    else
                    {
                        if (Current.Length > 0 && _cursorPosition > 0)
                        {
                            Current = Current.Remove(_cursorPosition - 1, 1);
                            _cursorPosition--;
                            if (_cursorPosition < 0)
                            {
                                _cursorPosition = 0;
                            }
                            if (_cursorPosition >= Current.Length)
                            {
                                _cursorPosition = Current.Length - 1;
                            }
                        }
                    }
                    break;
                case Keys.Delete:
                    Current = "";
                    break;
                case Keys.D0:
                    if (Input.Keyboard.CurrentState[Keys.LeftShift] == KeyState.Down || Input.Keyboard.CurrentState[Keys.RightShift] == KeyState.Down)
                    {
                        if (Azerty)
                        {
                            AddToCurrent("0");
                        }
                        else
                        {
                            AddToCurrent(")");
                        }
                    }
                    else
                    {
                        if (Azerty)
                        {
                            AddToCurrent('à');
                        }
                        else
                        {
                            AddToCurrent('0');
                        }
                    }
                    break;
                case Keys.D1:
                    if (Input.Keyboard.CurrentState[Keys.LeftShift] == KeyState.Down || Input.Keyboard.CurrentState[Keys.RightShift] == KeyState.Down)
                    {
                        if (Azerty)
                        {
                            AddToCurrent('1');
                        }
                        else
                        {
                            AddToCurrent('!');
                        }
                    }
                    else
                    {
                        if (Azerty)
                        {
                            AddToCurrent('&');
                        }
                        else
                        {
                            AddToCurrent('1');
                        }
                    }
                    break;
                case Keys.D2:
                    if (Input.Keyboard.CurrentState[Keys.LeftShift] == KeyState.Down || Input.Keyboard.CurrentState[Keys.RightShift] == KeyState.Down)
                    {
                        if (Azerty)
                        {
                            AddToCurrent('2');
                        }
                        else
                        {
                            AddToCurrent('@');
                        }
                    }
                    else
                    {
                        if (Azerty)
                        {
                            AddToCurrent('é');
                        }
                        else
                        {
                            AddToCurrent('2');
                        }
                    }
                    break;
                case Keys.D3:
                    if (Input.Keyboard.CurrentState[Keys.LeftShift] == KeyState.Down || Input.Keyboard.CurrentState[Keys.RightShift] == KeyState.Down)
                    {
                        if (Azerty)
                        {
                            AddToCurrent('3');
                        }
                        else
                        {
                            AddToCurrent('#');
                        }
                    }
                    else
                    {
                        if (Azerty)
                        {
                            AddToCurrent('"');
                        }
                        else
                        {
                            AddToCurrent('3');
                        }
                    }
                    break;
                case Keys.D4:
                    if (Input.Keyboard.CurrentState[Keys.LeftShift] == KeyState.Down || Input.Keyboard.CurrentState[Keys.RightShift] == KeyState.Down)
                    {
                        if (Azerty)
                        {
                            AddToCurrent('4');
                        }
                        else
                        {
                            AddToCurrent('$');
                        }
                    }
                    else
                    {
                        if (Azerty)
                        {
                            AddToCurrent('\'');
                        }
                        else
                        {
                            AddToCurrent('4');
                        }
                    }
                    break;
                case Keys.D5:
                    if (Input.Keyboard.CurrentState[Keys.LeftShift] == KeyState.Down || Input.Keyboard.CurrentState[Keys.RightShift] == KeyState.Down)
                    {
                        if (Azerty)
                        {
                            AddToCurrent('5');
                        }
                        else
                        {
                            AddToCurrent('%');
                        }
                    }
                    else
                    {
                        if (Azerty)
                        {
                            AddToCurrent('(');
                        }
                        else
                        {
                            AddToCurrent('5');
                        }
                    }
                    break;
                case Keys.D6:
                    if (Input.Keyboard.CurrentState[Keys.LeftShift] == KeyState.Down || Input.Keyboard.CurrentState[Keys.RightShift] == KeyState.Down)
                    {
                        if (Azerty)
                        {
                            AddToCurrent('6');
                        }
                        else
                        {
                            AddToCurrent('^');
                        }
                    }
                    else
                    {
                        if (Azerty)
                        {
                            AddToCurrent('-');
                        }
                        else
                        {
                            AddToCurrent('6');
                        }
                    }
                    break;
                case Keys.D7:
                    if (Input.Keyboard.CurrentState[Keys.LeftShift] == KeyState.Down || Input.Keyboard.CurrentState[Keys.RightShift] == KeyState.Down)
                    {
                        if (Azerty)
                        {
                            AddToCurrent('7');
                        }
                        else
                        {
                            AddToCurrent('&');
                        }
                    }
                    else
                    {
                        if (Azerty)
                        {
                            AddToCurrent('è');
                        }
                        else
                        {
                            AddToCurrent('7');
                        }
                    }
                    break;
                case Keys.D8:
                    if (Input.Keyboard.CurrentState[Keys.LeftShift] == KeyState.Down || Input.Keyboard.CurrentState[Keys.RightShift] == KeyState.Down)
                    {
                        if (Azerty)
                        {
                            AddToCurrent('8');
                        }
                        else
                        {
                            AddToCurrent('*');
                        }
                    }
                    else
                    {
                        if (Azerty)
                        {
                            AddToCurrent('_');
                        }
                        else
                        {
                            AddToCurrent('8');
                        }
                    }
                    break;
                case Keys.D9:
                    if (Input.Keyboard.CurrentState[Keys.LeftShift] == KeyState.Down || Input.Keyboard.CurrentState[Keys.RightShift] == KeyState.Down)
                    {
                        if (Azerty)
                        {
                            AddToCurrent('9');
                        }
                        else
                        {
                            AddToCurrent('(');
                        }
                    }
                    else
                    {
                        if (Azerty)
                        {
                            AddToCurrent('ç');
                        }
                        else
                        {
                            AddToCurrent('9');
                        }
                    }
                    break;
                case Keys.OemComma:
                    if (Input.Keyboard.CurrentState[Keys.LeftShift] == KeyState.Down || Input.Keyboard.CurrentState[Keys.RightShift] == KeyState.Down)
                    {
                        if (Azerty)
                        {
                            AddToCurrent('?');
                        }
                        else
                        {
                            AddToCurrent('<');
                        }
                    }
                    else
                    {
                        if (Azerty)
                        {
                            AddToCurrent(',');
                        }
                        else
                        {
                            AddToCurrent(',');
                        }
                    }
                    break;
                case Keys.OemPeriod:
                    if (Input.Keyboard.CurrentState[Keys.LeftShift] == KeyState.Down || Input.Keyboard.CurrentState[Keys.RightShift] == KeyState.Down)
                    {
                        if (Azerty)
                        {
                            AddToCurrent('.');
                        }
                        else
                        {
                            AddToCurrent('>');
                        }

                    }
                    else
                    {
                        if (Azerty)
                        {
                            AddToCurrent(';');
                        }
                        else
                        {
                            AddToCurrent('.');
                        }
                    }
                    break;
                case Keys.OemQuestion:
                    if (Input.Keyboard.CurrentState[Keys.LeftShift] == KeyState.Down || Input.Keyboard.CurrentState[Keys.RightShift] == KeyState.Down)
                    {
                        if (Azerty)
                        {
                            AddToCurrent('/');
                        }
                        else
                        {
                            AddToCurrent('?');
                        }
                    }
                    else
                    {
                        if (Azerty)
                        {
                            AddToCurrent(':');
                        }
                        else
                        {
                            AddToCurrent('/');
                        }
                    }
                    break;
                case Keys.OemSemicolon:
                    if (Input.Keyboard.CurrentState[Keys.LeftShift] == KeyState.Down || Input.Keyboard.CurrentState[Keys.RightShift] == KeyState.Down)
                    {
                        if (Azerty)
                        {

                        }
                        else
                        {
                            AddToCurrent(':');
                        }
                    }
                    else
                    {
                        if (Azerty)
                        {

                        }
                        else
                        {
                            AddToCurrent(';');
                        }
                    }
                    break;
                case Keys.OemQuotes:
                    if (Input.Keyboard.CurrentState[Keys.LeftShift] == KeyState.Down || Input.Keyboard.CurrentState[Keys.RightShift] == KeyState.Down)
                    {
                        if (Azerty)
                        {

                        }
                        else
                        {
                            AddToCurrent('"');
                        }
                    }
                    else
                    {
                        if (Azerty)
                        {

                        }
                        else
                        {
                            AddToCurrent('\'');
                        }
                    }
                    break;
                case Keys.OemBackslash:
                    if (Input.Keyboard.CurrentState[Keys.LeftShift] == KeyState.Down || Input.Keyboard.CurrentState[Keys.RightShift] == KeyState.Down)
                    {
                        if (Azerty)
                        {

                        }
                        else
                        {
                            AddToCurrent('|');
                        }
                    }
                    else
                    {
                        if (Azerty)
                        {

                        }
                        else
                        {
                            AddToCurrent('\\');
                        }
                    }
                    break;
                case Keys.OemOpenBrackets:
                    if (Input.Keyboard.CurrentState[Keys.LeftShift] == KeyState.Down || Input.Keyboard.CurrentState[Keys.RightShift] == KeyState.Down)
                    {
                        if (Azerty)
                        {

                        }
                        else
                        {
                            AddToCurrent('{');
                        }
                    }
                    else
                    {
                        if (Azerty)
                        {

                        }
                        else
                        {
                            AddToCurrent('[');
                        }
                    }
                    break;
                case Keys.OemCloseBrackets:
                    if (Input.Keyboard.CurrentState[Keys.LeftShift] == KeyState.Down || Input.Keyboard.CurrentState[Keys.RightShift] == KeyState.Down)
                    {
                        if (Azerty)
                        {

                        }
                        else
                        {
                            AddToCurrent('}');
                        }
                    }
                    else
                    {
                        if (Azerty)
                        {

                        }
                        else
                        {
                            AddToCurrent(']');
                        }
                    }
                    break;
                case Keys.OemMinus:
                    if (Input.Keyboard.CurrentState[Keys.LeftShift] == KeyState.Down || Input.Keyboard.CurrentState[Keys.RightShift] == KeyState.Down)
                    {
                        if (Azerty)
                        {

                        }
                        else
                        {
                            AddToCurrent('_');
                        }
                    }
                    else
                    {
                        if (Azerty)
                        {

                        }
                        else
                        {
                            AddToCurrent('-');
                        }
                    }
                    break;
                case Keys.OemPlus:
                    if (Input.Keyboard.CurrentState[Keys.LeftShift] == KeyState.Down || Input.Keyboard.CurrentState[Keys.RightShift] == KeyState.Down)
                    {
                        if (Azerty)
                        {

                        }
                        else
                        {
                            AddToCurrent('+');
                        }
                    }
                    else
                    {
                        if (Azerty)
                        {

                        }
                        else
                        {
                            AddToCurrent('=');
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
                        if (_cursorPosition > 0)
                        {
                            _cursorPosition--;
                        }
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

        private static void AddToCurrent(string sub)
        {
            Current = Current.Insert(Math.Max(0, _cursorPosition - 1), sub);
        }

        private static void AddToCurrent(char sub) => AddToCurrent(sub.ToString());

        /// <summary>
        /// When the user's want to 
        /// </summary>
        private static void EnterCommand()
        {
            if (Current.Length == 0)
            {
                return;
            }

            // Get the command data
            string[] data = Current.Split(' ');
            if (data.Length == 0)
            {
                return;
            }

            if (Commands.ContainsKey(data[0]))
            {
                Commands[data[0]].Action?.Invoke(data);
            }

            // Manage history
            if (History.Count >= maxHistoryCapacity)
            {
                History.RemoveAt(0);
            }
            History.Add(Current);

            // Reset command
            Current = _tempCommand = "";
            _cursorPosition = 0;
        }

        /// <summary>
        /// Add a command to the console
        /// </summary>
        /// <param name="command"></param>
        public static void AddCommand(Command command)
        {
            if (Commands.ContainsKey(command.Usage))
            {
                return;
            }

            Commands.Add(command.Usage, command);
        }

        /// <summary>
        /// Remove a command from the console
        /// </summary>
        /// <param name="name"></param>
        public static void RemoveCommand(string name)
        {
            if (!Commands.ContainsKey(name))
            {
                return;
            }

            Commands.Remove(name);
        }

        /// <summary>
        /// Draw the console
        /// </summary>
        /// <param name="spriteBatch"></param>
        public static void Draw(SpriteBatch spriteBatch)
        {
            if (!Opened)
            {
                return;
            }

            var viewport = spriteBatch.GraphicsDevice.Viewport;
            var consoleRect = new Rectangle(consolePadding, viewport.Height - consolePadding - 30, viewport.Width - consolePadding * 2, 30);

            spriteBatch.Begin();

            // Console structure
            Debug.FillRectangle(spriteBatch, consoleRect, Color.Black * consoleOpacity);
            Debug.FillRectangle(spriteBatch, new(consoleRect.X, consoleRect.Y - 300 - consolePadding, consoleRect.Width, 300), Color.Black * consoleOpacity);

            // Text display
            int commandY = (int)(consoleRect.Y + consoleRect.Height / 2 - _textHeight / 2);
            spriteBatch.DrawString(_font, DisplayCommand, new(consoleRect.X + 5, commandY), Color.White);

            // Cursor
            if (_cursorBlink)
            {
                Vector2 subPos = _font.MeasureString(Current.Substring(0, _cursorPosition));
                spriteBatch.DrawString(_font, "|", new(consoleRect.X + 5 + _prefixSize.X + subPos.X, commandY), Color.White);
            }

            spriteBatch.End();
        }

        #region Commands

        [Command("fullscreen", "Toggle the fullscreen mode")]
        private static void Fullscreen()
        {
            GameManager.ToggleFullscreen();
        }

		#endregion
	}
}
