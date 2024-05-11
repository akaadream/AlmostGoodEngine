using ImGuiNET;
using System.Collections.Generic;
using System.Numerics;

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

		public override Vector2 Draw()
		{
			ImGui.Begin(Label);

			foreach (var child in Children)
			{
				child.Draw();
			}

			ImGui.End();

			return Vector2.Zero;
		}
	}
}
