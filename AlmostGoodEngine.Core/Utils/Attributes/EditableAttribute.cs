using System;

namespace AlmostGoodEngine.Core.Utils.Attributes
{
	public class EditableAttribute : Attribute
	{
		public string DisplayName { get; set; }

		public EditableAttribute(string displayName = "")
		{
			DisplayName = displayName;
		}
	}
}
