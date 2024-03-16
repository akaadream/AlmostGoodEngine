using Microsoft.Xna.Framework.Input;

namespace AlmostGoodEngine.Inputs
{
    public class InputButtonsCondition : IInputCondition<Buttons>
    {
        /// <summary>
        /// Gamepad index
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Buttons
        /// </summary>
        public Buttons[] Buttons { get; set; }

        public InputButtonsCondition(int index, params Buttons[] buttons)
        {
            Index = index;
            Buttons = buttons;
        }

        /// <summary>
        /// If the gamepad is available
        /// </summary>
        /// <returns></returns>
        private bool IsAvailable()
        {
            return Input.GamePads.Count - 1 >= Index;
        }

        /// <summary>
        /// Return true if one of the asked buttons has been pressed
        /// </summary>
        /// <returns></returns>
        public bool Pressed()
        {
            if (!IsAvailable())
            {
                return false;
            }

            foreach (var button in Buttons)
            {
                if (Input.GamePads[Index].IsPressed(button))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Return true if one of the asked buttons has been released
        /// </summary>
        /// <returns></returns>
        public bool Released()
        {
            if (!IsAvailable())
            {
                return false;
            }

            foreach (var button in Buttons)
            {
                if (Input.GamePads[Index].IsReleased(button))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Return true if one of the asked buttons is down
        /// </summary>
        /// <returns></returns>
        public bool Down()
        {
            if (!IsAvailable())
            {
                return false;
            }

            foreach (var button in Buttons)
            {
                if (Input.GamePads[Index].IsDown(button))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Replace the previous button with the next one
        /// </summary>
        /// <param name="previous"></param>
        /// <param name="next"></param>
        public void Replace(Buttons previous, Buttons next)
        {
            for (int i = Buttons.Length - 1; i >= 0; i--)
            {
                if (Buttons[i] == previous)
                {
                    Buttons[i] = next;
                }
            }
        }

        /// <summary>
        /// Replace all the asked buttons with the given buttons array
        /// </summary>
        /// <param name="buttons"></param>
        public void ReplaceAll(params Buttons[] buttons)
        {
            Buttons = buttons;
        }
    }
}
