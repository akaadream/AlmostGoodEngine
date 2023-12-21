using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linearstar.Windows.RawInput;

namespace AlmostGoodEngine.Inputs
{
    sealed class RawInputReceiverWindow
    {
        public event EventHandler<RawInputEventArgs> Input;

        public RawInputReceiverWindow()
        {

        }
    }
}
