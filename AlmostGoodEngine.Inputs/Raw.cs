using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linearstar.Windows.RawInput;

namespace AlmostGoodEngine.Inputs
{
    public static class Raw
    {

        public static void Initialize()
        {
            var devices = RawInputDevice.GetDevices();
            var keyboards = devices.OfType<RawInputKeyboard>();
            var mice = devices.OfType<RawInputMouse>();
        }

        public static void Update()
        {

        }
    }
}
