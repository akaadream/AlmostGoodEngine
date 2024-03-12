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
        public static RawInputDevice[] Devices { get; set; }
        //public static IEnumerable<RawInputKeyboard> Keyboards { get; set; }
        public static List<RawInputKeyboard> Keyboards { get; set; }
        public static RawInputKeyboardData CurrentKeyboardState { get; set; }

        public static void Initialize()
        {
            var hwnd = IntPtr.Zero;

            Keyboards = new();
            Devices = RawInputDevice.GetDevices();
            RawInputDevice.RegisterDevice(HidUsageAndPage.Keyboard, RawInputDeviceFlags.ExInputSink | RawInputDeviceFlags.NoLegacy, hwnd);
            
        }

        public static void Update()
        {
            foreach (var keyboard in Keyboards)
            {
                //keyboard.Keys
            }
        }

        private static IntPtr Hook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_INPUT = 0x00FF;

            if (msg == WM_INPUT)
            {
                var data = RawInputData.FromHandle(lParam);

                var sourceDeviceHandle = data.Header.DeviceHandle;
                var sourceDevice = data.Device;

                switch (data)
                {
                    case RawInputKeyboardData keyboard:
                        CurrentKeyboardState = keyboard;
                        Console.WriteLine(keyboard.Keyboard.VirutalKey);
                        break;
                }
            }

            return IntPtr.Zero;
        }
    }
}
