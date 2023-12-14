using System;

namespace AlmostGoodEngine.Core.Utils
{
    public static class Logger
    {
        public static void Log(string msg, string header = "Almost Good Engine", ConsoleColor headerColor = ConsoleColor.Yellow, ConsoleColor messageColor = ConsoleColor.White)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("[");
            Console.ForegroundColor = headerColor;
            Console.Write(header);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("] ");
            Console.ForegroundColor = messageColor;
            Console.WriteLine(msg);
        }
    }
}
