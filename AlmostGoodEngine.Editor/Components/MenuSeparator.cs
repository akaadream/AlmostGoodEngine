using ImGuiNET;
using System.Numerics;

namespace AlmostGoodEngine.Editor.Components
{
	public class MenuSeparator : MenuComponent
	{
		public override string Text => "";

		public override Vector2 Draw()
		{
			base.Draw();

			ImGui.Separator();

			return Vector2.Zero;
		}
	}
}
