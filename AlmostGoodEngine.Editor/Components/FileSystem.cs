using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AlmostGoodEngine.Editor.Components
{
	public class FileSystem : Component
	{
		public FileSystem()
		{

		}

		public override Vector2 Draw()
		{
			base.Draw();

			ImGui.TreeNode("");

			return Vector2.Zero;
		}
	}
}
