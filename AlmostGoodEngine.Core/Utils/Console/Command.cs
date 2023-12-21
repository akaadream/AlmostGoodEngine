using System;

namespace AlmostGoodEngine.Core.Utils.Console
{
    public class Command
    {
        /// <summary>
        /// The name of the command
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The helper (arguments) of the command
        /// </summary>
        public string Helper { get; set; }

        /// <summary>
        /// The callback action executed when the user is using the command
        /// </summary>
        public Action<string[]> Action { get; set; }

        public Command(string name, string helper, Action<string[]> action = null)
        {
            Name = name;
            Helper = helper;
            Action = action;
        }
    }
}
