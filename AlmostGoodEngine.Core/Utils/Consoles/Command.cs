using System;

namespace AlmostGoodEngine.Core.Utils.Consoles
{
	public struct Command
	{
		public Action<string[]> Action { get; set; }
		public string Usage { get; set; }
		public string Description { get; set; }
	}
}
