using ImGuiNET;
using System.Numerics;

namespace AlmostGoodEngine.Editor.Themes
{
	public class DraculaTheme : Theme
	{
		public override void Apply()
		{
			// Bacgrounds
			ImGui.PushStyleColor(ImGuiCol.WindowBg, new Vector4(0.1f, 0.1f, 0.13f, 1.0f));
			ImGui.PushStyleColor(ImGuiCol.MenuBarBg, new Vector4(0.16f, 0.16f, 0.21f, 1.0f));

			// Borders
			ImGui.PushStyleColor(ImGuiCol.Border, new Vector4(0.44f, 0.37f, 0.61f, 0.29f));
			ImGui.PushStyleColor(ImGuiCol.BorderShadow, new Vector4(0.0f, 0.0f, 0.0f, 0.24f));

			// Texts
			ImGui.PushStyleColor(ImGuiCol.Text, new Vector4(1.0f, 1.0f, 1.0f, 1.0f));
			ImGui.PushStyleColor(ImGuiCol.TextDisabled, new Vector4(0.5f, 0.5f, 0.5f, 1.0f));

			// Headers
			ImGui.PushStyleColor(ImGuiCol.Header, new Vector4(0.13f, 0.13f, 0.17f, 1.0f));
			ImGui.PushStyleColor(ImGuiCol.HeaderHovered, new Vector4(0.19f, 0.2f, 0.25f, 1.0f));
			ImGui.PushStyleColor(ImGuiCol.HeaderActive, new Vector4(0.16f, 0.16f, 0.21f, 1.0f));

			// Buttons
			ImGui.PushStyleColor(ImGuiCol.Button, new Vector4(0.13f, 0.13f, 0.17f, 1.0f));
			ImGui.PushStyleColor(ImGuiCol.ButtonHovered, new Vector4(0.19f, 0.2f, 0.25f, 1.0f));
			ImGui.PushStyleColor(ImGuiCol.ButtonActive, new Vector4(0.16f, 0.16f, 0.21f, 1.0f));
			ImGui.PushStyleColor(ImGuiCol.CheckMark, new Vector4(0.74f, 0.85f, 0.98f, 1.0f));

			// Popups
			ImGui.PushStyleColor(ImGuiCol.PopupBg, new Vector4(0.1f, 0.1f, 0.13f, 0.92f));

			// Sliders
			ImGui.PushStyleColor(ImGuiCol.SliderGrab, new Vector4(0.44f, 0.37f, 0.61f, 0.54f));
			ImGui.PushStyleColor(ImGuiCol.SliderGrabActive, new Vector4(0.74f, 0.85f, 0.98f, 0.54f));

			// Frame BG
			ImGui.PushStyleColor(ImGuiCol.FrameBg, new Vector4(0.13f, 0.13f, 0.17f, 1.0f));
			ImGui.PushStyleColor(ImGuiCol.FrameBgHovered, new Vector4(0.19f, 0.2f, 0.25f, 1.0f));
			ImGui.PushStyleColor(ImGuiCol.FrameBgActive, new Vector4(0.16f, 0.16f, 0.21f, 1.0f));

			// Tabs
			ImGui.PushStyleColor(ImGuiCol.DockingPreview, new Vector4(0.16f, 0.16f, 0.21f, 1.0f));
			ImGui.PushStyleColor(ImGuiCol.DockingPreview, new Vector4(0.24f, 0.24f, 0.32f, 1.0f));
			ImGui.PushStyleColor(ImGuiCol.DockingPreview, new Vector4(0.2f, 0.22f, 0.27f, 1.0f));
			ImGui.PushStyleColor(ImGuiCol.DockingPreview, new Vector4(0.16f, 0.16f, 0.21f, 1.0f));
			ImGui.PushStyleColor(ImGuiCol.DockingPreview, new Vector4(0.16f, 0.16f, 0.21f, 1.0f));

			// Title
			ImGui.PushStyleColor(ImGuiCol.DockingPreview, new Vector4(0.16f, 0.16f, 0.21f, 1.0f));
			ImGui.PushStyleColor(ImGuiCol.DockingPreview, new Vector4(0.16f, 0.16f, 0.21f, 1.0f));
			ImGui.PushStyleColor(ImGuiCol.DockingPreview, new Vector4(0.16f, 0.16f, 0.21f, 1.0f));

			// Scrollbar
			ImGui.PushStyleColor(ImGuiCol.DockingPreview, new Vector4(0.1f, 0.1f, 0.13f, 1.0f));
			ImGui.PushStyleColor(ImGuiCol.DockingPreview, new Vector4(0.16f, 0.16f, 0.21f, 1.0f));
			ImGui.PushStyleColor(ImGuiCol.DockingPreview, new Vector4(0.19f, 0.2f, 0.25f, 1.0f));
			ImGui.PushStyleColor(ImGuiCol.DockingPreview, new Vector4(0.24f, 0.24f, 0.32f, 1.0f));

			// Separator
			ImGui.PushStyleColor(ImGuiCol.DockingPreview, new Vector4(0.44f, 0.37f, 0.61f, 1.0f));
			ImGui.PushStyleColor(ImGuiCol.DockingPreview, new Vector4(0.74f, 0.58f, 0.98f, 1.0f));
			ImGui.PushStyleColor(ImGuiCol.DockingPreview, new Vector4(0.84f, 0.58f, 1.0f, 1.0f));

			// Resize Grip
			ImGui.PushStyleColor(ImGuiCol.DockingPreview, new Vector4(0.44f, 0.37f, 0.61f, 1.0f));
			ImGui.PushStyleColor(ImGuiCol.DockingPreview, new Vector4(0.74f, 0.58f, 0.98f, 1.0f));
			ImGui.PushStyleColor(ImGuiCol.DockingPreview, new Vector4(0.84f, 0.58f, 1.0f, 1.0f));

			// Docking
			ImGui.PushStyleColor(ImGuiCol.DockingPreview, new Vector4(0.44f, 0.37f, 0.61f, 1.0f));

			ImGui.PushStyleVar(ImGuiStyleVar.TabRounding, 4f);
			ImGui.PushStyleVar(ImGuiStyleVar.ScrollbarRounding, 9f);
			ImGui.PushStyleVar(ImGuiStyleVar.WindowRounding, 7f);
			ImGui.PushStyleVar(ImGuiStyleVar.GrabRounding, 3f);
			ImGui.PushStyleVar(ImGuiStyleVar.FrameRounding, 3f);
			ImGui.PushStyleVar(ImGuiStyleVar.PopupRounding, 4f);
			ImGui.PushStyleVar(ImGuiStyleVar.ChildRounding, 4f);
		}
	}
}
