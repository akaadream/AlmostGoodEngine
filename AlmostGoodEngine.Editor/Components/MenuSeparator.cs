using ImGuiNET;

namespace AlmostGoodEngine.Editor.Components
{
	public class MenuSeparator : MenuComponent
	{
		public override string Text => "";

		public override void Draw()
		{
			base.Draw();

			ImGui.Separator();
		}
	}
}
