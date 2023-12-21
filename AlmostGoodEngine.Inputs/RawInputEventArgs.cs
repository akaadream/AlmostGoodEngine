using Linearstar.Windows.RawInput;
using System;

namespace AlmostGoodEngine.Inputs
{
    internal class RawInputEventArgs : EventArgs
    {
        public RawInputData Data { get; private set; }

        public RawInputEventArgs(RawInputData data)
        {
            Data = data;
        }
    }
}
