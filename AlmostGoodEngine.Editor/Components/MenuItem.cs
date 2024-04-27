using ImGuiNET;
using System;

namespace AlmostGoodEngine.Editor.Components
{
	public class MenuItem(string label, string shortcut = "", bool enabled = true, Action onActivated = null) : MenuComponent
	{
		public Action OnActivated { get; set; } = onActivated;

		public string Label { get; set; } = label;
		public string Shortcut { get; set; } = shortcut;
		public bool Enabled { get; set; } = enabled;
		public bool Selected { get; set; } = false;
		public bool Activable { get; set; } = false;

		public MenuItem(string label, string shortcut, bool activable, bool selected, bool enabled, Action onActivated = null):
			this(label, shortcut, enabled, onActivated)
		{
			Activable = activable;
			Selected = selected;
		}

		public override void Draw()
		{
			base.Draw();

			if (ImGui.MenuItem(Label, Shortcut, Selected, Enabled))
			{
				OnActivated?.Invoke();
				if (Activable)
				{
					Selected = !Selected;
				}
			}
		}
	}
}
