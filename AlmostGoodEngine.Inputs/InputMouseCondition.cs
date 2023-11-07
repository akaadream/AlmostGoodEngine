using Microsoft.Xna.Framework.Input;

namespace AlmostGoodEngine.Inputs
{
    public class InputMouseCondition : IInputCondition<MouseState>
    {
        /// <summary>
        /// Mouse left button
        /// </summary>
        public bool LeftButton { get; set; }

        /// <summary>
        /// Mouse right button
        /// </summary>
        public bool RightButton { get; set; }

        /// <summary>
        /// Mouse middle button
        /// </summary>
        public bool MiddleButton { get; set; }

        public InputMouseCondition(bool leftButton = false, bool rightButton = false, bool middleButton = false)
        {
            LeftButton = leftButton;
            RightButton = rightButton;
            MiddleButton = middleButton;
        }

        /// <summary>
        /// Return true if one of the asked buttons has been pressed
        /// </summary>
        /// <returns></returns>
        public bool Pressed()
        {
            if (LeftButton && InputManager.Mouse.IsLeftButtonPressed()) return true;
            if (RightButton && InputManager.Mouse.IsRightButtonPressed()) return true;
            if (MiddleButton && InputManager.Mouse.IsMiddleButtonPressed()) return true;

            return false;
        }

        /// <summary>
        /// Return true if one of the asked buttons has been released
        /// </summary>
        /// <returns></returns>
        public bool Released()
        {
            if (LeftButton && InputManager.Mouse.IsLeftButtonReleased()) return true;
            if (RightButton && InputManager.Mouse.IsRightButtonReleased()) return true;
            if (MiddleButton && InputManager.Mouse.IsMiddleButtonReleased()) return true;

            return false;
        }

        /// <summary>
        /// Return true if one of the asked buttons is down
        /// </summary>
        /// <returns></returns>
        public bool Down()
        {
            if (LeftButton && InputManager.Mouse.IsLeftButtonDown()) return true;
            if (RightButton && InputManager.Mouse.IsRightButtonDown()) return true;
            if (MiddleButton && InputManager.Mouse.IsMiddleButtonDown()) return true;

            return false;
        }

        /// <summary>
        /// Not used.
        /// </summary>
        /// <param name="previous"></param>
        /// <param name="next"></param>
        public void Replace(MouseState previous, MouseState next)
        {
            return;
        }

        /// <summary>
        /// Not used.
        /// </summary>
        /// <param name="states"></param>
        public void ReplaceAll(params MouseState[] states)
        {
            return;
        }
    }
}
