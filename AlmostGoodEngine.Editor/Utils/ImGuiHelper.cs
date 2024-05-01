using AlmostGoodEngine.Editor.Components;
using ImGuiNET;
using System.Collections.Generic;
using System.Numerics;

namespace AlmostGoodEngine.Editor.Utils
{
	public static class ImGuiHelper
	{
		public static Vector2 GetSizeOf(List<string> texts)
		{
			Vector2 size = Vector2.Zero;
			foreach (var text in texts)
			{
				size += ImGui.CalcTextSize(text);
			}

			return size;
		}

		public static Vector2 GetSizeOf(List<MenuComponent> menus)
		{
			Vector2 size = Vector2.Zero;
			foreach (var menu in menus)
			{
				size += ImGui.CalcTextSize(menu.Text);
			}

			return size;
		}
	}
}
