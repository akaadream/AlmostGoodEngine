using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace AlmostGoodEngine.Inputs
{
    public class TouchPanelManager
    {
        /// <summary>
        /// Current touch panel state
        /// </summary>
        public TouchCollection CurrentState { get; private set; }

        /// <summary>
        /// Touch panel state on the previous frame
        /// </summary>
        public TouchCollection PreviousState { get; private set; }

        /// <summary>
        /// The minimum pressure needed to detect touches
        /// </summary>
        public float Pressure { get; set; }

        public TouchPanelManager(float pressure = 0.0f)
        {
            Pressure = pressure;
        }

        /// <summary>
        /// Update states
        /// </summary>
        public void Update()
        {
            PreviousState = CurrentState;
            CurrentState = TouchPanel.GetState();
        }

        /// <summary>
        /// Return true if a touch panel is connected
        /// </summary>
        /// <returns></returns>
        public bool IsConnected() => CurrentState.IsConnected;

        /// <summary>
        /// Return true if the given position has been touched
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool IsTouched(Vector2 position) => TouchedDown(CurrentState, position);

        /// <summary>
        /// Return true if the position is contained inside the touches
        /// </summary>
        /// <param name="touches"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        private static bool TouchedDown(TouchCollection touches, Vector2 position)
        {
            foreach (var touched in touches)
            {
                if (touched.Position == position)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Return true if all the given positions has been touched
        /// </summary>
        /// <param name="positions"></param>
        /// <returns></returns>
        public bool AreTouched(params Vector2[] positions)
        {
            foreach (var position in positions)
            {
                if (!IsTouched(position))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Return true if the given position was touched on the previous frame
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool WasTouched(Vector2 position) => !IsTouched(position) && !TouchedDown(PreviousState, position);
    }
}
