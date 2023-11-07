namespace AlmostGoodEngine.Inputs
{
    internal interface IInputCondition<T>
    {
        /// <summary>
        /// Check pressed inputs
        /// </summary>
        /// <returns></returns>
        public bool Pressed();

        /// <summary>
        /// Check released inputs
        /// </summary>
        /// <returns></returns>
        public bool Released();

        /// <summary>
        /// Check down inputs
        /// </summary>
        /// <returns></returns>
        public bool Down();

        /// <summary>
        /// Replace all the inputs with new ones
        /// </summary>
        /// <param name="values"></param>
        void ReplaceAll(params T[] values);

        /// <summary>
        /// Replace an input by another one
        /// </summary>
        /// <param name="previous"></param>
        /// <param name="next"></param>
        void Replace(T previous, T next);
    }
}
