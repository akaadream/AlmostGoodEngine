using ImGuiNET;
using System.Collections.Generic;

namespace AlmostGoodEngine.Editor.Components
{
	public class Menu(string label) : MenuComponent
	{
		public string Label { get; set; } = label;

		public List<MenuComponent> Children { get; set; } = [];

		public override void Draw()
		{
			if (ImGui.BeginMenu(Label))
			{
				foreach (var child in Children)
				{
					child.Draw();
				}

				ImGui.EndMenu();
			}
		}
	}
}
