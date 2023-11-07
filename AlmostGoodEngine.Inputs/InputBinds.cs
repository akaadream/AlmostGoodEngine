using System;

namespace AlmostGoodEngine.Inputs
{
    public class InputBinds
    {
        /// <summary>
        /// The name of the inputs binding
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Keys condition
        /// </summary>
        public InputKeysCondition InputKeysCondition { get; set; }

        /// <summary>
        /// Buttons condition
        /// </summary>
        public InputButtonsCondition InputButtonsCondition { get; set; }

        /// <summary>
        /// Mouse condition
        /// </summary>
        public InputMouseCondition InputMouseCondition { get; set; }

        /// <summary>
        /// Sticks condition
        /// </summary>
        public InputSticksCondition InputSticksCondition { get; set; }

        /// <summary>
        /// Callback condition for pressed check
        /// </summary>
        public Func<bool> PressedCondition { get; set; }

        /// <summary>
        /// Callback condition for release check
        /// </summary>
        public Func<bool> ReleasedCondition { get; set; }

        /// <summary>
        /// Callback condition for down check
        /// </summary>
        public Func<bool> DownCondition { get; set; }


        public InputBinds(
            string name,
            InputKeysCondition keysCondition,
            InputButtonsCondition buttonsCondition,
            InputSticksCondition inputSticksCondition,
            InputMouseCondition inputMouseCondition)
        {
            Name = name;

            InputKeysCondition = keysCondition;
            InputButtonsCondition = buttonsCondition;
            InputSticksCondition = inputSticksCondition;
            InputMouseCondition = inputMouseCondition;
        }

        public InputBinds(
            string name,
            InputKeysCondition keysCondition,
            InputButtonsCondition buttonsCondition,
            InputSticksCondition inputSticksCondition) :
            this(name, keysCondition, buttonsCondition, inputSticksCondition, new InputMouseCondition())
        {

        }

        public InputBinds(
            string name,
            InputKeysCondition keysCondition,
            InputButtonsCondition buttonsCondition) :
            this(name, keysCondition, buttonsCondition, new InputSticksCondition(0))
        {

        }

        public InputBinds(
            string name,
            InputKeysCondition keysCondition) :
            this(name, keysCondition, new InputButtonsCondition(0))
        {

        }

        public InputBinds(string name) : this(name, new InputKeysCondition())
        {

        }

        /// <summary>
        /// Return true if all pressed conditions are checked
        /// </summary>
        /// <returns></returns>
        public bool Pressed()
        {
            if (PressedCondition != null && !PressedCondition.Invoke())
            {
                return false;
            }
            
            return InputKeysCondition.Pressed() || 
                InputButtonsCondition.Pressed() || 
                InputSticksCondition.Pressed() || 
                InputMouseCondition.Pressed();
        }
        
        /// <summary>
        /// Return true if all released conditions are checked
        /// </summary>
        /// <returns></returns>
        public bool Released()
        {
            if (ReleasedCondition != null && !ReleasedCondition.Invoke())
            {
                return false;
            }

            return InputKeysCondition.Released() ||
                InputButtonsCondition.Released() ||
                InputSticksCondition.Released() ||
                InputMouseCondition.Released();
        }

        /// <summary>
        /// Return true if all down conditions are checked
        /// </summary>
        /// <returns></returns>
        public bool Down()
        {
            if (DownCondition != null && !DownCondition.Invoke())
            {
                return false;
            }

            return InputKeysCondition.Down() ||
                InputButtonsCondition.Down() ||
                InputSticksCondition.Down() ||
                InputMouseCondition.Down();
        }
    }
}
