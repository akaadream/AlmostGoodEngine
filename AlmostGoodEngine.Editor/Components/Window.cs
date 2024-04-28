using ImGuiNET;
using System.Collections.Generic;

namespace AlmostGoodEngine.Editor.Components
{
	public class Window : Component
	{
		public string Label { get; set; }

		public List<Component> Children { get; set; }

		public Window(string label)
		{
			Label = label;
			Children = [];
		}

		public override void Draw()
		{
			ImGui.Begin(Label);

			foreach (var child in Children)
			{
				child.Draw();
			}

			ImGui.End();
		}
	}
}
