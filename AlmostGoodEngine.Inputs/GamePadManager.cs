using Microsoft.Xna.Framework.Input;

namespace AlmostGoodEngine.Inputs
{
    public class GamePadManager
    {
        /// <summary>
        /// Current state of the gamepad instance
        /// </summary>
        public GamePadState CurrentState { get; private set; }

        /// <summary>
        /// The state of the gamepad instance on the previous frame
        /// </summary>
        public GamePadState PreviousState { get; private set; }

        /// <summary>
        /// The index of the gamepad
        /// </summary>
        public int Index { get; private set; }

        /// <summary>
        /// Gamepad's joysticks deadzones
        /// </summary>
        public float DeadZone { get; set; }

        private float vibrationTimer { get; set; }
        private float vibrationDuration { get; set; }
        private bool doVibration { get; set; }
        private float leftMotor { get; set; }
        private float rightMotor { get; set; }

        public GamePadManager(int index)
        {
            Index = index;

            vibrationDuration = vibrationTimer = 0f;
            leftMotor = rightMotor = 0f;
            doVibration = false;
        }

        /// <summary>
        /// Update the gamepad state
        /// </summary>
        public void Update()
        {
            PreviousState = CurrentState;
            CurrentState = GamePad.GetState(Index);

            // Make the gamepad vibrate
            if (doVibration)
            {
                // End of vibrations
                if (vibrationTimer >= vibrationDuration)
                {
                    doVibration = false;
                }
                else
                {
                    GamePad.SetVibration(Index, leftMotor, rightMotor);
                }
            }
        }

