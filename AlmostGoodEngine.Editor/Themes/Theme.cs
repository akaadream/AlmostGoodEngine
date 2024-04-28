using ImGuiNET;

namespace AlmostGoodEngine.Editor.Themes
{
	public abstract class Theme
	{
		/// <summary>
		/// The main viewport of ImGui
		/// </summary>
		protected ImGuiViewportPtr Viewport { get; set; }

		public Theme()
		{
			Viewport = ImGui.GetMainViewport();
		}

		/// <summary>
		/// Apply new rules
		/// </summary>
		public virtual void Apply()
		{
			ImGui.PushStyleVar(ImGuiStyleVar.WindowRounding, 4f);
			ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new System.Numerics.Vector2(0, 0));
		}
	}
}
