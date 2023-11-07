using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace AlmostGoodEngine.Inputs
{
    public class InputSticksCondition : IInputCondition<Vector2>
    {
        /// <summary>
        /// Gamepad index
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Left stick usage
        /// </summary>
        public bool LeftStick { get; set; }

        /// <summary>
        /// Right stick usage
        /// </summary>
        public bool RightStick { get; set; }

        /// <summary>
        /// Left stick position
        /// </summary>
        public Vector2 LeftStickValue { get; set; }

        /// <summary>
        /// Right stick position
        /// </summary>
        public Vector2 RightStickValue { get; set; }


        public InputSticksCondition(int index, bool leftStick = false, bool rightStick = false, Vector2 leftStickValue = new Vector2(), Vector2 rightStickValue = new Vector2())
        {
            Index = index;
            LeftStick = leftStick;
            RightStick = rightStick;
            LeftStickValue = leftStickValue;
            RightStickValue = rightStickValue;
        }

        /// <summary>
        /// Return true if the given gamepad is connected
        /// </summary>
        /// <returns></returns>
        public bool IsConnected()
        {
            return GamePad.GetCapabilities(Index).IsConnected;
        }

        /// <summary>
        /// Return true if one of the asked sticks is at least on an asked position
        /// </summary>
        /// <returns></returns>
        public bool Down()
        {
            if (!IsConnected()) return false;

            if (LeftStick)
            {
                float horizontal = InputManager.GamePads[Index].GetHorizontalAxis(true);
                float vertical = InputManager.GamePads[Index].GetVerticalAxis(true);

                if (LeftStickValue.X > 0 && horizontal > LeftStickValue.X)
                {
                    return true;
                }

                if (LeftStickValue.X < 0 && horizontal < LeftStickValue.X)
                {
                    return true;
                }

                if (LeftStickValue.Y > 0 && vertical > LeftStickValue.Y)
                {
                    return true;
                }

                if (LeftStickValue.Y < 0 && vertical < LeftStickValue.Y)
                {
                    return true;
                }
            }

            if (RightStick)
            {
                float horizontal = InputManager.GamePads[Index].GetRightHorizontalAxis(true);
                float vertical = InputManager.GamePads[Index].GetRightVerticalAxis(true);

                if (RightStickValue.X > 0 && horizontal > RightStickValue.X)
                {
                    return true;
                }

                if (RightStickValue.X < 0 && horizontal < RightStickValue.X)
                {
                    return true;
                }

                if (RightStickValue.Y > 0 && vertical > RightStickValue.Y)
                {
                    return true;
                }

                if (RightStickValue.Y < 0 && vertical < RightStickValue.Y)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Not used.
        /// </summary>
        /// <returns></returns>
        public bool Pressed()
        {
            return false;
        }

        /// <summary>
        /// Not used.
        /// </summary>
        /// <returns></returns>
        public bool Released()
        {
            return false;
        }

        /// <summary>
        /// Not used.
        /// </summary>
        /// <param name="previous"></param>
        /// <param name="next"></param>
        public void Replace(Vector2 previous, Vector2 next)
        {
            return;
        }

        /// <summary>
        /// Not used
        /// </summary>
        /// <param name="values"></param>
        public void ReplaceAll(params Vector2[] values)
        {
            return;
        }
    }
}