        /// <summary>
        /// Make the gamepad vibration during the given duration
        /// </summary>
        /// <param name="duration"></param>
        public void Vibrate(float duration, float leftIntensity = 1f, float rightIntensity = 1f)
        {
            vibrationDuration = duration;
            vibrationTimer = 0f;
            leftMotor = leftIntensity;
            rightMotor = rightIntensity;
            doVibration = true;
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
        /// If true if the given button is down
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public bool IsDown(Buttons button)
        {
            return CurrentState.IsButtonDown(button);
        }

        /// <summary>
        /// Return true if the given buttons are down
        /// </summary>
        /// <param name="buttons"></param>
        /// <returns></returns>
        public bool AreDown(params Buttons[] buttons)
        {
            foreach (var button in buttons)
            {
                // The button is not down
                if (!CurrentState.IsButtonDown(button))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Return true if the given button has been pressed
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public bool IsPressed(Buttons button)
        {
            return IsDown(button) && !PreviousState.IsButtonDown(button);
        }

        /// <summary>
        /// Return true if the given button has been released
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public bool IsReleased(Buttons button)
        {
            return !IsDown(button) && PreviousState.IsButtonDown(button);
        }

        /// <summary>
        /// Get the left stick horizontal value
        /// </summary>
        /// <returns></returns>
        public float GetHorizontalAxis(bool ignoreDeadZone = false)
        {
            float finalDeadzone = !ignoreDeadZone ? DeadZone : 0f;
            return GetValue(CurrentState.ThumbSticks.Left.X, finalDeadzone);
        }

        /// <summary>
        /// Get the left stick vertical value
        /// </summary>
        /// <returns></returns>
        public float GetVerticalAxis(bool ignoreDeadZone = false)
        {
            float finalDeadzone = !ignoreDeadZone ? DeadZone : 0f;
            return GetValue(CurrentState.ThumbSticks.Left.Y, finalDeadzone);
        }

        /// <summary>
        /// Get the right stick horizontal value
        /// </summary>
        /// <param name="ignoreDeadZone"></param>
        /// <returns></returns>
        public float GetRightHorizontalAxis(bool ignoreDeadZone = false)
        {
            float finalDeadzone = !ignoreDeadZone ? DeadZone : 0f;
            return GetValue(CurrentState.ThumbSticks.Right.X, finalDeadzone);
        }


        /// <summary>
        /// Get the right stick vertical value
        /// </summary>
        /// <param name="ignoreDeadZone"></param>
        /// <returns></returns>
        public float GetRightVerticalAxis(bool ignoreDeadZone = false)
        {
            float finalDeadzone = !ignoreDeadZone ? DeadZone : 0f;
            return GetValue(CurrentState.ThumbSticks.Right.Y, finalDeadzone);
        }

        /// <summary>
        /// Get the left stick horizontal direction (left: -1, right: 1, nothing: 0)
        /// </summary>
        /// <param name="ignoreDeadZone"></param>
        /// <returns></returns>
        public int GetHorizontalDirection(bool ignoreDeadZone = false)
        {
            float finalDeadzone = !ignoreDeadZone ? DeadZone : 0f;
            return GetDirection(CurrentState.ThumbSticks.Left.X, finalDeadzone);
        }

        /// <summary>
        /// Get the left stick vertical direction (up: 1, down: -1, nothing: 0)
        /// </summary>
        /// <param name="ignoreDeadZone"></param>
        /// <returns></returns>
        public int GetVerticalDirection(bool ignoreDeadZone = false)
        {
            float finalDeadzone = !ignoreDeadZone ? DeadZone : 0f;
            return GetDirection(CurrentState.ThumbSticks.Left.Y, finalDeadzone);
        }

        /// <summary>
        /// Get the right stick horizontal direction (left: -1, right: 1, nothing: 0)
        /// </summary>
        /// <param name="ignoreDeadZone"></param>
        /// <returns></returns>
        public int GetRightHorizontalDirection(bool ignoreDeadZone = false)
        {
            float finalDeadzone = !ignoreDeadZone ? DeadZone : 0f;
            return GetDirection(CurrentState.ThumbSticks.Right.X, finalDeadzone);
        }

        /// <summary>
        /// Get the right stick vertical direction (up: 1, down: -1, nothing: 0)
        /// </summary>
        /// <param name="ignoreDeadZone"></param>
        /// <returns></returns>
        public int GetRightVerticalDirection(bool ignoreDeadZone = false)
        {
            float finalDeadzone = !ignoreDeadZone ? DeadZone : 0f;
            return GetDirection(CurrentState.ThumbSticks.Right.Y, finalDeadzone);
        }

        /// <summary>
        /// Get a final value depending on a stick value and the deadzone
        /// </summary>
        /// <returns></returns>
        private static float GetValue(float value, float deadZone)
        {
            if (value < -deadZone || value > deadZone)
            {
                return value;
            }

            return 0f;
        }

        /// <summary>
        /// Get a stick direction depending on its value and the deadzone
        /// </summary>
        /// <param name="value"></param>
        /// <param name="deadZone"></param>
        /// <returns></returns>
        private static int GetDirection(float value, float deadZone)
        {
            if (value < -deadZone)
            {
                return -1;
            }

            if (value > deadZone)
            {
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// Return true if any button of the gamepad has been pressed
        /// </summary>
        /// <returns></returns>
        public bool AnyButtonPressed()
        {
            return IsPressed(Buttons.A) ||
                IsPressed(Buttons.B) ||
                IsPressed(Buttons.Y) ||
                IsPressed(Buttons.X) ||
                IsPressed(Buttons.LeftTrigger) ||
                IsPressed(Buttons.LeftShoulder) ||
                IsPressed(Buttons.RightTrigger) ||
                IsPressed(Buttons.RightShoulder) ||
                IsPressed(Buttons.Start) ||
                IsPressed(Buttons.BigButton) ||
                IsPressed(Buttons.DPadDown) ||
                IsPressed(Buttons.DPadLeft) ||
                IsPressed(Buttons.DPadRight) ||
                IsPressed(Buttons.DPadUp) ||
                IsPressed(Buttons.LeftStick) ||
                IsPressed(Buttons.RightStick);
        }

        /// <summary>
        /// Return true if any button of the gamepad is down
        /// </summary>
        /// <returns></returns>
        public bool AnyButtonDown()
        {
            return IsDown(Buttons.A) ||
                IsDown(Buttons.B) ||
                IsDown(Buttons.Y) ||
                IsDown(Buttons.X) ||
                IsDown(Buttons.LeftTrigger) ||
                IsDown(Buttons.LeftShoulder) ||
                IsDown(Buttons.RightTrigger) ||
                IsDown(Buttons.RightShoulder) ||
                IsDown(Buttons.Start) ||
                IsDown(Buttons.BigButton) ||
                IsDown(Buttons.DPadDown) ||
                IsDown(Buttons.DPadLeft) ||
                IsDown(Buttons.DPadRight) ||
                IsDown(Buttons.DPadUp) ||
                IsDown(Buttons.LeftStick) ||
                IsDown(Buttons.RightStick);
        }

        /// <summary>
        /// Return true if the gamepad has been used
        /// </summary>
        /// <returns></returns>
        public bool Used()
        {
            return AnyButtonDown() ||
                GetHorizontalDirection() != 0 ||
                GetVerticalDirection() != 0 ||
                GetRightHorizontalDirection() != 0 ||
                GetRightVerticalDirection() != 0;
        }

        /// <summary>
        /// Return true if a diagonal is down using DPad
        /// </summary>
        /// <returns></returns>
        public bool DPadDiagonal()
        {
            return (IsDown(Buttons.DPadUp) && IsDown(Buttons.DPadLeft)) ||
                (IsDown(Buttons.DPadUp) && IsDown(Buttons.DPadRight)) ||
                (IsDown(Buttons.DPadDown) && IsDown(Buttons.DPadLeft)) ||
                (IsDown(Buttons.DPadDown) && IsDown(Buttons.DPadRight));

        }
    }
}
