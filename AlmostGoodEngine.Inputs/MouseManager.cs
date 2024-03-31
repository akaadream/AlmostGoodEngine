using Microsoft.Xna.Framework.Input;

namespace AlmostGoodEngine.Inputs
{
    public class MouseManager
    {
        /// <summary>
        /// The mouse current state
        /// </summary>
        public MouseState CurrentState { get; private set; }

        /// <summary>
        /// The mouse previous frame state
        /// </summary>
        public MouseState PreviousState { get; private set; }

        /// <summary>
        /// X absolute coordinate of the mouse
        /// </summary>
        public int X
        {
            get => CurrentState.X;
            set => Mouse.SetPosition(value, CurrentState.Y);
        }

        /// <summary>
        /// Y absolute coordinate of the mouse
        /// </summary>
        public int Y
        {
            get => CurrentState.Y;
            set => Mouse.SetPosition(CurrentState.X, value);
        }

        public MouseManager()
        {
            PreviousState = new MouseState();
            CurrentState = new MouseState();
        }

        /// <summary>
        /// Update mouse states
        /// </summary>
        public void Update()
        {
            PreviousState = CurrentState;
            CurrentState = Mouse.GetState();
        }

        /// <summary>
        /// Return true if a button state is pressed
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public static bool IsDown(ButtonState button) => button == ButtonState.Pressed;

        /// <summary>
        /// Return true if the left mouse button is down
        /// </summary>
        /// <returns></returns>
        public bool IsLeftButtonDown() => IsDown(CurrentState.LeftButton);

        /// <summary>
        /// Return true if the middle mouse button is down
        /// </summary>
        /// <returns></returns>
        public bool IsMiddleButtonDown() => IsDown(CurrentState.MiddleButton);

        /// <summary>
        /// Return true if the right mouse button is down
        /// </summary>
        /// <returns></returns>
        public bool IsRightButtonDown() => IsDown(CurrentState.RightButton);

        /// <summary>
        /// Return true if the left mouse button has been pressed
        /// </summary>
        /// <returns></returns>
        public bool IsLeftButtonPressed() => IsLeftButtonDown() && PreviousState.LeftButton == ButtonState.Released;

        /// <summary>
        /// Return true if the middle mouse button has been pressed
        /// </summary>
        /// <returns></returns>
        public bool IsMiddleButtonPressed() => IsMiddleButtonDown() && PreviousState.MiddleButton == ButtonState.Released;

        /// <summary>
        /// Return true if the right mouse button has been pressed
        /// </summary>
        /// <returns></returns>
        public bool IsRightButtonPressed() => IsRightButtonDown() && PreviousState.RightButton == ButtonState.Released;

        /// <summary>
        /// Return true if the left mouse button has been released
        /// </summary>
        /// <returns></returns>
        public bool IsLeftButtonReleased() => !IsLeftButtonDown() && PreviousState.LeftButton == ButtonState.Pressed;

        /// <summary>
        /// Return true if the middle mouse button has been released
        /// </summary>
        /// <returns></returns>
        public bool IsMiddleButtonReleased() => !IsMiddleButtonDown() && PreviousState.MiddleButton == ButtonState.Pressed;

        /// <summary>
        /// Return true if the right mouse button has been released
        /// </summary>
        /// <returns></returns>
        public bool IsRightButtonReleased() => !IsRightButtonDown() && PreviousState.RightButton == ButtonState.Pressed;

        /// <summary>
        /// return the mouse wheel value
        /// </summary>
        /// <returns></returns>
        public int WheelValue()
        {
            return CurrentState.ScrollWheelValue;
        }

        /// <summary>
        /// return the mouse wheel delta (movement between the current frame and the previous one)
        /// </summary>
        /// <returns></returns>
        public int WheelDelta()
        {
            return CurrentState.ScrollWheelValue - PreviousState.ScrollWheelValue;
        }

        /// <summary>
        /// return true if the user scroll up
        /// </summary>
        /// <returns></returns>
        public bool IsWheelMovedUp()
        {
            return WheelDelta() > 0;
        }

        /// <summary>
        /// return true if the user scroll down
        /// </summary>
        /// <returns></returns>
        public bool IsWheelMovedDown()
        {
            return WheelDelta() < 0;
        }

        /// <summary>
        /// return true if the mouse moved between the current frame and the previous one
        /// </summary>
        /// <returns></returns>
        public bool Moved()
        {
            return CurrentState.X != PreviousState.X || CurrentState.Y != PreviousState.Y;
        }

        /// <summary>
        /// return true if the mouse is used (movement or button down)
        /// </summary>
        /// <returns></returns>
        public bool Used()
        {
            return Moved() ||
                IsLeftButtonDown() ||
                IsMiddleButtonDown() ||
                IsRightButtonDown();
        }
    }
}
