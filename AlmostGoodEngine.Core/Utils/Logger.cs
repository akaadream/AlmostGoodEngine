using System;

namespace AlmostGoodEngine.Core.Utils
{
    public static class Logger
    {
        public static (int Left, int Top) CursorPosition { get => Console.GetCursorPosition(); }
        public static (int Left, int Top) PreviousPosition { get; private set; }
        public static string PreviousLog { get; private set; }

        private static int _times = 1;

        public static void Log(string msg, string header = "Almost Good Engine", ConsoleColor headerColor = ConsoleColor.Yellow, ConsoleColor messageColor = ConsoleColor.White)
        {
			// Check if the previous line was the same
			if (PreviousLog == AsString(msg, header))
			{
                _times++;
				Console.SetCursorPosition(PreviousPosition.Left, PreviousPosition.Top);
                msg = msg + " (" + _times + ")";
			}
            else
            {
                _times = 1;
                PreviousLog = AsString(msg, header);
            }

			PreviousPosition = CursorPosition;

            Console.ForegroundColor = ConsoleColor.DarkGray;
            if (!string.IsNullOrEmpty(msg))
            {
				Console.Write("[");
				Console.ForegroundColor = headerColor;
				Console.Write(header);
				Console.ForegroundColor = ConsoleColor.DarkGray;
				Console.Write("] ");
			}
            
            Console.ForegroundColor = messageColor;
            Console.WriteLine(msg);

            // TODO: push the same message inside the in-game console
        }

        private static string AsString(string msg, string header)
        {
            if (string.IsNullOrEmpty(header))
            {
                return msg;
            }

            return "[" + header + "] " + msg;
        }
    }
}
