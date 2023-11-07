using Microsoft.Xna.Framework.Input;

namespace AlmostGoodEngine.Inputs
{
    public class KeyboardManager
    {
        /// <summary>
        /// Current keyboard state
        /// </summary>
        public KeyboardState CurrentState { get; private set; }

        /// <summary>
        /// Keyboard state on the previous frame
        /// </summary>
        public KeyboardState PreviousState { get; private set; }

        /// <summary>
        /// The basic alphabet ([a-z)]
        /// Basically the characters that users can write using their keyboard
        /// </summary>
        private static readonly string[] Alphabet = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

        public KeyboardManager() { }

        /// <summary>
        /// update keyboard states
        /// </summary>
        public void Update()
        {
            PreviousState = CurrentState;
            CurrentState = Keyboard.GetState();
        }

        /// <summary>
        /// return true if the given key is down
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsDown(Keys key)
        {
            return CurrentState.IsKeyDown(key);
        }

        /// <summary>
        /// return true if the given key has been pressed
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsPressed(Keys key)
        {
            return IsDown(key) && !PreviousState.IsKeyDown(key);
        }

        /// <summary>
        /// return true if the given key has been released
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsReleased(Keys key)
        {
            return !IsDown(key) && PreviousState.IsKeyDown(key);
        }

        /// <summary>
        /// Get horizontal direction (using arrows)
        /// </summary>
        /// <returns></returns>
        public int GetHorizontalDirection()
        {
            return GetDirection(Keys.Left, Keys.Right);
        }

        /// <summary>
        /// Get vertical direction (using arrows)
        /// </summary>
        /// <returns></returns>
        public int GetVerticalDirection()
        {
            return GetDirection(Keys.Up, Keys.Down);
        }

        /// <summary>
        /// Get the direction from a min key to a max key
        /// </summary>
        /// <param name="minKey"></param>
        /// <param name="maxKey"></param>
        /// <returns></returns>
        public int GetDirection(Keys minKey, Keys maxKey)
        {
            if (IsDown(minKey)) return -1;
            if (IsDown(maxKey)) return 1;
            return 0;
        }

        /// <summary>
        /// return true if the given letter is contained inside the basic alphabet ([a-z])
        /// </summary>
        /// <param name="letter"></param>
        /// <returns></returns>
        private static bool HasLetter(string letter)
        {
            foreach (string str in Alphabet)
            {
                if (str == letter.ToLower())
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// return true if the given key can result in a writable character
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsWritable(Keys key)
        {
            return key == Keys.Space ||
                key == Keys.D0 ||
                key == Keys.D1 ||
                key == Keys.D2 ||
                key == Keys.D3 ||
                key == Keys.D4 ||
                key == Keys.D5 ||
                key == Keys.D6 ||
                key == Keys.D7 ||
                key == Keys.D8 ||
                key == Keys.D9 ||
                key == Keys.OemComma ||
                key == Keys.OemPeriod ||
                key == Keys.OemQuestion ||
                key == Keys.OemSemicolon ||
                key == Keys.OemQuotes ||
                key == Keys.OemBackslash ||
                key == Keys.OemOpenBrackets ||
                key == Keys.OemCloseBrackets ||
                key == Keys.OemMinus ||
                key == Keys.OemPlus ||
                HasLetter(key.ToString());
        }

        /// <summary>
        /// return true if the keyboard is used
        /// </summary>
        /// <returns></returns>
        public bool Used()
        {
            return CurrentState.GetPressedKeyCount() > 0;
        }
    }
}
