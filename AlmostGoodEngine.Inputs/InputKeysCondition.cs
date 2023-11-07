using Microsoft.Xna.Framework.Input;

namespace AlmostGoodEngine.Inputs
{
    public class InputKeysCondition : IInputCondition<Keys>
    {
        /// <summary>
        /// Asked keyboard keys
        /// </summary>
        public Keys[] Keys { get; set; }

        public InputKeysCondition(params Keys[] keys)
        {
            Keys = keys;
        }

        /// <summary>
        /// Return true if one of the asked keys has been pressed
        /// </summary>
        /// <returns></returns>
        public bool Pressed()
        {
            foreach (var key in Keys)
            {
                if (InputManager.Keyboard.IsPressed(key))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Return true if one of the asked keys has been released
        /// </summary>
        /// <returns></returns>
        public bool Released()
        {
            foreach (var key in Keys)
            {
                if (InputManager.Keyboard.IsReleased(key))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Return true if one of the asked keys is down
        /// </summary>
        /// <returns></returns>
        public bool Down()
        {
            foreach (var key in Keys)
            {
                if (InputManager.Keyboard.IsDown(key))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Replace a current key by a new one
        /// </summary>
        /// <param name="previous"></param>
        /// <param name="next"></param>
        public void Replace(Keys previous, Keys next)
        {
            for (int i = Keys.Length - 1; i >= 0; i--)
            {
                if (Keys[i] == previous)
                {
                    Keys[i] = next;
                }
            }
        }

        /// <summary>
        /// Replace all the current asked keys by the new keys array
        /// </summary>
        /// <param name="keys"></param>
        public void ReplaceAll(params Keys[] keys)
        {
            Keys = keys;
        }
    }
}
