using ImGuiNET;
using System.Collections.Generic;
using System.Numerics;

namespace AlmostGoodEngine.Editor.Components
{
	public class Menu(string label, string icon = "") : MenuComponent
	{
		public string Label { get; set; } = label;
		public string Icon { get; set; } = icon;

		public override string Text
		{
			get
			{
				if (string.IsNullOrEmpty(Icon))
				{
					return Label;
				}

				return Icon + " " + Label;
			}
		}

		public List<MenuComponent> Children { get; set; } = [];

		public override Vector2 Draw()
		{
			Vector2 size = Vector2.Zero;
			if (ImGui.BeginMenu(Text))
			{
				size = ImGui.GetItemRectSize();
				foreach (var child in Children)
				{
					child.Draw();
				}

				ImGui.EndMenu();
			}
			return size;
		}
	}
}
